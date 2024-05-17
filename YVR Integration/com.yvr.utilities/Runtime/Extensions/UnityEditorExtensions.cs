using UnityEditor;

#if UNITY_EDITOR
public static class UnityEditorExtensions
{
    public static T GetValue<T>(this SerializedProperty property)
    {
        return property.serializedObject.targetObject.GetNestedFieldOrProperty<T>(property.propertyPath);
    }
}
#endif