using ProductManagement.Core.Enums;

namespace ProductManagement.Core.Entities;
public class Product : BaseEntity {
    public string Name { get; set; }
    public QuantityPerUnit QuantityPerUnit { get; set; }
    public int ReorderLevel { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public double UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
}

