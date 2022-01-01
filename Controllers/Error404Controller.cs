using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class Error404Controller: Controller
    {
        private readonly ILogger<Error404Controller> _logger;

        public Error404Controller(ILogger<Error404Controller> logger)
        {
            _logger = logger;
        }

        public IActionResult Page404()
        {
            return View();
        }
    }
}
