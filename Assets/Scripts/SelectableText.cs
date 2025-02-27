using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectableText : MonoBehaviour
{
    //Text�̐F��ς���
    //�I�𒆂Ȃ物�F�F�����łȂ��Ȃ甒

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

    //�I�𒆂Ȃ�F��ς���
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
