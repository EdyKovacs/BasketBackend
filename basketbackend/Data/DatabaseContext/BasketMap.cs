using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class BasketMap
    {
        public BasketMap(EntityTypeBuilder<Basket> entityBuilder)
        {
            entityBuilder.HasKey(basket => basket.Id);
            entityBuilder.ToTable("basket");
            entityBuilder.Property(basket => basket.Id).HasColumnName("id");
            entityBuilder.Property(basket => basket.Customer).HasColumnName("customer");
            entityBuilder.Property(basket => basket.PaysVAT).HasColumnName("paysvat");
            entityBuilder.Property(basket => basket.Paid).HasColumnName("paid");
            entityBuilder.Property(basket => basket.Closed).HasColumnName("closed");
            entityBuilder.HasMany(basket => basket.Items).WithOne(item => item.Basket);
        }
    }
}