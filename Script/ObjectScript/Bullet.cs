using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform enemy;

    EnemyHP enemyHP;
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 40.0f, 1 << 7);
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

            Vector3 dir = transform.position - enemy.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleToQua = Quaternion.AngleAxis((angle - 90), Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleToQua, 0.5f);
        }
        else
            return;

        transform.position = Vector3.MoveTowards(transform.position, enemy.position, Time.deltaTime * 20.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "CuttingEnemy"))
        {
            enemyHP = other.gameObject.GetComponent<EnemyHP>();
            enemyHP.UnderAttack(10f);
            Destroy(this.gameObject);
        }
    }
}
