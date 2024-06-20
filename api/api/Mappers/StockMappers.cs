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
	}
}
