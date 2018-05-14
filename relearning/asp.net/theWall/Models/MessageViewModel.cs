using System;
using System.ComponentModel.DataAnnotations;

namespace theWall.Models
{
    public class MessageViewModel
    {
        [Required]
        [MinLength(1)]
        public string Message { get; set; }
    }

    public class CommentViewModel
    {
        [Required]
        [MinLength(1)]
        public string Comment { get; set; }
    }


    public class MessageAndCommentViewModel
    {
        public Messages MessageViewModel { get; set; }
        public Comments CommentViewModel { get; set; }
    }
}