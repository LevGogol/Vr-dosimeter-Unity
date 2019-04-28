using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BoxSaver : MonoBehaviour
{
    private Box box;

    public static BoxSaver Instance { get; private set; }

    private Dictionary<string, object> boxFields;
    private Dictionary<string, object> machineFields;
    private Quaternion rangeRotation;

    private void Start()
    {
        Instance = this;
        
        box = GetComponent<Box>();
        if(box == null) Debug.LogError("Not found Box");
    }

    public void StateSave()
    {
        boxFields = GetFieldsMap(box);
        machineFields = GetFieldsMap(boxFields["machine"]);
        rangeRotation = GetComponentInChildren<RoundTumbler>().GetRotation();
    }

    public void StateLoad()
    {
        if (boxFields == null)
        {
            return;
        }

        SetFields(box, boxFields);
        SetFields(boxFields["machine"], machineFields);
        
        GetComponentInChildren<TableTumbler>().RotationTumbler();
        GetComponentInChildren<PowerTumbler>().RotationTumbler();
        GetComponentInChildren<RoundTumbler>().SetRotation(rangeRotation);
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
}