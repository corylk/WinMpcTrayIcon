using System;
using System.Diagnostics;
using System.Text;
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
            var args = new StringBuilder();
            args.Append(GetArg("h", _config.MpdIp));
            args.Append(GetArg("p", _config.MpdPort));
            args.Append(GetArg("P", _config.MpdPassword));
            args.Append(cmd);

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(_config.MpcPath, args.ToString())
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            return p;
        }

        private static string GetArg(string key, string value)
        {
            return !string.IsNullOrEmpty(value) ? $"-{key} {value} " : string.Empty;
        }
    }
}
