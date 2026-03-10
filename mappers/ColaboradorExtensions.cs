using System;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class ColaboradorExtensions
{
     public static ColaboradorDTO ToColaboradorDTO(this ColaboradorResponse colaborador)
    {        
         return new ColaboradorDTO
        {
            id = colaborador.id,
            Nombre = colaborador.first_name ?? String.Empty,
            ApellidoPaterno = colaborador.surname ?? String.Empty,
            ApellidoMaterno = colaborador.second_surname ?? String.Empty,
            
            CURP = colaborador.curp ?? String.Empty,
            RFC = colaborador.rfc ?? String.Empty,
            Correo = colaborador.personal_email ?? String.Empty,
            NSS = colaborador.social_security ?? String.Empty,
            Direccion = colaborador.address ?? String.Empty,
            CodigoPostal = colaborador.postal_code ?? String.Empty,
            
            Telefono = colaborador.phone ?? String.Empty,
            FechaNacimiento = colaborador.birthday?.ToString("yyyy-dd-MM") ?? "1990-01-01",
            
            Sexo = colaborador.gender == "M" ? "Masculino" : "Femenino",
            Ext  =colaborador.custom_attributes?.Ext ?? String.Empty,            

            IdColaborador = colaborador.custom_attributes?.idColaborador?.Trim() ?? "**",
            TipoColaborador = colaborador.custom_attributes?.tipoColaborador ?? String.Empty,
            Alergias = colaborador.custom_attributes?.Alergias ?? String.Empty,
            NumInt = colaborador.custom_attributes?.numInt ?? String.Empty,
            NumExt = colaborador.custom_attributes?.numExt ?? String.Empty,
            CentroCostos = colaborador.current_job?.cost_center ?? String.Empty,

            // Datos de contacto de emergencia
            Beneficiario1 = colaborador.custom_attributes?.Beneficiario1 ?? String.Empty,
            Beneficiario2 = colaborador.custom_attributes?.Beneficiario2 ?? String.Empty,
            Beneficiario3 = colaborador.custom_attributes?.Beneficiario3 ?? String.Empty,

            ParentescoBeneficiario1 = colaborador.custom_attributes?.parentescoBeneficiario1 ?? String.Empty,
            ParentescoBeneficiario2 = colaborador.custom_attributes?.parentescoBeneficiario2 ?? String.Empty,
            ParentescoBeneficiario3 = colaborador.custom_attributes?.parentescoBeneficiario3 ?? String.Empty,

            FechaNacimientoBeneficiario1 = colaborador.custom_attributes?.fecNacBeneficiario1 ?? String.Empty,
            FechaNacimientoBeneficiario2 = colaborador.custom_attributes?.fecNacBeneficiario2 ?? String.Empty,
            FechaNacimientoBeneficiario3 = colaborador.custom_attributes?.fecNacBeneficiario3 ?? String.Empty,

            PorcentajeBeneficiario1 = colaborador.custom_attributes?.porcBeneficiario1,
            PorcentajeBeneficiario2 = colaborador.custom_attributes?.porcBeneficiario2,
            PorcentajeBeneficiario3 = colaborador.custom_attributes?.porcBeneficiario3,
            

            ContactoEmergencia1 = colaborador.custom_attributes?.contactoEmergencia1 ?? String.Empty,
            ContactoEmergencia2 = colaborador.custom_attributes?.contactoEmergencia2 ?? String.Empty,
            ParentescoContactoEmerg1 = colaborador.custom_attributes?.parentestoContactoEmerg1 ?? String.Empty,
            ParentescoContactoEmerg2 = colaborador.custom_attributes?.parentestoContactoEmerg2 ?? String.Empty,

            TelContactoEmerg1 = colaborador.custom_attributes?.telContactoEmerg1 ?? String.Empty,
            TelContactoEmerg2 = colaborador.custom_attributes?.telContactoEmerg2 ?? String.Empty,
            TipoSangre = colaborador.custom_attributes?.tipoSangre ?? String.Empty,             
            EstadoCivil = colaborador.custom_attributes?.EstadoCivil ?? String.Empty,
            
            NivelAcademico = colaborador.custom_attributes?.NivelAcademico ?? String.Empty,
            Colonia = colaborador.custom_attributes?.Colonia ?? String.Empty,
            Delegacion = colaborador.custom_attributes?.Delegacion ?? String.Empty,
            Poblacion = colaborador.custom_attributes?.Delegacion ?? String.Empty,


            BossId= colaborador.current_job?.boss.id ?? 0
                                                              


        };
    }

}
