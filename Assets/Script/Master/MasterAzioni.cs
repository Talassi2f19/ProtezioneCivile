using System.Collections;
using System.Collections.Generic;
using Script.Master;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MasterAzioni : MonoBehaviour
{
    [SerializeField] private GameObject tipo1;
    [SerializeField] private GameObject tipo2;
    [SerializeField] private GameObject tipo3;
    [SerializeField] private GameObject buttonTipo1;
    [SerializeField] private GameObject buttonTipo2;
    [SerializeField] private GameObject buttonTipo3;

    public void Tipo1()
    {
        buttonTipo1.GetComponent<Image>().color = Color.gray;
        buttonTipo2.GetComponent<Image>().color = Color.white;
        buttonTipo3.GetComponent<Image>().color = Color.white;
        tipo1.SetActive(true);
        tipo2.SetActive(false);
        tipo3.SetActive(false);
    }
    public void Tipo2()
    {
        buttonTipo1.GetComponent<Image>().color = Color.white;
        buttonTipo2.GetComponent<Image>().color = Color.gray;
        buttonTipo3.GetComponent<Image>().color = Color.white;
        tipo1.SetActive(false);
        tipo2.SetActive(true);
        tipo3.SetActive(false);
    }
    public void Tipo3()
    {
        buttonTipo1.GetComponent<Image>().color = Color.white;
        buttonTipo2.GetComponent<Image>().color = Color.white;
        buttonTipo3.GetComponent<Image>().color = Color.gray;
        tipo1.SetActive(false);
        tipo2.SetActive(false);
        tipo3.SetActive(true);
    }

    public void ClearTipo()
    {
        buttonTipo1.GetComponent<Image>().color = Color.white;
        buttonTipo2.GetComponent<Image>().color = Color.white;
        buttonTipo3.GetComponent<Image>().color = Color.white;
        tipo1.SetActive(false);
        tipo2.SetActive(false);
        tipo3.SetActive(false);
    }
    
}
