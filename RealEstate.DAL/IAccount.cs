using System.Threading.Tasks;

namespace RealEstate.DAL
{    
    public interface IAccount 
    {
        Task CreateRealEstateEnquiry(string FirstName, string LastName, string Email, string Phone, string Comment);
    }
}
