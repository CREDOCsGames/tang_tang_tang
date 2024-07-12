using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tail : MonoBehaviour
{
    [SerializeField] TailManager tailManager;

    public int myTailType;

    public bool isChaseTail;

    float moveSpeed = 30f;

    public int PosNum;

    SpriteRenderer mySprite;
    Rigidbody2D myRigid;

    public float hp;

    private bool isStun;
    private float stunTime;

    public bool exterTail;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isChaseTail == false && isStun == false && exterTail == false)
        {
            for(int i = 0; i < tailManager.tails.Length; i++) 
            {
                if (tailManager.tails[i] == 0)
                {
                    PlayerMove.Instance.isTails[i] = true;
                    tailManager.tails[i] = this.myTailType;
                    PosNum = i;
                    break;
                }
            }

            isChaseTail = true;
            tailManager.existingTail[tailManager.PresentStageNum - 1] = false;
        }

        if (collision.gameObject.CompareTag("Player") && isChaseTail == false && isStun == false && exterTail == true)
        {
            for (int i = 0; i < tailManager.tails.Length; i++)
            {
                if (tailManager.tails[i] == 0)
                {
                    tailManager.tails[i] = this.myTailType;
                    PosNum = i;
                    break;
                }
            }

            switch (tailManager.PresentStageNum)
            {
                case 1:
                    tailManager.RemainTails1Box.remainTails[PosNum].TailType = 0;
                    tailManager.RemainTails1Box.remainTails[PosNum].TailPos = new Vector3(0f, 0f, 0f);
                    isStun = true;
                    break;
                case 2:
                    tailManager.RemainTails2Box.remainTails[PosNum].TailType = 0;
                    tailManager.RemainTails2Box.remainTails[PosNum].TailPos = new Vector3(0f, 0f, 0f);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                default:
                    break;
            }


            isChaseTail = true;
        }



        if (collision.gameObject.CompareTag("CuttingEnemy") && isChaseTail == true)
        {
            for(int i = PosNum; i < tailManager.tails.Length; i++)
            {
                tailManager.tails[i] = 0;
                PlayerMove.Instance.isTails[i] = false;
            }

            switch (tailManager.PresentStageNum)
            {
                case 1:
                    tailManager.RemainTails1Box.remainTails[PosNum].TailType = this.myTailType;
                    tailManager.RemainTails1Box.remainTails[PosNum].TailPos = this.transform.position;
                    isStun = true;
                    break;
                case 2:
                    tailManager.RemainTails2Box.remainTails[PosNum].TailType = this.myTailType;
                    tailManager.RemainTails2Box.remainTails[PosNum].TailPos = this.transform.position;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                default: 
                    break;
            }

        }

    }

    private void Start()
    {
        isStun = false;
        stunTime = 0.0f;

        mySprite = GetComponent<SpriteRenderer>();
        myRigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(PosNum >= 0)
        {
            if (PlayerMove.Instance.isTails[PosNum] == false)
            {
                this.isChaseTail = false;
                isStun = true;

                switch (tailManager.PresentStageNum)
                {
                    case 1:
                        tailManager.RemainTails1Box.remainTails[PosNum].TailType = this.myTailType;
                        tailManager.RemainTails1Box.remainTails[PosNum].TailPos = this.transform.position;
                        break;
                    case 2:
                        tailManager.RemainTails2Box.remainTails[PosNum].TailType = this.myTailType;
                        tailManager.RemainTails2Box.remainTails[PosNum].TailPos = this.transform.position;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    default:
                        break;
                }
            }
        }
        


        if (isChaseTail)
        {
            this.gameObject.layer = 14;
            transform.position = Vector3.MoveTowards(transform.position, PlayerMove.Instance.transform.GetChild(PosNum).position, Time.deltaTime * moveSpeed);
            transform.rotation = PlayerMove.Instance.transform.rotation;
        }
        else
        {
            this.gameObject.layer = 10;
        }

        if(hp <= 0 && PosNum >= 0)
        {
            isChaseTail = false;
            tailManager.tails[PosNum] = 0;
            Destroy(this.gameObject);
        }

        myRigid.velocity = new Vector2(0f, 0f);

        if(isStun == true)
        {
            stunTime += Time.deltaTime;
        }

        if (stunTime >= 3.0f)
        {
            isStun = false;
            stunTime = 0.0f;
        }

    }

    public void UnderAttack(float damage)
    {
        hp -= damage;
        TailHPBarManager.Instance.UnderAttack(damage, PosNum);
    }
}
