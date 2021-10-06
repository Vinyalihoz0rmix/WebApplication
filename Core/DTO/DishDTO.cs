﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public string Path { get; set; }
        public bool AddMenu { get; set; }
    }
}
