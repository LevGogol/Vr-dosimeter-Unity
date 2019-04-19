using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Radiation : MonoBehaviour {
    private void OnTriggerStay(Collider other) {
        Hose temp = other.gameObject.GetComponent<Hose>();
        if (temp != null) {
            float power = 1/Vector3.Distance(temp.transform.position, transform.position); //TODO деление на ноль. Нормализовать
            temp.OnRadiation(power);
        }
    }


}
