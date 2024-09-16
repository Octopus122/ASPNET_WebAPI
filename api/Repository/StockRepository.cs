using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Data;

// Для класса Stock
using api.Models;

//Для работы ToListAsync()
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

using api.DTOs.Stock;


namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public Task<List<Stock>> GetAllStocksAsync()
        {
            return _dbContext.Stock.Include(C => C.Comments).ToListAsync();
        }
        // ? - нужен для ситуации, если нужный Stock не найдётся
        public Task<Stock?> GetByIdAsync(int id)
        {
            return _dbContext.Stock.Include(C => C.Comments).FirstOrDefaultAsync(x=> x.Id == id);
        }
        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _dbContext.Stock.AddAsync(stock);
            await _dbContext.SaveChangesAsync();

            return stock;
        }
        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO updateStock)
        {
            var stock = await _dbContext.Stock.FindAsync(id);

            if (stock == null)
            {
                return null;
            }
            stock.Symbol = updateStock.Symbol;
            stock.Purchase = updateStock.Purchase;
            stock.MarketCap = updateStock.MarketCap;
            stock.LastDiv = updateStock.LastDiv;
            stock.CompanyName = updateStock.CompanyName;
            stock.Industry = updateStock.Industry;
            
            await _dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _dbContext.Stock.FindAsync(id);
            if (stock == null)
            {
                return null;
            }

            // Нет Async для Remove
            _dbContext.Stock.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _dbContext.Stock.AnyAsync(x => x.Id == id);
        }
    }
}