using System;
using System.Diagnostics;

namespace WinMpcTrayIcon.Mpc
{
    public class MpcClient
    {
        private readonly string _mpcPath;

        public MpcClient(string mpcPath = "mpc")
        {
            _mpcPath = mpcPath;
        }

        public void Cmd(string cmd)
        {
            if (Enum.IsDefined(typeof(Command), cmd))
                GetProc(cmd).Start();
        }

        public string GetInfo()
        {
            var p = GetProc(Command.status.ToString());
            p.Start();
            string q = "";

            while ( ! p.HasExited ) {
                q += p.StandardOutput.ReadToEnd();
            }

            return q.TrimEnd('\r', '\n');;
        }

        private Process GetProc(string cmd)
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
    }
}
