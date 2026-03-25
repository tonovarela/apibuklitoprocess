
using System.Net;
using System.Text.Json;
using apiBukLitoprocess.Clases;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.mappers;
using apiBukLitoprocess.repository.interfaces;
using apiBukLitoprocess.responseApi;


namespace apiBukLitoprocess.Services;




public record GetColaboradorResult(bool IsError, int StatusCode, string? ErrorMessage, ColaboradorDTO? colaborador)
{
    public static GetColaboradorResult Ok(ColaboradorDTO colaborador) => new(false, 200, null, colaborador);
    public static GetColaboradorResult Fail(string message, int codeError) => new(true, codeError, message, null);
}

public class ColaboradorService
{
    private readonly RestClientService _restClient;
    private readonly IColaboradorRepository _colaboradorRepository;


    private readonly HashSet<string> EventosValidos = new(StringComparer.OrdinalIgnoreCase){
        "employee_update",
        "job_hire",
        "job_termination",        
        "job_movement"
        };


    public ColaboradorService(RestClientService restClient, IColaboradorRepository colaboradorRepository)
    {
        _restClient = restClient;
        _colaboradorRepository = colaboradorRepository;
    }



    public async Task<GetColaboradorResult> handleEventWebhook(WebhookPayloadBody bodyPayload)
    {
        string eventType = bodyPayload.EventType;
        int idEmployeeBuk = bodyPayload.EmployeeId;

        EventLogger.Info("webhook_event", bodyPayload);    

        if (string.IsNullOrWhiteSpace(eventType))
        {                        
            await RegistrarBitacoraAsync(BitacoraDTO.NoSoportado(idEmployeeBuk, eventType));
            return GetColaboradorResult.Fail("Evento inválido", 400);
        }
                    
        if (!EventosValidos.Contains(eventType))
        {
            await RegistrarBitacoraAsync(BitacoraDTO.NoSoportado(idEmployeeBuk, eventType));
            return GetColaboradorResult.Fail("Evento no soportado", 400);
        }

        GetColaboradorResult result = await GetColaboradorByIdBuk(idEmployeeBuk);        
        if (result.IsError || result.colaborador is null)
        {
            await RegistrarBitacoraAsync(BitacoraDTO.Error(idEmployeeBuk, eventType, $"No se pudo obtener el colaborador de Buk: {result.ErrorMessage}"));
            return result;
        }
        ColaboradorDTO colaborador = result.colaborador;
        await AsignarJefeAsync(colaborador);
        try
        {
            switch (eventType)
            {
                case "employee_update" or "job_movement":
                    await _colaboradorRepository.Actualizar(colaborador);
                    break;

                case "job_hire":
                    //TODO: Pendiente el saber como colocar el departamento y puesto en el colaborador, ya que la API de Buk no los trae de forma directa, se tendría que obtener a través de otro endpoint o mapearlo de alguna forma con la información que se tiene del colaborador
                    await ProcesarJobHireAsync(idEmployeeBuk, colaborador);
                    break;

                case "job_termination":
                    
                    await _colaboradorRepository.RegistrarBaja(idEmployeeBuk.ToString(),
                                                               colaborador.ConceptoBaja,
                                                               colaborador.FechaBaja);
                    break;
            }
            await RegistrarBitacoraAsync(BitacoraDTO.Exito(idEmployeeBuk, eventType));
            return GetColaboradorResult.Ok(colaborador);
        }
        catch (InvalidOperationException ex)
        {
            await RegistrarBitacoraAsync(BitacoraDTO.Omitido(idEmployeeBuk, eventType, $"{ex.Message}"));
            return GetColaboradorResult.Fail(ex.Message, 409);
        }
        catch (Exception ex)
        {
            await RegistrarBitacoraAsync(BitacoraDTO.Error(idEmployeeBuk, eventType, $"Error al procesar evento: {ex.GetBaseException().Message}"));
            return GetColaboradorResult.Fail($"Error al procesar evento {eventType}: {ex.Message}", 500);
        }
    }

    public async Task<List<ColaboradorDTO>> Sincronizar()
    {
        var colaboradores = new List<ColaboradorDTO>();
        var firstPageResponse = await _restClient.GetAsync<ResponseListColaborador>("/employees");
        if (firstPageResponse?.data == null)
        {
            return colaboradores;
        }
        colaboradores.AddRange(firstPageResponse.data.Select(colaborador => colaborador.ToColaboradorDTO()));
        int totalPages = firstPageResponse.pagination?.total_pages ?? 1;
        if (totalPages <= 1)
        {

            return colaboradores;
        }
        var pageTasks = Enumerable.Range(2, totalPages - 1).Select(page => _restClient.GetAsync<ResponseListColaborador>($"/employees?page={page}"));
        var pageResponses = await Task.WhenAll(pageTasks);
        foreach (var pageResponse in pageResponses)
        {
            if (pageResponse?.data == null)
            {
                continue;
            }
            colaboradores.AddRange(pageResponse.data.Select(colaborador => colaborador.ToColaboradorDTO()));
        }
        foreach (var colaborador in colaboradores)
        {
            await _colaboradorRepository.Actualizar(colaborador.id!.Value, colaborador.IdColaborador);
        }

        return colaboradores;
    }


    #region Métodos privados

    private async Task ProcesarJobHireAsync(long idEmployeeBuk, ColaboradorDTO colaborador)
    {
        var colaboradorDB = await _colaboradorRepository.BuscarPorId((int)idEmployeeBuk);
        if (colaboradorDB is not null)
            throw new InvalidOperationException("Colaborador ya existe en la base de datos");

        await RegistrarSiNoExisteAsync(colaborador);
        await _restClient.PatchAsync($"/employees/{idEmployeeBuk}", new
        {
            custom_attributes = new { idColaborador = colaborador.IdColaborador }
        });
    }

    private async Task<GetColaboradorResult> GetColaboradorByIdBuk(long idEmployeeBuk)
    {
        try
        {
            var response = await _restClient.GetAsync<ResponseColaborador>($"/employees/{idEmployeeBuk}");
            if (response?.data == null)
            {
                return GetColaboradorResult.Fail("Colaborador no encontrado", 404);
            }
            return GetColaboradorResult.Ok(response.data.ToColaboradorDTO());
        }
        catch (JsonException ex)
        {
            return GetColaboradorResult.Fail($"Error al deserializar JSON: {ex.Message}", 500);
        }
        catch (HttpRequestException httpEx)
        {
            var statusCode = httpEx.StatusCode.HasValue ? (int)httpEx.StatusCode.Value : (int)HttpStatusCode.InternalServerError;
            return GetColaboradorResult.Fail(httpEx.Message, statusCode);
        }
        catch (Exception e)
        {
            return GetColaboradorResult.Fail(e.GetBaseException().Message, 500);
        }
    }


    private async Task AsignarJefeAsync(ColaboradorDTO colaborador)
    {
        if (!colaborador.BossId.HasValue || colaborador.BossId.Value <= 0)
        {
            return;
        }

        var resultBoss = await GetColaboradorByIdBuk(colaborador.BossId.Value);
        if (!resultBoss.IsError && resultBoss.colaborador is not null)
        {
            colaborador.ReportaA = resultBoss.colaborador.IdColaborador;
        }
    }

    private async Task RegistrarSiNoExisteAsync(ColaboradorDTO colaborador)
    {

        var nuevoCodigoPersonal = await _colaboradorRepository.ObtenerSiguienteClavePersonal();
        colaborador.IdColaborador = nuevoCodigoPersonal.ToString();
        await _colaboradorRepository.Insertar(colaborador, nuevoCodigoPersonal);
    }



    private async Task RegistrarBitacoraAsync(BitacoraDTO bitacora)
    {
        try
        {
            await _colaboradorRepository.InsertarBitacora(bitacora);
        }
        catch (Exception ex)
        {
            EventLogger.Error("Error al registrar bitácora", ex, new { bitacora, error = ex.GetBaseException().Message });
        }
    }
    #endregion

}
