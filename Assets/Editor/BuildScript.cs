using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.Linq;

public class WebGLBuilder
{
    [MenuItem("Build/Build WebGL ALL")]
    public static void BuildWebGLAll()
    {
        BuildWebGLUser();
        BuildWebGLMaster();
    }
    
    [MenuItem("Build/Build WebGL User")]
    public static void BuildWebGLUser()
    {
        // Set build path
        string buildPath = "build/user";

        // Get scenes
        string[] scenes = new string[]
        {
            "Assets/_Scenes/User/login.unity",
            "Assets/_Scenes/User/attesaRuoli.unity",
            "Assets/_Scenes/User/elezioni.unity",
            "Assets/_Scenes/User/EndGame.unity",
            "Assets/_Scenes/User/game.unity",
            "Assets/_Scenes/User/risultatiElezioni.unity",
            "Assets/_Scenes/User/selezioneCOC.unity"
        };

        // Set WebGL specific settings
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        PlayerSettings.WebGL.decompressionFallback = true;
        PlayerSettings.WebGL.dataCaching = false;
        PlayerSettings.WebGL.template = "PROJECT:PWACustom";
        
        // Build the player
         BuildReport report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.WebGL, BuildOptions.CleanBuildCache);
    }
    
    [MenuItem("Build/Build WebGL Master")]
    public static void BuildWebGLMaster()
    {
        // Set build path
        string buildPath = "build/master";

        // Get scenes
        string[] scenes = new string[]
        {
            "Assets/_Scenes/Master/start.unity",
            "Assets/_Scenes/Master/elezioni.unity",
            "Assets/_Scenes/Master/EndGame.unity",
            "Assets/_Scenes/Master/game.unity",
            "Assets/_Scenes/Master/GenRuoli.unity",
            "Assets/_Scenes/Master/playerLogin.unity",
            "Assets/_Scenes/Master/risultatiElezioni.unity"
        };
        

        // Set WebGL specific settings
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        PlayerSettings.WebGL.decompressionFallback = true;
        PlayerSettings.WebGL.dataCaching = false;
        PlayerSettings.WebGL.template = "PROJECT:PWACustom";
        
        // Build the player
        BuildReport report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.WebGL, BuildOptions.CleanBuildCache);
    }
}
