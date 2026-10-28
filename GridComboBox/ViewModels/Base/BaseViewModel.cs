using PropertyChanged;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GridComboBox.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event EventHandler? RequestClose;
        public ICommand CloseRequestCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, args) => { };
        private readonly SemaphoreSlim _uiLock = new SemaphoreSlim(1, 1);

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));            
        }

        public BaseViewModel() {
            PropertyChanged += (sender, e) =>
            {
                if (string.IsNullOrEmpty(e.PropertyName)) return;
                if (e.PropertyName == nameof(IsBusy)) return;
                if (e.PropertyName == nameof(IsDirty)) return;
                if (sender != null) {
                    var type = sender.GetType().GetProperty(e.PropertyName);
                    if(type != null && Attribute.IsDefined(type, typeof(RastrearCambioSinGuardarAttribute))) {
                        IsDirty = true;
                    }
                }
            };

            CloseRequestCommand = new RelayCommand(() => Execute(Close), CanExecuteWhenNotBusy);
        }

        public bool IsBusy { get; private set; }

        public bool IsDirty { get; protected set; } = false;

        protected bool CanExecuteWhenNotBusy() => !IsBusy;

        protected async Task ExecuteAsync(Func<Task> action)
        {
            if (!await _uiLock.WaitAsync(0))
                return;

            IsBusy = true;
            try
            {
                await action();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
                _uiLock.Release();
                Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        protected async Task ExecuteAsync(Func<object?, Task> action, object? parameter)
        {
            if (!await _uiLock.WaitAsync(0))
                return;
            IsBusy = true;
            try
            {
                await action(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
                _uiLock.Release();
                Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        protected void Execute(Action action)
        {
            if (!_uiLock.Wait(0))
                return;

            IsBusy = true;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
                _uiLock.Release();
                Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        protected void Execute(Action<object?> action, object? parameter)
        {
            if (!_uiLock.Wait(0)) return;

            IsBusy = true;
            try
            {
                action(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
                _uiLock.Release();
                Application.Current.Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
            }
        }

        protected void ShowDialogModalLocally(Window dialog, Window owner)
        {
            dialog.Owner = owner;

            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            owner.IsEnabled = false;

            dialog.Closed += (e, args) =>
            {
                owner.IsEnabled = true;
            };

            dialog.Show();
        }

        private void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

    }


}
