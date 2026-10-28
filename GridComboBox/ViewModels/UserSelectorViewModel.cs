using GridComboBox.Models;
using GridComboBox.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace GridComboBox.ViewModels
{
    public class UserSelectorViewModel : BaseViewModel
    {
        private bool _isSelecting;
        private string _searchText = string.Empty;
        private User? _selectedUser;

        public ObservableCollection<User> Users { get; set; }
        public ICollectionView FilteredUsers { get; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;

                // If text is changing because we are programmatically selecting an item,
                // do NOT refresh the filter (which would collapse the dropdown).
                if (_isSelecting) return;

                FilteredUsers.Refresh();
            }
        }

        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;

                if (value != null)
                {
                    // Lock filter updates while we safely update the display text
                    _isSelecting = true;
                    SearchText = value.Name;
                    _isSelecting = false;
                }
            }
        }

        public UserSelectorViewModel()
        {
            Users = new ObservableCollection<User>(Data.UserReader.GetUsers(20000));

            FilteredUsers = CollectionViewSource.GetDefaultView(Users);
            FilteredUsers.Filter = FilterUser;
        }

        private bool FilterUser(object item)
        {
            if (item is not User user) return false;

            // If search box is empty, or if text matches the active selection, show all records
            if (string.IsNullOrWhiteSpace(SearchText) || (_selectedUser != null && SearchText == _selectedUser.Name))
            {
                return true;
            }

            // Case-insensitive substring search across Name and Email
            return (!string.IsNullOrEmpty(user.Name) && user.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                   (!string.IsNullOrEmpty(user.Email) && user.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }
    }
}
