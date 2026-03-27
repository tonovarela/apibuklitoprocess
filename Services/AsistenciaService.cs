using apiBukLitoprocess.Clases;
using apiBukLitoprocess.repository.interfaces;

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
    

}
