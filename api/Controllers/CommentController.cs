using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Repository;
using api.Mappers;
using api.DTOs.Comment;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository comRepo, IStockRepository stockRepo)
        {
            _commentRepository = comRepo;
            _stockRepository = stockRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments.Select(x => x.ToCommentDTO()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentbyId([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> PostComment([FromRoute]int stockId, [FromBody] CreateCommentDTO comment)
        {
            if (! await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exists!");
            }
            var newComment = await _commentRepository.CreateCommentAsync(comment.ToCommentFromCreate(stockId));

            return CreatedAtAction(nameof(GetCommentbyId), new {id = newComment.Id}, newComment.ToCommentDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutComment([FromRoute]int id, [FromBody] UpdateCommentDTO upComment)
        {
            var comment = await _commentRepository.UpdateCommentAsync(id, upComment.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetCommentbyId), new {id = comment.Id}, comment.ToCommentDTO());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteCommentAsync(id);    
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    
    }
}