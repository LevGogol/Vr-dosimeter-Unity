using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTumbler : Interactive
{
    [SerializeField] private Box box;
    
    private bool isOn = false;

    public bool IsOn {
        get => isOn;
        set {
            if(isOn == value) return;
            
            isOn = value;
            box.SetScoreboard(isOn);
        }
    }
    
    public override void Action() {
        IsOn = !isOn;
        //    transform.Rotate(new Vector3(0, 180, 0));   не работает пока у моделек нет центра
    }
}
