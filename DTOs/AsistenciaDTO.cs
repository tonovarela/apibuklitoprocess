using System;

namespace apiBukLitoprocess.DTOs;

public class AsistenciaDTO
{
    public long id_asistencia { get; set; }
    public required string rfc{ get; set; }
    //public required string Nombre { get; set; }
     public required string entrada { get; set; }
     public required string salida { get; set; }
    public DateTime dia { get; set; }
    public bool turno_noche { get; set; }    
    // public long TrabajadorId { get; set; }
    public  required string turno { get; set; }   
    public required string codigo_turno { get; set; }

}
