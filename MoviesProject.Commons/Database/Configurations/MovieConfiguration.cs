using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesProject.Commons.Models;

namespace MoviesProject.Commons.Database.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(m => m.Title)
            .HasMaxLength(100);

        builder
            .Property(m => m.OpenningCrawl)
            .HasColumnType("text");

        builder
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("now()");

        builder
            .Property(m => m.UpdatedAt)
            .HasDefaultValueSql("now()");

    }
}
