using Model.ViewModels;
using Persistence;
using System.Linq;

namespace Service
{
    public interface IHomeService
    {
        bool CheckCredentials(LoginVM vm, ref int travelerId);
    }

    public class HomeService:IHomeService
    {
        private LastaContext _HomeService;
        
        public HomeService(LastaContext context)
        {
            _HomeService = context;
        }

        public bool CheckCredentials(LoginVM vm, ref int travelerId)
        {
           var traveler= _HomeService.Traveler.Where(x =>x.Username == vm.Username && x.Password == vm.Password).FirstOrDefault();

            if (traveler == null)
                return false;
            travelerId = traveler.TravelerId;
            return true;
        }


    }
}
