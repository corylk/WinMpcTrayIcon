using System;
using System.Diagnostics;

namespace WinMpcTrayIcon.Mpc
{
    public class MpcClient
    {
        private readonly string _mpcPath;

        public MpcClient(string mpcPath = "C:/Scripts/mpc/mpc.exe")
        {
            _mpcPath = mpcPath;
        }

        public void Cmd(string cmd)
        {
            SendCommand(cmd).Start();
        }

        public string GetInfo()
        {
            var p = SendCommand(Command.status.ToString());
            p.Start();
            string q = "";

            while ( ! p.HasExited ) {
                q += p.StandardOutput.ReadToEnd();
            }

            return q.TrimEnd('\r', '\n');;
        }

        public Status GetStatus()
        {
            var q = GetInfo();
            var statusStr = ParseStatus(q);

            if (Enum.TryParse(statusStr, out Status status))
                return status;

            return Status.stopped;
        }

        private Process SendCommand(string cmd)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(_mpcPath, cmd)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            return p;
        }

        private static string ParseStatus(string q)
        {
            string statusStr = null;

            if (q.Contains("[") && q.Contains("]"))
                statusStr = q?.Split("[")[1]?.Split("]")[0];

            return statusStr;
        }
    }
}
