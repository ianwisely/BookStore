using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.AuthorFirstName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.AuthorLastName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CoverImage)
                .IsRequired();

            this.Property(t => t.ISBN)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Book");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.AuthorFirstName).HasColumnName("AuthorFirstName");
            this.Property(t => t.AuthorLastName).HasColumnName("AuthorLastName");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.CoverImage).HasColumnName("CoverImage");
            this.Property(t => t.ISBN).HasColumnName("ISBN");
            this.Property(t => t.AmountInStock).HasColumnName("AmountInStock");

            // Relationships
            this.HasMany(t => t.Orders)
                .WithMany(t => t.Books)
                .Map(m =>
                    {
                        m.ToTable("BookOrder");
                        m.MapLeftKey("BookId");
                        m.MapRightKey("OrderId");
                    });


        }
    }
}
