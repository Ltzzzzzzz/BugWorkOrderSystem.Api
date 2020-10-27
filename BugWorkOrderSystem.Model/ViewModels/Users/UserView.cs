using System;

namespace BugWorkOrderSystem.Model.ViewModels.Users
{
    public class UserView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string Role { get; set; }
    }
}
