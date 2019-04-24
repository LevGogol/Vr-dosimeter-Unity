using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTumbler : Interactive
{
    [SerializeField] private Box box;
    private AudioSource click;


    public override void Action() {
        Box.Range curRange = box.GetRange();
        box.SetRange(box.NextRange());
        transform.parent.parent.Rotate(new Vector3(0, curRange > box.GetRange() ? -90 : 18, 0));
        click.Play();
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
