
using System.Security.Cryptography;
using System.Text;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.helpers;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class ChecadaExtensions
{
  
    public static ChecadaDTO ToChecadaDTO(this ChecadaRest checadaRest)
    {
        string rawId = $"{checadaRest.RFC}-{checadaRest.Dia}-{checadaRest.Mes}-{checadaRest.Ano}-{checadaRest.Hora}-{checadaRest.Minutos}-{checadaRest.Segundos}-{checadaRest.Tipo}";
        string hashId = HashGenerator.Generate(rawId);
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
