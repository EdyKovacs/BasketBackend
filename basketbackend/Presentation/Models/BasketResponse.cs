using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace basketbackend.Presentation.Models
{
    public class BasketResponse
    {
        public int Id { get; set; }
        public double TotalNet { get; set; }
        public double TotalGross { get; set; }
        public string Customer { get; set; }
        public bool Closed { get; set; }
        public bool PaysVAT { get; set; }
        public ICollection<Item> Items { get; set; }

        public override bool Equals(object obj)
            => obj is BasketResponse response &&
                   Id == response.Id &&
                   TotalNet == response.TotalNet &&
                   TotalGross == response.TotalGross &&
                   Customer == response.Customer &&
                   Closed == response.Closed &&
                   PaysVAT == response.PaysVAT &&
                   Enumerable.SequenceEqual(Items, response.Items);

        public override int GetHashCode() => HashCode.Combine(Id, TotalNet, TotalGross, Customer, Closed, PaysVAT, Items);
    }
}
