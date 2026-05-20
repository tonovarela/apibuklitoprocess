using System;
using apiBukLitoprocess.DTOs;

namespace apiBukLitoprocess.repository.interfaces;

public interface IAsistenciaRepository
{

  public Task InsertarLoteChecadasIgnorandoDuplicados(List<ChecadaDTO> checadaDTOs);

  public Task InsertarJornadasIgnorandoDuplicados(List<JornadaDTO> jornadas);
    

  public Task InsertarAsistenciasIgnorandoDuplicados(List<AsistenciaDTO> asistencias);

}
