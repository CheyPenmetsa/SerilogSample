using Microsoft.Extensions.Logging;
using ResidentApi.BusinessLogic.Models;
using ResidentApi.BusinessLogic.Repositories;

namespace ResidentApi.BusinessLogic.Manager
{
    public class ResidentManager : IResidentManager
    {
        private readonly ILogger<ResidentManager> _logger;

        private readonly IResidentRepository _residentRepository;

        public ResidentManager(ILogger<ResidentManager> logger, 
            IResidentRepository residentRepository)
        {
            _logger = logger;
            _residentRepository = residentRepository;
        }

        public int CreateResident(CreateResidentRequest createResidentRequest)
        {
            _logger.LogDebug($"Manager call for creating resident: {createResidentRequest}");
            var residentId = _residentRepository.CreateResident(createResidentRequest);
            _logger.LogInformation($"Manager created resident with Id: {residentId}");
            return residentId;
        }

        public List<ResidentModel> GetAllResidents()
        {
            return _residentRepository.GetAllResidents();
        }

        public ResidentModel GetResidentById(int id)
        {
            _logger.LogDebug($"Querying resident with id:{id}");
            return _residentRepository.GetResidentById(id);
        }
    }
}
