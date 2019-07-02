using System.Reflection;
using CustomTools.Attributes;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

	[CustomPropertyDrawer( typeof( ActionButtonAttribute ) )]
	public class ActionButtonAttributeDrawer : PropertyDrawer {

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
			var parent = property.serializedObject.targetObject;
			var calledType = parent.GetType();
			var methods = calledType.GetMethods( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly ).ConvertAll( info => info.Name );
			var popupRect = new Rect( position.xMin, position.yMin, 18f, position.height );
			var buttonRect = new Rect( (position.xMin + 25f), position.yMin, (position.width - 25f), position.height );
			int index = -1;
			for ( var i = 0; (i < methods.Length) && (index == -1); i++ ) {
				if ( methods[ i ].Equals( property.stringValue ) ) {
					index = i;
				}
			}
			index = EditorGUI.Popup( popupRect, string.Empty, index, methods );
			if ( index != -1 ) {
				property.stringValue = methods[ index ];
			}
			if ( GUI.Button( buttonRect, property.stringValue ) ) {
				var info = calledType.GetMethod( property.stringValue, (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) );
				if ( !info.IsNull() ) {
					info.Invoke( parent, null );
				}
			}
		}
	}
}