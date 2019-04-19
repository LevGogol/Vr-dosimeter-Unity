using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clocks : Interactive {
    private DateTime nowTime;
    private Text time;
    private float second;
    private float speed;
    private bool isContinue;

    public DateTime NowTime {
        get { return nowTime;}
        set {
            nowTime = value; 
            time.text = String.Format("{0:T}", nowTime);
        }
    }

    void Start() {
        time = GetComponent<Text>();
        NowTime = DateTime.Now;
    }

    void Update() {
        second += Time.deltaTime;
        if (second > 1) {
            NowTime = NowTime.AddSeconds(1);
            //time.text = nowTime.Hour + ":" + nowTime.Minute + ":" + nowTime.Second;
            
            second -= 1;
        }
        
    }

    public override void PointEnter() {
        isContinue = true;
        speed = 0;
        StartCoroutine(Acceleration());
    }
    
    public override void PointExit() {
        isContinue = false;
        StopCoroutine(Acceleration());
    }

    public override void Action() {
        throw new NotImplementedException();
    }

    private IEnumerator Acceleration() {
        while (isContinue) {
            speed += speed < 100 ? 0.01f : 0;
            NowTime = NowTime.AddSeconds(speed);
            yield return null;
        }
    }
}
