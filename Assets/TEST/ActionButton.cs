using System;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.test
{
    public class ActionButton : MonoBehaviour
    {
        public bool flax;

        private void Update()
        {
            if (flax)
            {
                flax = false;
                chiama();    
            }
        }

        public void chiama()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json").Then(e =>
            {
                int h = 0;
                foreach (var var in new JSONObject(e.Text).list)
                {
                    h += var.intValue;
                }
            });
        }
    }
}
