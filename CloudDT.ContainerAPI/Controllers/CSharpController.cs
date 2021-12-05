using CloudDT.ContainerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class CSharpController : ControllerBase
    {
        private readonly CSharpService csharpService;

        public CSharpController(CSharpService csharpService)
        {
            this.csharpService = csharpService;
        }

        public IActionResult Index(string code)
        {
            if(Request.Method == "Options")
                return Ok();

            csharpService.Save(code).Run();
            return Ok();
        }
    }
}
