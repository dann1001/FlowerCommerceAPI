// using Microsoft.Extensions.Localization;
// using Microsoft.AspNetCore.Mvc;

// namespace FlowerCommerceAPI.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProductsController : ControllerBase
//     {
//         private readonly IStringLocalizer<ProductsController> _localizer;

//         public ProductsController(IStringLocalizer<ProductsController> localizer)
//         {
//             _localizer = localizer;
//         }

//         [HttpGet("{id}")]
//         public IActionResult GetProduct(int id)
//         {
//             // Example localized strings
//             var localizedName = _localizer["Product_Name"];
//             var localizedDescription = _localizer["Product_Description"];

//             // Example response with localized fields
//             var product = new
//             {
//                 Id = id,
//                 Name = localizedName,
//                 Description = localizedDescription,
//                 Price = 10.99M,
//                 Stock = 100
//             };

//             return Ok(product);
//         }
//     }
// }