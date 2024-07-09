using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Salva : MonoBehaviour
{
    [SerializeField]private List<Sprite> sprite;
    [SerializeField]private RectTransform transform;
    [ContextMenu("genera")]
    void Start()
    {
        Sprite tmp = sprite[Random.Range(0, sprite.Count)];
        GetComponent<SpriteRenderer>().sprite = tmp;
        transform.sizeDelta = tmp.bounds.size;
    }
}
