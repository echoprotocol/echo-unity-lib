using CustomTools.Attributes;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

	[CustomPropertyDrawer( typeof( EnumFlagsAttribute ) )]
	public class EnumFlagsAttributeDrawer : PropertyDrawer {

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            property.intValue = EditorGUI.MaskField( position, label, property.intValue, property.enumDisplayNames );
		}
	}
}