using UnityEditor;
using UnityEngine;

public class BoxModifierAttribute : PropertyModifierAttribute
{
    private int leftOutPadding = 0;
    private int rightOutPadding = 0;
    private int topOuterPadding = 0;
    private int bottomOutPadding = 0;

    public BoxModifierAttribute(int left, int right, int top, int bottom)
    {
        this.leftOutPadding = left;
        this.rightOutPadding = right;
        this.topOuterPadding = top;
        this.bottomOutPadding = bottom;
    }

#if UNITY_EDITOR
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label, float height)
    {
        return height + topOuterPadding + bottomOutPadding;
    }

    public override void BeforeGUI(ref bool visible, ref Rect position, SerializedProperty property, GUIContent label)
    {
        if (!visible) return;

        // Draw box in the whole space, the space width is the origin width, the space height is added by top and bottom
        GUI.Box(position, GUIContent.none);

        RectOffset rectOffset = new RectOffset(leftOutPadding, rightOutPadding, topOuterPadding, bottomOutPadding);
        position = rectOffset.Remove(position); // The whole space minus outer padding is the space for gui content
    }
#endif
}
