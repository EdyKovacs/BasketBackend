using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class ItemMap
    {
        public ItemMap(EntityTypeBuilder<Item> entityBuilder)
        {
            entityBuilder.HasKey(item => item.Id);
            entityBuilder.ToTable("item");
            entityBuilder.Property(item => item.Id).HasColumnName("id");
            entityBuilder.Property(item => item.Name).HasColumnName("name");
            entityBuilder.Property(item => item.Price).HasColumnName("price");
            entityBuilder.Property(item => item.BasketId).HasColumnName("basket_id");
            entityBuilder.HasOne(item => item.Basket).WithMany(basket => basket.Items);
        }
    }
}
