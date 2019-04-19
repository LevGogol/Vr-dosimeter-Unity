using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablo : MonoBehaviour {
    [SerializeField] private GameObject numbers;
    [SerializeField] private GameObject porog;
    [SerializeField] private GameObject Power;
    
    public void Enable() {
        numbers.SetActive(true);
    }

    public void Disable() {
        numbers.SetActive(false);
    }

    private void Start() {

    }
}
