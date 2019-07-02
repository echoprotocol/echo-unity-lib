using CustomTools.Attributes;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

    [CustomPropertyDrawer( typeof( OverrideLabelAttribute ) )]
    public class OverrideLabelAttributeDrawer : PropertyDrawer {
        
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            var content = new GUIContent {
                text = (( OverrideLabelAttribute )attribute).Title,
                tooltip = label.tooltip
            };
            EditorGUI.PropertyField( position, property, content );
		}
	}
}