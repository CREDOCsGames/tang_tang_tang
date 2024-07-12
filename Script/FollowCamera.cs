using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Size
{
    public float width;
    public float height;

    public Size(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

}

public class FollowCamera : MonoBehaviour
{
    Transform target;
    [SerializeField] BoxCollider2D background;

    Size camSize;
    Size backSize;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float ratio = Screen.width / (float)Screen.height;
        camSize = new Size(cam.orthographicSize * ratio, cam.orthographicSize);
        float sizeX = background.size.x;
        float sizeY = background.size.y;
        backSize = new Size(sizeX, sizeY);
    }

    void Update()
    {
        if(target == null)
        {
            target = PlayerMove.Instance.transform ?? null;
            return;
        }
             
        Movement();
    }

    void Movement()
    {
        float limitX = backSize.width - 2*camSize.width;
        float limitY = backSize.height - 2*camSize.height;

        float pivotX = background.transform.position.x;
        float pivotY = background.transform.position.y;

        Vector3 pos = target.position + new Vector3(0, 0, -10);
        pos.x = Mathf.Clamp(pos.x, 0f, pivotX + limitX);
        pos.y = Mathf.Clamp(pos.y, pivotY - limitY, 0f);

        transform.position = pos;
    }
}
