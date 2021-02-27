using System;
using System.Diagnostics;
using System.Linq;
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

        public void Cmd(Command cmd, string args = null)
        {
            GetProc(cmd, args).Start();
        }

        public void Cmd(Command cmd, out string outPut, string args = null)
        {
            var p = GetProc(cmd, args);
            p.Start();
            string q = "";

            while ( ! p.HasExited ) {
                q += p.StandardOutput.ReadToEnd();
            }

            outPut = q.TrimEnd('\r', '\n');;
        }

        public void Cmd(Command cmd, out Status status, string args = null)
        {
            Cmd(cmd, out string q, args);

            if (q.Contains("[") && q.Contains("]"))
            {
                var statusStr = q?.Split("[")[1]?.Split("]")[0];

                if (Enum.TryParse(statusStr, out status))
                    return;
            }

            status = Status.stopped;
        }

        public void Cmd(Command cmd, out MpcInfo info, string args = null)
        {
            Cmd(cmd, out string q, args);
            info = new MpcInfo();

            if (q.Contains("\r\n"))
            {
                var infoArr = q.Split("\r\n").ToList();

                if (infoArr.Count > 1 && infoArr[1].Contains("["))
                {
                    info.Track = infoArr[0]; // do more validation on these
                    info.Status = infoArr[1];
                    info.Playmodes = infoArr[2];
                }
                else
                {
                    info.Playmodes = infoArr[0];
                }
            }

            info.Playmodes = q;
        }

        public MpcPlaymodes GetPlaymodeStatus()
        {
            Cmd(Command.status, out MpcInfo q);
            var info = new MpcPlaymodes
            {
                Repeat = q.Playmodes?.Split("repeat: ")[1]?.Split(" ")[0] == "on",
                Random = q.Playmodes?.Split("random: ")[1]?.Split(" ")[0] == "on",
                Single = q.Playmodes?.Split("single: ")[1]?.Split(" ")[0] == "on",
                Consume = q.Playmodes?.Split("consume: ")[1].TrimEnd() == "on"
            };

            return info;
        }

        private Process GetProc(Command cmd, string args = null)
        {
            var command = new StringBuilder();
            command.Append(GetArg("h", _config.MpdIp));
            command.Append(GetArg("p", _config.MpdPort));
            command.Append(GetArg("P", _config.MpdPassword));
            command.Append(cmd.ToString());

            if (args != null)
                 command.Append(' ').Append(args);

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(_config.MpcPath, command.ToString())
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
