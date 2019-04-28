using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BoxSaver : MonoBehaviour
{
    private Box box;

    public static BoxSaver Instance { get; private set; }

    private Dictionary<int, Data> dataMap = new Dictionary<int, Data>();

    private void Start()
    {
        Instance = this;
        
        box = GetComponent<Box>();
        if(box == null) Debug.LogError("Not found Box");
    }

    public void StateSave() => StateSave(dataMap.Count);
    
    public void StateSave(int id)
    {
        var boxFields = GetFieldsMap(box);
        var machineFields = GetFieldsMap(boxFields["machine"]);
        var rangeRotation = GetComponentInChildren<RoundTumbler>().GetRotation();
        dataMap.Add(id, new Data(boxFields, machineFields, rangeRotation));
    }

    public void StateLoad() => StateLoad(dataMap.Count - 1);
    
    public void StateLoad(int id)
    {
        if(id < 0) return;
        
        var data = dataMap[id];
        if (data.boxFields == null)
        {
            return;
        }

        SetFields(box, data.boxFields);
        SetFields(data.boxFields["machine"], data.machineFields);
        
        GetComponentInChildren<TableTumbler>().RotationTumbler();
        GetComponentInChildren<PowerTumbler>().RotationTumbler();
        GetComponentInChildren<RoundTumbler>().SetRotation(data.rangeRotation);
        UpdateTablo();
    }

    private void UpdateTablo()
    {
        var tablo = GetComponentInChildren<Tablo>();
        if (box.IsEnabledScoreboard() && box.IsPower())
        {
            tablo.Enable();
        }
        else
        {
            tablo.Disable();
        }

        if (box.IsPower())
        {
            tablo.EnablePowerLamp();
        }
        else
        {
            tablo.DisablePowerLamp();
        }
    }

    private static void SetFields(object obj, Dictionary<string,object> fields)
    {
        var objType = obj.GetType();
        foreach (var field in fields)
        {
            var fld = objType.GetField(field.Key, BindingFlags.Instance | BindingFlags.NonPublic);
            if (fld != null) fld.SetValue(obj, field.Value);
        }
    }

    private static Dictionary<string, object> GetFieldsMap(object obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException();
        }
        
        return GetFields(obj).ToDictionary(field => field.Name, field => field.GetValue(obj));
    }

    private static IEnumerable<FieldInfo> GetFields(object obj) => obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

    private class Data
    {
        public readonly Dictionary<string, object> boxFields;
        public readonly Dictionary<string, object> machineFields;
        public readonly Quaternion rangeRotation;

        public Data(Dictionary<string, object> boxFields, Dictionary<string, object> machineFields, Quaternion rangeRotation)
        {
            this.boxFields = boxFields;
            this.machineFields = machineFields;
            this.rangeRotation = rangeRotation;
        }
    }
}