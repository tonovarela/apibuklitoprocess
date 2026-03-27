using System;

namespace apiBukLitoprocess.DTOs;

public class AsistenciaDTO
{
    public long IdChecada { get; set; }
    public required string RFC { get; set; }
    public required string Nombre { get; set; }
    public required string Entrada { get; set; }
    public required string Salida { get; set; }
    public DateOnly Dia { get; set; }
    public bool TurnoNoche { get; set; }    
    public long TrabajadorId { get; set; }
    public  required string Turno { get; set; }   
    public required string CodigoTurno { get; set; }

}
