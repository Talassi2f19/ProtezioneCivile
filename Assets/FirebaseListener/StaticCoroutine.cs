using System.Collections;
using UnityEngine;

namespace FirebaseListener
{
    public static class StaticCoroutine
    {
        private class CoroutineHolder : MonoBehaviour { }

        private static CoroutineHolder _runner;
        private static CoroutineHolder Runner
        {
            get
            {
                if (_runner == null)
                {
                    _runner = new GameObject("Static Coroutine Listener").AddComponent<CoroutineHolder>();
                    Object.DontDestroyOnLoad(_runner);
                }
                return _runner;
            }
        }

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            return Runner.StartCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine routine)
        {
            Runner.StopCoroutine(routine);
        }
    }
}