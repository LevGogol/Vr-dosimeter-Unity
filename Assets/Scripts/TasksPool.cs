using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TasksPool : MonoBehaviour {
    
    [SerializeField] private Clocks clocks;
    private static TasksPool instance = null;
    private readonly List<Task> tasks = new List<Task>();

    public static TasksPool Instance => instance;

    public DateTime NowTime => clocks.NowTime;

    public void Add(DateTime time, Task.Payload payload) => tasks.Add(new Task(time, payload));

    private void Start() {
        instance = this;
        if (clocks == null) {
            Debug.LogError("hi");
        }
    }

    private void Update() {
        var curTime = clocks.NowTime;
        for (var i = tasks.Count - 1; i >= 0; i--)
        {
            if (tasks[i].TaskTime > curTime) continue;
            tasks[i].TaskPayload();
            tasks.RemoveAt(i);
        }
       
    }

    public class Task
    {
        public delegate void Payload();
        
        public readonly DateTime TaskTime;
        public readonly Payload TaskPayload;

        public Task(DateTime time, Payload payload)
        {
            TaskTime = time;
            TaskPayload = payload;
        }
    }
}