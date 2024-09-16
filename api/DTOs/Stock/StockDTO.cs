using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

using api.DTOs.Comment;

namespace api.DTOs.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }  
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommetGetDTO>? Comments {get; set;}
    }
}