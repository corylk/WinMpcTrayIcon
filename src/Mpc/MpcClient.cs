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

        public void Cmd(Command cmd)
        {
            GetProc(cmd).Start();
        }

        public void Cmd(Command cmd, out string outPut)
        {
            var p = GetProc(cmd);
            p.Start();
            string q = "";

            while ( ! p.HasExited ) {
                q += p.StandardOutput.ReadToEnd();
            }

            outPut = q.TrimEnd('\r', '\n');;
        }

        public void Cmd(Command cmd, out Status status)
        {
            Cmd(cmd, out string q);

            if (q.Contains("[") && q.Contains("]"))
            {
                var statusStr = q?.Split("[")[1]?.Split("]")[0];

                if (Enum.TryParse(statusStr, out status))
                    return;
            }

            status = Status.stopped;
        }

        public MpcInfo GetPlaymodeStatus()
        {
            Cmd(Command.status, out string q);
            var info = new MpcInfo();

            if (q.Contains("repeat: ") &&
                q.Contains("random: ") &&
                q.Contains("single: ") &&
                q.Contains("consume: "))
            {
                info.Repeat = q?.Split("repeat: ")[1]?.Split(" ")[0] == "on";
                info.Random = q?.Split("random: ")[1]?.Split(" ")[0] == "on";
                info.Single = q?.Split("single: ")[1]?.Split(" ")[0] == "on";
                info.Consume = q?.Split("consume: ")[1].TrimEnd() == "on";
            }

            return info;
        }

        private Process GetProc(Command cmd)
        {
            var args = new StringBuilder();
            args.Append(GetArg("h", _config.MpdIp));
            args.Append(GetArg("p", _config.MpdPort));
            args.Append(GetArg("P", _config.MpdPassword));
            args.Append(cmd.ToString());

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
