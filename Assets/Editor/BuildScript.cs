using UnityEditor;
using Script.Utility;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class BuildScript
    {
        private static bool development = false;

        [MenuItem("Build/Build Development")]
        private static void ToggleBoolValue()
        {
            development = !development;
        }

        [MenuItem("Build/Build Development", true)]
        private static bool ToggleBoolValueValidate()
        {
            Menu.SetChecked("Build/Build Development", development);
            return true;
        }
        
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
            PlayerSettings.WebGL.template = "PWACustom";

            // Define scenes and build path
            string[] scenes = {
                "Assets/" + Scene.Master.Start + ".unity",
                "Assets/" + Scene.Master.Elezioni + ".unity",
                "Assets/" + Scene.Master.Game + ".unity",
                "Assets/" + Scene.Master.PlayerLogin + ".unity",
                "Assets/" + Scene.Master.RisultatiElezioni + ".unity",
                "Assets/" + Scene.Master.EndGame + ".unity"
                
            };
            string buildPath = "build/master";
            BuildTarget buildTarget = BuildTarget.WebGL;
            BuildOptions buildOptions =  BuildOptions.CleanBuildCache;

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
            PlayerSettings.WebGL.template = "PWACustom";

            // Define scenes and build path
            string[] scenes = {
                "Assets/" + Scene.User.Login + ".unity",
                // "Assets/" + Scene.User.Elezioni + ".unity",
                // "Assets/" + Scene.User.Game + ".unity",
                // "Assets/" + Scene.User.SelezioneCoc + ".unity",
                // "Assets/" + Scene.User.RisultatiElezioni + ".unity",
                // "Assets/" + Scene.User.AttesaRuoli + ".unity",
                // "Assets/" + Scene.User.EndGame + ".unity"
            };
            string buildPath = "build/user";
            BuildTarget buildTarget = BuildTarget.WebGL;
            BuildOptions buildOptions =  BuildOptions.CleanBuildCache;

            // Build the project
            BuildReport tmp = BuildPipeline.BuildPlayer(scenes, buildPath, buildTarget, buildOptions);
            Debug.Log(tmp.ToString());
        }
        [MenuItem("Build/FAST Build WebGL ALL")]
        public static void FastBuildAll()
        {
            FastBuildMaster();
            FastBuildUser();
        }
        
        [MenuItem("Build/Fast Build WebGL Master")]
        public static void FastBuildMaster()
        {
            // Set WebGL specific settings
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
            PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.Low);
            PlayerSettings.WebGL.decompressionFallback = false;
            PlayerSettings.WebGL.dataCaching = false;
            PlayerSettings.WebGL.template = "PWACustom";

            // Define scenes and build path
            string[] scenes = {
                "Assets/" + Scene.Master.Start + ".unity",
                "Assets/" + Scene.Master.Elezioni + ".unity",
                "Assets/" + Scene.Master.Game + ".unity",
                "Assets/" + Scene.Master.PlayerLogin + ".unity",
                "Assets/" + Scene.Master.RisultatiElezioni + ".unity",
                "Assets/" + Scene.Master.EndGame + ".unity"
            };
            string buildPath = "build/master";
            BuildTarget buildTarget = BuildTarget.WebGL;
            BuildOptions buildOptions =  development ? BuildOptions.Development | BuildOptions.None : BuildOptions.None;

            
            // Build the project
            BuildPipeline.BuildPlayer(scenes, buildPath, buildTarget, buildOptions);
        }
        
        [MenuItem("Build/Fast Build WebGL User")]
        public static void FastBuildUser()
        {
            string[] scenes = {
                "Assets/_Scenes/User/login.unity"
            };

            string buildPath = "buildddd/user";

            // Log delle scene incluse nella build
            foreach (string scene in scenes)
            {
                Debug.Log("Including scene: " + scene);
            }

            // Controlla se la cartella di destinazione esiste, altrimenti la crea
            if (!System.IO.Directory.Exists(buildPath))
            {
                System.IO.Directory.CreateDirectory(buildPath);
            }

            // Esegui la build
            BuildReport report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.WebGL, BuildOptions.None);
            BuildSummary summary = report.summary;

            // Mostra il risultato della build
            Debug.Log("Build Summary: " + summary.ToString());

            // Controlla se la build è riuscita
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }
            else if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("Build failed");
            }

            // Aggiungi log dettagliati
            if (report.steps != null)
            {
                foreach (var step in report.steps)
                {
                    Debug.Log("Step: " + step.name);
                    foreach (var message in step.messages)
                    {
                        Debug.Log(message.content);
                    }
                }
            };
        }
    }
}