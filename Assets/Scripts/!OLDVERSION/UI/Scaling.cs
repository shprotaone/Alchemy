using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    public GameObject tableBG;
    public GameObject ordersBG;
    public GameObject canvas;
    public GameObject cauldron;
    public GameObject[] generic;
    public Vector3 potionDataScale;

    private void Awake()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        Sprite s = tableBG.GetComponent<SpriteRenderer>().sprite;
        float unitWidth = s.textureRect.width / s.pixelsPerUnit;
        float unitHeight = s.textureRect.height / s.pixelsPerUnit;

        tableBG.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
        ordersBG.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);

        canvas.transform.localScale = new Vector3(tableBG.transform.localScale.x / 1.736f / 100, tableBG.transform.localScale.y / 1.389f / 100, 1);
        cauldron.transform.localScale = new Vector3(tableBG.transform.localScale.x / 1.736f * 0.8f, tableBG.transform.localScale.y / 1.389f * 0.7f, 1);

        foreach (var item in generic)
            item.transform.localScale = new Vector3(tableBG.transform.localScale.x / 1.736f, tableBG.transform.localScale.y / 1.389f, 1);
    }
}