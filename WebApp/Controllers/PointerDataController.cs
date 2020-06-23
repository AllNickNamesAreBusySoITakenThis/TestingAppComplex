using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointerDataController : Controller
    {
        private readonly ILogger<PointerDataController> _logger;

        public PointerDataController(ILogger<PointerDataController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<PointerData> Get()
        {
            return new PointerData[]
            {
                new PointerData()
                {
                    Name="p1",
                    Description="Point 1",
                    North=1,
                    East=1,
                    Hight=1,
                    Value=1
                },
                new PointerData()
                {
                    Name="p2",
                    Description="Point 2",
                    North=2,
                    East=2,
                    Hight=2,
                    Value=2
                },
            };
        }
    }
}
