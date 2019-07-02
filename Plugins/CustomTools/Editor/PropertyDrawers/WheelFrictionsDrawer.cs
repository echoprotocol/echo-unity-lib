using System.Reflection;
using CustomTools.Attributes;
using CustomTools.Extensions.Core;
using CustomTools.Fields;
using UnityEditor;
using UnityEngine;


namespace CustomTools.Editor.Attributes {

    [CustomPropertyDrawer( typeof( ApplyWheelFrictionsAttribute ) )]
    [CustomPropertyDrawer( typeof( WheelFrictions ) )]
    public class WheelFrictionsDrawer : PropertyDrawer {

        const int ROW_COUNT = 6;


		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            EditorGUI.BeginProperty( position, label, property );
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var rowHeight = position.height / (property.isExpanded ? ROW_COUNT : 1);
            property.isExpanded = EditorGUI.Foldout( new Rect( position ) { height = rowHeight }, property.isExpanded, label, true, EditorStyles.foldout );
            if ( property.isExpanded ) {
                var fullGridRect = new Rect( position ) { height = rowHeight * (ROW_COUNT - 1) };
                fullGridRect.y += rowHeight;
                GUI.Box( fullGridRect, string.Empty );
                var headerWidth = DrawHeader( property, fullGridRect, rowHeight );
                var gridRect = new Rect( fullGridRect ) { width = (fullGridRect.width - headerWidth) / 2f };
                gridRect.x += headerWidth;
                DrawGrid( "Forward", property.FindPropertyRelative( "forward" ), gridRect, rowHeight );
                gridRect.x += gridRect.width;
                DrawGrid( "Sideways", property.FindPropertyRelative( "sideways" ), gridRect, rowHeight );
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
		}

        public override float GetPropertyHeight( SerializedProperty property, GUIContent label ) => base.GetPropertyHeight( property, label ) * (property.isExpanded ? ROW_COUNT : 1) * 1.25f;

        float DrawHeader( SerializedProperty property, Rect fullGridRect, float rowHeight ) {
            var style = new GUIStyle( GUI.skin.box ) { alignment = TextAnchor.MiddleLeft };
            var extremumTitle = new GUIContent( "Extremum" );
            var asymptoteTitle = new GUIContent( "Asymptote" );
            var stiffnessTitle = new GUIContent( "Stiffness" );
            var cellWidth = Mathf.Max(
                Mathf.Max( style.CalcSize( extremumTitle ).x, style.CalcSize( asymptoteTitle ).x ),
                style.CalcSize( stiffnessTitle ).x
            );
            var cellRect = new Rect( fullGridRect ) { height = rowHeight * 2f, width = cellWidth };
            var applyWheelFrictionsAttribute = attribute as ApplyWheelFrictionsAttribute;
            if ( !applyWheelFrictionsAttribute.IsNull() ) {
                var buttonTitle = applyWheelFrictionsAttribute.Title.IsNullOrEmpty() ? "Apply to wheel" : applyWheelFrictionsAttribute.Title;
                cellRect.width = Mathf.Max( cellRect.width, new GUIStyle( GUI.skin.label ) { wordWrap = true, fontSize = 9 }.CalcSize( new GUIContent( buttonTitle ) ).x );
                GUI.Box( cellRect, string.Empty );
                var buttonRect = new Rect( cellRect );
                buttonRect.y += 2.5f;
                buttonRect.x += 2.5f;
                buttonRect.height -= 5f;
                buttonRect.width -= 5f;
                var backgroundColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.green;
                if ( GUI.Button( buttonRect, buttonTitle, new GUIStyle( GUI.skin.button ) { wordWrap = true } ) ) {
                    var parent = property.serializedObject.targetObject;
                    parent.GetType().GetMethod( applyWheelFrictionsAttribute.ActionName, (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) )?.Invoke( parent, null );
                }
                GUI.backgroundColor = backgroundColor;
            } else {
                GUI.Box( cellRect, string.Empty );
            }
            cellRect.height /= 2f;
            cellRect.y += rowHeight * 2f;
            EditorGUI.LabelField( cellRect, extremumTitle, style );
            cellRect.y += rowHeight;
            EditorGUI.LabelField( cellRect, asymptoteTitle, style );
            cellRect.y += rowHeight;
            EditorGUI.LabelField( cellRect, stiffnessTitle, style );
            return cellRect.width;
        }

        void DrawGrid( string title, SerializedProperty property, Rect gridSize, float rowHeight ) {
            var style = new GUIStyle( GUI.skin.box ) { alignment = TextAnchor.MiddleCenter };
            var cellRect = new Rect( gridSize ) { height = rowHeight };
            EditorGUI.LabelField( cellRect, new GUIContent( title ), new GUIStyle( style ) { fontStyle = FontStyle.Bold } );
            cellRect.y += rowHeight;
            cellRect.width = gridSize.width / 2f;
            EditorGUI.LabelField( cellRect, new GUIContent( "Force" ), style );
            cellRect.x += gridSize.width / 2f;
            EditorGUI.LabelField( cellRect, new GUIContent( "Slip" ), style );
            cellRect.y += rowHeight;
            cellRect.x -= gridSize.width / 2f;
            var field = property.FindPropertyRelative( "extremumValue" );
            field.floatValue = EditorGUI.FloatField( cellRect, field.floatValue, style );
            cellRect.x += gridSize.width / 2f;
            field = property.FindPropertyRelative( "extremumSlip" );
            field.floatValue = EditorGUI.FloatField( cellRect, field.floatValue, style );
            cellRect.y += rowHeight;
            cellRect.x -= gridSize.width / 2f;
            field = property.FindPropertyRelative( "asymptoteValue" );
            field.floatValue = EditorGUI.FloatField( cellRect, field.floatValue, style );
            cellRect.x += gridSize.width / 2f;
            field = property.FindPropertyRelative( "asymptoteSlip" );
            field.floatValue = EditorGUI.FloatField( cellRect, field.floatValue, style );
            cellRect.y += rowHeight;
            cellRect.x -= gridSize.width / 2f;
            cellRect.width = gridSize.width;
            field = property.FindPropertyRelative( "stiffness" );
            field.floatValue = EditorGUI.FloatField( cellRect, field.floatValue, style );
        }
    }
}