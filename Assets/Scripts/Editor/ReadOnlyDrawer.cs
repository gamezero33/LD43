using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        string valueStr;

        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueStr = prop.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                valueStr = prop.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                valueStr = prop.floatValue.ToString((attribute as ReadOnlyAttribute).precision);
                break;
            case SerializedPropertyType.String:
                valueStr = prop.stringValue;
                break;
            case SerializedPropertyType.Vector2:
                valueStr = prop.vector2Value.ToString((attribute as ReadOnlyAttribute).precision);
                break;
            case SerializedPropertyType.Vector3:
                valueStr = prop.vector3Value.ToString((attribute as ReadOnlyAttribute).precision);
                break;
			case SerializedPropertyType.Enum:
				valueStr = prop.enumDisplayNames[prop.enumValueIndex];
				break;
			default:
                valueStr = "(not supported)";
                break;
        }

        EditorGUI.LabelField(position, label.text, valueStr, new GUIStyle( ( attribute as ReadOnlyAttribute ).style ) );

		//base.OnGUI( position, prop, label );
    }
}