using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.test
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField]
        private string code = "";

        public void SetCode(string code)
        {
            this.code = code;
        }
        
        public void Send()
        {
            string toSend = "{\"code\":\"" + code + "\"}";
            RestClient.Post(Info.DBUrl + "event.json", toSend);
        }
    }
}
