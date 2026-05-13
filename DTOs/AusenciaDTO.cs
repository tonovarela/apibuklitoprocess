using System;

namespace apiBukLitoprocess.DTOs;

public class AusenciaDTO
{
    public long id_Ausencia { get; set; }
    public  string? personal { get; set; }
    public required long id_colaborador { get; set; }
    public required string tipo { get; set; }
    public required string fecha { get; set; }
    public required string justificacion { get; set; }
    public string? horaEntrada { get; set; }
    public string? horaSalida { get; set; }




}
