using Defective.JSON;
using Script.Utility;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.User.Prefabs
{
    public class MostraRuolo : MonoBehaviour
    {
        [SerializeField] private Image roleImage;
        [SerializeField] private new TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI description;
        
        private void Start()
        {
            string filePath = Application.dataPath + "/FileUtili/infoRuoli.json";
            JSONObject json = new JSONObject(File.ReadAllText(filePath));
            json = json.GetField(Info.localUser.role.ToString());
            
            name.text = json.GetField("Nome").stringValue;
            description.text = json.GetField("Descrizione").stringValue;
            roleImage.sprite = Resources.Load<Sprite>(Application.dataPath + json.GetField("Sprite").stringValue);
        }
    }
}
