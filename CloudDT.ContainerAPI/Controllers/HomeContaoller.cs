using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class HomeContaoller : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("CloudDevTools API");
        }
    }
}
