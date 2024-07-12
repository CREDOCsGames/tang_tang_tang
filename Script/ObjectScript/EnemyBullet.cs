using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 destination;
    Transform friend;
    Tail tail;

    public float damage;

    private void Start()
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
            destination = 10 * (friend.position - transform.position);

        }

    }
    void Update()
    {
        Movement();

        if(transform.position == destination)
            Destroy(this.gameObject);

    }

    public void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 30.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            PlayerMove.Instance.UnderAttack(damage);
            Destroy(this.gameObject);
        }
        else if(other != null && other.gameObject.tag == "Tail")
        {
            tail = other.GetComponent<Tail>();
            if(tail.isChaseTail == true)
            {
                tail.UnderAttack(damage);
                Destroy(this.gameObject);
            }
            
        }
    }
}
