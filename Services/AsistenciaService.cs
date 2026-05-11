using apiBukLitoprocess.Clases;
using apiBukLitoprocess.conf;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.mappers;
using apiBukLitoprocess.repository.implementation;
using apiBukLitoprocess.repository.interfaces;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.Services;

public class AsistenciaService
{
    private readonly RestClientService _restClient;
    private readonly IAsistenciaRepository _asistenciaRepository;

    public AsistenciaService(RestClientService restClient, IAsistenciaRepository asistenciaRepository)
    {
        _restClient = restClient;
        _asistenciaRepository = asistenciaRepository;
    }

    public async Task<List<ChecadaDTO>> RegistroChecadas(DateOnly desde)
    {
        List<ColaboradorDTO> colaboradores = await ObtenerColaboradoresActivos();
        List<ChecadaDTO> checadaDTOs = new List<ChecadaDTO>();
        List<string> ListRFC = colaboradores.Select(c => c.RFC).ToList();
        foreach (var rfc in ListRFC)
        {
            var checadasColaborador = await ObtenerChecadas(rfc, desde);
            if (checadasColaborador.Count > 0)
            {
                checadaDTOs.AddRange(checadasColaborador);
            }
        }

        Console.WriteLine($"Total de colaboradores activos obtenidos: {colaboradores.Count}");
        await _asistenciaRepository.InsertarLoteChecadasIgnorandoDuplicados(checadaDTOs);
        return checadaDTOs;
    }


    private async Task<List<ColaboradorDTO>> ObtenerColaboradoresActivos()
    {

        return await ObtenerPaginadoAsync<ResponseListColaborador, ColaboradorResponse, ColaboradorDTO>(
            ApiClientNames.Buk,
            page => page == 1
                ? "employees/active?page_size=100"
                : $"employees/active?page={page}&page_size=100",
            response => response.data,
            response => response.pagination?.TotalPages ?? 1,
            colaborador => colaborador.ToColaboradorDTO()
            );
    }



    public async Task<List<ChecadaDTO>> ObtenerChecadas(string RFC, DateOnly desde)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        try
        {
            return await ObtenerPaginadoAsync<ResponseChecada, ChecadaRest, ChecadaDTO>(
                ApiClientNames.Asistencia,
                page => page == 1
                    ? $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100"
                    : $"obtenerRegistroAsistencia?obra_id=36915&from={desde:dd-MM-yyyy}&to={today:dd-MM-yyyy}&dni_colaborador={RFC}&page_size=100&page={page}",
                response => response.Data,
                response => response.Pagination?.TotalPages ?? 1,
                checada => checada.ToChecadaDTO());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.GetBaseException().Message);
            return new List<ChecadaDTO>();
        }       
    }




    /// <summary>
    /// Recupera todos los registros de un endpoint paginado, obteniendo la primera página,
    /// calculando el total de páginas disponibles y consultando el resto en paralelo.
    /// </summary>
    /// <typeparam name="TResponse">
    /// Tipo de la respuesta deserializada devuelta por el cliente HTTP.
    /// </typeparam>
    /// <typeparam name="TOrigen">
    /// Tipo de los elementos contenidos en la colección de la respuesta.
    /// </typeparam>
    /// <typeparam name="TDestino">
    /// Tipo final al que se transforma cada elemento antes de devolverlo.
    /// </typeparam>
    /// <param name="clientName">
    /// Nombre del cliente HTTP configurado que se utilizará para hacer la petición.
    /// </param>
    /// <param name="construirUrl">
    /// Función que construye la URL a consultar en función del número de página.
    /// </param>
    /// <param name="obtenerItems">
    /// Función que extrae la colección de elementos desde la respuesta del servicio.
    /// </param>
    /// <param name="obtenerTotalPaginas">
    /// Función que obtiene el total de páginas disponibles a partir de la primera respuesta.
    /// </param>
    /// <param name="mapear">
    /// Función que transforma cada elemento recuperado al tipo de salida esperado.
    /// </param>
    /// <returns>
    /// Lista con todos los elementos de todas las páginas ya transformados al tipo destino.
    /// Si la respuesta inicial es nula o no contiene elementos, devuelve una lista vacía.
    /// </returns>
    private async Task<List<TDestino>> ObtenerPaginadoAsync<TResponse, TOrigen, TDestino>(
       string clientName,
       Func<int, string> construirUrl,
       Func<TResponse, IEnumerable<TOrigen>?> obtenerItems,
       Func<TResponse, int> obtenerTotalPaginas,
       Func<TOrigen, TDestino> mapear)
    {
        var resultado = new List<TDestino>();

        var primeraRespuesta = await _restClient.GetAsync<TResponse>(clientName, construirUrl(1));
        if (primeraRespuesta == null)
        {
            return resultado;
        }

        var primerosItems = obtenerItems(primeraRespuesta);
        if (primerosItems == null)
        {
            return resultado;
        }

        resultado.AddRange(primerosItems.Select(mapear));

        int totalPaginas = obtenerTotalPaginas(primeraRespuesta);
        if (totalPaginas <= 1)
        {
            return resultado;
        }

        var tareasPaginas = Enumerable.Range(2, totalPaginas - 1)
            .Select(page => _restClient.GetAsync<TResponse>(clientName, construirUrl(page)));

        var respuestas = await Task.WhenAll(tareasPaginas);

        foreach (var respuesta in respuestas)
        {
            if (respuesta == null)
            {
                continue;
            }

            var items = obtenerItems(respuesta);
            if (items == null)
            {
                continue;
            }

            resultado.AddRange(items.Select(mapear));
        }

        return resultado;
    }











}


