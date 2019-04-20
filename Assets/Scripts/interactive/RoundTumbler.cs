using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTumbler : Interactive
{
    [SerializeField] private Box box;
    

    public override void Action() {
        box.SetRange(box.NextRange());
    }
}
