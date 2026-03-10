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
     
        try {

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
                                email=@Correo,                                
                                Direccion=@Direccion,
                                Colonia=@Colonia,
                                Delegacion=@Delegacion,
                                Poblacion=@Poblacion,
                                Estado=@Estado,
                                Pais=@Pais,
                                CodigoPostal=@CodigoPostal,
                                Telefono=@Telefono,
                                FechaNacimiento=@FechaNacimiento,
                                


                                NivelAcademico=@NivelAcademico,
                                Sexo=@Sexo,
                                EstadoCivil=@EstadoCivil,
                                Beneficiario = @Beneficiario1,
                                BeneficiarioNacimiento = @BeneficiarioNacimiento1,
                                ParentescoBeneficiario = @ParentescoBeneficiario1,
                                PorcentajeBeneficiario = @PorcentajeBeneficiario1,

                                Beneficiario2 = @Beneficiario2,
                                Beneficiario2Nacimiento = @BeneficiarioNacimiento2,                                
                                Parentesco2 = @ParentescoBeneficiario2,
                                Porcentaje2 = @PorcentajeBeneficiario2,

                                Beneficiario3 = @Beneficiario3,
                                Beneficiario3Nacimiento = @BeneficiarioNacimiento3,
                                Parentesco3 = @ParentescoBeneficiario3,
                                Porcentaje3 = @PorcentajeBeneficiario3,

                                DireccionNumero = @NumExt,
                                DireccionNumeroInt = @NumInt,
                                CentroCostos = @CentroCostos
                                

                                where personal=@personal";
            var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", colaborador.id);                    
            command.Parameters.AddWithValue("@ApellidoPaterno", colaborador.ApellidoPaterno);
            command.Parameters.AddWithValue("@ApellidoMaterno", colaborador.ApellidoMaterno);
            command.Parameters.AddWithValue("@Nombre", colaborador.Nombre);
            command.Parameters.AddWithValue("@personal", colaborador.IdColaborador);
            command.Parameters.AddWithValue("@Curp", colaborador.CURP);
            command.Parameters.AddWithValue("@Rfc", colaborador.RFC);
            command.Parameters.AddWithValue("@Correo", colaborador.Correo);
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
            command.Parameters.AddWithValue("@EstadoCivil", colaborador.EstadoCivil);  
            command.Parameters.AddWithValue("@NivelAcademico", colaborador.NivelAcademico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Sexo", colaborador.Sexo ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@Beneficiario1", colaborador.Beneficiario1 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@BeneficiarioNacimiento1", colaborador.BeneficiarioNacimiento1 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@ParentescoBeneficiario1", colaborador.ParentescoBeneficiario1 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@PorcentajeBeneficiario1", colaborador.PorcentajeBeneficiario1 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@Beneficiario2", colaborador.Beneficiario2 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@BeneficiarioNacimiento2", colaborador.BeneficiarioNacimiento2 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@ParentescoBeneficiario2", colaborador.ParentescoBeneficiario2 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@PorcentajeBeneficiario2", colaborador.PorcentajeBeneficiario2 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@Beneficiario3", colaborador.Beneficiario3 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@BeneficiarioNacimiento3", colaborador.BeneficiarioNacimiento3 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@ParentescoBeneficiario3", colaborador.ParentescoBeneficiario3 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@PorcentajeBeneficiario3", colaborador.PorcentajeBeneficiario3 ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@NumExt", colaborador.NumExt ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@NumInt", colaborador.NumInt ?? (object)DBNull.Value);
            // command.Parameters.AddWithValue("@CentroCostos", colaborador.CentroCostos ?? (object)DBNull.Value);


            await command.ExecuteNonQueryAsync();    

        } 
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error al actualizar colaborador  con id {colaborador.IdColaborador}: {ex.Message}");        
        }
    }

    public async Task Actualizar(long id, string idColaborador)
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

   

    public async Task<ColaboradorDTO?> BuscarPorId(int id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        {
            var query = "SELECT * FROM dbo.Personal where usuario=@Id";
            var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);                    
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                Console.WriteLine($"Colaborador encontrado: {reader["Nombre"]} {reader["ApellidoPaterno"]} {reader["ApellidoMaterno"]}");
                return new ColaboradorDTO
                {
                    id = reader["usuario"] as long?,
                    Nombre = reader["Nombre"].ToString() ?? "Sin nombre",
                    ApellidoPaterno = reader["ApellidoPaterno"].ToString() ?? "Sin apellido paterno",
                    ApellidoMaterno = reader["ApellidoMaterno"].ToString() ?? "Sin apellido materno",
                    IdColaborador = reader["personal"].ToString() ?? "Sin id colaborador",
                    CURP = reader["Registro"].ToString() ?? "Sin curp",
                    RFC = reader["Registro2"].ToString() ?? "Sin rfc",
                    Correo = reader["email"].ToString() ?? "Sin correo",
                    NSS = reader["Registro3"].ToString() ?? "Sin NSS",
                    Direccion = reader["Direccion"].ToString() ?? "Sin dirección",
                    Colonia = reader["Colonia"].ToString() ?? "Sin colonia",
                    Delegacion = reader["Delegacion"].ToString() ?? "Sin delegación",
                    Poblacion = reader["Poblacion"].ToString() ?? "Sin población",
                    Estado = reader["Estado"].ToString() ?? "Sin estado",
                    Pais = reader["Pais"].ToString() ?? "Sin país",
                    CodigoPostal = reader["CodigoPostal"].ToString() ?? "Sin código postal",
                    Telefono = reader["Telefono"].ToString() ?? "Sin teléfono",
                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("yyyy-MM-dd"),
                    EstadoCivil = reader["EstadoCivil"].ToString() ?? "Sin estado civil"
                };
            }            
             
                return null;
        }
    }
        
    
}
