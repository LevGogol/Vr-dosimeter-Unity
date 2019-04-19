using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clocks : MonoBehaviour {
    private DateTime nowTime = DateTime.Now;
    [SerializeField] private Text time;


    void Start() {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        nowTime.AddSeconds(Time.deltaTime);
        time.text = nowTime.Hour + ":" + nowTime.Minute + ":" + nowTime.Second;
    }
}
