using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Master.Prefabs
{
    //classe inirente al gameObject da visualizzare nell'interfaccia del master con il nome del player 
    public class PlayerMaster : MonoBehaviour
    {
        private GenericUser user;
        private string id;
    
        public TMP_Text buttonText;
    
        private void Start()
        {
            buttonText.text = user.name;
        }

        public void Set(string id, GenericUser user)
        {
            this.id = id;
            this.user = user;
        }

        public void OnClick()
        {
            RestClient.Delete(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + id +".json").Catch(Debug.LogError);
        }
    
    
    }
}
