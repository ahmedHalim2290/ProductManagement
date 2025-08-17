using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
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
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdProduct = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != productDto.Id)
                return BadRequest("ID mismatch between URL and request body");

            await _productService.UpdateProductAsync(id, productDto);
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
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term cannot be empty");

            var results = await _productService.SearchProductsAsync(term);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products");
            return StatusCode(500, "An error occurred while searching products");
        }
    }

    [HttpGet("statistics/reorder")]
    public async Task<IActionResult> GetProductsNeedReorder()
    {
        try
        {
            var products = await _productService.GetProductsNeedReorderAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting products that need reorder");
            return StatusCode(500, "An error occurred while getting products that need reorder");
        }
    }

    [HttpGet("statistics/largest-supplier")]
    public async Task<IActionResult> GetLargestSupplier()
    {
        try
        {
            var supplier = await _productService.GetLargestSupplierAsync();
            return Ok(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting largest supplier");
            return StatusCode(500, "An error occurred while getting largest supplier");
        }
    }

    [HttpGet("statistics/min-orders-product")]
    public async Task<IActionResult> GetProductWithMinOrders()
    {
        try
        {
            var product = await _productService.GetProductWithMinOrdersAsync();
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product with minimum orders");
            return StatusCode(500, "An error occurred while getting product with minimum orders");
        }
    }
}