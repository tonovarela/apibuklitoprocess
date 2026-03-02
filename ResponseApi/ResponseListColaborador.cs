using System.Collections;
using System;
using System.Collections.Generic;


namespace apiBukLitoprocess.responseApi;

public class ResponseListColaborador
{ 
    public Pagination? pagination { get; set; }  = null ;             
    public IEnumerable<BodyResponseColaborador> data { get; set; } = new List<BodyResponseColaborador>();
}

public class Pagination{ 

    public string? next { get; set; }

    public string? previous { get; set; }

    public int count { get; set; } =0;

    public int? total_pages { get; set; }
    
}
