using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Data.Contract
{
    public interface IStateRepository
    {
         IEnumerable<State> GetAllStates();

         //State GetStateById(int id);

        IEnumerable<State> GetStateByCountryId(int countryId);
    }
}
