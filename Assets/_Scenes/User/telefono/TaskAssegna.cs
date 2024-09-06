using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scenes.User.telefono
{
    public class TaskAssegna : MonoBehaviour
    {
        [SerializeField]private int codice;
        [SerializeField]private GameObject gameObjectPC;
        [SerializeField]private GameObject gameObjectCRI;
        [SerializeField]private GameObject gameObjectGGEV;
        [SerializeField]private GameObject gameObjectPolizia;
        [SerializeField]private GameObject gameObjectFuoco;
        [SerializeField]private GameObject sbagliato;
        [SerializeField]private GameObject corretto;
        [SerializeField]private GameObject buttAvanti;
        [SerializeField]private GameObject buttInvia;
        [SerializeField]private TextMeshProUGUI testo;

        private int selected = 0;
        
        private void OnEnable()
        {
            selected = 0;
            buttAvanti.SetActive(false);
            buttInvia.SetActive(true);
            buttInvia.GetComponent<Button>().interactable = false;
    
            gameObjectPC.SetActive(true);
            gameObjectCRI.SetActive(true);
            gameObjectGGEV.SetActive(true);
            gameObjectPolizia.SetActive(true);
            gameObjectFuoco.SetActive(true);
        
            corretto.SetActive(false);
            sbagliato.SetActive(false);
        }


        public void SetCodice(int value, string problema)
        {
            codice = value;
            testo.text = problema;
        }
    
        public void Click(int n)
        {
            buttInvia.GetComponent<Button>().interactable = true;
            gameObjectPC.GetComponent<Image>().color = Color.white;
            gameObjectCRI.GetComponent<Image>().color = Color.white;
            gameObjectGGEV.GetComponent<Image>().color = Color.white;
            gameObjectPolizia.GetComponent<Image>().color = Color.white;
            gameObjectFuoco.GetComponent<Image>().color = Color.white;
            switch (n)
            {
                case 1:
                    selected = 1;
                    gameObjectPC.GetComponent<Image>().color = Color.green;
                    break;
                case 2:
                    selected = 2;
                    gameObjectCRI.GetComponent<Image>().color = Color.green;
                    break;
                case 3:
                    selected = 3;
                    gameObjectGGEV.GetComponent<Image>().color = Color.green;
                    break;
                case 4:
                    selected = 4;
                    gameObjectPolizia.GetComponent<Image>().color = Color.green;
                    break;
                case 5:
                    selected = 5;
                    gameObjectFuoco.GetComponent<Image>().color = Color.green;
                    break;
            }
        }

        public void Controlla()
        {
            if(selected == 0)
                return;
    
            buttAvanti.SetActive(true);
            buttInvia.SetActive(false);
    
            gameObjectPC.SetActive(false);
            gameObjectCRI.SetActive(false);
            gameObjectGGEV.SetActive(false);
            gameObjectPolizia.SetActive(false);
            gameObjectFuoco.SetActive(false);


            const int refPC = 1;
            const int refCri = 2;
            const int refGGEV = 3;
            const int refPolizia = 4;
            const int refFuoco = 5;
            
            
            switch (codice)
            {
                case 1110:
                    ControllaP2(refPC,10);
                    break;
                case 1111:
                    ControllaP2(refPC,11);
                    break;
                case 1112:
                    ControllaP2(refPC,12);
                    break;
                case 1113:
                    ControllaP2(refPC,13);
                    break;
                case 1120:
                    ControllaP2(refGGEV,20);
                    break;
                case 1130:
                    ControllaP2(refCri,30);
                    break;
                case 1131:
                    ControllaP2(refCri,31);
                    break;
                case 1141:
                    ControllaP2(refPolizia,41);
                    break;
                case 1150:
                    ControllaP2(refFuoco, 50);
                    break;
            }
        }

        private void ControllaP2(int selectNum, int codeTask)
        {
            int score;
            if (selected == selectNum)
            {
                corretto.SetActive(true);
                score = 5;
            }
            else
            {
                score = -2;
                sbagliato.SetActive(true);
            }
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":" + codeTask + "}");
            
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + score) + "}").Catch(Debug.Log);
            }).Catch(Debug.Log);
        }

        public void Avanti()
        {
            transform.parent.gameObject.SetActive(false);
        
        
            gameObjectPC.GetComponent<Image>().color = Color.white;
            gameObjectCRI.GetComponent<Image>().color = Color.white;
            gameObjectGGEV.GetComponent<Image>().color = Color.white;
            gameObjectPolizia.GetComponent<Image>().color = Color.white;
            gameObjectFuoco.GetComponent<Image>().color = Color.white;
        
            buttAvanti.SetActive(false);
            buttInvia.SetActive(true);
    
            gameObjectPC.SetActive(true);
            gameObjectCRI.SetActive(true);
            gameObjectGGEV.SetActive(true);
            gameObjectPolizia.SetActive(true);
            gameObjectFuoco.SetActive(true);
        
            corretto.SetActive(false);
            sbagliato.SetActive(false);
        }
    
    }
}
