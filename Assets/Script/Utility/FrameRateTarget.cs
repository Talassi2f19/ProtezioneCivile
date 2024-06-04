using UnityEngine;

namespace Script.Utility
{
	public class FrameRateTarget
	{
		//Init is called before the first scene is loaded
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		static void Init()
		{
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
		}
	}
}
