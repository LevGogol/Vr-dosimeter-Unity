using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Radiation : MonoBehaviour
{
    public float power = 0.000051f; 
    
    private void OnTriggerStay(Collider other) {
        Hose temp = other.gameObject.GetComponent<Hose>();
        if (temp == null) return;
        
        float distance = Vector3.Distance(temp.transform.position, transform.position);
        float attenuationCoefficient = 1f / (distance <= 0 ? 1f : distance);
        temp.OnRadiation(power * attenuationCoefficient);
    }

    private void OnTriggerExit(Collider other) {
        Hose temp = other.gameObject.GetComponent<Hose>();
        if (temp == null) return;
        temp.OnRadiation(0);
    }
}
