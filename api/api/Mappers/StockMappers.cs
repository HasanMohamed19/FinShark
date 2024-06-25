using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
	public static class StockMappers
	{
		public static StockDto ToStockDto(this Stock stockModel)
		{
			return new StockDto
			{
				Id = stockModel.Id,
				Symbol = stockModel.Symbol,
				CompanyName = stockModel.CompanyName,
				Industry = stockModel.Industry,
				LastDiv = stockModel.LastDiv,
				MarketCap = stockModel.MarketCap,
				Purchase = stockModel.Purchase,
				Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
			};
		}

		public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockRequesDto)
		{
			return new Stock
			{
				Symbol = stockRequesDto.Symbol,
				CompanyName = stockRequesDto.CompanyName,
				Purchase = stockRequesDto.Purchase,
				LastDiv = stockRequesDto.LastDiv,
				Industry = stockRequesDto.Industry,
				MarketCap = stockRequesDto.MarketCap
			};
		}

		public static Stock ToStockFromFMP(this FMPStock fmpStock)
		{
			return new Stock
			{
				Symbol = fmpStock.symbol,
				CompanyName = fmpStock.companyName,
				Purchase = (decimal)fmpStock.price,
				LastDiv = (decimal)fmpStock.lastDiv,
				Industry = fmpStock.industry,
				MarketCap = fmpStock.mktCap
			};
		}
	}
}
