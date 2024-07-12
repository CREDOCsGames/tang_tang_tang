using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] Image playerHP;
    void Start()
    {
        
    }

    void Update()
    {
        playerHP.fillAmount = PlayerMove.Instance.hp / 100f;
    }
}
