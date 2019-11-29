using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserList.Data;
using UserList.Dtos.Location;
using UserList.Dtos.Pagination;
using UserList.Dtos.Phone;
using UserList.Dtos.User;
using UserList.Dtos.User.ConnectedUser;
using UserList.Dtos.User.Search;
using UserList.Helpers.Statuses;
using UserList.Inf.Interfaces.User;
using UserList.Models.Phone;
using UserList.Models.User;

namespace UserList.Inf.Repos.User
{
    public class UserRepo : IUser
    {
        private readonly IHostingEnvironment _env;
        private readonly DataContext _context;
        public UserRepo(IHostingEnvironment env, DataContext context)
        {
            _env = env;
            _context = context;
        }

        #region User

        public async Task<string> AddUser(AddUserDto model)
        {
            try
            {
                var user = new Models.User.User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Identity = model.Identity,
                    BirthDate = model.BirthDate,
                    CityId = model.CityId,
                    Phones = new List<Phone>()
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                if (model.Image != null)
                {
                    var path = Path.Combine(_env.WebRootPath, "files", "images", $"{user.Id}");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    using (var stream = new FileStream(Path.Combine(path, model.Image.FileName.Split('\\').Last()),
                        FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                        user.Image = $"/files/images/{user.Id}/{model.Image.FileName.Split('\\').Last()}";
                    }
                }
                else
                    user.Image = "http://inyogo.com/img/image_not_available.png";

                var phone = new Phone
                {
                    Number = model.PhoneNumber,
                    Type = model.PhoneType,
                    UserId = user.Id
                };

                await _context.Phones.AddAsync(phone);
                
                
                _context.Users.Update(user);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<GetUserDto> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.Include(u=>u.Phones).Include(u=>u.ConnectedUsers).ThenInclude(u=>u.UserConnected).FirstOrDefaultAsync(u => u.Id == id);

                return new GetUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Identity = user.Identity,
                    Image = user.Image,
                    City = (await _context.Cities.FirstOrDefaultAsync(c=>c.Id==user.CityId)).Name,
                    Phones = user.Phones.Select(p=> new PhoneDto
                    {
                        Id = p.Id,
                        Number = p.Number,
                        Type = p.Type
                    }).ToList(),
                    ConnectedUsers = user.ConnectedUsers.Select(u=> new GetConnectedUserDto
                    {
                        Id = u.Id,
                        Name = u.UserConnected.FirstName + " " + u.UserConnected.LastName,
                        Type = u.Type
                    }).ToList()
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<UserForListDto>> GetUsersSimpleList()
        {
            return await _context.Users.Select(u => new UserForListDto
            {
                Id = u.Id,
                Name = u.FirstName + " " + u.LastName
            }).ToListAsync();
        }

        public async Task<UsersFilteredDto> GetUsersSearch(SearchDto model, int page, int pageSize)
        {
            try
            {
                model.FirstName = model.FirstName ?? "";
                model.LastName = model.LastName ?? "";
                model.Identity = model.Identity ?? "";

                var users = await _context.Users.Where(u => 
                    u.FirstName.Contains(model.FirstName) && u.LastName.Contains(model.LastName) && u.Identity.Contains(model.Identity)).ToListAsync();

                if (model.Gender != null)
                    users = users.Where(u => u.Gender.Contains(model.Gender)).ToList();
                if (model.City != 0)
                    users = users.Where(u => u.CityId == model.City).ToList();
                if (model.BirthDate != null)
                    users = users.Where(u => u.BirthDate.Date == Convert.ToDateTime(model.BirthDate).Date).ToList();
                
                var count = users.Count;

                var userRes = users.Skip((page - 1) * pageSize).Take(pageSize).Select(user => new SimpleUserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Identity = user.Identity,
                    Image = user.Image
                }).ToList();

                return new UsersFilteredDto
                {
                    Users = userRes,
                    Count = count
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> EditUser(EditUserDto model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.Identity = model.Identity;
                user.BirthDate = model.BirthDate;
                user.CityId = model.CityId;

                if (model.Image != null)
                {
                    var path = Path.Combine(_env.WebRootPath, "files", "images", $"{user.Id}");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    else
                    {
                        var di = new DirectoryInfo(path);
                        foreach (var file in di.GetFiles())
                            file.Delete();
                    }

                    using (var stream = new FileStream(Path.Combine(path, model.Image.FileName.Split('\\').Last()), FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                        user.Image = $"/files/images/{user.Id}/{model.Image.FileName.Split('\\').Last()}";
                    }
                }

                _context.Users.Update(user);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<string> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                _context.Users.Remove(user);

                var path = Path.Combine(_env.WebRootPath, "files", "images", $"{user.Id}");
                if (Directory.Exists(path))
                    Directory.Delete(path, true);

                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        #endregion

        #region ConnectedUser

        public async Task<string> AddConnectedUser(AddConnectedUserDto model)
        {
            try
            {
                var cu = new ConnectedUser
                {
                    Type = model.Type,
                    UserId = model.UserId,
                    UserConnectedId = model.ConnectedUserId
                };

                await _context.ConnectedUsers.AddAsync(cu);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<string> RemoveConnection(int id)
        {
            try
            {
                var cu = await _context.ConnectedUsers.FirstOrDefaultAsync(u => u.Id == id);

                _context.ConnectedUsers.Remove(cu);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<List<ConnectedUsersReportDto>> GetConnectedUsersReport(int id)
        {
            try
            {
                var cUsers = await _context.ConnectedUsers.Where(u => u.UserId == id).ToListAsync();

                return cUsers.GroupBy(u => u.Type).Select(cu => new ConnectedUsersReportDto {Type = cu.Key, Amount = cu.Count()}).ToList();
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Phone

        public async Task<string> AddPhone(PhoneDto model)
        {
            try
            {
                var phone = new Phone
                {
                    Type = model.Type,
                    Number = model.Number,
                    UserId = model.UserId
                };

                await _context.Phones.AddAsync(phone);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<string> EditPhone(PhoneDto model)
        {
            try
            {
                var phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == model.Id);
                phone.Type = model.Type;
                phone.Number = model.Number;

                _context.Phones.Update(phone);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        public async Task<string> RemovePhone(int id)
        {
            try
            {
                var phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);

                _context.Phones.Remove(phone);
                return Statuses.Success;
            }
            catch
            {
                return Statuses.Error;
            }
        }

        #endregion

        #region Location
        public async Task<List<GetCityDto>> GetCities()
        {
            try
            {
                return await _context.Cities.Select(c => new GetCityDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
