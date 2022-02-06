using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        [HttpGet("v1/categories")]
        public IActionResult Get([FromServices] BlogDataContext context)
        {
            var categories = context.Categories.ToList();

            return Ok(categories);
        }



    }
}