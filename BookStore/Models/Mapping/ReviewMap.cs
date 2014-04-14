using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Models.Mapping
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comment)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Review");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Rating).HasColumnName("Rating");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.NumberOfLikes).HasColumnName("NumberOfLikes");
            this.Property(t => t.DateAdded).HasColumnName("DateAdded");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Reviews)
                .HasForeignKey(d => d.UserId);

        }
    }
}
