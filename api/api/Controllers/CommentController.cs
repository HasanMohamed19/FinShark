﻿using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{

		private readonly ICommentRepository _commentRepo;
		private readonly IStockRepository _stockRepo;
		private readonly IFMPService _fmpSerivce;
		private readonly UserManager<AppUser> _userManager;

		public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, IFMPService fmpService, UserManager<AppUser> userManager)
		{
			_commentRepo = commentRepo;
			_stockRepo = stockRepo;
			_userManager = userManager;
			_fmpSerivce = fmpService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comments = await _commentRepo.GetAllAsync();
			var commentDto = comments.Select(c => c.ToCommentDto());

			return Ok(commentDto);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comment = await _commentRepo.GetByIdAsync(id);
			if (comment == null)
			{
				return BadRequest("Comment does not exist");
			}

			return Ok(comment.ToCommentDto());
		}

		[HttpPost("{symbol:alpha}")]
		public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var stock = await _stockRepo.GetBySymbolAsync(symbol);

			if (stock == null)
			{
				// populate from fmpService
				stock = await _fmpSerivce.FindStockBySymbolAsync(symbol);
				if (stock == null)
				{
					return BadRequest("Stock does not exists");
				} else
				{
					await _stockRepo.CreateAsync(stock);
				}
			}

			var username = User.GetUsername();
			var appUser = await _userManager.FindByNameAsync(username);

			var commentModel = commentDto.ToCommentFromCreate(stock.Id);

			commentModel.AppUserId = appUser.Id;

			await _commentRepo.CreateAsync(commentModel);
			return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());

			if (comment == null)
			{
				return NotFound("Comment not found");
			}
			return Ok(comment.ToCommentDto());
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var comment = await _commentRepo.DeleteAsync(id);
			if (comment == null)
			{
				return NotFound("Comment does not exist");
			}

			return Ok(comment.ToCommentDto());
		}
	}
}
