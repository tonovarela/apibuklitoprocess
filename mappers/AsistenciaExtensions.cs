using System;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class AsistenciaExtensions
{

    public static AsistenciaDTO ToAsistenciaDTO(this AsistenciaRest asistenciaRest)
    {
        return new AsistenciaDTO
        {
            IdChecada = asistenciaRest.Id,
            TrabajadorId = asistenciaRest.TrabId,
            RFC = asistenciaRest.RutTrabajador ?? String.Empty,
            Nombre = $"{asistenciaRest.Nombre} {asistenciaRest.ApellidoPaterno} {asistenciaRest.ApellidoMaterno}".Trim(),
            Entrada = asistenciaRest.EntradaFormat ?? String.Empty,
            Salida = asistenciaRest.SalidaFormat ?? String.Empty,
            Dia = DateOnly.FromDateTime(asistenciaRest.Entrada.DateTime),
            TurnoNoche = asistenciaRest.TurnoNoche,
            Turno = asistenciaRest.Turno,
            CodigoTurno = asistenciaRest.CodigoTurno
        };


    }

}
