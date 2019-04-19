using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TasksPool
{
    private static TasksPool instance = null;
    private readonly List<Task> tasks = new List<Task>();
    
    public static TasksPool Instance => instance ?? (instance = new TasksPool());

    public void Add(DateTime time, Task.Payload payload) => tasks.Add(new Task(time, payload));

    public IEnumerator Courutine()
    {
        return Update();
    }

    private IEnumerator Update()
    {
        while (true)
        {
            var curTime = DateTime.Now;
            for (var i = tasks.Count - 1; i >= 0; i--)
            {
                if (tasks[i].TaskTime > curTime) continue;
                tasks[i].TaskPayload();
                tasks.RemoveAt(i);
            }

            yield return new WaitForSeconds(1f);
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