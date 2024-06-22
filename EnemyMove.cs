using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] PlayerMove player;

    float moveSpeed = 5.0f;
    Vector2 dir;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        dir = player.transform.position - transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleToQua = Quaternion.AngleAxis((angle - 90), Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleToQua, 0.5f);

        transform.position = (Vector2)transform.position + (Vector2)dir * moveSpeed * Time.deltaTime;
    }
}
