using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager1 : MonoBehaviour
{
    [SerializeField] Tail[] tailPrefabs = new Tail[3];
    [SerializeField] TailManager tailManager;
    [SerializeField] PlayerMove player;
    [SerializeField] EnemyMove[ ] enemyPrefabs = new EnemyMove[3];
    [SerializeField] GameObject existingTail;

    [SerializeField] Stage1RemainTails RemainTails1Box;

    int waveNum;
    
    Tail[] chaseTails = new Tail[5];

    void Awake()
    {
        if (tailManager.existingTail[0] == true)
        {
            Instantiate(existingTail, new Vector3(50f, -15f, 0f), Quaternion.identity);
        }

        waveNum = 1;

        Instantiate(player);
        player.transform.position = new Vector3(40f, -25f, 0f);

        for (int i = 0; i < tailManager.tails.Length; i++)
        {
            if (tailManager.tails[i] != 0)
            {
                for (int j = 0; j < tailPrefabs.Length; j++)
                {
                    if (tailPrefabs[j].myTailType == tailManager.tails[i])
                    {
                        chaseTails[i] = Instantiate(tailPrefabs[j], player.transform.GetChild(i).position, Quaternion.identity);
                        player.isTails[i] = true;
                        chaseTails[i].isChaseTail = true;
                        chaseTails[i].PosNum = i;
                        chaseTails[i].exterTail = true;
                        TailHPBarManager.Instance.hp[i] = chaseTails[i].hp;
                    }
                }

            }
        }

        //Instantiate(enemyPrefabs[0], new Vector3(33f, -30f, 0f), Quaternion.identity);
        //Instantiate(enemyPrefabs[0], new Vector3(40f, -34f, 0f), Quaternion.identity);
        //Instantiate(enemyPrefabs[0], new Vector3(47f, -30f, 0f), Quaternion.identity);
        //Instantiate(enemyPrefabs[0], new Vector3(34f, -18f, 0f), Quaternion.identity);
        //Instantiate(enemyPrefabs[0], new Vector3(44f, -17f, 0f), Quaternion.identity);

        for (int i = 0; i < RemainTails1Box.remainTails.Length; i++)
        {
            if (RemainTails1Box.remainTails[i].TailType != 0)
            {
                Instantiate(tailPrefabs[RemainTails1Box.remainTails[i].TailType - 1], RemainTails1Box.remainTails[i].TailPos, Quaternion.identity);
            }
        }


    }

    void Update()
    {
        for (int i = 0; i < tailManager.tails.Length; i++)
        {
            if (tailManager.tails[i] != 0)
            {
                for (int j = 0; j < tailPrefabs.Length; j++)
                {
                    if (tailPrefabs[j].myTailType == tailManager.tails[i])
                    {
                        chaseTails[i] = tailPrefabs[j];
                        TailHPBarManager.Instance.hp[i] = chaseTails[i].hp;
                    }
                }

            }
        }

        if(player.enemyCount > 5) 
        {
            waveNum++;
        }


    }
}
