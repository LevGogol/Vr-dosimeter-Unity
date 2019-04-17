using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModeButton : Interactive {
    [SerializeField] private GameObject[] arrows;
    private bool isEnable = false;
    [SerializeField] private GameObject box;
    // Start is called before the first frame update
    
    public override void Action() {

        if (isEnable) {
            box.transform.position = new Vector3(-2, 1, 3.6f);
            transform.position = new Vector3(-2, 2, 3.6f);
            box.transform.rotation = Quaternion.Euler(0, 180, 0);
            isEnable = false;
        } else {
            box.transform.position = new Vector3(0, 1.35f, 1.2f);
            transform.position = new Vector3(0.8f, 1.7f, 0.95f);
            isEnable = true;
        }
        
        foreach (var x in arrows) {
            x.SetActive(isEnable);
        }
        
    }
}
