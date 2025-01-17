﻿using UnityEditor;
using UnityEngine;
using Assets._InspectorExtensions;
using System;

public class ReadOnlyAttribute : PropertyAttribute
{
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        string valueStr = "";

        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueStr = prop.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                valueStr = prop.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                valueStr = prop.floatValue.ToString("0.00000");
                break;
            case SerializedPropertyType.String:
                valueStr = prop.stringValue;
                break;
            //case SerializedPropertyType.ObjectReference:
            //    var target = prop.serializedObject.targetObject;
            //    var obj = target.GetType().GetProperty(prop.propertyPath);
            //    //valueStr = obj.woodcuttingPanel.name;
            //    //valueStr = obj.name;
            //    break;
            default:
                valueStr = "(not supported)";
                break;
        }

        EditorGUI.LabelField(position, label.text, valueStr);
    }
}