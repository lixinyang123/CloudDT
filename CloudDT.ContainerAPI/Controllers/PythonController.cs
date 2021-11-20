using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class PythonController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("Hi Python");
        }
    }
}
