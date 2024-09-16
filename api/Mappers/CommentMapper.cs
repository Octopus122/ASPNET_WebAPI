using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommetGetDTO ToCommentDTO (this Comment comment)
        {
            return new CommetGetDTO 
            { 
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDTO comment, int id)
        {
            return new Comment 
            { 
                Title = comment.Title,
                Content = comment.Content,
                StockId = id
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentDTO comment)
        {
            return new Comment 
            { 
                Title = comment.Title,
                Content = comment.Content,
            };
        }
    }
}