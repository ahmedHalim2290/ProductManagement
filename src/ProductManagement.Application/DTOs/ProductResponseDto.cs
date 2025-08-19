using ProductManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs;
public class ProductResponseDto {
    public int Id { get; set; }
    public string Name { get; set; }
    public QuantityPerUnit QuantityPerUnit { get; set; }
    public string QuantityPerUnitName { get; set; }
    public int ReorderLevel { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public double UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
}