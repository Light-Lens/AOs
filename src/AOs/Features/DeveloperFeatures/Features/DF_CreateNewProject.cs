using System.Text.Json;

partial class DeveloperFeatures
{
    private void New(string folder_name)
    {
        // Create new project.
        folder_name = Utils.String.Strings(folder_name);
        FileIO.FolderSystem.Create(folder_name);
        Directory.SetCurrentDirectory(folder_name);

        // Ask to link github repo.
        TerminalColor.Print("1. ", ConsoleColor.Gray, false);
        TerminalColor.Print("If you haven't created a new GitHub repository, please goto: https://github.com/new.", ConsoleColor.White);

        TerminalColor.Print("2. ", ConsoleColor.Gray, false);
        TerminalColor.Print("By default the code will pulled from the 'master' branch.", ConsoleColor.White);

        TerminalColor.Print("3. ", ConsoleColor.Gray, false);
        TerminalColor.Print("To change the branch for the new project: <REPO-URL> [branch-name]", ConsoleColor.White);

        TerminalColor.Print("4. ", ConsoleColor.Gray, false);
        TerminalColor.Print("If you don't want to initialize git then just press ENTER.", ConsoleColor.White);

        TerminalColor.Print("Please enter your GitHub remote repository URL$ ", ConsoleColor.White, false);
        string repo_link = Console.ReadLine();
        List<string> repo_data = repo_link.Split().ToList();

        if (!Utils.String.IsEmpty(repo_link))
        {
            if (repo_data.Count == 1)
                repo_data.Add("master");

            sys_utils.CommandPrompt("git init");
            sys_utils.CommandPrompt($"git remote add origin {repo_data[0]}");
            sys_utils.CommandPrompt($"git pull origin {repo_data[1]}");
        }

        else
            repo_data.Add("");

        // Setup AOs.dev
        FileIO.FolderSystem.Create("AOs.dev");

        string CurrentDir = Directory.GetCurrentDirectory();

        // Setup project.json
        ProjectTemplate project_template = new()
        {
            project_name = Path.GetFileName(CurrentDir),
            project_path = CurrentDir,
            github_URL = repo_data[0],
            github_branch = repo_data[1],
            build = 0,
            clean_project_waste = new List<string>()
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string project_json_obj = JsonSerializer.Serialize(project_template, options);
        FileIO.FileSystem.Write("AOs.dev\\project.json", project_json_obj);

        // Setup tasks.json
        TasksConfig tasks_template = new()
        {
            tasks = new()
            {
                {
                    "help", new TasksDetails()
                    {
                        description = "Show the dotnet help message",
                        command = "dotnet help",
                        update_build_number = false,
                        call_other_tasks = new List<string>()
                    }
                }
            }
        };

        string tasks_json_obj = JsonSerializer.Serialize(tasks_template, options);
        FileIO.FileSystem.Write("AOs.dev\\tasks.json", tasks_json_obj);
    }
}
