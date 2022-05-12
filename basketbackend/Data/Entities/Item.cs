using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Data
{
    public class Item
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [JsonIgnore]
        public int BasketId { get; set; }
        [JsonIgnore]
        public Basket Basket { get; set; }

        public override bool Equals(object obj)
            => obj is Item item &&
                   Id == item.Id &&
                   Name == item.Name &&
                   Price == item.Price &&
                   BasketId == item.BasketId;

        public override int GetHashCode() => HashCode.Combine(Id, Name, Price, BasketId, Basket);
    }
}
