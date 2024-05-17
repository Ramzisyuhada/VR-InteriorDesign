using UnityEditor;
using UnityEngine;

public class ColorModifierAttribute : PropertyModifierAttribute
{
    private Color targetColor = default;
#if UNITY_EDITOR
    private Color backupGUIColor = default;
#endif

    public ColorModifierAttribute(float r, float g, float b, float a) { targetColor = new Color(r, g, b, a); }

    public ColorModifierAttribute(Color color) { targetColor = color; }

#if UNITY_EDITOR
    public override void BeforeGUI(ref bool visible, ref Rect position, SerializedProperty property, GUIContent label)
    {
        backupGUIColor = GUI.color;

        if (!visible) return;

        GUI.color = targetColor;
    }

    public override void AfterGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.color = backupGUIColor;
    }
#endif
}