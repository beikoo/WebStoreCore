namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Newtonsoft.Json;
    using Services.CustomModels;
    using Services.Implementations;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private ProductManager manager;
        public ProductsController(ProductManager productManager)
        {
            this.manager = productManager;
        }

        [HttpGet]
        public IActionResult AllProducts()
        {
            var all = manager.AllProducts;
            var res = JsonConvert.SerializeObject(all);
            return Ok(res);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            if (id != 0)
            {
                var product = manager.Get(id);
                if (product != null)
                {
                    var res = JsonConvert.SerializeObject(product);
                    return Ok(res);
                }

            }
            return NotFound();
        }


        [HttpPost]
        [Route("add")]
        public IActionResult AddProduct(ProductModel model)
        {

            var res = manager.Add(model);
            if (res.Length == 0)
            {
                return Ok(JsonConvert.SerializeObject(model));
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                var res = manager.Delete(id);
                if (res.Length != 0)
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }
    }
}