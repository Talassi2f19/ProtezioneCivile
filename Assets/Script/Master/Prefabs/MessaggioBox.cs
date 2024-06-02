using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Master.Prefabs
{
    public class MessaggioBox : MonoBehaviour
    {
        [SerializeField] private Image selectFront;
        [SerializeField] private TextMeshProUGUI testo;
        [SerializeField] private int id;

        private string tt;
        public void SetText(string str, int id)
        {
            tt = str;
            testo.text = str;
            this.id = id;
        }

        public string GetTesto()
        {
            return tt;
        }
    
        public void Selected(bool flag)
        {
            if(flag)
                selectFront.color = Color.blue;
            else
                selectFront.color = Color.white;
        }
    
        public void Click()
        {
            transform.parent.parent.gameObject.GetComponent<MessaggiMaster>().MessaggioSelezionato(id);
        }
    }
}
