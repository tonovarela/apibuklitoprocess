using System;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class SolicitudExtensions
{

 public static SolicitudDTO toSolicitudDTO(this VacacionesRest vacaciones)
    {

        return new SolicitudDTO
        {
            id_solicitud = vacaciones.id,
            id_colaborador = vacaciones.employee_id,
            tipo = TipoSolicitud.VACACIONES,
            fechaSolicitud = vacaciones.requested_at.DateTime,
            fechaInicio = vacaciones.start_date.ToDateTime(new TimeOnly(0, 0)),
            fechaFin = vacaciones.end_date.ToDateTime(new TimeOnly(0, 0)),
            id_autorizo = vacaciones.approved_by_id
        };
    }

}
