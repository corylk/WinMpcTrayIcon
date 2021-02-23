using System.Windows;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;

        private MpcClient _mpc;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _mpc = new MpcClient("C:/Scripts/mpc/mpc.exe");
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            notifyIcon.TrayToolTipOpen += new RoutedEventHandler(NotifyIcon_TrayToolTipOpen);
            notifyIcon.TrayMouseDoubleClick += new RoutedEventHandler(NotifyIcon_TrayMouseDoubleClick);
            SetIcon(3 - (int)_mpc.GetStatus());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }

        private void NotifyIcon_TrayToolTipOpen(object sender, RoutedEventArgs e)
        {
            notifyIcon.ToolTipText = _mpc.GetInfo();
        }

        private void NotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            SetIcon();
        }

        private void SetIcon(int? action = null)
        {
            if (action == null)
                action = (int)_mpc.GetStatus();

            if (action == 2)
                notifyIcon.Icon = new Icon($"Icons/play.ico");
            else
                notifyIcon.Icon = new Icon($"Icons/pause.ico");
        }
    }
}
