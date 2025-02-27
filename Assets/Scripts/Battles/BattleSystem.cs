using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]GameController gameController;
    [SerializeField] ActionSelectionUI actionSelectionUI;
    [SerializeField]MoveSelectionUI moveSelectionUI;
    [SerializeField]BattleDialog battleDialog;

    [SerializeField] BattleUnit  playerUnit;
    [SerializeField] BattleUnit enemyUnit;

    public UnityAction OnBattleOver;
    
    enum State
    {
        Start,
        ActionSlection,
        MoveSelection,
        RunTurns,
        BattleOver,
    }

    State state;

    public void BattleStart(Battler player, Battler enemy)
    {
        state = State.Start;
        Debug.Log("�o�g���J�n");
        actionSelectionUI.Init();
        moveSelectionUI.Init(player.Moves);
        //ActionSelection();
        actionSelectionUI.Close();
        StartCoroutine(SetupBattle(player, enemy));
    }

    IEnumerator SetupBattle(Battler player, Battler enemy)
    {
        playerUnit.Setup(player);
        enemyUnit.Setup(enemy);

        yield return battleDialog.TypeDialog($"{enemy.Base.Name}������ꂽ�I\n�ǂ�����H");
        ActionSelection();
    }

    private void BattleOver()
    {
        moveSelectionUI.DeleteMoveTexts();
        gameController.EndBattle();
        OnBattleOver?.Invoke();

    }

    void ActionSelection()
    {
        state = State.ActionSlection;
        actionSelectionUI.Open();
    }

    void MoveSelection()
    {
        state = State.MoveSelection;
        moveSelectionUI.Open();
    }

    IEnumerator RunTurns()
    {
        state=State.RunTurns;
        Move playerMove = playerUnit.Battler.Moves[moveSelectionUI.SelectedIndex];
        yield return RunMove(playerMove, playerUnit, enemyUnit);
        if (state == State.BattleOver) 
        {
            yield return battleDialog.TypeDialog($"{enemyUnit.Battler.Base.Name}����������", auto: false);
            //���x���A�b�v
            //�|�����G����o���l������
            playerUnit.Battler.hasExp += enemyUnit.Battler.Base.Exp;
            yield return battleDialog.TypeDialog($"{playerUnit.Battler.Base.Name}�͂�������{enemyUnit.Battler.Base.Exp}������", auto: false);
            //���ȏ�o���l�����܂�ƁA���x�����オ��
            if (playerUnit.Battler.IsLevelUp())
            {
               playerUnit.UpdateUI();
                yield return battleDialog.TypeDialog($"{playerUnit.Battler.Base.Name}�̓��x��{playerUnit.Battler.Level}�ɂȂ���", auto: false);
                //����̃��x���Ȃ�Z���o����
                Move learnedMove = playerUnit.Battler.LearnedMove();
                if(learnedMove != null)
                {
                    yield return battleDialog.TypeDialog($"{playerUnit.Battler.Base.Name}��{learnedMove.Base.Name}�����ڂ����I", auto: false);
                }
            }
            BattleOver();
            yield break;
        }
        //Debug.Log("���҂̍U������");
        //yield return battleDialog.TypeDialog("YY�̍U��", auto: false);
        //yield return battleDialog.TypeDialog("XX�̍U��",auto: false);
        Move enemyMove = enemyUnit.Battler.GetRandomMove();
        yield return RunMove(enemyMove, enemyUnit, playerUnit);
        if(state == State.BattleOver)
        {
            yield return battleDialog.TypeDialog($"{playerUnit.Battler.Base.Name}�͂�����Ă��܂���");
            BattleOver();
            yield break;
        }
        yield return battleDialog.TypeDialog("�ǂ�����H");
        ActionSelection();
    }
    
    IEnumerator RunMove(Move move,BattleUnit sourceUnit,BattleUnit targetUnit)
    {
        string resultText = move.Base.RunMoveResult(sourceUnit, targetUnit);
        yield return battleDialog.TypeDialog(resultText, auto: false);
        //int damage = targetUnit.Battler.TakeDamage(move,sourceUnit.Battler);
        //yield return battleDialog.TypeDialog($"{sourceUnit.Battler.Base.Name}��{move.Base.Name}\n{targetUnit.Battler.Base.Name}��{damage}�̃_���[�W", auto: false);
        sourceUnit.UpdateUI();
        targetUnit.UpdateUI();

        if (targetUnit.Battler.HP <= 0)
        {
            state = State.BattleOver;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Start:
                break;
            case State.ActionSlection:
                HandleActionSelection();
                break;
            case State.MoveSelection:
                HandleMoveSelection();
                break;
            case State.BattleOver:
                break;
        }
    }

    void HandleActionSelection()
    {
        actionSelectionUI.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actionSelectionUI.SelectedIndex == 0)
            {
                MoveSelection();
            }
            else if(actionSelectionUI.SelectedIndex == 1)
            {
                BattleOver();//������
            }
        }
    }
    void HandleMoveSelection()
    {
        moveSelectionUI.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
           //�Z�����s����
           actionSelectionUI.Close();
           moveSelectionUI.Close();
           StartCoroutine(RunTurns()); 
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            moveSelectionUI.Close();
            ActionSelection();
        }
    }
}
