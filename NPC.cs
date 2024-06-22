using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] PlayerMove Player;

    bool crash;
    float moveSpeecd = 17f;

    Transform space;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            crash = true;

            for (int i = 0; i < Player.space.Length; i++)
            {
                if (Player.space[i] == false)
                {
                    this.space = Player.transform.GetChild(i);
                    Player.space[i] = true;
                    break;
                }

            }
        }
            
    }

    private void Start()
    {
        crash = false;
    }
    private void Update()
    {
        if(crash)
        {
            transform.position = Vector3.MoveTowards(transform.position, space.position, Time.deltaTime * moveSpeecd);
            transform.rotation = Player.transform.rotation;
        }
        
    }
}
