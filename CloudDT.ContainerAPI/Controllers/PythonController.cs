using CloudDT.ContainerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudDT.ContainerAPI.Controllers
{
    public class PythonController : ControllerBase
    {
        private readonly PythonService pythonService;

        public PythonController(PythonService pythonService)
        {
            this.pythonService = pythonService;
        }

        public IActionResult Index(string code)
        {
            if(Request.Method == "Options")
                return Ok();
            
            pythonService.Save(code).Run();
            return Ok();
        }
    }
}
