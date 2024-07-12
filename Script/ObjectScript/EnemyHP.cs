using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float HP;

    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
            PlayerMove.Instance.enemyCount++;
        }
            
    }

    public void UnderAttack(float damage)
    {
        HP -= damage;
    }
}
