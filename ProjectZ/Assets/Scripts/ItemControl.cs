using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public float Speed;
    public float LeftBound;
    public float RightBound;
    public float TopBound;
    public float BottomBound;
    public float GG;

    private void Start()
    {
        transform.position = new Vector3(RightBound, TopBound, 0);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(transform.position.x - RightBound) < GG) transform.position += new Vector3(0, -Speed * Time.deltaTime, 0);
        if (Mathf.Abs(transform.position.x - LeftBound) < GG) transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
        if (Mathf.Abs(transform.position.y - BottomBound) < GG) transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
        if (transform.position.y > GG + TopBound) Destroy(gameObject);
    }
}
