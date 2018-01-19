using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PersonalPortfolio.Models
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ApplicationUser User { get; set; }

        public override bool Equals(System.Object otherBlogPost)
        {
            if (!(otherBlogPost is BlogPost))
            {
                return false;
            }
            else
            {
                BlogPost newItem = (BlogPost)otherBlogPost;
                return this.BlogPostId.Equals(newItem.BlogPostId);
            }
        }

        public override int GetHashCode()
        {
            return this.BlogPostId.GetHashCode();
        }

    }
}
