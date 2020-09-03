using System.Diagnostics;

namespace RavenLauncher.Builders {
    public static class RavenAppBuilder {
        public static int Build(string path, bool isReleaseMode) {
            Process process = ProcessBuilderUtil.Builder(
                "dotnet", 
                "publish -c " + (isReleaseMode ? "Release" : "Debug") +
                " -o " + Settings.BuildDir,
                Settings.CloneDir);
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}