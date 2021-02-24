using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace WinMpcTrayIcon.Mpc
{
    public class MpcClient
    {
        private readonly MpcConfig _config;

        public MpcClient()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build().Get<MpcConfig>();
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
                StartInfo = new ProcessStartInfo(_config.MpcPath, cmd)
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
