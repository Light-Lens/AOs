using System.Text.Json;

partial class EntryPoint
{
    public static readonly List<string> AllCommands = [];
    public static Settings SettingsObj;

    public static void LoadSettings()
    {
        string SettingsFilepath = Path.Combine(Obsidian.root_dir, "Files.x72\\root\\settings.json");
        string JsonData = FileIO.FileSystem.ReadAllText(SettingsFilepath);

        SettingsObj = JsonSerializer.Deserialize<Settings>(JsonData);
        AllCommands.AddRange(SettingsObj.cmds.SelectMany(cmd => cmd.cmd_names));
    }

    public class Settings
    {
        public string default_else_shell { get; set; }
        public string username { get; set; }
        public string[] startlist { get; set; }
        public List<CmdsTemplate> cmds { get; set; }
    }

    public class CmdsTemplate
    {
        public string[] cmd_names { get; set; }
        public string help_message { get; set; }
        public List<string[]> supported_args { get; set; }
        public string[] default_values { get; set; }
        public bool is_flag { get; set; }
        public int min_arg_len { get; set; }
        public int max_arg_len { get; set; }
        public string method { get; set; }
        public string location { get; set; }
    }
}
