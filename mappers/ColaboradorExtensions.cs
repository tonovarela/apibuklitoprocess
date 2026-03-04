using System;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class ColaboradorExtensions
{
     public static ColaboradorDTO ToColaboradorDTO(this BodyResponseColaborador colaborador)
    {        
         return new ColaboradorDTO
        {
            id = colaborador.id,
            Nombre = colaborador.first_name ?? String.Empty,
            ApellidoPaterno = colaborador.surname ?? String.Empty,
            ApellidoMaterno = colaborador.second_surname ?? String.Empty,
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
