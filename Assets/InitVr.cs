using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitVr : MonoBehaviour
{
    private void Start()
    {
        if (!Oculus.Platform.Core.IsInitialized())
        {
            Debug.LogWarning("Not init Oculus");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
