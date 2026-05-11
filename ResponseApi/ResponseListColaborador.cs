


namespace apiBukLitoprocess.responseApi;

public class ResponseListColaborador
{ 
    public required Pagination pagination { get; set; }              
    public IEnumerable<ColaboradorResponse> data { get; set; } = new List<ColaboradorResponse>();
}


