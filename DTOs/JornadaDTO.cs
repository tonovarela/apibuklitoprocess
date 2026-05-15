using System;

namespace apiBukLitoprocess.DTOs;

public class JornadaDTO
{
    public required string Id_Jornada { get; set; }
    public required string RFC { get; set; }
    public required DateTime Fecha { get; set; }
    public  required string Jornada { get; set; }
    public required string Turno { get; set; }

}
