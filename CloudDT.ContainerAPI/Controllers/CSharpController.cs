using CloudDT.ContainerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class DotnetController : ControllerBase
    {
        private readonly CSharpService dotnetService;

        public DotnetController(CSharpService dotnetService)
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
