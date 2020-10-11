using Log5RLibs.Services;

namespace RavenLauncher.Schemes {
    public static class LauncherScheme {
        private const int MAX_INDEX = -15;
        
        // Thread Name
        private static readonly string LauncherInfo     = $"{"Launcher"         , MAX_INDEX}";
        
        // Status Name
        private static readonly string StatClone        = $"{"Cloning..."       , MAX_INDEX}";
        private static readonly string StatCloneFailure = $"{"Clone Failure"    , MAX_INDEX}";
        private static readonly string StatBuild        = $"{"Building..."      , MAX_INDEX}";
        private static readonly string StatBuildFailure = $"{"Build Failure"    , MAX_INDEX}";
        private static readonly string StatCollect      = $"{"Collecting..."    , MAX_INDEX}";
        private static readonly string StatFailCollect  = $"{"Collect Failure"  , MAX_INDEX}";
        private static readonly string StatRecvCollect  = $"{"Recovery Booting" , MAX_INDEX}";
        private static readonly string StatUpdateCheck  = $"{"UpdateCheck"      , MAX_INDEX}";
        private static readonly string StatFoundUpdate  = $"{"Found Update"     , MAX_INDEX}";
        
        // Schemes
        public static readonly AlCConfigScheme LauncherInfoScheme   = new AlCConfigScheme(0, null            , LauncherInfo);
        public static readonly AlCConfigScheme LauncherCautScheme   = new AlCConfigScheme(1, null            , LauncherInfo);
        public static readonly AlCConfigScheme CloneInfoScheme      = new AlCConfigScheme(0, StatClone       , LauncherInfo);
        public static readonly AlCConfigScheme CloneFailureScheme   = new AlCConfigScheme(3, StatCloneFailure, LauncherInfo);
        public static readonly AlCConfigScheme BuildInfoScheme      = new AlCConfigScheme(0, StatBuild       , LauncherInfo);
        public static readonly AlCConfigScheme BuildFailureScheme   = new AlCConfigScheme(3, StatBuildFailure, LauncherInfo);
        public static readonly AlCConfigScheme RunCollectorScheme   = new AlCConfigScheme(0, StatCollect     , LauncherInfo);
        public static readonly AlCConfigScheme FailureCollectScheme = new AlCConfigScheme(3, StatFailCollect , LauncherInfo);
        public static readonly AlCConfigScheme RecoveryBootScheme   = new AlCConfigScheme(1, StatRecvCollect , LauncherInfo);
        public static readonly AlCConfigScheme UpdateCheckScheme    = new AlCConfigScheme(0, StatUpdateCheck , LauncherInfo);
        public static readonly AlCConfigScheme FoundUpdateScheme    = new AlCConfigScheme(0, StatFoundUpdate , LauncherInfo);

    }
}