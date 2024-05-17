using System;
using UnityEngine;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public abstract class PropertyModifierAttribute : Attribute
{
    public int order { get; set; } // The modifier attributes are retrived by Refelection, thus the origin order can not be guaranteed

#if UNITY_EDITOR
    public virtual float GetPropertyHeight(SerializedProperty property, GUIContent label, float height) { return height; }

    public virtual void BeforeGUI(ref bool visible, ref Rect position, SerializedProperty property, GUIContent label) { }

    public virtual void AfterGUI(Rect position, SerializedProperty property, GUIContent label) { }
#endif

}