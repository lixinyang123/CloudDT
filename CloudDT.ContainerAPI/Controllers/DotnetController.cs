using CloudDT.ContainerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class DotnetController : ControllerBase
    {
        private readonly DotnetService dotnetService;

        public DotnetController(DotnetService dotnetService)
        {
            this.dotnetService = dotnetService;
        }

        [HttpPost]
        public IActionResult Index(string code)
        {
            dotnetService.Save(code).Run();
            return Ok();
        }
    }
}
