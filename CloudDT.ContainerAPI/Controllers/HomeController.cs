using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("Hello Cloud Dev Tools");
        }
    }
}
