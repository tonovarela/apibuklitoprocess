using System;

namespace apiBukLitoprocess.DTOs;

public class ChecadaDTO
{
    public required String Id_Checada { get; set; }
    public required String RFC { get; set; }
    public required String Tipo { get; set; }
    public required int Dia { get; set; }
    public required int Mes { get; set; }
    public required int Anio { get; set; }

    public required int Hora { get; set; }
    public required int Minuto { get; set; }
    public required int Segundo { get; set; }

    public required DateTime Fecha { get; set; }



}
