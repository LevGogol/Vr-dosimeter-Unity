using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO переписать, возможно наследовать кнопки надо, или отдельный компонент
public class CheckButton : Interactive
{
    [SerializeField] private Box box;
    private AudioSource click;
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;
    
    private bool isOn = false;

    public bool IsOn {
        get => isOn;
        set {
            if(isOn == value) return;
            
            isOn = value;
            
            if (isOn) {
                click.clip = onSound;
            } else {
                click.clip = offSound;
            }
            click.Play();
        }
    }
    
    public void PointEnter() {
        box.PressingOnTestButton();
        IsOn = true;
    }
    
    public void PointExit() {
        box.ReleaseTheTestButton();
        IsOn = false;
    }
    
    public override void Action() {
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
