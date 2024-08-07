using Proyecto26;
using Script.Utility;
using TEST;
using UnityEngine;

namespace Script.test
{
    public class TaskController : MonoBehaviour
    {


        public string key;
        public bool flag;
        public int n = 4;

        // Update is called once per frame
        void Update()
        {
            if (flag)
            {
                flag = false;
                hh();
            }
        }

        private void hh()
        {
            for (int i = 0; i < n; i++)
            {
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json",
                    new Task("abscds", i).GetTaks().ToString()).Catch(Debug.LogError);
            }
            
        }
        
    }
}
