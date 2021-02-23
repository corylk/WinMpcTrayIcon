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
            _mpc = new MpcClient();
        }

        public ICommand SendCommand => new DelegateCommand
        {
            CommandAction = (string cmd) => _mpc.Cmd(cmd)
        };

        public ICommand PlayPauseCommand => new DelegateCommand
        {
            CommandAction = (string cmd) =>
            {
                var cmdInt = 3 - (int)_mpc.GetStatus();
                cmd = ((Command)cmdInt).ToString();
                _mpc.Cmd(cmd);
            }
        };

        public static ICommand ExitApplicationCommand => new DelegateCommand
        {
            CommandAction = (string cmd) => Application.Current.Shutdown()
        };

        public string GetStatus => _mpc.GetInfo();
    }
}
