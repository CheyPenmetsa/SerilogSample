using ResidentApi.BusinessLogic.Models;

namespace ResidentApi.BusinessLogic.Repositories
{
    public interface IResidentRepository
    {
        int CreateResident(CreateResidentRequest createResidentRequest);

        List<ResidentModel> GetAllResidents();

        ResidentModel GetResidentById(int id);
    }
}
