using UnityEngine;

public class EnumConditionAttribute : PropertyAttribute
{
    public string EnumFieldName { get; private set; }
    public int EnumValue { get; private set; }

    public EnumConditionAttribute(string enumFieldName, int enumValue)
    {
        EnumFieldName = enumFieldName;
        EnumValue = enumValue;
    }
}
