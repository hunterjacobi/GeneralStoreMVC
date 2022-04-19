using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace GeneralStoreMVC.Data
{
    public partial class Customer
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
