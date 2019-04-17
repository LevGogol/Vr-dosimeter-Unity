using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PowerTumbler : Interactive {
    [SerializeField] private Tablo tablo;
    
    private bool isOn = false;

    public bool IsOn {
        get { return isOn;}
        set {
            isOn = value;
            if (isOn) {
                tablo.Enable();
            } else {
                tablo.Disable();
            }
        }
    }
    
    public override void Action() {
        IsOn = !isOn;
        //    transform.Rotate(new Vector3(0, 180, 0));   не работает пока у моделек нет центра
    }
}
