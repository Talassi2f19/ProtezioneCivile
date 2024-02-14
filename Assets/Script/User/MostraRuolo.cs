using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MostraRuolo : MonoBehaviour
{
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text description;
    
    private string roleImagePath;
    private string roleName;
    private string roleDescription; //TODO vedere se tenere o meno la descrizione come attributo perché peserà tanto
    
    public void setRoleName(string roleName)
    {
        this.roleName = roleName;
        name.text = this.roleName;
    }

    public void setRoleDescription(string roleDescription)
    {
        this.roleDescription = roleDescription;
        description.text = this.roleDescription;
    }
    
    public void setRoleImage(string roleImagePath)
    {
        this.roleImagePath = roleImagePath;
        Sprite immagine = Resources.Load<Sprite>(this.roleImagePath);
        roleImage.sprite = immagine;
    }
    
    public void ExitRoleInfo()
    {
        gameObject.SetActive(false);
        
    }
}
