using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour {
    private GameObject radiation;
    [SerializeField] private GameObject radiationPrefab;
    
    // Start is called before the first frame update
    void Start() {
        radiation = Instantiate(radiationPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
