// ReSharper disable CommentTypo IdentifierTypo

using System;
using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Script.test
{
    public class Prova : MonoBehaviour
    {
        public string plRole;
        
        private JSONObject myRoleAction;
        private JSONObject action;

        private Listeners liss;

        private string code;
        private string id;

        public GameObject[] button;
        private void Start()
        {
            action = new JSONObject(Resources.Load("Action").ToString());
            myRoleAction = new JSONObject(Resources.Load("Association").ToString()).GetField(plRole);
            
            liss = new Listeners(Info.DBUrl + "event.json");
            liss.Start(Recive);
        }

        private bool isFirst = false;
        
        private void Recive(string str)
        {
            // event: put
            // data: {"path":"/-fdsdsfdsffsdf","data":{"code":"a1"}}
            Debug.Log(str);
            if (isFirst)
            {
                if (str.Contains("path"))
                {
                    str = str.Replace("/", "").Split("data: ",2)[1];
                              
                    JSONObject kk = new JSONObject(str);
                    id = kk.GetField("path").stringValue;
                    code = kk.GetField("data").GetField("code").stringValue;
                    Debug.Log(id + "  " + code);
                    if (myRoleAction.GetField(code) != null)
                    {
                        MakeAction();
                    }
                }
            }
            isFirst = true;
        }

        private void MakeAction()
        {
            List<string> val = myRoleAction.GetField(code).GetField("action").keys;
            for (int i = 0; i < val.Count; i++)
            {
                button[i].SetActive(true);
                button[i].GetComponentInChildren<Text>().text = action[val[i]].stringValue;
                button[i].GetComponent<ActionButton>().SetCode(val[i]);
            }
        }
        
        
        public void SendFirst()
        {
            Send("a1");
        }
        
        public void Send(string code)
        {
            string toSend = "{\"code\":\""+code+"\"}";
            RestClient.Post(Info.DBUrl + "event.json", toSend);
        }

    }
}