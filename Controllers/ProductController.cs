using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Data;
using ShopBridge.Dtos;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _producRepo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepo productRepo, IMapper mapper)
        {
            _producRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _producRepo.GetAllProducts();
                return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _producRepo.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProductReadDto>(product));
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                var productModel = _mapper.Map<Product>(product);
                await _producRepo.AddProduct(productModel);
                var productReadDto = _mapper.Map<ProductReadDto>(productModel);
                return CreatedAtRoute(nameof(GetProductById), new { id = productModel.ProductId }, productReadDto);
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                var productFromDb = await _producRepo.GetProductById(id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                _mapper.Map(product, productFromDb);
                await _producRepo.UpdateProduct(productFromDb);
                return NoContent();
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var productFromDb = await _producRepo.GetProductById(id);
                if (productFromDb == null)
                {
                    return NotFound();
                }
                await _producRepo.RemoveProduct(productFromDb);
                return NoContent();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
