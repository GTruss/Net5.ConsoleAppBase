﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Net5.Cli;

namespace Net5.Web.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v3.0")]
    [ApiVersion("3.0")]
    public partial class MainServiceController : ControllerBase {
        private readonly ILogger<MainServiceController> _logger;
        private readonly MainService _mainService;

        public MainServiceController(ILogger<MainServiceController> logger, MainService mainService) {
            _logger = logger;
            _mainService = mainService;
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        public async Task<ActionResult> Get() {
            try {
                _logger.LogInformation("Running MainService...");
                _mainService.Run();
                _logger.LogInformation("MainService finished.");

                return Ok("Success");
            }
            catch (Exception ex) {
                _logger.LogCritical(ex, "There was an unexpected error while running the main service.");
                return Problem(ex.ToString(), "MainService", (int)HttpStatusCode.InternalServerError,
                    "There was an unexpected error while running the main service.", ex.GetType().ToString());
            }

        }

    }
}
