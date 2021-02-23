using System.Windows;
using System.Windows.Input;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public class NotifyIconViewModel
    {
        private readonly MpcClient _mpc;

        public NotifyIconViewModel()
        {
            _mpc = new MpcClient("C:/Scripts/mpc/mpc.exe");
        }

        public ICommand SendCommand => new DelegateCommand
        {
            CommandAction = (string cmd) => _mpc.SendCommand(cmd).Start()
        };

        public ICommand PlayPauseCommand => new DelegateCommand
        {
            CommandAction = (string cmd) =>
            {
                var cmdInt = 3 - (int)_mpc.GetStatus();
                _mpc.SendCommand((Command)cmdInt).Start();
            }
        };

        public static ICommand ExitApplicationCommand => new DelegateCommand
        {
            CommandAction = (string cmd) => Application.Current.Shutdown()
        };

        public string GetStatus => _mpc.GetInfo();
    }
}
