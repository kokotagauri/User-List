using System.Collections.Generic;
using System.Threading.Tasks;
using UserList.Dtos.Location;
using UserList.Dtos.Pagination;
using UserList.Dtos.Phone;
using UserList.Dtos.User;
using UserList.Dtos.User.ConnectedUser;
using UserList.Dtos.User.Search;

namespace UserList.Inf.Interfaces.User
{
    public interface IUser
    {
        #region User

        Task<string> AddUser(AddUserDto model);
        Task<GetUserDto> GetUser(int id);
        Task<List<UserForListDto>> GetUsersSimpleList();
        Task<UsersFilteredDto> GetUsersSearch(SearchDto model,int page, int pageSize);
        Task<string> EditUser(EditUserDto model);
        Task<string> DeleteUser(int id);

        #endregion

        #region ConnectedUser

        Task<string> AddConnectedUser(AddConnectedUserDto model);
        Task<string> RemoveConnection(int id);
        Task<List<ConnectedUsersReportDto>> GetConnectedUsersReport(int id);

        #endregion

        #region Phone

        Task<string> AddPhone(PhoneDto model);
        Task<string> EditPhone(PhoneDto model);
        Task<string> RemovePhone(int id);

        #endregion

        #region Location

        Task<List<GetCityDto>> GetCities();

        #endregion
    }
}
