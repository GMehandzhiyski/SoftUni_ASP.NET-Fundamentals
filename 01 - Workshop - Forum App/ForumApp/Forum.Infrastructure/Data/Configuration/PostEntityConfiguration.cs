namespace Forum.Infrastructure.Data.Configuration
{
    using Forum.Infrastructure.Data.Models;
    using Forum.Infrastructure.Data.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        private readonly PostSeeder _seeder;
        public PostEntityConfiguration()
        {
            _seeder = new PostSeeder();
        }
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.
                   HasData(_seeder.GeneratePosts());
        }
    }
}
