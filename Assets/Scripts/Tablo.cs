using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablo : MonoBehaviour {
    [SerializeField] private GameObject numbers;
    [SerializeField] private GameObject powerLamp;
    [SerializeField] private GameObject rangeLamp;
    
    public void Enable() {
        numbers.SetActive(true);
    }

    public void Disable() {
        numbers.SetActive(false);
    }

    public void SetScore(float score)
    {
        if (numbers.activeSelf)
        {
            numbers.GetComponent<Numbers>().SetNumber((int) score);
        }
    }

    public void EnablePowerLamp()
    {
        powerLamp.SetActive(true);
    }

    public void DisablePowerLamp()
    {
        powerLamp.SetActive(false);
    }

    public void EnableRangeLamp()
    {
        rangeLamp.SetActive(true);
    }

    public void DisableRangeLamp()
    {
        rangeLamp.SetActive(false);
    }

    private void Start() {

    }
}
