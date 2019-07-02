using CustomTools.Fields;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

    [CustomPropertyDrawer( typeof( FactorCurve ) )]
    public class FactorCurveDrawer : PropertyDrawer {

        const int ROW_COUNT = 4;


		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            EditorGUI.BeginProperty( position, label, property );
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            if ( property.isExpanded ) {
                GUI.Box( position, string.Empty, EditorStyles.helpBox );
            }
            var rowHeight = position.height / (property.isExpanded ? ROW_COUNT : 1);
            var factorValueWidth = 40f;
            var factorLableWidth = 15f;
            var iconWidth = rowHeight;
            var titleRect = new Rect( position ) { height = rowHeight };
            titleRect.width -= iconWidth + factorLableWidth + factorValueWidth;
            property.isExpanded = EditorGUI.Foldout( titleRect, property.isExpanded, label, true, EditorStyles.foldout );
            var iconRect = new Rect( position ) {
                height = rowHeight,
                width = iconWidth
            };
            iconRect.x += titleRect.width;
            EditorGUI.PropertyField( iconRect, property.FindPropertyRelative( "curve" ), GUIContent.none );
            var factorLableRect = new Rect( position ) {
                width = factorLableWidth,
                height = rowHeight
            };
            factorLableRect.x += titleRect.width + iconWidth;
            EditorGUI.LabelField( factorLableRect, new GUIContent( "x" ), new GUIStyle( EditorStyles.label ) { alignment = TextAnchor.MiddleCenter } );
            var factorValueRect = new Rect( position ) {
                width = factorValueWidth,
                height = rowHeight
            };
            factorValueRect.x += titleRect.width + iconWidth + factorLableWidth;
            EditorGUI.PropertyField( factorValueRect, property.FindPropertyRelative( "factor" ), GUIContent.none );
            if ( property.isExpanded ) {
                var curveRect = new Rect( position ) { height = rowHeight * (ROW_COUNT - 1) };
                curveRect.y += rowHeight;
                EditorGUI.PropertyField( curveRect, property.FindPropertyRelative( "curve" ), GUIContent.none );
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
		}

        public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) => base.GetPropertyHeight( property, label ) * (property.isExpanded ? ROW_COUNT : 1);
    }
}