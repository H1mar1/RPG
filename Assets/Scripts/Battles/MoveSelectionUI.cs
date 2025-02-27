using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectionUI : MonoBehaviour
{
    //�Z�̐������L�т�UI�����
    //�g����Z��UI�ɔ��f
    //�g����Z�̐�����Text�𐶐� => Prefab�𐶐�

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
        //�����̎q�v�f��<SelectableText>�R���|�[�l���g�������Ă�����̂��W�߂�
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
