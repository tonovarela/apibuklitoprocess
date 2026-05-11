using apiBukLitoprocess.Clases;
using apiBukLitoprocess.conf;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.mappers;
using apiBukLitoprocess.repository.implementation;
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

    public async Task<List<ChecadaDTO>> RegistroChecadas(DateOnly desde)
    {
        List<ColaboradorDTO> colaboradores = await ObtenerColaboradoresActivos();
        List<ChecadaDTO> checadaDTOs = new List<ChecadaDTO>();
        List<string> ListRFC = colaboradores.Select(c => c.RFC).ToList();
        foreach (var rfc in ListRFC)
        {
            var checadasColaborador = await ObtenerChecadas(rfc, desde);
            if (checadasColaborador.Count > 0)
            {
                checadaDTOs.AddRange(checadasColaborador);
            }
        }

        Console.WriteLine($"Total de colaboradores activos obtenidos: {colaboradores.Count}");
        await _asistenciaRepository.InsertarLoteChecadasIgnorandoDuplicados(checadaDTOs);
        return checadaDTOs;
    }


    private async Task<List<ColaboradorDTO>> ObtenerColaboradoresActivos()
    {
        var colaboradores = new List<ColaboradorDTO>();
        var firstPageResponse = await _restClient.GetAsync<ResponseListColaborador>(ApiClientNames.Buk, "employees/active?page_size=100");
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
        var pageTasks = Enumerable.Range(2, totalPages - 1).Select(page => _restClient.GetAsync<ResponseListColaborador>(ApiClientNames.Buk, $"employees/active?page={page}&page_size=100"));
        var pageResponses = await Task.WhenAll(pageTasks);
        foreach (var pageResponse in pageResponses)
        {
            if (pageResponse?.data == null)
            {
                continue;
            }
            colaboradores.AddRange(pageResponse.data.Select(colaborador => colaborador.ToColaboradorDTO()));
        }
        return colaboradores;

    }

    public async Task<List<ChecadaDTO>> ObtenerChecadas(string RFC, DateOnly desde)
    {
        var checadas = new List<ChecadaDTO>();
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        try
        {
            var firstPageResponse = await _restClient.GetAsync<ResponseChecada>(ApiClientNames.Asistencia, $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100");
            if (firstPageResponse?.Data == null)
            {

                return checadas;
            }
            checadas.AddRange(firstPageResponse.Data.Select(checada => checada.ToChecadaDTO()));
            long totalPages = firstPageResponse.Pagination?.TotalPages ?? 1;
            if (totalPages <= 1)
            {
                return checadas;
            }
            var pageTasks = Enumerable.Range(2, (int)totalPages - 1).Select(page => _restClient.GetAsync<ResponseChecada>(ApiClientNames.Asistencia, $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100&page={page}"));
            var pageResponses = await Task.WhenAll(pageTasks);
            foreach (var pageResponse in pageResponses)
            {
                if (pageResponse?.Data == null)
                {
                    continue;
                }
                checadas.AddRange(pageResponse.Data.Select(checada => checada.ToChecadaDTO()));
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.GetBaseException().Message);
        }

        return checadas;
    }



  

}


