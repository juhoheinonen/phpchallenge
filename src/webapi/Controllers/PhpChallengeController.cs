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

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public PhpChallengeController(ILogger<PhpChallengeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "test";
        }

        [HttpPost("sanaruudukko")]
        public string Sanaruudukko(string rivit, string sanat)
        {
            int result = PhpChallengeService.Sanaruudukko(rivit, sanat);

            return result.ToString();
        }
    }
}
