using ResidentApi.BusinessLogic.Models;

namespace ResidentApi.BusinessLogic.Manager
{
    public interface IResidentManager
    {
        int CreateResident(CreateResidentRequest createResidentRequest);

        List<ResidentModel> GetAllResidents();

        ResidentModel GetResidentById(int id);
    }
}
