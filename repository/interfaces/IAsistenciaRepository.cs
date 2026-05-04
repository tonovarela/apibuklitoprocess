using System;
using apiBukLitoprocess.DTOs;

namespace apiBukLitoprocess.repository.interfaces;

public interface IAsistenciaRepository
{

  public Task InsertarLoteChecadasIgnorandoDuplicados(List<ChecadaDTO> checadaDTOs);
    

}
