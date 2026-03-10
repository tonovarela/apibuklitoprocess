using System;
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
    public ColaboradorService(RestClientService restClient, IColaboradorRepository colaboradorRepository)
    {
        _restClient = restClient;
        _colaboradorRepository = colaboradorRepository;
    }

    public async Task<GetColaboradorResult> handleEventWebhook(string eventType, int idEmployee)
    {

        GetColaboradorResult result = await GetColaboradorById(idEmployee);
        if (result.IsError || result.colaborador == null)
        {
            return result;
        }
        ColaboradorDTO colaborador = result.colaborador;
       //Se obtiene el jefe
        var resultBoss = await GetColaboradorById(colaborador.BossId ?? 0 );                
        if (!resultBoss.IsError && resultBoss.colaborador != null)
        {             
            result.colaborador.ReportaA = resultBoss.colaborador.IdColaborador;
        }
        

        if (eventType == "employee_update")
        {            
            await _colaboradorRepository.Actualizar(result.colaborador);
        }
        return result;
    }

    public async Task<List<ColaboradorDTO>> sincronizar()
    {
        var colaboradores = new List<ColaboradorDTO>();
        var firstPageResponse = await _restClient.GetAsync<ResponseListColaborador>("/employees");
        if (firstPageResponse?.data == null)
        {
            return colaboradores;
        }
        colaboradores.AddRange(firstPageResponse.data.Select(colaborador=>colaborador.ToColaboradorDTO()));
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
            colaboradores.AddRange(pageResponse.data.Select(colaborador=>colaborador.ToColaboradorDTO()));
        }
        //colaboradores.ForEach(async colaborador => await _colaboradorRepository.Actualizar(colaborador.id!.Value, colaborador.IdColaborador));
        return colaboradores;
    }


    private async Task<GetColaboradorResult> GetColaboradorById(long idEmployee)
    {
        try
        {
            var response = await _restClient.GetAsync<ResponseColaborador>($"/employees/{idEmployee}");
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


}
