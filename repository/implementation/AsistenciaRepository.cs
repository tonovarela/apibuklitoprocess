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

    public async Task InsertarJornadasIgnorandoDuplicados(List<JornadaDTO> jornadas)
    {

        Console.WriteLine("Iniciando inserción de jornadas en la base de datos...");
        if (jornadas is null || jornadas.Count == 0) 
        return;

        using var connection = (SqlConnection)_dbConnectionFactory.CreateConnection();
        using var tx = connection.BeginTransaction();
        const string sql = @"
            INSERT INTO Buk.dbo.Jornada
                (id_jornada, rfc, fecha, jornada, inicio, fin)
            VALUES
                (@Id, @Rfc, @Fecha, @Jornada, @Inicio, @Fin);";
       
    
        try
        {
            using var cmd = new SqlCommand(sql, connection, tx);
            cmd.Parameters.Add("@Id", SqlDbType.VarChar, 200);
            cmd.Parameters.Add("@Rfc", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
            cmd.Parameters.Add("@Jornada", SqlDbType.VarChar, 50);              
            cmd.Parameters.Add("@Inicio", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@Fin", SqlDbType.VarChar, 50);

            foreach (var a in jornadas)
            {
                cmd.Parameters["@Id"].Value = a.Id_Jornada;
                cmd.Parameters["@Rfc"].Value = a.RFC;
                cmd.Parameters["@Fecha"].Value = a.Fecha;
                cmd.Parameters["@Jornada"].Value = a.Jornada;            
                cmd.Parameters["@Inicio"].Value = (object?)a.Inicio ?? DBNull.Value;
                cmd.Parameters["@Fin"].Value = (object?)a.Fin ?? DBNull.Value;
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    //Console.WriteLine(ex.Message);
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

            cmd.Parameters.Add("@Id", SqlDbType.VarChar, 200);
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





}
