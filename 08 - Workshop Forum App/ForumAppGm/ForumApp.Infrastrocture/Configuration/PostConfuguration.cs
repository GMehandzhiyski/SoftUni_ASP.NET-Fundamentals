using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Configuration
{
    public class PostConfuguration : IEntityTypeConfiguration<Post>
    {
        private Post[] initialPosts = new Post[]
            {
                new Post(){ Id = 1, Title = "My First Posts", Content = "My First post content" },
                new Post(){ Id = 2, Title = "My Second Posts", Content = "My Second post content" },
                new Post(){ Id = 3, Title = "My Third Posts", Content = "My Third post content" }

            };

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }
    }
}
