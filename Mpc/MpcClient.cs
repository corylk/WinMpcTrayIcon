using System;
using System.Diagnostics;

namespace WinMpcTrayIcon.Mpc
{
    public class MpcClient
    {
        private string _mpcPath;

        public MpcClient(string mpcPath)
        {
            _mpcPath = mpcPath;
        }

        public Process SendCommand(string cmd)
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

        public Process SendCommand(Command cmd)
        {
            return SendCommand(cmd.AsArg());
        }

        public Status GetStatus()
        {
            var q = GetInfo();
            var statusStr = ParseStatus(q);

            if (Enum.TryParse(statusStr, out Status status))
                return status;

            return Status.stopped;
        }

        public string GetInfo()
        {
            var p = SendCommand(Command.Status);
            p.Start();
            string q = "";

            while ( ! p.HasExited ) {
                q += p.StandardOutput.ReadToEnd();
            }

            return q.TrimEnd('\r', '\n');;
        }

        public MpcInfo GetToggles()
        {
            var q = GetInfo();
            var info = new MpcInfo
            {
                Repeat = q?.Split("repeat: ")[1]?.Split(" ")[0] == "on",
                Random = q?.Split("random: ")[1]?.Split(" ")[0] == "on",
                Single = q?.Split("single: ")[1]?.Split(" ")[0] == "on",
                Consume = q?.Split("consume: ")[1] == "on"
            };

            return info;
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
