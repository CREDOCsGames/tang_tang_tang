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
    [SerializeField] Transform target;
    [SerializeField] BoxCollider2D background;

    Size camSize;
    Size backSize;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        float ratio = Screen.width / (float)Screen.height;
        camSize = new Size(cam.orthographicSize * ratio, cam.orthographicSize);
        float sizeX = background.size.x * background.transform.lossyScale.x;
        float sizeY = background.size.y * background.transform.lossyScale.y;
        backSize = new Size(sizeX / 2f, sizeY / 2f);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float limitX = backSize.width - camSize.width;
        float limitY = backSize.height - camSize.height;

        float pivotX = background.transform.position.x;
        float pivotY = background.transform.position.y;

        Vector3 pos = target.position + new Vector3(0, 0, -10);
        pos.x = Mathf.Clamp(pos.x, pivotX - limitX, pivotX + limitX);
        pos.y = Mathf.Clamp(pos.y, pivotY - limitY, pivotY + limitY);

        transform.position = pos;
    }
}
