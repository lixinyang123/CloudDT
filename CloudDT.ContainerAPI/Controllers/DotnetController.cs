using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class DotnetController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("Hi .NET");
        }
    }
}
