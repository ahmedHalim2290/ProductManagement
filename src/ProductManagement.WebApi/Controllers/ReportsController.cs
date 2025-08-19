using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using ProductManagement.Core.Enums;
using ProductManagement.Core.Exceptions;
namespace ProductManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase {
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;

    private readonly ILogger<ProductsController> _logger;
    public ReportsController(IProductService productService, ILogger<ProductsController> logger, ISupplierService supplierService)
    {
        _productService = productService;
        _logger = logger;
        _supplierService = supplierService;
    }

    [HttpGet("reorder")]
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
  
    [HttpGet("min-orders-product")]
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

    [HttpGet("largest-supplier")]
    public async Task<IActionResult> GetLargestSupplier()
    {
        try
        {
            var supplier = await _supplierService.GetLargestSupplierAsync();
            return Ok(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting largest supplier");
            return StatusCode(500, "An error occurred while getting largest supplier");
        }
    }

}