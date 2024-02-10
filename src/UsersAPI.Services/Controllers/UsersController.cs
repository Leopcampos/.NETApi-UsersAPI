using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Add()
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult Update()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}