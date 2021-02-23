﻿using System;
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

        public Process SendCommand(Command cmd)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(_mpcPath, cmd.AsArg())
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };

            return p;
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

        private static string ParseStatus(string q)
        {
            string statusStr = null;

            if (q.Contains("[") && q.Contains("]"))
                statusStr = q?.Split("[")[1]?.Split("]")[0];

            return statusStr;
        }
    }
}
