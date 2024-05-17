using UnityEditor;
using UnityEngine;

public enum ConditionalDisplayComparisonType
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterOrEqual,
    SmallThen,
    SmallOrEqual,
}

public enum ConditionalDisplayDisablingType
{
    ReadOnly,
    DoNotDraw
}

public class ConditionalDisplayModifierAttribute : PropertyModifierAttribute
{

    private string targetPropertyName = null;
    private object targetValue = null;
    private ConditionalDisplayComparisonType comparisonType;
    private ConditionalDisplayDisablingType disablingType;
#if UNITY_EDITOR
    private bool conditionMet = false;
#endif



    public ConditionalDisplayModifierAttribute(string targetPropertyName, object targetValue,
        ConditionalDisplayComparisonType comparisonType = ConditionalDisplayComparisonType.Equal, ConditionalDisplayDisablingType disablingType = ConditionalDisplayDisablingType.DoNotDraw)
    {
        this.targetPropertyName = targetPropertyName;
        this.targetValue = targetValue;
        this.comparisonType = comparisonType;
        this.disablingType = disablingType;
    }

#if UNITY_EDITOR
    private SerializedProperty targetProperty = null;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label, float height)
    {
        return conditionMet ? height : 0;
    }

    public override void BeforeGUI(ref bool visible, ref Rect position, SerializedProperty property, GUIContent label)
    {
        targetProperty = property.serializedObject.FindProperty(targetPropertyName);

        object targetPropertyValue = targetProperty.GetValue<object>();

        NumericType numericTargetPropertyValue = null;
        NumericType numericTargetValue = null;

        try
        {
            numericTargetPropertyValue = new NumericType(targetPropertyValue);
            numericTargetValue = new NumericType(targetValue);
        }
        catch (NumericTypeExpectedException)
        {
            if (comparisonType != ConditionalDisplayComparisonType.Equal && comparisonType != ConditionalDisplayComparisonType.NotEqual)
            {
                Debug.LogError("The only comparsion types available to type '" + targetPropertyValue.GetType() + "' are Equals and NotEqual.");
                return;
            }
        }

        conditionMet = false;

        switch (comparisonType)
        {
            case ConditionalDisplayComparisonType.Equal:
                conditionMet = targetPropertyValue.Equals(targetValue);
                break;
            case ConditionalDisplayComparisonType.NotEqual:
                conditionMet = !targetPropertyValue.Equals(targetValue);
                break;
            case ConditionalDisplayComparisonType.GreaterThan:
                conditionMet = numericTargetPropertyValue > numericTargetValue;
                break;
            case ConditionalDisplayComparisonType.GreaterOrEqual:
                conditionMet = numericTargetPropertyValue >= numericTargetValue;
                break;
            case ConditionalDisplayComparisonType.SmallThen:
                conditionMet = numericTargetPropertyValue < numericTargetValue;
                break;
            case ConditionalDisplayComparisonType.SmallOrEqual:
                conditionMet = numericTargetPropertyValue <= numericTargetValue;
                break;
        }

        visible = conditionMet;
    }
#endif
}
