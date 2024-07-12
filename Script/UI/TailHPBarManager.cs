using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TailHPBarManager : Singleton<TailHPBarManager>
{
    [Header("HPBar")]
    [SerializeField] Image[] HPBarUI = new Image[5];
    [SerializeField] Image[] CharUI = new Image[5];
    [SerializeField] TailManager tailManager;
    public float[] hp = new float[5];

    private void Start()
    {
        for (int i = 0; i < tailManager.tails.Length; i++)
        {
            if (tailManager.tails[i] != 0)
            {
                HPBarUI[i].gameObject.SetActive(true);
                switch (tailManager.tails[i])
                {
                    case 1:
                        CharUI[i].sprite = Resources.Load<Sprite>("Dealer");
                        break;
                    case 2:
                        CharUI[i].sprite = Resources.Load<Sprite>("Healer");
                        break;
                    case 3:
                        CharUI[i].sprite = Resources.Load<Sprite>("Tanker");
                        break;
                    default:
                        break;

                }
            }
            else
            {
                HPBarUI[i].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < tailManager.tails.Length; i++)
        {
            if (tailManager.tails[i] != 0)
            {
                HPBarUI[i].gameObject.SetActive(true);
                HPBarUI[i].fillAmount = hp[i] / 100f;
                switch (tailManager.tails[i])
                {
                    case 1:
                        CharUI[i].sprite = Resources.Load<Sprite>("Dealer");
                        break;
                    case 2:
                        CharUI[i].sprite = Resources.Load<Sprite>("Healer");
                        break;
                    case 3:
                        CharUI[i].sprite = Resources.Load<Sprite>("Tanker");
                        break;
                    default:
                        break;

                }
            }
            else
            {
                HPBarUI[i].gameObject.SetActive(false);
            }
        }
    }

    public void UnderAttack(float damage, int num)
    {
        hp[num] -= damage;
    }
}
