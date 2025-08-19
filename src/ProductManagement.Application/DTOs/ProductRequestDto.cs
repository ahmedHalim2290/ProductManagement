using ProductManagement.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs;
public class ProductRequestDto {
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, EnumDataType(typeof(QuantityPerUnit))]
    
    public QuantityPerUnit QuantityPerUnit { get; set; }

    [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [Range(0, double.MaxValue)]
    public double UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    public int UnitsInStock { get; set; }

    [Range(0, int.MaxValue)]
    public int UnitsOnOrder { get; set; }
}