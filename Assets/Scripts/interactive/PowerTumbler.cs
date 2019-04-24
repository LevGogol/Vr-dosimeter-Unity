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
    
    private bool isOn = false;

    public bool IsOn {
        get => isOn;
        set {
            if(isOn == value) return;
            
            transform.parent.parent.Rotate(new Vector3(0, 180, 0)); 
            isOn = value;
            box.SetPower(isOn);
            if (isOn) {
                click.clip = onSound;
            } else {
                click.clip = offSound;
            }
            click.Play();
        }
    }
    
    public override void Action() {
        IsOn = !isOn;
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
