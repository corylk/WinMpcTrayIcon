using System.Windows;
using System.Drawing;
using Hardcodet.Wpf.TaskbarNotification;
using WinMpcTrayIcon.Mpc;

namespace WinMpcTrayIcon
{
    /// <summary>
    /// Simple application. Check the XAML for comments.
    /// </summary>
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
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
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
            var command = ((Command)action).ToString().ToLower();
            notifyIcon.Icon = new Icon($"Icons/{command}.ico");
        }
    }
}
