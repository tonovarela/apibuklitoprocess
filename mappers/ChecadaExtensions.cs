
using System.Security.Cryptography;
using System.Text;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class ChecadaExtensions
{

    public static string GenerateHashId(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }


    public static ChecadaDTO ToChecadaDTO(this ChecadaRest checadaRest)
    {
        string rawId = $"{checadaRest.RFC}-{checadaRest.Dia}-{checadaRest.Mes}-{checadaRest.Ano}-{checadaRest.Hora}-{checadaRest.Minutos}-{checadaRest.Segundos}-{checadaRest.Tipo}";
        string hashId = GenerateHashId(rawId);
        return new ChecadaDTO
        {
            Id_Checada = hashId,
            RFC = checadaRest.RFC,
            Dia = checadaRest.Dia,
            Hora = checadaRest.Hora,
            Tipo = checadaRest.Tipo,
            Mes = checadaRest.Mes,
            Anio = checadaRest.Ano,
            Segundo = checadaRest.Segundos,
            Minuto = checadaRest.Minutos,
            Fecha = new DateTime(checadaRest.Ano, checadaRest.Mes, checadaRest.Dia, checadaRest.Hora, checadaRest.Minutos, checadaRest.Segundos)
        };

    }

}
