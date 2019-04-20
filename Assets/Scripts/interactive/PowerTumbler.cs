using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PowerTumbler : Interactive {
    [SerializeField] private Box box;
    
    private bool isOn = false;

    public bool IsOn {
        get => isOn;
        set {
            if(isOn == value) return;
            
            isOn = value;
            box.SetPower(isOn);
        }
    }
    
    public override void Action() {
        IsOn = !isOn;
        //    transform.Rotate(new Vector3(0, 180, 0));   не работает пока у моделек нет центра
    }

    private void Start()
    {
        if (box == null)
        {
            box = GetComponentInParent<Box>();
            if (box == null)
            {
                Debug.LogError("Not found box");
            }
        }
    }
}
