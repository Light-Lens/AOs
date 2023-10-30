partial class DeveloperFeatures
{
    private void LoadDevFeatures()
    {
        this.parser.Add(
            new string[]{"new"}, "Create a new project",
            default_values: new string[]{"."},
            min_args_length: 1, max_args_length: 1, method: this.New
        );
        this.parser.Add(new string[]{"tasks"}, "List all custom tasks to run in a developer environment", is_flag: true, method: this.Tasks);
        this.parser.Add(new string[]{"clean"}, "Delete temp/unnecessary files created by the programming language in the project", is_flag: true, method: this.CleanProject);
        this.parser.Add(new string[]{"ver", "version"}, "Show the current build number of the project", is_flag: true, method: this.ShowBuildNo);
        this.parser.Add(new string[]{"server"}, "Start a local web-server", is_flag: true, method: this.StartLocalServer);
    }
}
