using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [SerializeField]LayerMask solidObjectsLayer;
    [SerializeField]LayerMask encountLayer;
    [SerializeField] Battler battler;
    //[SerializeField]GameController gameController;
    public UnityAction<Battler> OnEncounts;//Encount�������Ɏ��s�������֐���o�^�ł���

    Animator animator;
    bool isMoving;

    public Battler Battler { get => battler;}

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //�L�[�{�[�h�ňړ�����
    //�ړ����͓��͂��󂯕t���Ȃ�

    private void Start()
    {
        battler.Init();
    }
    void Update()
    {
        if (isMoving == false)//���������Ă��Ȃ����
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (x != 0)
            {
                y = 0;
            }

            if (x != 0 || y != 0)
            {
                animator.SetFloat("InputX", x);
                animator.SetFloat("InputY", y);
                StartCoroutine(Move(new Vector2(x, y)));
            }
        }
        animator.SetBool("IsMoving", isMoving);
    }

    //1�}�X���X�ɋ߂Â���i�R���[�`���j
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;
        if (IsWalkable(targetPos) == false)
        {
            isMoving = false;
            yield break;
        }
        //���݂ƃ^�[�Q�b�g�̏ꏊ���Ⴄ�Ȃ�A�߂Â�������
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //�߂Â���      
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);//�i���ݒn�A�ڕW�l�A���x�j�F�ڕW�l�ɋ߂Â���
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        //�G�ɑ��������ׂ�
        CheckForEncounts();
    }

    void CheckForEncounts()
    {
        //����ƒn�_�ɁA�G�����邩���f����
        Collider2D  encount = Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer);
        if (encount)
        {
            if (Random.Range(0, 100) < 20)//0-99�܂ł̐����������_���ɑI�΂�āA���̐�����50��菬����������
            {
                Battler enemy = encount.GetComponent<EncountArea>().GetRandomBattler();
                //Debug.Log("�G�ɑ���");
                //gameController.StartBattle();
                OnEncounts?.Invoke(enemy);//����OnEncounts�Ɋ֐����o�^����Ă���Ύ��s����
            }
        }

    }

    //����̈ʒu�Ɉړ��ł��邩���肷��֐�
    bool IsWalkable(Vector3 targetPos)
    {
        //targetPos�𒆐S�ɉ~�`��Ray�����FSolidObjectLayer�ɂԂ�������true���Ԃ��Ă���B������false
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == false;
    }
}
