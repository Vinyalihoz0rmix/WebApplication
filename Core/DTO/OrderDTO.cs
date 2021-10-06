using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal FullPrice { get; set; }
        public int CountDish { get; set; }
    }
}
