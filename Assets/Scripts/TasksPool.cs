using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksPool : MonoBehaviour {
    
    [SerializeField] private Clocks clocks;
    private static TasksPool instance = null;
    private readonly List<Task> tasks = new List<Task>();

    public static TasksPool Instance => instance;

    public DateTime NowTime => clocks.NowTime;

    public void Add(string tag, DateTime time, Task.Payload payload) => tasks.Add(new Task(tag, time, payload));
    public void Add(DateTime time, Task.Payload payload) => tasks.Add(new Task(time, payload));

    public int Clean(string tag)
    {
        var count = 0;
        for (var i = tasks.Count - 1; i >= 0; i--)
        {
            if (tasks[i].tag.Equals(tag))
            {
                tasks.RemoveAt(i);
                count++;
            }
        }

        return count;
    }

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
            if (i >= tasks.Count || tasks[i].TaskTime > curTime) continue;
            tasks[i].TaskPayload();
            if (i >= tasks.Count) continue;
            tasks.RemoveAt(i);
        }
       
    }

    public class Task
    {
        public delegate void Payload();

        public readonly string tag = null;
        public readonly DateTime TaskTime;
        public readonly Payload TaskPayload;

        public Task(DateTime time, Payload payload)
        {
            TaskTime = time;
            TaskPayload = payload;
        }
        
        public Task(string tag, DateTime time, Payload payload)
        {
            this.tag = tag;
            TaskTime = time;
            TaskPayload = payload;
        }
    }
}