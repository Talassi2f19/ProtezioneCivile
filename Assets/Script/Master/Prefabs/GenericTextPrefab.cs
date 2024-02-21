using TMPro;
using UnityEngine;

namespace Script.Master.Prefabs
{
    public class GenericTextPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text testo;
    
        private string genericText;

        public void SetGenericText(string genericText)
        {
            this.genericText = genericText;
            testo.text = this.genericText;

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(testo.preferredWidth + 20, 40);

        }
    }
}
