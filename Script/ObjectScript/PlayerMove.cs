using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] TailManager tailManager;

    [Header("Weapons")]
    [SerializeField] GameObject[] weapons;

    public bool[] isTails = new bool[5];

    public float moveSpeed;
    public float hp;

    Transform enemy;
    GameObject bullet;

    float bulletCoolTime;

    public float x;
    public float y;

    public int enemyCount;

    private void Start()
    {
        bulletCoolTime = 0.0f;

        enemyCount = 0;
    }

    void Update()
    {
        bulletCoolTime += Time.deltaTime;

        Movement();

        if(bulletCoolTime > 2.0f)
        {
            MakingBullet();
            bulletCoolTime = 0.0f;
        }
        
        Direction();

    }

    private void Movement()
    {
        Vector3 dir = new Vector3(x, y, 0);
        dir = dir.normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void MakingBullet()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 20.0f, 1 << 7);
        if (monsters.Length > 0)
        {
            enemy = monsters[0].transform;
            float dis = (transform.position - monsters[0].transform.position).sqrMagnitude;

            for (int i = 1; i < monsters.Length; i++)
            {
                float dis2 = (transform.position - monsters[i].transform.position).sqrMagnitude;
                if (dis > dis2)
                {
                    dis = dis2;
                    enemy = monsters[i].transform;
                }
            }

        }
        else
            enemy = null;

        if (enemy != null)
        {
            bullet = Instantiate(bulletPrefab, weapons[0].transform.GetChild(0).transform.position, Quaternion.identity);
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 40.0f);
    }

    private void Direction()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 20.0f, 1 << 7);
        if(monsters.Length > 0 ) 
        {
            Transform enemy = monsters[0].transform;
            float dis = (transform.position - monsters[0].transform.position).sqrMagnitude;

            for (int i = 1; i < monsters.Length; i++)
            {
                float dis2 = (transform.position - monsters[i].transform.position).sqrMagnitude;
                if (dis > dis2)
                {
                    dis = dis2;
                    enemy = monsters[i].transform;
                }
            }

            Vector3 dir = transform.position - enemy.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleToQua = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleToQua, 0.5f);
        }
        
    }

    public void UnderAttack(float damage)
    {
        hp -= damage;
    }
}
