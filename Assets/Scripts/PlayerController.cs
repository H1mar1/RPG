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
    public UnityAction<Battler> OnEncounts;//Encountした時に実行したい関数を登録できる

    Animator animator;
    bool isMoving;

    public Battler Battler { get => battler;}

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //キーボードで移動する
    //移動中は入力を受け付けない

    private void Start()
    {
        battler.Init();
    }
    void Update()
    {
        if (isMoving == false)//もし動いていなければ
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

    //1マス徐々に近づける（コルーチン）
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;
        if (IsWalkable(targetPos) == false)
        {
            isMoving = false;
            yield break;
        }
        //現在とターゲットの場所が違うなら、近づけ続ける
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //近づける      
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);//（現在地、目標値、速度）：目標値に近づける
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        //敵に遭うか調べる
        CheckForEncounts();
    }

    void CheckForEncounts()
    {
        //特定と地点に、敵がいるか判断する
        Collider2D  encount = Physics2D.OverlapCircle(transform.position, 0.2f, encountLayer);
        if (encount)
        {
            if (Random.Range(0, 100) < 20)//0-99までの数字がランダムに選ばれて、その数字が50より小さかったら
            {
                Battler enemy = encount.GetComponent<EncountArea>().GetRandomBattler();
                //Debug.Log("敵に遭遇");
                //gameController.StartBattle();
                OnEncounts?.Invoke(enemy);//もしOnEncountsに関数が登録されていれば実行する
            }
        }

    }

    //特定の位置に移動できるか判定する関数
    bool IsWalkable(Vector3 targetPos)
    {
        //targetPosを中心に円形のRayを作る：SolidObjectLayerにぶつかったらtrueが返ってくる。だからfalse
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == false;
    }
}
