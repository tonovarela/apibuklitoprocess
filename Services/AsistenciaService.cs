using apiBukLitoprocess.Clases;
using apiBukLitoprocess.conf;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.mappers;
using apiBukLitoprocess.repository.interfaces;
using apiBukLitoprocess.responseApi;
namespace apiBukLitoprocess.Services;

public class AsistenciaService
{
    private readonly RestClientService _restClient;
    private readonly IAsistenciaRepository _asistenciaRepository;

    public AsistenciaService(RestClientService restClient, IAsistenciaRepository asistenciaRepository)
    {
        _restClient = restClient;
        _asistenciaRepository = asistenciaRepository;
    }


    public async Task<List<JornadaDTO>> registroJornada(DateOnly desde)
    {

        var jornadas = await _restClient.ObtenerPaginadoAsync<ResponseAsistencia, AsistenciaRest, JornadaDTO>(
            ApiClientNames.Asistencia,
            page => page == 1
                ? $"v2/asistencia-empresa?desde={desde:dd-MM-yyyy}&page_size=100"
                : $"v2/asistencia-empresa?desde={desde:dd-MM-yyyy}&page_size=100&page={page}",
            response => response.Data,
            response => response.Pagination?.TotalPages ?? 1,
            asistencia => asistencia.toJornadaDTO()
            );

        var jornadasUnicas = jornadas.GroupBy(x => new { x.RFC, Fecha = x.Fecha.Date })
                       .Select(g => g.First())
                       .ToList();
        await _asistenciaRepository.InsertarJornadasIgnorandoDuplicados(jornadasUnicas);              
        return jornadasUnicas;
    }

    public async Task<List<ChecadaDTO>> RegistroChecadas(DateOnly desde)
    {
        List<ColaboradorDTO> colaboradores = await ObtenerColaboradoresActivos();
        List<ChecadaDTO> checadaDTOs = new List<ChecadaDTO>();
        List<string> ListRFC = colaboradores.Select(c => c.RFC).ToList();
        Console.WriteLine($"Total colaboradores activos: {ListRFC.Count}");
        foreach (var rfc in ListRFC)
        {
            try
            {
                var checadasColaborador = await ObtenerChecadas(rfc, desde);
                if (checadasColaborador.Count > 0)
                {
                    EventLogger.Info($"Obtenidas {checadasColaborador.Count} checadas para RFC {rfc}");
                    checadaDTOs.AddRange(checadasColaborador);
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener checadas para RFC {rfc}: {e.GetBaseException().Message}");
            }
        }

        await _asistenciaRepository.InsertarLoteChecadasIgnorandoDuplicados(checadaDTOs);
        return checadaDTOs;
    }


    private async Task<List<ColaboradorDTO>> ObtenerColaboradoresActivos()
    {
        return await _restClient.ObtenerPaginadoAsync<ResponseListColaborador, ColaboradorResponse, ColaboradorDTO>(
            ApiClientNames.Buk,
            page => page == 1
                ? "employees/active?page_size=100"
                : $"employees/active?page={page}&page_size=100",
            response => response.data,
            response => response.pagination?.TotalPages ?? 1,
            colaborador => colaborador.ToColaboradorDTO()
            );
    }



    public async Task<List<ChecadaDTO>> ObtenerChecadas(string RFC, DateOnly desde)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        try
        {
            return await _restClient.ObtenerPaginadoAsync<ResponseChecada, ChecadaRest, ChecadaDTO>(
                ApiClientNames.Asistencia,
                page => page == 1
                    ? $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100"
                    : $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100&page={page}",
                response => response.Data,
                response => response.Pagination?.TotalPages ?? 1,
                checada => checada.ToChecadaDTO());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.GetBaseException().Message);
            return new List<ChecadaDTO>();
        }
    }




}


