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
            this.
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
        /// mpc play
        /// </summary>
        public ICommand PlayCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        _mpc.SendCommand(Command.Play).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc pause
        /// </summary>
        public ICommand PauseCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        _mpc.SendCommand(Command.Pause).Start();
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
        /// mpc repeat
        /// </summary>
        public ICommand RepeatCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var status = _mpc.SendCommand(Command.Repeat).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc random
        /// </summary>
        public ICommand RandomCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var status = _mpc.SendCommand(Command.Random).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc single
        /// </summary>
        public ICommand SingleCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var status = _mpc.SendCommand(Command.Single).Start();
                    }
                };
            }
        }

        /// <summary>
        /// mpc consume
        /// </summary>
        public ICommand ConsumeCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var status = _mpc.SendCommand(Command.Consume).Start();
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
        public string GetIsRepeatOn => _mpc.GetToggles().Repeat ? "/Icons/on.png" : "/Icons/off.png";
        public string GetIsRandomOn => _mpc.GetToggles().Random ? "/Icons/on.png" : "/Icons/off.png";
        public string GetIsSingleOn => _mpc.GetToggles().Single ? "/Icons/on.png" : "/Icons/off.png";
        public string GetIsConsumeOn => _mpc.GetToggles().Consume ? "/Icons/on.png" : "/Icons/off.png";

    }
}
