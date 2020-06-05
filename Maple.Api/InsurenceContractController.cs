using Maple.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maple.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurenceContractController : ControllerBase
    {

        private readonly ILogger<InsurenceContractController> _logger;
        private readonly IInsurenceContractService _service;
        public InsurenceContractController(IInsurenceContractService service, ILogger<InsurenceContractController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ContractsModel> GetAll()
        {
            return _service.GetAll();
        }
    }
}
