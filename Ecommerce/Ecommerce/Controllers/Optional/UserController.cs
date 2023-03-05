using Ecommerce.Models.Optional;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Ecommerce.Controllers.Optional
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult General(FormatInput jsonDocument)
        {
            //string h = "{\"Name\":\"hello\",\"Description\":\"abc\"}";
            Filter? Filter = jsonDocument.Filter != null? Newtonsoft.Json.JsonConvert.DeserializeObject<Filter>(jsonDocument.Filter) : null;
            Page? Page = jsonDocument.Page != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<Page>(jsonDocument.Page) : null;
            Sort? Sort = jsonDocument.Sort != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<Sort>(jsonDocument.Sort) : null;
            switch (jsonDocument.Method)
            {
                case "MakeOrder":
                    var Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Demo>(jsonDocument.Data!);
                    break;
                default:
                    break;
            }
            return Ok();
        }
    }
}
