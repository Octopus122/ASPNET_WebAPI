using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{

    public class CommentRepository: ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CommentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _dbContext.Comment.ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
           return await _dbContext.Comment.FindAsync(id);
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _dbContext.Comment.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return comment;
        }
        public async Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            var existingComment = await _dbContext.Comment.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;
            await _dbContext.SaveChangesAsync();
            return existingComment;
        }
        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var comment = await _dbContext.Comment.FirstOrDefaultAsync(x=>x.Id == id);
            if (comment == null)
            {
                return null;
            }
            _dbContext.Comment.Remove(comment); 
            await _dbContext.SaveChangesAsync();
            return comment;
        }
    }
}