using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionUI : MonoBehaviour
{
    //技の数だけ伸びるUIを作る
    //使える技をUIに反映
    //使える技の数だけTextを生成 => Prefabを生成

    [SerializeField] RectTransform movesParent;
    [SerializeField] SelectableText moveTexPrefab;
    List<SelectableText> selectableTexts=new List<SelectableText>();

    int selectedIndex;
    public int SelectedIndex { get => selectedIndex; }

    //private void Start()
    //{
    //    Init();
    //}
    public void Init(List<Move> moves)
    {
        //自分の子要素で<SelectableText>コンポーネントを持っているものを集める
        SetMovesUISize(moves);
        //selectableTexts = GetComponentsInChildren<SelectableText>();
    }

    void SetMovesUISize(List<Move> moves)
    {
        Vector2 uiSize = movesParent.sizeDelta;
        //uiSize.y = 50 + 55 * (count + 1);
        uiSize.y = 50 + 55 * moves.Count;
        movesParent.sizeDelta = uiSize;

        for (int i = 0; i < moves.Count; i++) 
        {
            SelectableText moveText = Instantiate(moveTexPrefab, movesParent);
            moveText.SetText(moves[i].Base.Name);
            selectableTexts.Add(moveText);
        }
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

        selectedIndex = Mathf.Clamp(selectedIndex, 0, selectableTexts.Count - 1);

        for (int i = 0; i < selectableTexts.Count; i++)
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

    public void DeleteMoveTexts()
    {
        foreach (var text in selectableTexts)
        {
            Destroy(text.gameObject);
        }
        selectableTexts.Clear();
    }
}
