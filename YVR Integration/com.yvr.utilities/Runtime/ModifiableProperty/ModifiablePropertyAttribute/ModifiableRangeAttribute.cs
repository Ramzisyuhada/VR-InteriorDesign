using UnityEditor;
using UnityEngine;

public class ModifiableRangeAttribute : ModifiablePropertyAttribute
{
    private float min = 0.0f;
    private float max = 0.0f;

    public ModifiableRangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

#if UNITY_EDITOR
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Same with the Unity built-in RangeAttribute drawer
        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, min, max, label);
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, (int)min, (int)max, label);
        else
            EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
    }
#endif
}
