using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : Interactive {
    private bool isTaked;
    public Camera camera;
    [Range(0, 1)]
    public float offset = 0.3f;

    public override void Action() {
        isTaked = true;
        StartCoroutine("Fly");
    }
    
    IEnumerator Fly() {
        while (isTaked) {
            
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(new Vector3(x: camera.pixelWidth / 2, y: camera.pixelHeight / 2 , z: 0));
            if (Physics.Raycast(ray, out hit)) {
                transform.position = hit.point + Vector3.right * offset;
                Debug.DrawLine(ray.origin, hit.point,Color.red);
            }
            
            yield return null;
        }
    }
    
    public void OnRadiation() {
        Debug.Log("hi");
    }

}
