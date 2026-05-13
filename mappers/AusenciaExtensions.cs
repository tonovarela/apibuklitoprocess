using System;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class AusenciaExtensions
{
    public static AusenciaDTO ToAusenciaDTO(this AusenciaRest ausencia)
    {
        return new AusenciaDTO
        {
            id_Ausencia = ausencia.Id,            
            id_colaborador = ausencia.EmployeeId,
            tipo = ausencia.Tipo,
            fecha = ausencia.Fecha_inicio.ToString("yyyy-MM-dd"),
            justificacion = ausencia.Justificacion,
            horaEntrada = ausencia.Inicio,
            horaSalida = ausencia.Fin
            
        };
    }

}
