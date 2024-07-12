using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] TailManager tailManager;

    Rigidbody2D myRigid;

    public float moveSpeed;

    Vector3 dir;

    [Header("Role")]
    public bool MiddleAttack;

    [Header("Position")]
    public bool LongDi;
    public bool ShortDi;

    bool ChaseMiddle = false;

    int lastTail;

    float curveConstant = 3.0f;

    float bulletCoolTime;

    Tail tailHP;

    Transform friend;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && (MiddleAttack == true))
        {
            curveConstant = 3.0f;
        }

    }

    private void Start()
    {
        bulletCoolTime = 0.0f;
        lastTail = -1;


        myRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bulletCoolTime += Time.deltaTime;

        CheckLastTail();
            
        if (ChaseMiddle && MiddleAttack)
        {
            moveSpeed = 13f;
            MiddleAttacking();
        }
        else if (LongDi)
        {
            LongDiAttacking(bulletPrefab);
        }

        else
        {
            GeneralAttacking();
        }

    }

    void GeneralAttacking()
    {
        Collider2D[] friends = Physics2D.OverlapCircleAll(transform.position, 20.0f, 1 << 14);
        if (friends.Length > 0)
        {
            friend = friends[0].transform;
            float dis = (transform.position - friends[0].transform.position).sqrMagnitude;

            for (int i = 1; i < friends.Length; i++)
            {
                float dis2 = (transform.position - friends[i].transform.position).sqrMagnitude;
                if (dis > dis2)
                {
                    dis = dis2;
                    friend = friends[i].transform;
                }
            }

        }
        else
            friend = null;


        if (friend != null)
        {
            Movement(friend.position);

        }
        else
        {
            Movement(PlayerMove.Instance.transform.position);
        }

        
    }

    void MiddleAttacking()
    {
        switch (lastTail)
        {
            case 0:
                CurveMovement(PlayerMove.Instance.transform.GetChild(0).position);
                break;
            case 1:
                CurveMovement(PlayerMove.Instance.transform.GetChild(1).position);
                break;
            case 2:
                CurveMovement(PlayerMove.Instance.transform.GetChild(1).position);
                break;
            case 3:
                CurveMovement(PlayerMove.Instance.transform.GetChild(2).position);
                break;
            case 4:
                CurveMovement(PlayerMove.Instance.transform.GetChild(2).position);
                break;
            default:
                break;

        }
    }


    void LongDiAttacking(GameObject bulletPrefab)
    {
        Collider2D[] friends = Physics2D.OverlapCircleAll(transform.position, 20.0f, 1 << 14);
        if (friends.Length > 0)
        {
            friend = friends[0].transform;
            float dis = (transform.position - friends[0].transform.position).sqrMagnitude;

            for (int i = 1; i < friends.Length; i++)
            {
                float dis2 = (transform.position - friends[i].transform.position).sqrMagnitude;
                if (dis > dis2)
                {
                    dis = dis2;
                    friend = friends[i].transform;
                }
            }

        }
        else
            friend = null;

        if (friend != null)
        {
            Movement(friend.position);

        }
        else
        {
            Movement(PlayerMove.Instance.transform.position);
        }

        if (bulletCoolTime > 1.0f && friend != null && bulletPrefab != null)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            bulletCoolTime = 0.0f;
        }
    }


    void CheckLastTail()
    {
        for(int i = 0; i < tailManager.tails.Length; i++) 
        {
            if (tailManager.tails[i] == 0)
            {
                if (i == 0)
                {
                    lastTail = i -1;
                    ChaseMiddle = false;
                    break;
                }
                else
                {
                    lastTail = i - 1;
                    ChaseMiddle = true;
                    break;
                }
                
            }

            if (i == 4 && tailManager.tails[i] != 0)
                lastTail = 4;
        }
    }

    void Movement(Vector3 destination)
    {
        dir = destination - transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleToQua = Quaternion.AngleAxis((angle - 90), Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleToQua, 0.5f);

        myRigid.velocity = new Vector2(dir.x * moveSpeed, dir.y * moveSpeed);
    }

    void CurveMovement(Vector3 destination) 
    {
        dir = destination - transform.position;
        dir += curveConstant * (new Vector3(dir.y, -dir.x, 0f));
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleToQua = Quaternion.AngleAxis((angle - 90), Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleToQua, 0.5f);

        myRigid.velocity = new Vector2 (dir.x * moveSpeed, dir.y * moveSpeed);

        curveConstant -= Time.deltaTime;

        if (curveConstant < 0f)
            curveConstant = 0f;


    }
}
