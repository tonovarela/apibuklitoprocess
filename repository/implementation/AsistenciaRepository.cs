using System;
using System.Data;
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

    // public async Task<bool> Existe(long idChecada)
    // {
    //      using var connection = _dbConnectionFactory.CreateConnection();
    //     {
    //         var query = "SELECT * FROM Buk.dbo.Asistencias where id=@Id;";
    //         var command = new SqlCommand(query, (SqlConnection)connection);
    //         command.Parameters.AddWithValue("@Id", idChecada);  
    //          using var reader = await command.ExecuteReaderAsync();                  
    //          return reader.HasRows;            
    //     }
        
    // }

    // public async Task Insertar(AsistenciaDTO asistencia)
    // {
    //     using var connection = _dbConnectionFactory.CreateConnection();
    //     {
    //         var query = @"INSERT INTO Buk.dbo.Asistencias 
    //                             (id,rfc,turno_noche,entrada,salida,dia,trab_id,nombre,turno,codigo_turno)
    //                             VALUES(@Id,@Rfc,@TurnoNoche,@Entrada,@Salida,@Dia,@TrabId,@Nombre,@Turno,@CodigoTurno);
    //          ";
    //          var command = new SqlCommand(query, (SqlConnection)connection);
    //             command.Parameters.AddWithValue("@Id", asistencia.IdChecada);
    //             command.Parameters.AddWithValue("@Rfc", asistencia.RFC);
    //             command.Parameters.AddWithValue("@Nombre", asistencia.Nombre);
    //             command.Parameters.AddWithValue("@TurnoNoche", asistencia.TurnoNoche);
    //             command.Parameters.AddWithValue("@Entrada", asistencia.Entrada);
    //             command.Parameters.AddWithValue("@Salida", asistencia.Salida);
    //             command.Parameters.AddWithValue("@Dia", asistencia.Dia);
    //             command.Parameters.AddWithValue("@TrabId", asistencia.TrabajadorId);                
    //             command.Parameters.AddWithValue("@Turno", asistencia.Turno);
    //             command.Parameters.AddWithValue("@CodigoTurno", asistencia.CodigoTurno);
            
    //          await command.ExecuteNonQueryAsync();
    //     }
    // }

     public async Task InsertarLoteChecadasIgnorandoDuplicados(List<ChecadaDTO> checadaDTOs)
    {
        Console.WriteLine("Iniciando inserción de checadas en la base de datos..."); 
        if (checadaDTOs is null || checadaDTOs.Count == 0) return;

          using var connection = (SqlConnection)_dbConnectionFactory.CreateConnection();
          using var tx = connection.BeginTransaction();

           const string sql = @"
        INSERT INTO Buk.dbo.Checadas
            (id, rfc, anio, mes,dia,hora, minutos, segundos, tipo, fecha)
        VALUES
            (@Id, @Rfc, @Anio, @Mes, @Dia, @Hora, @Minutos, @Segundos, @Tipo, @Fecha);";

       
       
    try
    {
        using var cmd = new SqlCommand(sql, connection, tx);

        cmd.Parameters.Add("@Id", SqlDbType.VarChar,200);
        cmd.Parameters.Add("@Rfc", SqlDbType.VarChar, 50);
        cmd.Parameters.Add("@Anio", SqlDbType.Int);
         cmd.Parameters.Add("@Mes", SqlDbType.Int);
         cmd.Parameters.Add("@Dia", SqlDbType.Int);
         cmd.Parameters.Add("@Hora", SqlDbType.Int);
        cmd.Parameters.Add("@Minutos", SqlDbType.Int);
        cmd.Parameters.Add("@Segundos", SqlDbType.Int);
        cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50);
        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime); 

        foreach (var a in checadaDTOs)
        {
            cmd.Parameters["@Id"].Value = a.Id_Checada;
            cmd.Parameters["@Rfc"].Value = a.RFC;
            cmd.Parameters["@Anio"].Value = a.Anio;
            cmd.Parameters["@Mes"].Value = a.Mes;
            cmd.Parameters["@Dia"].Value = a.Dia;
            cmd.Parameters["@Hora"].Value = a.Hora;
            cmd.Parameters["@Minutos"].Value = a.Minuto;
            cmd.Parameters["@Segundos"].Value = a.Segundo;
            cmd.Parameters["@Tipo"].Value = a.Tipo;
            cmd.Parameters["@Fecha"].Value = a.Fecha;

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex) 
            {
                Console.WriteLine(ex.Message);
                // Duplicate key (PK/Unique). Ignorar.
            }
        }

        tx.Commit();
    }
    catch
    {
        Console.WriteLine("Error durante la inserción de asistencias. Realizando rollback.");
        tx.Rollback();
        //throw;
    }


          
        
        
    }

    public async Task InsertarLoteIgnorandoDuplicados(List<AsistenciaDTO> asistencias)
{

    Console.WriteLine("Iniciando inserción de asistencias en la base de datos..."); 
    if (asistencias is null || asistencias.Count == 0) return;

    using var connection = (SqlConnection)_dbConnectionFactory.CreateConnection();
    using var tx = connection.BeginTransaction();

    const string sql = @"
        INSERT INTO Buk.dbo.Asistencias
            (id, rfc, turno_noche, dia,entrada,salida, trab_id, nombre, turno, codigo_turno)
        VALUES
            (@Id, @Rfc, @TurnoNoche, @Dia,@Entrada,@Salida, @TrabId, @Nombre, @Turno, @CodigoTurno);";

    try
    {
        using var cmd = new SqlCommand(sql, connection, tx);

        cmd.Parameters.Add("@Id", SqlDbType.BigInt);
        cmd.Parameters.Add("@Rfc", SqlDbType.VarChar, 50);
        cmd.Parameters.Add("@TurnoNoche", SqlDbType.Bit);
         cmd.Parameters.Add("@Entrada", SqlDbType.VarChar, 20);
         cmd.Parameters.Add("@Salida", SqlDbType.VarChar, 20);
         cmd.Parameters.Add("@Dia", SqlDbType.Date); // tu columna es DATE
        cmd.Parameters.Add("@TrabId", SqlDbType.BigInt);
        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200);
        cmd.Parameters.Add("@Turno", SqlDbType.VarChar, 100);
        cmd.Parameters.Add("@CodigoTurno", SqlDbType.VarChar, 50);

        foreach (var a in asistencias)
        {
            cmd.Parameters["@Id"].Value = a.IdChecada;
            cmd.Parameters["@Rfc"].Value = a.RFC;
            cmd.Parameters["@TurnoNoche"].Value = a.TurnoNoche;            
            cmd.Parameters["@Entrada"].Value = a.Entrada.Length > 3 ? a.Entrada : null;
            cmd.Parameters["@Salida"].Value = a.Salida.Length > 3 ? a.Salida : null;
            cmd.Parameters["@Dia"].Value = a.Dia.ToDateTime(TimeOnly.MinValue);
            cmd.Parameters["@TrabId"].Value = a.TrabajadorId;
            cmd.Parameters["@Nombre"].Value = a.Nombre;
            cmd.Parameters["@Turno"].Value = a.Turno;
            cmd.Parameters["@CodigoTurno"].Value = a.CodigoTurno;

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex) 
            {
                Console.WriteLine(ex.Message);
                // Duplicate key (PK/Unique). Ignorar.
            }
        }

        tx.Commit();
    }
    catch
    {
        Console.WriteLine("Error durante la inserción de asistencias. Realizando rollback.");
        tx.Rollback();
        //throw;
    }


}
}
