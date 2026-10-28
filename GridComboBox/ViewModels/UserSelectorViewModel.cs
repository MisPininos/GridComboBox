using GridComboBox.Models;
using GridComboBox.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GridComboBox.ViewModels
{
    public class UserSelectorViewModel : BaseViewModel
    {
        public User? SelectedUser { get; set; }        
        public List<User> Users { get; set; }       

        public UserSelectorViewModel()
        {
            Users = Data.UserReader.GetUsers(20000);
        }        
    }
}
