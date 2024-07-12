using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager2 : MonoBehaviour
{
    [SerializeField] Tail[] tailPrefabs = new Tail[3];
    [SerializeField] TailManager tailManager;
    [SerializeField] PlayerMove player;

    [SerializeField] Stage2RemainTails RemainTails2Box;

    Tail[] chaseTails = new Tail[5];
    Tail[] remainTails = new Tail[5];

    void Awake()
    {
        Instantiate(player);
        player.transform.position = new Vector3(0f, 0f, 0f);

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

        for(int k = 0; k < RemainTails2Box.remainTails.Length; k++)
        {
            if (RemainTails2Box.remainTails[k].TailType != 0)
            {
                remainTails[k] = Instantiate(tailPrefabs[RemainTails2Box.remainTails[k].TailType - 1], RemainTails2Box.remainTails[k].TailPos, Quaternion.identity);
                remainTails[k].exterTail = true;

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
    }
}
