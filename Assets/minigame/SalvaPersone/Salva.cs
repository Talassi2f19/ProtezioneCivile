using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Salva : MonoBehaviour
{
    [SerializeField]private List<Sprite> sprite;
    [SerializeField]private RectTransform transforms;
    [ContextMenu("genera")]
    void Start()
    {
        Sprite tmp = sprite[Random.Range(0, sprite.Count)];
        GetComponent<SpriteRenderer>().sprite = tmp;
        transforms.sizeDelta = tmp.bounds.size;
    }
}
