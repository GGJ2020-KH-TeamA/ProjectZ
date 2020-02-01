using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveryorMover : MonoBehaviour
{
    public PathController path;
    public GameObject slidePrefab;
    public float space = 0.5f;
    public float speed = 0.25f;

    private float offset = 0f;
    private List<GameObject> slideGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int slideCount = (int)(path.TotalDistance / space);
        float currentPosition = 0;
        for (int i = 0; i < slideCount; i++)
        {
            GameObject go = GameObject.Instantiate(slidePrefab);
            path.SetToPosition(go.transform, currentPosition, true);
            currentPosition += space;

            slideGameObjects.Add(go);
            go.transform.parent = this.transform;
        }
        slidePrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        offset += speed * Time.deltaTime;
        while (offset >= space)
        {
            offset -= space;
        }

        
        for (int i = 0; i < slideGameObjects.Count; i++)
        {
            path.SetToPosition(slideGameObjects[i].transform, space * i + offset, true);
        }
    }
}
