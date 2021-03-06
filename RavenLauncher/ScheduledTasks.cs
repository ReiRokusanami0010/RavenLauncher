﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using Log5RLibs.Services;
using Log5RLibs.utils;
using static RavenLauncher.Schemes.LauncherScheme;
using Timer = System.Timers.Timer;

namespace RavenLauncher {
    public static class ScheduledTasks {
        private static Timer _timer;
        public static void Fire() {
            AlConsole.WriteLine(LauncherInfoScheme, "Service Fired (*´ω｀*)");
            //_timer = new Timer(60 * 60 * 1000);
            _timer = Settings.IsMaintenanceMode ? new Timer(60 * 1000) : new Timer(60 * 60 * 1000);
            _timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            _timer.Start();
            CollectStart();
        }
        
        private static void OnTimeEvent(object obj, ElapsedEventArgs eventArgs) {
            _timer.Start();
            AlConsole.WriteLine(RunCollectorScheme, "Collect Start");
            int statusCode = CollectStart();
            if (statusCode != 0) {
                AlConsole.WriteLine(FailureCollectScheme, "コントローラーの起動に失敗しました。");
                Interval();
                if (!StatusChecker.IsRecoveryMode()) { Settings.RecvFile.Create(); }
                using (StreamWriter writer = new StreamWriter(Settings.StatusDir + Settings.RecoveryStat)) {
                    writer.Write($"Recovery Boot Time:{DateTime.Now:g}");
                }
                AlConsole.WriteLine(RecoveryBootScheme, "コントローラーを再起動します。");
                for (int i = 0; i < 5; i++) {
                    statusCode = CollectStart();
                    if (statusCode != 0) {
                        AlConsole.WriteLine(FailureCollectScheme, "コントローラーの再起動に失敗しました。");
                        AlConsole.WriteLine(FailureCollectScheme, $"リトライします。[{i}]");
                    } else {
                        AlConsole.WriteLine(FailureCollectScheme, $"リカバリーに成功しました。");
                        break;
                    }
                }
            }
            AlConsole.WriteLine(RunCollectorScheme, "Collect Finished Successfully!");
            AlConsole.WriteLine(LauncherInfoScheme, "Set Next Scheduled!");
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

        private static void Interval() {
            AlConsole.Write(AlStatusEnum.Caution, $"{"Recovery Booting", 16}", $"{"Launcher", 16}", "Interval.");
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.Write("\n");
        }

    }
}