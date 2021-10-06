using System.Collections.Generic;

namespace WebApplication.Models.Users
{
    public class ListUserViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
