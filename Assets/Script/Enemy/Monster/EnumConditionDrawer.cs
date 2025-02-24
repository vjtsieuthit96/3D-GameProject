using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumConditionAttribute))]
public class EnumConditionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EnumConditionAttribute enumCondition = (EnumConditionAttribute)attribute;
        SerializedProperty enumField = property.serializedObject.FindProperty(enumCondition.EnumFieldName);

        if (enumField != null && enumField.enumValueIndex == enumCondition.EnumValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        EnumConditionAttribute enumCondition = (EnumConditionAttribute)attribute;
        SerializedProperty enumField = property.serializedObject.FindProperty(enumCondition.EnumFieldName);

        if (enumField != null && enumField.enumValueIndex == enumCondition.EnumValue)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
