using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
}
