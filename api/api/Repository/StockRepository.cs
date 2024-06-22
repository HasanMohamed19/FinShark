using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
	public class StockRepository : IStockRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public StockRepository(ApplicationDBContext context)
		{
			_dbContext = context;
		}

		public async Task<List<Stock>> GetAllAsync()
		{
			return await _dbContext.Stocks.Include(c => c.Comments).ToListAsync();
		}

		public async Task<Stock> CreateAsync(Stock stockModel)
		{
			await _dbContext.AddAsync(stockModel);
			await _dbContext.SaveChangesAsync();
			return stockModel;
		}

		public async Task<Stock?> DeleteAsync(int id)
		{
			var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (stockModel == null)
			{
				return null;
			}

			_dbContext.Stocks.Remove(stockModel);
			await _dbContext.SaveChangesAsync();
			return stockModel;
		}

		public async Task<Stock?> GetByIdAsync(int id)
		{
			return await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<bool> StockExists(int id)
		{
			return _dbContext.Stocks.AnyAsync(s => s.Id == id);
		}

		public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
		{
			var existingStock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id);

			if (existingStock == null)
			{
				return null;
			}

			existingStock.Symbol = stockDto.Symbol;
			existingStock.CompanyName = stockDto.CompanyName;
			existingStock.Purchase = stockDto.Purchase;
			existingStock.LastDiv = stockDto.LastDiv;
			existingStock.Industry = stockDto.Industry;
			existingStock.MarketCap = stockDto.MarketCap;

			await _dbContext.SaveChangesAsync();
			return existingStock;
		}
	}
}
