using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        //NAVIGATIONAL PROPERTY
        [JsonIgnore]
        public Products Products { get; set; }

        public DateTime CreditDate { get; set; } = DateTime.Now;
        public DateTime? PaidDate { get; set; } 
        public bool IsPaid { get; set; } = false;
        public int CustomerId { get; set; }
        public int? Quantity { get; set; }
    }
}
