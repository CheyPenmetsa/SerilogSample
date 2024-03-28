using Microsoft.AspNetCore.Mvc;
using ResidentApi.BusinessLogic.Manager;
using ResidentApi.BusinessLogic.Models;

namespace ResidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly ILogger<ResidentController> _logger;

        private readonly IResidentManager _residentManager;

        public ResidentController(ILogger<ResidentController> logger,
            IResidentManager residentManager)
        {
            _logger = logger;
            _residentManager = residentManager;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetResidentById(int id)
        {
            _logger.LogDebug($"Call came for resident with Id : {id}");
            var resident = _residentManager.GetResidentById(id);
            _logger.LogDebug($"Resident : {resident}");
            return Ok(resident);
        }

        [HttpGet()]
        public IActionResult GetAllResidents()
        {
            var residents = _residentManager.GetAllResidents();
            _logger.LogDebug($"Total residents: {residents.Count}");
            return Ok(residents);
        }

        [HttpPost]
        public IActionResult CreateResident([FromBody] CreateResidentRequest createResidentRequest)
        {
            _logger.LogDebug($"Call came for create resident with request: {createResidentRequest}");
            var residentId = _residentManager.CreateResident(createResidentRequest);
            _logger.LogDebug($"Created resident with Id:{residentId}");
            return Ok(residentId);
        }


    }
}
