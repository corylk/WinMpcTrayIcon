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

        public ICommand MpcCmd => new DelegateCommand
        {
            CommandAction = (string cmd) => _mpc.Cmd(cmd)
        };

        public static ICommand Exit => new DelegateCommand
        {
            CommandAction = (string cmd) => Application.Current.Shutdown()
        };
    }
}
