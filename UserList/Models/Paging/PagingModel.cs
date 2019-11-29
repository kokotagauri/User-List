using System;
using System.Collections.Generic;
using UserList.Dtos.User;

namespace UserList.Models.Paging
{
    public class PagingModel
    {
        public int Count { get; set; }
        public int PageSize { get; set; } = 4;
        public int CurrentPage { get; set; } = 1;

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Identity { get; set; } = "";
        public string Gender { get; set; } = "";
        public int City { get; set; } = 0;
        public string BirthDate { get; set; }

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<SimpleUserDto> Data { get; set; }
    }
}
