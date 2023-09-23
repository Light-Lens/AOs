partial class Parser
{
    public void PrintHelp(Command details)
    {
        string names = string.Join(", ", details.Cmd_names);
        string desc = details.Help_message;
        string default_value = (details.Default_values != null && !Utils.Array.IsEmpty(details.Default_values)) ? $" (default: {string.Join(", ", details.Default_values)})" : "";
        string is_flag = details.Is_flag == true ? $" (is flag: true)" : "";
        string max_args_len = details.Max_args_length == 0 ? "Maximum arguments: ∞" : $"Maximum arguments: {details.Max_args_length}";
        string min_args_len = $"Minimum arguments: {details.Min_args_length}";

        new TerminalColor("Name:", ConsoleColor.Cyan);
        new TerminalColor(string.Format("{0," + -Utils.Maths.CalculatePadding(1) + "}", names), ConsoleColor.Gray, false);
        new TerminalColor(desc + "\n", ConsoleColor.DarkGray);

        new TerminalColor("Details:", ConsoleColor.Blue);
        new TerminalColor($"{names} [OPTIONS] {default_value}{is_flag}", ConsoleColor.Gray);
        Console.WriteLine();

        if (!details.Is_flag)
        {
            new TerminalColor(max_args_len, ConsoleColor.Gray);
            new TerminalColor(min_args_len, ConsoleColor.Gray);
            Console.WriteLine();

            if (details.Supported_args != null)
            {
                int i = 1;
                new TerminalColor("Options:", ConsoleColor.Magenta);
                foreach (var supported_args in details.Supported_args)
                {
                    string arg_names = string.Join(", ", supported_args.Key);
                    string arg_desc = supported_args.Value;

                    new TerminalColor(string.Format("{0," + -Utils.Maths.CalculatePadding(i) + "}", arg_names), ConsoleColor.Gray, false);
                    new TerminalColor(arg_desc, ConsoleColor.DarkGray);
                    i++;
                }

                Console.WriteLine();
            }
        }
    }

    public void GetHelp(string[] cmd_names)
    {
        cmd_names = Utils.Array.Reduce(cmd_names);

        if (Utils.Array.IsEmpty(cmd_names))
        {
            Console.WriteLine("Type `help <command-name>` for more information on a specific command");

            for (int i = 0; i < command_details.Count; i++)
            {
                var detail = command_details[i];

                string command_names = string.Join(", ", detail.Cmd_names);
                string description = detail.Help_message;

                new TerminalColor($"{i+1}. ", ConsoleColor.DarkGray, false);
                new TerminalColor(string.Format("{0," + -Utils.Maths.CalculatePadding(i+1) + "}", command_names), ConsoleColor.Gray, false);
                new TerminalColor(description, ConsoleColor.DarkGray);
            }
        }

        else
        {
            foreach (string name in cmd_names)
            {
                Command matching_cmd = FindMatchingCommand(name);
                if (matching_cmd.Cmd_names == null)
                {
                    new Error($"No information for command '{name}'");
                    continue;
                }

                PrintHelp(matching_cmd);
            }
        }
    }
}
