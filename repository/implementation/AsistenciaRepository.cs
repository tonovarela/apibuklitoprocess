using System;
using apiBukLitoprocess.Data;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.repository.interfaces;
using Microsoft.Data.SqlClient;

namespace apiBukLitoprocess.repository.implementation;

public class AsistenciaRepository : IAsistenciaRepository
{

    private readonly DbConnectionFactory _dbConnectionFactory;

    public AsistenciaRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> Existe(long idChecada)
    {
         using var connection = _dbConnectionFactory.CreateConnection();
        {
            var query = "SELECT * FROM Buk.dbo.Asistencias where id=@Id;";
            var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", idChecada);  
             using var reader = await command.ExecuteReaderAsync();                  
             return reader.HasRows;            
        }
        
    }

    public async Task Insertar(AsistenciaDTO asistencia)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        {
            var query = @"INSERT INTO Buk.dbo.Asistencias 
                                (id,rfc,turno_noche,entrada,salida,dia,trab_id,nombre,turno,codigo_turno)
                                VALUES(@Id,@Rfc,@TurnoNoche,@Entrada,@Salida,@Dia,@TrabId,@Nombre,@Turno,@CodigoTurno);
             ";
             var command = new SqlCommand(query, (SqlConnection)connection);
                command.Parameters.AddWithValue("@Id", asistencia.IdChecada);
                command.Parameters.AddWithValue("@Rfc", asistencia.RFC);
                command.Parameters.AddWithValue("@Nombre", asistencia.Nombre);
                command.Parameters.AddWithValue("@TurnoNoche", asistencia.TurnoNoche);
                command.Parameters.AddWithValue("@Entrada", asistencia.Entrada);
                command.Parameters.AddWithValue("@Salida", asistencia.Salida);
                command.Parameters.AddWithValue("@Dia", asistencia.Dia);
                command.Parameters.AddWithValue("@TrabId", asistencia.TrabajadorId);                
                command.Parameters.AddWithValue("@Turno", asistencia.Turno);
                command.Parameters.AddWithValue("@CodigoTurno", asistencia.CodigoTurno);
            
             await command.ExecuteNonQueryAsync();
        }
    }
}
