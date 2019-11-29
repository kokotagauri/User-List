using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using UserList.Data;
using UserList.Inf.Interfaces.Unit;
using UserList.Inf.Interfaces.User;
using UserList.Inf.Repos.User;

namespace UserList.Inf.Repos.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IHostingEnvironment _env;
        private IUser _userRepo;

        public UnitOfWork(DataContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IUser UserRepo
        {
            get { return _userRepo = _userRepo ?? new UserRepo(_env, _context); }
            set { }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
