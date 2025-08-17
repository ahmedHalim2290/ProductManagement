using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase {
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(id);
        return Ok(supplier);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdSupplier = await _supplierService.CreateSupplierAsync(supplierDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _supplierService.UpdateSupplierAsync(id, supplierDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _supplierService.DeleteSupplierAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/product-count")]
    public async Task<IActionResult> GetProductCount(int id)
    {
        var count = await _supplierService.GetProductCountBySupplierAsync(id);
        return Ok(count);
    }
}