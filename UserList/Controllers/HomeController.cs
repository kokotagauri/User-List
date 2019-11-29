using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserList.Dtos.Phone;
using UserList.Dtos.User;
using UserList.Dtos.User.ConnectedUser;
using UserList.Dtos.User.Search;
using UserList.Helpers.Statuses;
using UserList.Inf.Interfaces.Unit;
using UserList.Models.Paging;
using UserList.ViewModels.ConnectedUser;
using UserList.ViewModels.User;

namespace UserList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private const int pageSize = 4;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region User

        [Route("")]
        public async Task<IActionResult> Index(SearchDto model, int page = 1)
        {
            model = model ?? new SearchDto {FirstName = "", LastName = "", Identity = ""};
            var users =  await _unitOfWork.UserRepo.GetUsersSearch(model, page, pageSize);
            var pm = new PagingModel { Data = users.Users, Count = users.Count, FirstName = model.FirstName, LastName = model.LastName, Identity = model.Identity,
                Gender = model.Gender, BirthDate = model.BirthDate, City = model.City, CurrentPage = page, PageSize = pageSize};
            return View(pm);
        }

        [Route("detailed-search")]
        public async Task<IActionResult> DetailedSearch()
        {
            var cities = await _unitOfWork.UserRepo.GetCities();
            return View(cities);
        }

        public IActionResult DetailedRedirect([FromForm]SearchDto model)
        {
            return RedirectToAction("Index", model);
        }

        [Route("add")]
        public async Task<IActionResult> Add(AddUserDto model)
        {
            var vm = new AddUserVM {Cities = await _unitOfWork.UserRepo.GetCities(), User = model};
            return View(vm);
        }

        public async Task<IActionResult> AddRedirect([FromForm]AddUserValidatedDto model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Add", model);

            var addResult = await _unitOfWork.UserRepo.AddUser(model);
            await _unitOfWork.Commit();

            return RedirectToAction(addResult.Equals(Statuses.Success) ? "Index" : "Error");
        }

        [Route("{id}")]
        public async Task<IActionResult> Single(int id)
        {
            var user = await _unitOfWork.UserRepo.GetUser(id);
            var connectionReport = await _unitOfWork.UserRepo.GetConnectedUsersReport(id);
            var vm = new GetUserVM { User = user, ConnectionReport = connectionReport };
            return View(vm);
        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(EditUserDto model)
        {
            var user = await _unitOfWork.UserRepo.GetUser(model.Id);
            var vm = new EditUserVM { Cities = await _unitOfWork.UserRepo.GetCities(), User = user };
            return View(vm);
        }

        public async Task<IActionResult> EditRedirect([FromForm]EditUserValidatedDto model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", model);
            }

            var editResult = await _unitOfWork.UserRepo.EditUser(model);
            await _unitOfWork.Commit();

            return editResult.Equals(Statuses.Success) ? RedirectToAction("Single", new { id = model.Id }) : RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _unitOfWork.UserRepo.DeleteUser(id);
            await _unitOfWork.Commit();

            return deleteResult.Equals(Statuses.Success) ? RedirectToAction("Index") : RedirectToAction("Error");
        }

        #endregion

        #region Connected Users

        [Route("connect")]
        public async Task<IActionResult> Connect(AddConnectedUserDto model)
        {
            var users = await _unitOfWork.UserRepo.GetUsersSimpleList();
            return View(new ConnectVM { ConnectedUser = model ?? new AddConnectedUserDto(), Users = users});
        }

        public async Task<IActionResult> ConnectRedirect([FromForm]AddConnectedUserDto model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Connect", model);

            var addResult = await _unitOfWork.UserRepo.AddConnectedUser(model);
            await _unitOfWork.Commit();

            return addResult.Equals(Statuses.Success) ? RedirectToAction("Single", new {id = model.UserId}) : RedirectToAction("Error");
        }

        public async Task<IActionResult> RemoveConnection(int id, int userId)
        {
            var removeResult = await _unitOfWork.UserRepo.RemoveConnection(id);
            await _unitOfWork.Commit();

            return removeResult.Equals(Statuses.Success) ? RedirectToAction("Single", new {id = userId}) : RedirectToAction("Error");
        }

        #endregion

        #region Phones

        [Route("add-phone")]
        public IActionResult AddPhone(PhoneDto model)
        {
            return View(model);
        }

        public async Task<IActionResult> AddPhoneRedirect([FromForm]PhoneDto model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("AddPhone", model);

            var addResult = await _unitOfWork.UserRepo.AddPhone(model);
            await _unitOfWork.Commit();

            return addResult.Equals(Statuses.Success) ? RedirectToAction("Single", new { id = model.UserId }) : RedirectToAction("Error");
        }

        [Route("edit-phone")]
        public IActionResult EditPhone(PhoneDto model)
        {
            return View(model);
        }

        public async Task<IActionResult> EditPhoneRedirect([FromForm]PhoneDto model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditPhone", model);
            }

            var editResult = await _unitOfWork.UserRepo.EditPhone(model);
            await _unitOfWork.Commit();

            return editResult.Equals(Statuses.Success) ? RedirectToAction("Single", new { id = model.UserId }) : RedirectToAction("Error");
        }

        public async Task<IActionResult> RemovePhone(int id, int userId)
        {
            var removeResult = await _unitOfWork.UserRepo.RemovePhone(id);
            await _unitOfWork.Commit();

            return removeResult.Equals(Statuses.Success) ? RedirectToAction("Single", new { id = userId }) : RedirectToAction("Error");
        }

        #endregion

        #region Error

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }

        #endregion
    }
}
