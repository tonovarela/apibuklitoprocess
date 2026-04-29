using apiBukLitoprocess.Clases;
using apiBukLitoprocess.conf;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.mappers;
using apiBukLitoprocess.repository.interfaces;
using apiBukLitoprocess.responseApi;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Identity.Client;

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
        List<string> ListRFC = colaboradores.Select(c => c.RFC).ToList();
        foreach (var rfc in ListRFC)
        {

            var checadasColaborador = await ObtenerChecadas(rfc, desde);
            if (checadasColaborador.Count > 0)
            {
                Console.WriteLine($"RFC: {rfc}");
                Console.WriteLine($"Total de checadas obtenidas para RFC {rfc}: {checadasColaborador.Count}");
            }

        }

        Console.WriteLine($"Total de colaboradores activos obtenidos: {colaboradores.Count}");
        return new List<ChecadaDTO>();
    }



    public async Task<List<AsistenciaDTO>> RegistroAsistencias(DateOnly desde)
    {
        string rootEndpoint = $"v2/asistencia-empresa?page_size=100";
        var asistencias = new List<AsistenciaDTO>();

        var firstPageResponse = await _restClient.GetAsync<ResponseAsistencia>(ApiClientNames.Asistencia, $"{rootEndpoint}&desde={desde:dd-MM-yyyy}");

        if (firstPageResponse?.Data is null)
            return asistencias;
        asistencias.AddRange(firstPageResponse.Data
        .Where(asistencia => asistencia.SalidaFormat?.Length > 4)
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
            var asistenciasRegistar = pageResponse.Data
             .Where(asistencia => asistencia.SalidaFormat?.Length > 4)
             .Select(asistencia => asistencia.ToAsistenciaDTO()).ToList();

            asistencias.AddRange(asistenciasRegistar);
        }

        Console.WriteLine($"Total de asistencias obtenidas: {asistencias.Count}");
        await _asistenciaRepository.InsertarLoteIgnorandoDuplicados(asistencias);


        return asistencias;
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
        return checadas;
    }

}
