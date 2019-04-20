using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : Interactive
{
    [SerializeField] private Box box;
    
    public void PointEnter() {
        box.PressingOnTestButton();
    }
    
    public void PointExit() {
        box.ReleaseTheTestButton();
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
    }
}
