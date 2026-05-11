using System;

namespace apiBukLitoprocess.DTOs;

public class SolicitudDTO
{
  public long id_solicitud { get; set; }
  public long id_colaborador  { get; set; }
  public TipoSolicitud tipo { get; set; } 
  public DateTime fechaSolicitud    { get; set; }
  public DateTime fechaInicio { get; set; }
  public DateTime fechaFin { get; set; }
  public long id_autorizo { get; set; }
}


public enum TipoSolicitud
{
    VACACIONES,
    INCIDENCIA
}