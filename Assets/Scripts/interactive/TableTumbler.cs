using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTumbler : Interactive
{
    [SerializeField] private Box box;
    private AudioSource click;
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;

    private bool isOnTumbler = true;

    public void Click()
    {
        bool isOnScoreboard = box.IsEnabledScoreboard();
        box.SetScoreboard(!isOnScoreboard);
        bool isOnTumblerNow = box.IsEnabledScoreboard();

        RotationTumbler();
        ClickSound(isOnTumblerNow);
        isOnTumbler = isOnTumblerNow;
    }

    public void RotationTumbler()
    {
        if (box.IsEnabledScoreboard() != isOnTumbler)
        {
            transform.parent.parent.Rotate(0, 180, 0);
            isOnTumbler = box.IsEnabledScoreboard();
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
