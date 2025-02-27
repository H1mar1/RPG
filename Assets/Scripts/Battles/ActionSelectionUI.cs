using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectionUI : MonoBehaviour
{
    //�A�N�V����UI�̊Ǘ�
    //��������or�ɂ���̂ǂ��炩��I�𒆂���c�����ĐF��ς���
    //[SerializeField]
    SelectableText[] selectableTexts;

    int selectedIndex ;//0:���������A1�F�ɂ����I�����Ă���

    public int SelectedIndex { get => selectedIndex; }

    //private void Start()
    //{
    //    Init();
    //}
    public void Init()
    {
        //�����̎q�v�f��<SelectableText>�R���|�[�l���g�������Ă�����̂��W�߂�
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
