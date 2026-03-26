using System;
using System.Xml.Serialization;
using apiBukLitoprocess.DTOs;

namespace apiBukLitoprocess.repository.interfaces;

public interface IColaboradorRepository
{


public Task Actualizar(ColaboradorDTO colaborador);

public Task<ColaboradorDTO?> BuscarPorId(int id);

public Task  Actualizar(long id, string idColaborador);

public Task Insertar(ColaboradorDTO colaborador,int nuevoIdColaborador);

public Task InsertarBitacora(BitacoraDTO bitacora);

public Task<int> ObtenerSiguienteClavePersonal();

public Task RegistrarBaja(string idPersonalBuk, string conceptoBaja,string fechaBaja);


public Task<String> ObtenerEquivalenciaArea(long idAreaBuk);  

}
