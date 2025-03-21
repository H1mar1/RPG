using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectionUI : MonoBehaviour
{
    //アクションUIの管理
    //かかたうorにげるのどちらかを選択中かを把握して色を変える
    //[SerializeField]
    SelectableText[] selectableTexts;

    int selectedIndex ;//0:たたかう、1：にげるを選択している

    public int SelectedIndex { get => selectedIndex; }

    //private void Start()
    //{
    //    Init();
    //}
    public void Init()
    {
        //自分の子要素で<SelectableText>コンポーネントを持っているものを集める
        selectableTexts = GetComponentsInChildren<SelectableText>();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, selectableTexts.Length - 1);

        for (int i = 0; i < selectableTexts.Length; i++)
        {
            if (selectedIndex == i)
            {
                selectableTexts[i].SetSelectedColer(true);
            }
            else
            {
                selectableTexts[i].SetSelectedColer(false);
            }
        }
    }

    public void Open()
    {
        selectedIndex = 0;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
   
}
