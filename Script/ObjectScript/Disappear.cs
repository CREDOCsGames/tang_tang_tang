using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public bool isCrash;
    float alpha = 1f;
    SpriteRenderer mySprite;
    Tail tail;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tail")
            tail = collision.gameObject.GetComponent<Tail>();

    }

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        isCrash = false;
    }

    void Update()
    {
        if(tail != null && tail.PosNum >= 0)
            isCrash = true;

        mySprite.color = new Color(1, 1, 1, alpha);

        if (isCrash)
        {
            alpha -= Time.deltaTime;
        }

        if (alpha <= 0)
            Destroy(this.gameObject);
    }
}
