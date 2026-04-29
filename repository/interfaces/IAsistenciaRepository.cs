using System;
using apiBukLitoprocess.DTOs;

namespace apiBukLitoprocess.repository.interfaces;

public interface IAsistenciaRepository
{

  // public Task Insertar(AsistenciaDTO asistencia);

  // public Task<bool> Existe(long idChecada);

   public  Task InsertarLoteIgnorandoDuplicados(List<AsistenciaDTO> asistencias);

  public Task InsertarLoteChecadasIgnorandoDuplicados(List<ChecadaDTO> checadaDTOs);
    

}
