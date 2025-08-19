using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.DTOs;
public class SupplierRequestDto {
    public int Id { get; set; }

    [Required(ErrorMessage = "Supplier name is required")]
    [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters")]
    public string Name { get; set; }

}