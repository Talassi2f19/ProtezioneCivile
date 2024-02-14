using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class genericTextPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text testo;
    
    private string genericText;

    public void setGenericText(string genericText)
    {
        this.genericText = genericText;
        testo.text = this.genericText;

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(testo.preferredWidth + 20, 40);

    }
}
