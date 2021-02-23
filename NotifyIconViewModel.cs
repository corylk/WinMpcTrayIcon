using System.Windows;
using System.Windows.Input;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        private MpcClient _mpc;

        public NotifyIconViewModel()
        {
            _mpc = new MpcClient("C:/Scripts/mpc/mpc.exe");
        }

        /// <summary>
        /// mpc pause
        /// </summary>
        public ICommand PlayPauseCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        var action = 3 - (int)_mpc.GetStatus();
                        _mpc.SendCommand((Command)action).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc clear
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        _mpc.SendCommand(Command.Clear).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc stop
        /// </summary>
        public ICommand StopCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        _mpc.SendCommand(Command.Stop).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc status
        /// </summary>
        public ICommand StatusCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var status = _mpc.GetStatus();
                    }
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public static ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }

        public string GetStatus => _mpc.GetInfo();
    }
}
