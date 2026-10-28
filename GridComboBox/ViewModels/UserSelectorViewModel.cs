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
        public User SelectedUser { get; set; }        
        public List<Models.User> Users { get; set; }



        public event EventHandler RequestClose;
        public ICommand CloseCommand { get; set; }

        public UserSelectorViewModel()
        {
            Users = Data.UserReader.GetUsers(20);

            CloseCommand = new RelayCommand(() => Execute(Close), CanExecuteWhenNotBusy);
        }

        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
