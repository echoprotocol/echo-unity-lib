using CustomTools.Attributes;
using CustomTools.Extensions.Core.Array;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

    [CustomPropertyDrawer( typeof( EnumFilterAttribute ) )]
    public class EnumFilterAttributeDrawer : PropertyDrawer {

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            var enumFilterAttribute = ( EnumFilterAttribute )attribute;
            if ( enumFilterAttribute.Filter.IsNullOrEmpty() ) {
                property.enumValueIndex = EditorGUI.Popup( position, label.text, property.enumValueIndex, property.enumDisplayNames );
            } else {
                var filteredNames = enumFilterAttribute.Filter.ConvertAll( value => System.Enum.GetName( value.GetType(), value ) );
                var index = property.enumValueIndex;
                if ( index >= 0 && index < property.enumNames.Length ) {
                    var valueName = property.enumNames[ index ];
                    if ( filteredNames.Contains( valueName )) {
                        index = filteredNames.IndexOf( valueName );
                    } else {
                        index = 0;
                    }
                }
                index = EditorGUI.Popup( position, label.text, index, filteredNames );
                if ( index < 0 || index >= filteredNames.Length ) {
                    return;
                }
                property.enumValueIndex = property.enumNames.IndexOf( filteredNames[ index ] );
            }
		}
	}
}