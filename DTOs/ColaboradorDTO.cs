using System;

namespace apiBukLitoprocess.DTOs;

public class ColaboradorDTO
{

    public long?  id { get; set; }
    public required string Nombre { get; set; }
    public required string ApellidoPaterno { get; set; }

    public required string ApellidoMaterno { get; set; }

    public  string NombreCompleto { 
        get
        {
            return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";
        }
    }

     public required string IdColaborador  { get; set; }
     public required string CURP { get; set; }
     public required string RFC { get; set; }
     public  string? Correo { get; set; }

     public string NSS { get; set; } = "Sin NSS";

     public string Direccion { get; set; } = "Sin dirección";

     public string Colonia { get; set; } = "Sin colonia";
     public string Delegacion { get; set; } = "Sin delegación";

     public string Poblacion { get; set; } = "Sin población";
     public string Estado { get; set; } = "Sin estado";

     public string Pais { get; set; } = "Mexico";

     public string CodigoPostal { get; set; } = "Sin código postal";

     public string Telefono { get; set; } = "Sin teléfono";

     public required string FechaNacimiento { get; set; } 

     public required string EstadoCivil { get; set; }



}
