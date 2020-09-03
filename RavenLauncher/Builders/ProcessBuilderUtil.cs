using System.Diagnostics;

namespace RavenLauncher.Builders {
    public static class ProcessBuilderUtil {
        public static Process Builder(string command, string arguments, string workDirPath) {
            Process process = new Process() {
                StartInfo = new ProcessStartInfo(command, arguments) {
                    WorkingDirectory = workDirPath,
                    UseShellExecute = true
                }
            };
            return process;
        }
    }
}