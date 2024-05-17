using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ModifiablePropertyAttribute), true)]
public class ModifiablePropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ModifiablePropertyAttribute modifiedAttribute = (ModifiablePropertyAttribute)attribute;
        if (modifiedAttribute.modifierAttributesList == null)
            modifiedAttribute.modifierAttributesList = fieldInfo.GetCustomAttributes(typeof(PropertyModifierAttribute), false) // Do not get inherit attributes
                                                        .Cast<PropertyModifierAttribute>().OrderBy(attribute => attribute.order).ToList();

        float propertyHeight = modifiedAttribute.GetPropertyHeight(property, label); // Get the height of the property attribute
        modifiedAttribute.modifierAttributesList.ForEach(attribute => propertyHeight = attribute.GetPropertyHeight(property, label, propertyHeight));

        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ModifiablePropertyAttribute modifiedAttribute = (ModifiablePropertyAttribute)attribute;

        bool shouldDisplay = true;

        foreach (PropertyModifierAttribute attribute in modifiedAttribute.modifierAttributesList.AsEnumerable().Reverse())
            attribute.BeforeGUI(ref shouldDisplay, ref position, property, label);

        if (shouldDisplay)
            modifiedAttribute.OnGUI(position, property, label);

        foreach (PropertyModifierAttribute attribute in modifiedAttribute.modifierAttributesList)
            attribute.AfterGUI(position, property, label);
    }
}