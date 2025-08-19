using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Core.Enums;
using ProductManagement.Core.Exceptions;

namespace ProductManagement.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequestDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdProduct = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductRequestDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productDto.Id != productDto.Id)
                return BadRequest("ID mismatch between URL and request body");

            await _productService.UpdateProductAsync(productDto);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product");
            return StatusCode(500, "An error occurred while updating the product");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product");
            return StatusCode(500, "An error occurred while deleting the product");
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string? name,
    QuantityPerUnit? quantityPerUnit,
    int? reorderLevel,
    string? supplierName,
    double? unitPrice,
    int? unitsInStock,
    int? unitsOnOrder)
    {
        try
        {
            // Check if ALL parameters are null/empty
            if (string.IsNullOrWhiteSpace(name) &&
                !quantityPerUnit.HasValue &&
                !reorderLevel.HasValue &&
                string.IsNullOrWhiteSpace(supplierName) &&
                !unitPrice.HasValue &&
                !unitsInStock.HasValue &&
                !unitsOnOrder.HasValue)
            {
                return BadRequest("At least one search parameter must be provided");
            }

            var results = await _productService.SearchProductsAsync(name, quantityPerUnit, reorderLevel, supplierName, unitPrice, unitsInStock, unitsOnOrder);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products");
            return StatusCode(500, "An error occurred while searching products");
        }
    }

}