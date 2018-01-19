using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PersonalPortfolio.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int BlogPostId { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public virtual BlogPost BlogPost { get; set; }

        public override bool Equals(System.Object otherComment)
        {
            if (!(otherComment is Comment))
            {
                return false;
            }
            else
            {
                Comment newItem = (Comment)otherComment;
                return this.CommentId.Equals(newItem.CommentId);
            }
        }

        public override int GetHashCode()
        {
            return this.CommentId.GetHashCode();
        }

        public Comment(string Author, string Body, int BlogPostId)
        {

            this.Author = Author;
            this.Body = Body;
            this.BlogPostId = BlogPostId;
        }

        public Comment()
        {
        }

    }
}