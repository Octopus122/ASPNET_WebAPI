using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.DTOs.Stock;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO updateStock);
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> StockExists(int id);   

    }
}