using Proyecto26;
using Script.Utility;
using UnityEngine;

// ReSharper disable CommentTypo

namespace Script.Master
{
    public class Game : MonoBehaviour
    {

        public void InviaAllerta()
        {
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":1}").Catch(Debug.LogError);
        }
    }
}
