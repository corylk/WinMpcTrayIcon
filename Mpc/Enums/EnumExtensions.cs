namespace WinMpcTrayIcon.Mpc
{
    public static class EnumExtensions
    {
        public static string AsArg(this Command cmd)
        {
            return cmd.ToString().ToLower();
        }

        public static int ToInt(this Command cmd)
        {
            return (int)cmd;
        }

        public static Command ToCommand(this int cmd)
        {
            return (Status)cmd;
        }
    }
}