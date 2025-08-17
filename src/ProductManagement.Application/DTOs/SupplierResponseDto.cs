using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs;
public class SupplierResponseDto {
    public int Id { get; set; }
    public string Name { get; set; }

    public int ProductCount { get; set; }
}