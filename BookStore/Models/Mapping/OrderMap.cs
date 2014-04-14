using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Models.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Order");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DateAdded).HasColumnName("DateAdded");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.IsBought).HasColumnName("IsBought");
            this.Property(t => t.PriceAfterDiscount).HasColumnName("PriceAfterDiscount");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.UserId);

        }
    }
}
