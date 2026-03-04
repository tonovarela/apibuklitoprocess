using System;
using apiBukLitoprocess.Data;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.repository.interfaces;
using Microsoft.Data.SqlClient;

namespace apiBukLitoprocess.repository.implementation;

public class ColaboradorRepository : IColaboradorRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public ColaboradorRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }


    public async Task Actualizar(ColaboradorDTO colaborador)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        {
            var query = @"UPDATE dbo.Personal set 
                                usuario=@Id,
                                ApellidoPaterno=@ApellidoPaterno,
                                ApellidoMaterno=@ApellidoMaterno,
                                Nombre=@Nombre,
                                Registro=@Curp,
                                Registro2=@Rfc,
                                Registro3=@NSS,
                                Correo=@Correo,                                
                                Direccion=@Direccion,
                                Colonia=@Colonia,
                                Delegacion=@Delegacion,
                                Poblacion=@Poblacion,
                                Estado=@Estado,
                                Pais=@Pais,
                                CodigoPostal=@CodigoPostal,
                                Telefono=@Telefono,
                                FechaNacimiento=@FechaNacimiento
                                where personal=@personal";
            var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", colaborador.id);                    
            command.Parameters.AddWithValue("@ApellidoPaterno", colaborador.ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", colaborador.ApellidoMaterno);
            command.Parameters.AddWithValue("@Nombre", colaborador.Nombre);
            command.Parameters.AddWithValue("@personal", colaborador.IdColaborador);
            command.Parameters.AddWithValue("@Curp", colaborador.CURP);
            command.Parameters.AddWithValue("@Rfc", colaborador.RFC);
            command.Parameters.AddWithValue("@Correo", colaborador.Correo ?? "Sin correo");
            command.Parameters.AddWithValue("@NSS", colaborador.NSS);
            command.Parameters.AddWithValue("@Direccion", colaborador.Direccion);
            command.Parameters.AddWithValue("@Colonia", colaborador.Colonia);
            command.Parameters.AddWithValue("@Delegacion", colaborador.Delegacion);
            command.Parameters.AddWithValue("@Poblacion", colaborador.Poblacion);
            command.Parameters.AddWithValue("@Estado", colaborador.Estado);
            command.Parameters.AddWithValue("@Pais", colaborador.Pais);
            command.Parameters.AddWithValue("@CodigoPostal", colaborador.CodigoPostal);
            command.Parameters.AddWithValue("@Telefono", colaborador.Telefono);
            command.Parameters.AddWithValue("@FechaNacimiento", colaborador.FechaNacimiento);   
            await command.ExecuteNonQueryAsync();    
        }
    }

    public async  Task Actualizar(long id, string idColaborador)
    {
         using var connection = _dbConnectionFactory.CreateConnection();
        {
            var query = "UPDATE dbo.Personal set usuario=@Id where personal=@personal";
            var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);                    
            command.Parameters.AddWithValue("@personal", idColaborador);
            await command.ExecuteNonQueryAsync();
        }
    }

   

    public void BuscarPorId(int id)
    {
        throw new NotImplementedException();
    }
}
