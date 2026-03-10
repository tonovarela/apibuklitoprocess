using System.Collections;
using System;
using System.Collections.Generic;


namespace apiBukLitoprocess.responseApi;

public class ResponseListColaborador
{ 
    public required Pagination pagination { get; set; }              
    public IEnumerable<ColaboradorResponse> data { get; set; } = new List<ColaboradorResponse>();
}

public class Pagination{ 

    public string? next { get; set; }

    public string? previous { get; set; }

    public int count { get; set; } =0;

    public int total_pages { get; set; } =0;
    
}
