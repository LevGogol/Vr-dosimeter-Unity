using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : Interactive {
    [SerializeField] private GameObject box;
    [SerializeField] private Vector3 direction;
    [SerializeField] [Range(0, 10)]private float speed;
    private bool isContinue;
    
    public void PointEnter() {
        isContinue = true;
        StartCoroutine(Rotate());
    }
    
    public void PointExit() {
        isContinue = false;
    }
    
    public override void Action() {
        
    }

    IEnumerator Rotate() {
        while (isContinue) {
            box.transform.Rotate(direction, speed, Space.World);
            yield return null;
        }
    }
}
