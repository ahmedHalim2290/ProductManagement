﻿namespace ProductManagement.Core.Entities;
public class Supplier : BaseEntity {
    public string Name { get; set; } 
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

