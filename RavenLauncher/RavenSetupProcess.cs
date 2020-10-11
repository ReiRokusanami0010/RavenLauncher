using System;
using System.Runtime.InteropServices;
using LibGit2Sharp;
using Log5RLibs.Services;
using RavenLauncher.Builders;
using static RavenLauncher.Schemes.LauncherScheme;

namespace RavenLauncher {
    public static class RavenSetupProcess {
        public static void GitClone(string repoUrl, string clonePath, [Optional] string gitToken) {
            CloneOptions options = new CloneOptions() {
                CredentialsProvider = (a, b, c) => new UsernamePasswordCredentials() {
                    Username = gitToken ?? Settings.GithubUserName,
                    Password = gitToken != null ? String.Empty : Settings.GithubPassWord
                }
            };

            try {
                AlConsole.WriteLine(CloneInfoScheme, $"クローンを開始します。[ {repoUrl.Substring(37).Replace(".git", "")} ]");
                Repository.Clone(repoUrl, clonePath, options);
                AlConsole.WriteLine(CloneInfoScheme, "クローンに成功しました。");
            } catch (Exception e) {
                AlConsole.WriteLine(CloneFailureScheme, "クローンに失敗しました。");
                Console.WriteLine(e.StackTrace);
                Environment.Exit(-1);
            }
        }

        public static void BuildController(string clonePath) {
            AlConsole.WriteLine(BuildInfoScheme, "ビルドを開始します。");
            int statusCode = RavenAppBuilder.Build(clonePath, true);
            if (statusCode != 0) {
                AlConsole.WriteLine(BuildFailureScheme, "ビルドに失敗しました。");
                AlConsole.WriteLine(BuildFailureScheme, $"ステータスコード : [{statusCode}]");
                Environment.Exit(-1);
            }
            AlConsole.WriteLine(BuildInfoScheme, "ビルドに成功。");
        }
    }
}