using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Stock;
using api.Interfaces;

namespace api
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController( IStockRepository repository)
        {
            _stockRepository = repository;
        }

        // Для получение (чтения) информации
        [HttpGet]
        // public IActionResult GetAll() -- предыдущая версия
        public async Task<IActionResult> GetAll()
        {
            // Получает все stocks (вызывая DbGet), в виде списка
            // Используем репозиторий, взаимодействующий с базой данных 
            var stocks = await _stockRepository.GetAllStocksAsync();
            //Преобразуем в DTO (убираем комменты)
            var stockDTO = stocks.Select(x => x.ToStockDTO());

            return Ok(stockDTO);
        }
        // dotnet получает id из пути и дальше перегружает его в int 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Производится поиск по id
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                // вид IActionResult
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }
        [HttpPost]
        // FromBody - напрямую из http
        // CreateStockDTO - DTO для контроля и валидации введенной пользователем информации 
        public async Task<IActionResult> PostStock([FromBody] CreateStockRequestDTO stockDTO)
        {
            // var stock = stockDTO.ToStockFromCreateDTO();
            // _dbContext.Stock.Add(stock);
            // _dbContext.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest("Поля заполнены не правильно");
            }

            var createdStock = await _stockRepository.CreateStockAsync(stockDTO.ToStockFromCreateDTO());

            // Вызывает GetById с созданным id от созданного stock и выводит его на экран. 
            return CreatedAtAction (nameof(GetById), new {id = createdStock.Id}, createdStock.ToStockDTO());
        }

        [HttpPut("{id}")]
        // Еще один способ прописать id в путь
        // [Route("{id}")]
        public async Task<IActionResult> PutStock([FromRoute] int id, [FromBody] UpdateStockDTO updateStock)
        {
            // var stock = _dbContext.Stock.FirstOrDefault(x=>x.Id == id);
            // var stock = _dbContext.Stock.Find(id);

            // if (stock == null)
            // {
            //     return NotFound();
            // }
            // stock.Symbol = updateStock.Symbol;
            // stock.Purchase = updateStock.Purchase;
            // stock.MarketCap = updateStock.MarketCap;
            // stock.LastDiv = updateStock.LastDiv;
            // stock.CompanyName = updateStock.CompanyName;
            // stock.Industry = updateStock.Industry;
            
            // _dbContext.SaveChanges();
            var stock = await _stockRepository.UpdateStockAsync(id, updateStock);
            if (stock == null)
            {
                return NotFound();
            }

            return CreatedAtAction (nameof(GetById), new {id = stock.Id}, stock.ToStockDTO());
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> StockDelete([FromRoute] int id)
        {
            // var stock = _dbContext.Stock.Find(id);
            // if (stock == null)
            // {
            //     return NotFound();
            // }
            // _dbContext.Stock.Remove(stock);
            // _dbContext.SaveChanges();

            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}