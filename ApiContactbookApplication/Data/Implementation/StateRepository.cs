using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Data.Implementation
{
    public class StateRepository : IStateRepository
    {
        private readonly IAppDbContext _appDbContext;

        public StateRepository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<State> GetAllStates()
        {
            List<State> states = _appDbContext.States.ToList();
            return states;
        }

        //public State GetStateById(int id)
        //{
        //    var state = _appDbContext.States.FirstOrDefault(c => c.StateId == id);

        //    return state;
        //}

        public IEnumerable<State> GetStateByCountryId(int countryId)
        {
            var state = _appDbContext.States.Where(c => c.CountryId == countryId).ToList();

            return state;
        }
    }
}
