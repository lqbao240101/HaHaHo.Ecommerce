using Ecommerce.Models.Optional;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Ecommerce.Controllers.Optional
{
    [Route("api/[controller]")]
    [ApiController]
    public class Unauthentication : ControllerBase
    {
        private readonly IUserService _userService;

        public Unauthentication(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> General(FormatInput jsonDocument)
        {
            //string h = "{\"Name\":\"hello\",\"Description\":\"abc\"}";
            try
            {
                Filter? Filter = jsonDocument.Filter != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<Filter>(jsonDocument.Filter) : null;
                Page? Page = jsonDocument.Page != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<Page>(jsonDocument.Page) : null;
                Sort? Sort = jsonDocument.Sort != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<Sort>(jsonDocument.Sort) : null;

            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
            switch (jsonDocument.Method)
            {
                case "Register":
                    try
                    {
                        var Data = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterViewModel>(jsonDocument.Data!);
                        var result = await _userService.ResgisterUserAsync(Data!);

                        if (result.IsSuccess)
                            return Ok(result);

                        return BadRequest(result);
                    }
                    catch
                    {
                        return BadRequest("Something went wrong!");
                    }
                case "Login":
                    try
                    {
                        var Data = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginViewModel>(jsonDocument.Data!);
                        var result = await _userService.LoginUserAsync(Data!);

                        if (result.IsSuccess)
                            return Ok(result);

                        return BadRequest(result);
                    }
                    catch
                    {
                        return BadRequest("Something went wrong!");
                    }


                default:
                    return BadRequest("Something went wrong!");

            }
        }
    }
}
