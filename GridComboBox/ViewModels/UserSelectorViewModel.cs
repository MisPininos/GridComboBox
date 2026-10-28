using GridComboBox.Models;
using GridComboBox.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridComboBox.ViewModels
{
    public class UserSelectorViewModel : BaseViewModel
    {
        public User SelectedUser { get; set; }
        public List<Models.User> Users { get; set; }
        public UserSelectorViewModel()
        {
            Users = Data.UserReader.GetUsers(20);
        }
    }
}
