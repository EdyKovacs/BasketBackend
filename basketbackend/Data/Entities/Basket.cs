using System.Collections.Generic;

namespace Data
{
    public class Basket
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public bool Closed { get; set; }
        public bool Paid { get; set; }
        public ICollection<Item> Items { get; set; }

        public Basket(string customer, bool paysVAT)
        {
            Customer = customer;
            PaysVAT = paysVAT;
        }
    }
}
