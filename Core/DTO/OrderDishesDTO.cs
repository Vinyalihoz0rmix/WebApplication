﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class OrderDishesDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public string Path { get; set; }
    }
}
