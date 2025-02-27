using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectableText : MonoBehaviour
{
    //Textの色を変える
    //選択中なら黄色：そうでないなら白

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string newName)
    {
        if (text == null) 
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = newName;
    }

    //private void Start()
    //{
    //  SetSelectedColer(true);
    //}

    //選択中なら色を変える
    public void SetSelectedColer(bool selected)
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        if (selected)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.white;
        }
    }
}
