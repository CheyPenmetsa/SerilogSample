using Microsoft.Extensions.Logging;
using ResidentApi.BusinessLogic.Models;

namespace ResidentApi.BusinessLogic.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ILogger<ResidentRepository> _logger;

        private static List<ResidentModel> residents = new List<ResidentModel>();

        public ResidentRepository(ILogger<ResidentRepository> logger)
        {
            _logger = logger;
        }

        public int CreateResident(CreateResidentRequest createResidentRequest)
        {
            _logger.LogDebug($"Repository call for creating resident: {createResidentRequest}");
            if (!residents.Any(x => x.Email.Equals(createResidentRequest.Email)))
            {
                var residentId = residents.Count() + 1;
                var resident = new ResidentModel()
                {
                    ResidentId = residentId,
                    Email = createResidentRequest.Email,
                    Age = createResidentRequest.Age,
                    FirstName = createResidentRequest.FirstName,
                    LastName = createResidentRequest.LastName
                };
                residents.Add(resident);
                _logger.LogDebug($"Repository created resident with Id: {residentId}");
                return residentId;
            }
            _logger.LogInformation($"Resident found for {createResidentRequest.Email}");
            return residents.First(x=>x.Email.Equals(createResidentRequest.Email)).ResidentId;
            throw new NotImplementedException();
        }

        public List<ResidentModel> GetAllResidents()
        {
            _logger.LogDebug($"Total residents count:{residents.Count}");
            return residents;
        }

        public ResidentModel GetResidentById(int id)
        {
            _logger.LogDebug($"Querying resident with id:{id}");
            if (!residents.Any(x => x.ResidentId.Equals(id)))
            {
                throw new ArgumentOutOfRangeException($"Resident not found for Id : {id}");
            }
            return residents.First(x => x.ResidentId.Equals(id));
        }
    }
}
