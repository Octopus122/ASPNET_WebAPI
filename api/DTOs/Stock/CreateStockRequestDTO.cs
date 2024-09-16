using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
    public class CreateStockRequestDTO
    {
        // data validation пример
        [Required]
        [MinLength(3, ErrorMessage = "Symbol должен быть длиннеее 3")]
        [MaxLength(8, ErrorMessage = "Symbol должен быть короче 8")]
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }  
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}