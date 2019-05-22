﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RoundTumbler : Interactive
{
    [SerializeField] private Box box;
    private AudioSource click;

    bool isTouch = false;
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<VRTK_InteractNearTouchCollider>() != null)
        {
            isTouch = true;
        }
        
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<VRTK_InteractNearTouchCollider>() != null)
        {
            isTouch = false;
        }
        
    }


    public override void Action() {
        if (isTouch)
        {
            Box.Range curRange = box.GetRange();
            box.SetRange(box.NextRange());
            transform.parent.parent.Rotate(new Vector3(0, curRange > box.GetRange() ? -120 : 30, 0));
            click.Play();
        }
    }

    public Quaternion GetRotation() => transform.parent.parent.rotation;
    

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

    public void SetRotation(Quaternion rangeRotation)
    {
        //TODO: баг с поворотом, сделать нормально.ы
        var parent = transform.parent.parent;
        parent.rotation = rangeRotation;
        /*Quaternion r = parent.rotation;
        parent.rotation = new Quaternion(r.x, rangeRotation.y, r.z, r.w);*/
    }
}
