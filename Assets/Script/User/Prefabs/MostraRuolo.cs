using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.User.Prefabs
{
    public class MostraRuolo : MonoBehaviour
    {
        [SerializeField] private Image roleImage;
        [SerializeField] private new TMP_Text name;
        [SerializeField] private TMP_Text description;
    
        //private string roleImagePath;
        //private string roleName;
        //private string roleDescription;


        private void Start()
        {
            //TODO carica in base al ruolo in autoamtico
        }

        //public void SetRoleName(string roleName)
        //{
        //    this.roleName = roleName;
        //    name.text = this.roleName;
        //}

        //public void SetRoleDescription(string roleDescription)
        //{
        //    this.roleDescription = roleDescription;
        //    description.text = this.roleDescription;
        //}
    
        //public void SetRoleImage(string roleImagePath)
        //{
        //    this.roleImagePath = roleImagePath;
        //    Sprite immagine = Resources.Load<Sprite>(this.roleImagePath);
        //    roleImage.sprite = immagine;
        //}
    
        public void ExitRoleInfo()
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        
        }
    }
}
