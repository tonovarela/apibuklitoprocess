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

    public async Task<List<AsistenciaDTO>> RegistroAsistencias(DateOnly desde)
    {
        string rootEndpoint = $"v2/asistencia-empresa?page_size=100";
        var asistencias = new List<AsistenciaDTO>();
        var firstPageResponse = await _restClient.GetAsync<ResponseAsistencia>(ApiClientNames.Asistencia, $"{rootEndpoint}&desde={desde:dd-MM-yyyy}");
        if (firstPageResponse?.Data is null)
            return asistencias;
        asistencias.AddRange(firstPageResponse.Data
        .Where(asistencia => asistencia.SalidaFormat?.Length>4)
        .Select(asistencia => asistencia.ToAsistenciaDTO()));
        long totalPages = firstPageResponse.Pagination?.TotalPages ?? 1;
        Console.WriteLine($"Total de páginas: {firstPageResponse.Data.Count} en la primera página, Total de páginas: {totalPages}");
        if (totalPages <= 1)
        {
            await _asistenciaRepository.InsertarLoteIgnorandoDuplicados(asistencias);
            Console.WriteLine($"Total de asistencias obtenidas: {asistencias.Count}");
            return asistencias;
        }

        var pageTasks = Enumerable.Range(2, (int)totalPages - 1).Select(page => _restClient.GetAsync<ResponseAsistencia>(ApiClientNames.Asistencia, $"{rootEndpoint}&page={page}&desde={desde:dd-MM-yyyy}"));
        var pageResponses = await Task.WhenAll(pageTasks);
        foreach (var pageResponse in pageResponses)
        {
            if (pageResponse?.Data is null)
                continue;
           var asistenciasRegistar =     pageResponse.Data
            .Where(asistencia => asistencia.SalidaFormat?.Length>4)
            .Select(asistencia => asistencia.ToAsistenciaDTO()).ToList();

            asistencias.AddRange(asistenciasRegistar);
        }

        Console.WriteLine($"Total de asistencias obtenidas: {asistencias.Count}");
        await _asistenciaRepository.InsertarLoteIgnorandoDuplicados(asistencias);
        

        return asistencias;
    }


}
