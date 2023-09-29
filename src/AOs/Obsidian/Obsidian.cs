partial class Obsidian
{
    public string version;
    public string[] prompt_preset;
    public ConsoleColor current_foreground_color = original_foreground_color;

    private string prompt;

    public Obsidian()
    {
        this.version = $"AOs 2023 [Version {version_no}]";
        this.prompt = "$ ";
        this.prompt_preset = new string[0];
    }

    public List<(string cmd, string[] args)> TakeInput(string input="")
    {
        List<(string cmd, string[] args)> output = new();
        string CMD = input.Trim();

        Console.ForegroundColor = current_foreground_color;
        if (Utils.String.IsEmpty(CMD))
        {
            SetPrompt(this.prompt_preset);
            new TerminalColor(this.prompt, ConsoleColor.White, false);

            CMD = Console.ReadLine().Trim();

            if (Utils.String.IsEmpty(CMD))
                return new List<(string cmd, string[] args)>(); // (cmd: "", args: new string[0])
        }

        if (CMD.First() == '_')
            CMD = CMD.Substring(1).Trim();

        // Set history.
        History.Set(CMD);

        // Some lexer stuff.
        List<string[]> ListOfToks = new Lexer(CMD).Tokens;
        foreach (string[] Toks in ListOfToks)
        {
            if (Utils.String.IsEmpty(Toks.FirstOrDefault()))
                continue;

            // Split the Toks into a cmd and Args variable and array respectively.
            string input_cmd = Utils.String.Strings(Toks.FirstOrDefault());
            string[] input_args = Utils.Array.Trim(Toks.Skip(1).ToArray());

            // Add input_cmd & input_args to output.
            output.Add((input_cmd, input_args));
        }

        return output;
    }
}
