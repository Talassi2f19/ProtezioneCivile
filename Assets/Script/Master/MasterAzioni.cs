using Proyecto26;
using Script.Utility;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Script.Master
{
    public class MasterAzioni : MonoBehaviour
    {
        [SerializeField] private GameObject tipo1;
        [SerializeField] private GameObject tipo2;
        [SerializeField] private GameObject buttonTipo1;
        [SerializeField] private GameObject buttonTipo2;

        public void Tipo1()
        {
            buttonTipo1.GetComponent<Image>().color = Color.gray;
            buttonTipo2.GetComponent<Image>().color = Color.white;
            tipo1.SetActive(true);
            tipo2.SetActive(false);
        }
        public void Tipo2()
        {
            buttonTipo1.GetComponent<Image>().color = Color.white;
            buttonTipo2.GetComponent<Image>().color = Color.gray;
            tipo1.SetActive(false);
            tipo2.SetActive(true);
        }
    
        public void ClearTipo()
        {
            buttonTipo1.GetComponent<Image>().color = Color.white;
            buttonTipo2.GetComponent<Image>().color = Color.white;
            tipo1.SetActive(false);
            tipo2.SetActive(false);
        }


        public void T1B1(Button tmp)
        {
            tmp.interactable = false;
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":1}").Catch(Debug.Log);
        }
    
    }
}
