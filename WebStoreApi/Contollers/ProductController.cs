namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.CustomModels;
    using Services.Implementations;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private ProductManager manager;
        public ProductController(ProductManager productManager)
        {
            this.manager = productManager;
        }

        [HttpGet]
        public IActionResult AllProducts()
        {
            var all = manager.AllProducts;

            return Ok(all);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddProduct(ProductModel model)
        {

            var res = manager.Add(model);
            if (res.Length == 0)
            {
                return Created("api/product", model);
            }
            return BadRequest();
        }
    }
}