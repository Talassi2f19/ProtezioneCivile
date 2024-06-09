using System;
using _Scenes.User.telefono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class taskSeleziona : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    [SerializeField]private GameObject avanti;
    [SerializeField]private Transform parent;
    [SerializeField]private Logica logica;
    [SerializeField]private VolontariTaks volontariTaks;
    [SerializeField]private RichiesteVolontari richiesteVolontari;

    private GameObject button;
    private int codice;
    
    
    public void NuovaTask(int cod)
    {
        avanti.SetActive(true);
        GameObject tmp = Instantiate(prefab, parent);
        tmp.GetComponentInChildren<TextMeshProUGUI>().text = "testo"+ cod;
        tmp.GetComponent<Button>().onClick.AddListener(()=>Select(cod, tmp));
    }

    private void Select(int k, GameObject h)
    {

        try
        {
            button.GetComponent<Image>().color = Color.white;
        }
        catch
        {
            // ignored
        }

        button = h;
        codice = k;
        h.GetComponent<Image>().color = Color.green;
    }
    
    public void ApriAssenga()
    {
        logica.TaskAssenga(codice);
        button.SetActive(false);
        if(parent.childCount <= 1)
            avanti.SetActive(false);
        Destroy(button);
    }
    
    public void AvantiVolontari()
    {
        volontariTaks.Assegna(codice);
        button.SetActive(false);
        if(parent.childCount <= 1)
            avanti.SetActive(false);
        Destroy(button);
    }

    public void ProcediRichiesta()
    {
        richiesteVolontari.ConfermaRichiesta(codice);
        button.SetActive(false);
        if(parent.childCount <= 1)
            avanti.SetActive(false);
        Destroy(button);
    }
}
