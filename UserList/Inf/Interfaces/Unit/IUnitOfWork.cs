using System.Threading.Tasks;
using UserList.Inf.Interfaces.User;

namespace UserList.Inf.Interfaces.Unit
{
    public interface IUnitOfWork
    {
        IUser UserRepo { get; set; }
        Task Commit();
    }
}
