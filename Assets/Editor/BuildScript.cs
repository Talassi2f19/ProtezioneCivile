using UnityEditor;
using Script.Utility;

namespace Editor
{
    public class BuildScript
    {
        [MenuItem("Build/Build WebGL ALL")]
        public static void BuildAll()
        {
            BuildMaster();
            BuildUser();
        }
        
        [MenuItem("Build/Build WebGL Master")]
        public static void BuildMaster()
        {
            // Set WebGL specific settings
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
            PlayerSettings.WebGL.decompressionFallback = true;
            PlayerSettings.WebGL.dataCaching = false;

            // Define scenes and build path
            string[] scenes = {
                "Assets/" + Scene.Master.Start + ".unity",
                "Assets/" + Scene.Master.Elezioni + ".unity",
                "Assets/" + Scene.Master.Game + ".unity",
                "Assets/" + Scene.Master.PlayerLogin + ".unity",
                "Assets/" + Scene.Master.RisultatiElezioni + ".unity"
            };
            string buildPath = "build/Master";
            BuildTarget buildTarget = BuildTarget.WebGL;
            BuildOptions buildOptions = BuildOptions.None;

            // Build the project
            BuildPipeline.BuildPlayer(scenes, buildPath, buildTarget, buildOptions);
        }
        
        [MenuItem("Build/Build WebGL User")]
        public static void BuildUser()
        {
            // Set WebGL specific settings
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
            PlayerSettings.WebGL.decompressionFallback = true;
            PlayerSettings.WebGL.dataCaching = false;

            // Define scenes and build path
            string[] scenes = {
                "Assets/" + Scene.User.Login + ".unity",
                "Assets/" + Scene.User.Elezioni + ".unity",
                "Assets/" + Scene.User.Game + ".unity",
                "Assets/" + Scene.User.SelezioneCoc + ".unity",
                "Assets/" + Scene.User.RisultatiElezioni + ".unity",
                "Assets/" + Scene.User.AttesaRuoli + ".unity"
            };
            string buildPath = "build/User";
            BuildTarget buildTarget = BuildTarget.WebGL;
            BuildOptions buildOptions = BuildOptions.None;

            // Build the project
            BuildPipeline.BuildPlayer(scenes, buildPath, buildTarget, buildOptions);
        }
    }
}