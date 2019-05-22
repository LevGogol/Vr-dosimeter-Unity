using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocation : MonoBehaviour
{


    public Transform Left;

    public Transform Head;

    private float time;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangePos()
    {
        Vector3 pos = transform.position - Left.position;
        Head.position += pos;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.3)
        {
            ChangePos();
            time = -10000000000;
        }
    }
}
