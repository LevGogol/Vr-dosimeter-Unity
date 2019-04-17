using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : Interactive {
    [SerializeField] private GameObject box;
    [SerializeField] private Vector3 direction;
    [SerializeField] [Range(0, 10)]private float speed;

    public void PointEnter() {
        StartCoroutine("Rotate");
    }
    
    public void PointExit() {
        StopCoroutine("Rotate");
    }
    
    public override void Action() {
        
    }

    IEnumerator Rotate() {
        while (true) {
            box.transform.Rotate(direction, speed, Space.World);
            yield return null;
        }
    }
}
