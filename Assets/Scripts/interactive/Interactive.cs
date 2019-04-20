using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour{
    public void PointEnter() {
        Invoke("Action", 1.5f);
    }

    public void PointExit() {
        CancelInvoke("Action");
    }

    public abstract void Action();

}
