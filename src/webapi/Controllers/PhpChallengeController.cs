using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhpChallengeController : ControllerBase
    {
        private readonly ILogger<PhpChallengeController> _logger;

        private PhpChallengeService _phpChallengeService;

        private PhpChallengeService PhpChallengeService
        {
            get
            {
                if (_phpChallengeService == null)
                {
                    _phpChallengeService = new PhpChallengeService();
                }

                return _phpChallengeService;
            }
        }

        public PhpChallengeController(ILogger<PhpChallengeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "test";
        }

        [HttpPost("73.php")]
        public string IsInfinite([FromQuery]int a, [FromQuery]int b)
        {
            int result = PhpChallengeService.IsRepeatingFraction(a, b);

            return result.ToString();
        }

        public class IntInput
        {
            public int a { get; set; }

            public int b { get; set; }
        }
    }
}