using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class NodeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("Hi Node");
        }
    }
}
