﻿using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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

		public async Task<List<Stock>> GetAllAsync(QueryObject query)
		{
			var stocks = _dbContext.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.ComapnyName))
			{
				stocks = stocks.Where(s => s.CompanyName.Contains(query.ComapnyName));
			}

			if (!string.IsNullOrWhiteSpace(query.Symbol))
			{
				stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
			}

			if (!string.IsNullOrWhiteSpace(query.SortBy))
			{
				if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
				{
					stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
				}
			}

			var skipNumber = (query.PageNumber - 1) * query.PageSize;

			return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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

		public async Task<Stock?> GetBySymbolAsync(string symbol)
		{
			return await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
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
