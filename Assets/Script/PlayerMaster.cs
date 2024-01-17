using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script
{
    public class PlayerMaster : MonoBehaviour
    {
        private User user;
        private string id;
    
        public TMP_Text buttonText;
    
        private void Start()
        {
            buttonText.text = user.name;
        }

        public void Set(string id, User user)
        {
            this.id = id;
            this.user = user;
        }

        public void OnClick()
        {
            RestClient.Delete(Info.DBUrl + Info.SessionCode + "/players/"+ id +".json");
        }
    
    
    }
}
