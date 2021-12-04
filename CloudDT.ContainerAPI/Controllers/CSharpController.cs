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
        [HttpOptions]
        public IActionResult Index(string code)
        {
            if(Request.Method == "Options")
                return Ok();

            dotnetService.Save(code).Run();
            return Ok();
        }
    }
}
