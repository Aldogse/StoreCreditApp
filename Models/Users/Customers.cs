using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Credit> Credits { get; set; } = null!;
        public int TotalCreditAmount { get; set; } = 0;
    }
}
