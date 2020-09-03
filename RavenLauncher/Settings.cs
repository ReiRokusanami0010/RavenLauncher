using System;
using System.Diagnostics;

namespace RavenLauncher {
    public static class Settings {
        // Github Repo Url
        public static readonly string ControllerRepoUrl = "https://github.com/ReiRokusanami0010/YoutubeDataBaseController.git";
        public static readonly string ConfigRepoUrl = "https://github.com/ReiRokusanami0010/NekomataLibrary.git";
        
        // Target Directories
        public static readonly string CloneDir  = AppDomain.CurrentDomain.BaseDirectory + "Temps/";
        public static readonly string BuildDir  = AppDomain.CurrentDomain.BaseDirectory + "Controller/";
        public static readonly string ConfigDir = BuildDir + "Config/";
        
        // Program Argument
        public static string DataBaseUserName { get; set; }
        public static string DataBasePassWord { get; set; }
        public static bool   DataBaseIsLocal  { get; set; }
        public static string GithubUserName   { get; set; }
        public static string GithubPassWord   { get; set; }
        public static string GithubToken      { get; set; }
        
        // App Directories
        public static readonly string Controller = BuildDir + "YoutubeDatabaseController";
    }
}