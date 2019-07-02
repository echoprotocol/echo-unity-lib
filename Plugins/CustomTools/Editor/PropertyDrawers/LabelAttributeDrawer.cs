using CustomTools.Attributes;
using CustomTools.Extensions.Core;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

	[CustomPropertyDrawer( typeof( LabelAttribute ) )]
    public class LabelAttributeDrawer : PropertyDrawer {

        float lastTime;
        string value = string.Empty;


		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            var updateInterval = (( LabelAttribute )attribute).UpdateInterval;
            if ( (Time.realtimeSinceStartup - lastTime) > updateInterval) {
                lastTime = Time.realtimeSinceStartup;
                value = string.Format("Type {0} doesn't support implementation as label", property.type);
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        value = property.boolValue.ToString();
                        break;
                    case SerializedPropertyType.Integer:
                        value = property.intValue.ToString();
                        break;
                    case SerializedPropertyType.Float:
                        value = property.floatValue.ToString("0.00000000");
                        break;
                    case SerializedPropertyType.String:
                        value = property.stringValue;
                        break;
                    case SerializedPropertyType.Vector2:
                        value = property.vector2Value.ToString();
                        break;
                    case SerializedPropertyType.Vector3:
                        value = property.vector3Value.ToString();
                        break;
                    case SerializedPropertyType.Vector4:
                        value = property.vector4Value.ToString();
                        break;
                }
            }
            if ( updateInterval > 0f ) {
                var content = new GUIContent {
                    text = label.text + string.Format( " ({0} s)", updateInterval )
                };
                var postfix = string.Format(" (update interval: {0} s)", updateInterval);
                if ( !label.tooltip.OrEmpty().Contains( postfix ) ) {
                    content.tooltip = (label.tooltip + postfix).Trim();
                }
                label = content;
            }
            EditorGUI.LabelField( position, label, new GUIContent( "value: " + value ) );
		}
	}
}