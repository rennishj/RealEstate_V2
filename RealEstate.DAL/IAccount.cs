using System.Threading.Tasks;

namespace RealEstate.DAL
{    
    public interface IAccount 
    {
        Task AddComment(string FirstName, string LastName, string Email, string Phone, string Comment);
    }
}
