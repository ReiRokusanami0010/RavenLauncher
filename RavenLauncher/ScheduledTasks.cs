using System;
using System.Diagnostics;
using System.Timers;
using Log5RLibs.Services;

using static RavenLauncher.Schemes.LauncherScheme;

namespace RavenLauncher {
    public static class ScheduledTasks {
        private static Timer _timer;
        public static void Fire() {
            AlConsole.WriteLine(LauncherInfoScheme, "Service Fired (*´ω｀*)");
            //_timer = new Timer(60 * 60 * 1000);
            _timer = new Timer(60 * 1000);
            _timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            _timer.Start();
        }
        
        private static void OnTimeEvent(object obj, ElapsedEventArgs eventArgs) {
            AlConsole.WriteLine(RunCollectorScheme, "Collect Start");
            int statusCode = CollectStart();
            if (statusCode != 0) {
                AlConsole.WriteLine(FailureCollectScheme, "コントローラーの起動に失敗しました。");
                Environment.Exit(-2);
            }
            AlConsole.WriteLine(RunCollectorScheme, "Collect Finished Successfully!");
            AlConsole.WriteLine(LauncherInfoScheme, "Set Next Scheduled!");
            _timer.Start();
        }

        private static int CollectStart() {
            Process process = new Process {
                StartInfo = new ProcessStartInfo() {
                    FileName = Settings.Controller,
                    Arguments = $"--user {Settings.DataBaseUserName} --pass {Settings.DataBasePassWord}" + " " +
                                $"{(Settings.DataBaseIsLocal ? "--local true" : "")}",
                    UseShellExecute = true
                }
            };
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
        
    }
}