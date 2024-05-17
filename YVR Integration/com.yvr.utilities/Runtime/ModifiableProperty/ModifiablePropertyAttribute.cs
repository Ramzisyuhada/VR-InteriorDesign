using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ModifiablePropertyAttribute : PropertyAttribute
{
    public List<PropertyModifierAttribute> modifierAttributesList = null;

#if UNITY_EDITOR
    public virtual void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);
    }

    public virtual float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
#endif
}
