using System.Reflection;
using CustomTools.Attributes;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

	[CustomPropertyDrawer( typeof( ButtonAttribute ) )]
    public class ButtonAttributeDrawer : PropertyDrawer {

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            var buttonAttribute = ( ButtonAttribute )attribute;
            var title = buttonAttribute.Title.IsNullOrEmpty() ? buttonAttribute.ActionName : buttonAttribute.Title;
            var buttonWidth = Mathf.Min( position.width * 0.75f, GUI.skin.button.CalcSize( new GUIContent( title ) ).x );
            var propertyRect = new Rect( position );
            propertyRect.width -= buttonWidth;
            EditorGUI.PropertyField( propertyRect, property, label, true );
            var buttonRect = new Rect( position ) {
                width = buttonWidth,
                height = EditorGUI.GetPropertyHeight( property, label, false )
            };
            buttonRect.x += propertyRect.width;
            if ( GUI.Button( buttonRect, buttonAttribute.Title ) ) {
                var parent = property.serializedObject.targetObject;
                parent.GetType().GetMethod( buttonAttribute.ActionName, (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) )?.Invoke( parent, null );
            }
        }

        public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) {
            if ( property.isExpanded ) {
                return EditorGUI.GetPropertyHeight( property, label, true );
            }
            return base.GetPropertyHeight( property, label );
        }
	}
}