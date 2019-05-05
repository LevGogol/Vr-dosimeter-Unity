using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PowerTumbler : Interactive {
    [SerializeField] private Box box;
    private AudioSource click;
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;
    
    private bool isOnTumbler = false;

    public void Click()
    {
        bool isOnPower = box.IsPower();
        box.SetPower(!isOnPower);
        bool isOnTumblerNow = box.IsPower();

        RotationTumbler();
        ClickSound(isOnTumblerNow);
        isOnTumbler = isOnTumblerNow;
    }

    public void RotationTumbler()
    {
        if (box.IsPower() != isOnTumbler)
        {
            transform.parent.parent.Rotate(0, 180, 0);
            isOnTumbler = box.IsPower();
        }
    }

    private void ClickSound(bool isOn)
    {
        click.clip = isOn ? onSound : offSound;
        click.Play();
    }
    
    public override void Action() {
        Click();
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

        click = GetComponent<AudioSource>();
    }
}
