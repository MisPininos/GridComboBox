using System;
using System.Collections.Generic;
using System.Text;

namespace GridComboBox.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; } = Role.User;
    }

    public enum Role
    {
        Admin,
        User,
        Guest
    }
}
