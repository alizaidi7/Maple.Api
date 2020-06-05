using Maple.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        public async IAsyncEnumerable<ContractsModel> GetAll()
        {
            await foreach (var item in _service.GetAll())
                yield return item;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ContractDTO dto)
        {
            try
            {
                await _service.Save(dto);
                return new CustomJsonResult("Contract created successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CustomJsonResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContractDTO dto)
        {
            try
            {
                await _service.Update(dto);
                return new CustomJsonResult("Contract updated successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CustomJsonResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ContractDTO dto)
        {
            try
            {
                await _service.Delete(dto);
                return new CustomJsonResult("Contract deleted successfully.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CustomJsonResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
