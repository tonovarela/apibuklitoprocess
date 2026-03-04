using System;
using System.Net;
using System.Text.Json;
using apiBukLitoprocess.Clases;
using apiBukLitoprocess.DTOs;
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

        var result = await GetColaboradorById(idEmployee);
        if (result.IsError || result.colaborador == null)
        {
            return result;
        }
        Console.WriteLine(eventType);

        if (eventType == "employee_update")
        {
            Console.WriteLine($"Actualizando colaborador {result.colaborador.NombreCompleto} con id {result.colaborador.IdColaborador}");
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
        colaboradores.AddRange(firstPageResponse.data.Select(MapToColaboradorDTO));
        int totalPages = firstPageResponse.pagination?.total_pages ?? 1;
        if (totalPages <= 1)
        {

            return colaboradores;
        }
        var pageTasks = Enumerable
            .Range(2, totalPages - 1)
            .Select(page => _restClient.GetAsync<ResponseListColaborador>($"/employees?page={page}"));
        var pageResponses = await Task.WhenAll(pageTasks);
        foreach (var pageResponse in pageResponses)
        {
            if (pageResponse?.data == null)
            {
                continue;
            }
            colaboradores.AddRange(pageResponse.data.Select(MapToColaboradorDTO));
        }

        colaboradores.ForEach(async colaborador => await _colaboradorRepository.Actualizar(colaborador.id!.Value, colaborador.IdColaborador));

        return colaboradores;
    }


    private async Task<GetColaboradorResult> GetColaboradorById(int idEmployee)
    {

        try
        {
            var response = await _restClient.GetAsync<ResponseColaborador>($"/employees/{idEmployee}");
            if (response?.data == null)
            {
                return GetColaboradorResult.Fail("Colaborador no encontrado", 404);
            }
            return GetColaboradorResult.Ok(MapToColaboradorDTO(response.data));
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

    private ColaboradorDTO MapToColaboradorDTO(BodyResponseColaborador colaborador)
    {
        return new ColaboradorDTO
        {
            id = colaborador.id,
            Nombre = colaborador.first_name!,
            ApellidoPaterno = colaborador.surname!,
            ApellidoMaterno = colaborador.second_surname!,
            IdColaborador = colaborador.custom_attributes?.idColaborador?.Trim() ?? "**",
            CURP = colaborador.curp ?? "Sin curp",
            RFC = colaborador.rfc ?? "Sin rfc",
            Correo = colaborador.personal_email ?? "Sin correo",
            NSS = colaborador.social_security ?? "Sin NSS",
            Direccion = colaborador.address ?? "Sin dirección",
            CodigoPostal = colaborador.postal_code ?? "55555",
            Colonia = colaborador.custom_attributes?.Colonia ?? "Sin colonia",
            Delegacion = colaborador.custom_attributes?.Delegacion ?? "Sin delegación",
            Poblacion = colaborador.custom_attributes?.Delegacion ?? "Sin población",
            Telefono = colaborador.phone ?? "Sin teléfono",
            FechaNacimiento = colaborador.birthday?.ToString("yyyy-dd-MM") ?? "1990-01-01",
            EstadoCivil = colaborador.civil_status ?? "Soltero",

        };
    }

}
