using System.Windows;
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
            _mpc = new MpcClient();
            CreateNotifyIcon();
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

        public void CreateNotifyIcon()
        {
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            notifyIcon.TrayToolTipOpen += new RoutedEventHandler(NotifyIcon_TrayToolTipOpen);
        }
    }
}
