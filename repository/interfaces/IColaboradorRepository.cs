using System;
using System.Xml.Serialization;
using apiBukLitoprocess.DTOs;

namespace apiBukLitoprocess.repository.interfaces;

public interface IColaboradorRepository
{


public Task Actualizar(ColaboradorDTO colaborador);

public  Task<ColaboradorDTO?> BuscarPorId(int id);

public Task  Actualizar(long id, string idColaborador);


}
