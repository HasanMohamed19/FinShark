using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[Route("/api/stock")]
	[ApiController]
    // ControllerBase no view Support
	public class StockController : ControllerBase
	{
        //public IActionResult Index()
        //{
        //	return View();
        //}

        private readonly ApplicationDBContext _dbContext;

        public StockController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _dbContext.Stocks.ToList()
                .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _dbContext.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }

    }
}
