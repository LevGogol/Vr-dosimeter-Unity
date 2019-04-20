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

    public void SetScore(float score)
    {
        if (numbers.activeSelf)
        {
            numbers.GetComponent<Numbers>().SetNumber((int) score);
        }
    }

    public void EnablePowerLamp()
    {
        //TODO: включение лампочки сети
    }

    public void DisablePowerLamp()
    {
        //TODO: выключение лампочки сети
    }

    public void EnableRangeLamp()
    {
        //TODO: включение лампочки порога
    }

    public void DisableRangeLamp()
    {
        //TODO: выключение лампочки порога
    }

    private void Start() {

    }
}
