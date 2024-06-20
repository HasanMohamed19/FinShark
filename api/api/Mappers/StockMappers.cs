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
				Purchase = stockModel.Purchase
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
	}
}
