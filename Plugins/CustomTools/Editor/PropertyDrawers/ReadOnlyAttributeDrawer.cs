using CustomTools.Attributes;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

    [CustomPropertyDrawer( typeof( ReadOnlyAttribute ) )]
    public class ReadOnlyAttributeDrawer : PropertyDrawer {
        
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            using ( var scope = new EditorGUI.DisabledGroupScope( true ) ) {
                EditorGUI.PropertyField( position, property, label, true );
            }
		}

        public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) => EditorGUI.GetPropertyHeight( property, label, true );
	}
}