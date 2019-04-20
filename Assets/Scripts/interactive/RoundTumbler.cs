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
