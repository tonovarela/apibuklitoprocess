

namespace apiBukLitoprocess.DTOs;

public class SolicitudDTO
{
  public long id_solicitud { get; set; }
  public long id_colaborador  { get; set; }

  public string personal { get; set; } = string.Empty;
  public TipoSolicitud tipo { get; set; } 
  public DateTime fechaSolicitud    { get; set; }
  public DateTime fechaInicio { get; set; }
  public DateTime fechaFin { get; set; }
  public DateTime fechaAutorizacion { get; set; }
  public long id_autorizo { get; set; }

  public double diasHabiles { get; set; }
  
}


public enum TipoSolicitud
{
    VACACIONES = 1,
    INCIDENCIA = 2
}