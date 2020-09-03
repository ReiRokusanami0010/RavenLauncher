using System;
using Log5RLibs.Services;

using static RavenLauncher.Schemes.LauncherScheme;

namespace RavenLauncher {
    public static class Engine {
        public static void Main(string[] args) {
            // Start Message
            AlConsole.WriteLine(LauncherInfoScheme, $"Start Up RavenLauncher.");
            
            // Arguments Parse.
            ArgParser.Decomposition(args);
            
            // Setup
            RavenSetupProcess.GitClone(Settings.ControllerRepoUrl, Settings.CloneDir, gitToken: Settings.GithubToken);
            RavenSetupProcess.BuildController(Settings.CloneDir);
            RavenSetupProcess.GitClone(Settings.ConfigRepoUrl   , Settings.ConfigDir, gitToken: Settings.GithubToken);
            
            // Service Starter
            ScheduledTasks.Fire();

            Console.ReadLine();
        }
    }
}