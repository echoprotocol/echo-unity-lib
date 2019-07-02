#if UNITY_EDITOR
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Action = System.Action;
using Directory = System.IO.Directory;


namespace CustomTools.Editor {

    public class EditorTools {

        #region Vector
        public static Vector3 DrawVector3( Vector3 value ) {
            var fieldWidth = GUILayout.MinWidth( 30f );
            var prefixWidth = GUILayout.Width( 15f );
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( "X", prefixWidth );
            value.x = EditorGUILayout.FloatField( value.x, fieldWidth );
            EditorGUILayout.LabelField( "Y", prefixWidth );
            value.y = EditorGUILayout.FloatField( value.y, fieldWidth );
            EditorGUILayout.LabelField( "Z", prefixWidth );
            value.z = EditorGUILayout.FloatField( value.z, fieldWidth );
            EditorGUILayout.EndHorizontal();
            return value;
        }

        public static Vector2 DrawVector2( Vector2 value ) {
            var fieldWidth = GUILayout.MinWidth( 45f );
            var prefixWidth = GUILayout.Width( 15f );
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( "X", prefixWidth );
            value.x = EditorGUILayout.FloatField( value.x, fieldWidth );
            EditorGUILayout.LabelField( "Y", prefixWidth );
            value.y = EditorGUILayout.FloatField( value.y, fieldWidth );
            EditorGUILayout.EndHorizontal();
            return value;
        }
        #endregion


        #region Float
        public static float DrawFloatField( float value, string title, string tooltip, bool enabled, params GUILayoutOption[] options ) {
            if ( enabled ) {
                return EditorGUILayout.FloatField( new GUIContent( title, tooltip ), value, options );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.FloatField( new GUIContent( title, tooltip ), value, options );
            GUI.color = saveColor;
            return value;
        }

        public static float DrawFloatField( float value, string title, bool enabled, params GUILayoutOption[] options ) {
            if ( enabled ) {
                return EditorGUILayout.FloatField( title, value, options );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.FloatField( title, value, options );
            GUI.color = saveColor;
            return value;
        }

        public static float DrawFloatField( float value, string title, bool enabled = true ) {
            if ( enabled ) {
                return EditorGUILayout.FloatField( title, value );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.FloatField( title, value );
            GUI.color = saveColor;
            return value;
        }
        #endregion


        #region Enum
        public static System.Enum DrawEnumField( System.Enum value, string title, string tooltip, bool enabled, params GUILayoutOption[] options ) {
            if ( enabled ) {
                return EditorGUILayout.EnumPopup( new GUIContent( title, tooltip ), value, options );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.EnumPopup( new GUIContent( title, tooltip ), value, options );
            GUI.color = saveColor;
            return value;
        }

        public static System.Enum DrawEnumField( System.Enum value, string title, bool enabled, params GUILayoutOption[] options ) {
            if ( enabled ) {
                return EditorGUILayout.EnumPopup( title, value, options );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.EnumPopup( title, value, options );
            GUI.color = saveColor;
            return value;
        }

        public static System.Enum DrawEnumField( System.Enum value, string title, bool enabled = true ) {
            if ( enabled ) {
                return EditorGUILayout.EnumPopup( title, value );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            EditorGUILayout.EnumPopup( title, value );
            GUI.color = saveColor;
            return value;
        }
        #endregion


        #region Button
        static bool DrawEnabledButton( GUIContent content, GUIStyle style, Color backgroundColor, params GUILayoutOption[] options ) {
            var saveBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = backgroundColor;
            var result = GUILayout.Button( content, style ?? GUI.skin.button, options );
            GUI.backgroundColor = saveBackgroundColor;
            return result;
        }

        static bool DrawDisabledButton( GUIContent content, GUIStyle style, params GUILayoutOption[] options ) {
            var saveBackgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.gray;
            style = new GUIStyle( style ?? GUI.skin.button );
            style.hover = style.active = style.focused = style.normal;
            style.onHover = style.onActive = style.onFocused = style.onNormal;
            GUILayout.Button( content, style, options );
            GUI.backgroundColor = saveBackgroundColor;
            return false;
        }

        public static bool DrawButton( string title, Action action, bool enabled = true ) {
            return DrawButton( title, string.Empty, action, null, enabled, null );
        }

        public static bool DrawButton( string title, Action action, bool enabled, params GUILayoutOption[] options ) {
            return DrawButton( title, string.Empty, action, null, enabled, null, options );
        }

        public static bool DrawButton( string title, Action action, float width ) {
            return DrawButton( title, string.Empty, action, null, true, null, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, bool enabled, float width ) {
            return DrawButton( title, string.Empty, action, null, enabled, null, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, string tooltip, Action action, bool enabled ) {
            return DrawButton( title, tooltip, action, null, enabled, null );
        }

        public static bool DrawButton( string title, string tooltip, Action action, bool enabled, float width ) {
            return DrawButton( title, tooltip, action, null, enabled, null, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, string tooltip, Action action, GUIStyle style, Color? backgroundColor, float width ) {
            return DrawButton( title, tooltip, action, style, true, backgroundColor, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, Color? backgroundColor, float width ) {
            return DrawButton( title, string.Empty, action, null, true, backgroundColor, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, Color? backgroundColor, params GUILayoutOption[] options ) {
            return DrawButton( title, string.Empty, action, null, true, backgroundColor, options );
        }

        public static bool DrawButton( string title, Action action, bool enabled, Color? backgroundColor, params GUILayoutOption[] options ) {
            return DrawButton( title, string.Empty, action, null, enabled, backgroundColor, options );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, float width ) {
            return DrawButton( title, string.Empty, action, style, true, null, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, Color backgroundColor ) {
            return DrawButton( title, string.Empty, action, style, true, backgroundColor );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, bool enabled = true ) {
            return DrawButton( title, string.Empty, action, style, enabled, null );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, Color? backgroundColor, float width ) {
            return DrawButton( title, string.Empty, action, style, true, backgroundColor, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, bool enabled, float width ) {
            return DrawButton( title, string.Empty, action, style, enabled, null, GUILayout.Width( width ) );
        }

        public static bool DrawButton( string title, Action action, GUIStyle style, bool enabled, Color? backgroundColor, params GUILayoutOption[] options ) {
            return DrawButton( title, string.Empty, action, style, enabled, backgroundColor, options );
        }

        public static bool DrawButton( string title, string tooltip, Action action, GUIStyle style, bool enabled, Color? backgroundColor, params GUILayoutOption[] options ) {
            if ( enabled ) {
                return DrawEnabledButton( new GUIContent( title, tooltip ), style, ( backgroundColor ?? GUI.backgroundColor ), options ) && action.SafeInvoke();
            }
            return DrawDisabledButton( new GUIContent( title, tooltip ), style, options );
        }
        #endregion


        #region Toggle
        public static bool DrawToggle( bool value, string title, string tooltip, bool enabled, float width ) {
            if ( enabled ) {
                return GUILayout.Toggle( value, new GUIContent( title, tooltip ), GUILayout.Width( width ) );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            GUILayout.Toggle( value, new GUIContent( title, tooltip ), GUILayout.Width( width ) );
            GUI.color = saveColor;
            return value;
        }

        public static bool DrawToggle( bool value, string title, bool enabled, float width ) {
            if ( enabled ) {
                return GUILayout.Toggle( value, title, GUILayout.Width( width ) );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            GUILayout.Toggle( value, title, GUILayout.Width( width ) );
            GUI.color = saveColor;
            return value;
        }

        public static bool DrawToggle( bool value, string title, bool enabled = true ) {
            if ( enabled ) {
                return GUILayout.Toggle( value, title );
            }
            var saveColor = GUI.color;
            GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            GUILayout.Toggle( value, title );
            GUI.color = saveColor;
            return value;
        }
        #endregion

      
        #region Object
        public static T DrawObjectField<T>( T value, string title, string tooltip, bool allowSceneObjects, bool enabled, params GUILayoutOption[] options ) where T : Object {
            if ( enabled ) {
                value = ( T )EditorGUILayout.ObjectField( new GUIContent( title, tooltip ), value, typeof( T ), allowSceneObjects, options );
            } else {
                var saveColor = GUI.color;
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
                EditorGUILayout.ObjectField( new GUIContent( title, tooltip ), value, typeof( T ), allowSceneObjects, options );
                GUI.color = saveColor;
            }
            if ( value.IsNull() ) {
                return null;
            }
            return value;
        }

        public static T DrawObjectField<T>( T value, string title, bool allowSceneObjects, bool enabled, params GUILayoutOption[] options ) where T : Object {
            if ( enabled ) {
                value = ( T )EditorGUILayout.ObjectField( title, value, typeof( T ), allowSceneObjects, options );
            } else {
                var saveColor = GUI.color;
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
                EditorGUILayout.ObjectField( title, value, typeof( T ), allowSceneObjects, options );
                GUI.color = saveColor;
            }
            if ( value.IsNull() ) {
                return null;
            }
            return value;
        }

        public static T DrawObjectField<T>( T value, string title, bool allowSceneObjects, bool enabled = true ) where T : Object {
            if ( enabled ) {
                value = ( T )EditorGUILayout.ObjectField( title, value, typeof( T ), allowSceneObjects );
            } else {
                var saveColor = GUI.color;
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
                EditorGUILayout.ObjectField( title, value, typeof( T ), allowSceneObjects );
                GUI.color = saveColor;
            }
            if ( value.IsNull() ) {
                return null;
            }
            return value;
        }

        public static T DrawObjectField<T>( T value, bool allowSceneObjects, bool enabled = true ) where T : Object {
            if ( enabled ) {
                value = ( T )EditorGUILayout.ObjectField( value, typeof( T ), allowSceneObjects );
            } else {
                var saveColor = GUI.color;
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
                EditorGUILayout.ObjectField( value, typeof( T ), allowSceneObjects );
                GUI.color = saveColor;
            }
            if ( value.IsNull() ) {
                return null;
            }
            return value;
        }
        #endregion


        #region Label
        public static void DrawLabel( string title, string tooltip ) {
            DrawLabel( new GUIContent( title, tooltip ), null, true );
        }

        public static void DrawLabel( string title, string tooltip, float width ) {
            DrawLabel( new GUIContent( title, tooltip ), null, true, GUILayout.Width( width ) );
        }

        public static void DrawLabel( GUIContent content, GUIStyle style, params GUILayoutOption[] options ) {
            DrawLabel( content, style, true, options );
        }

        public static void DrawLabel( GUIContent content, GUIStyle style, bool enabled, params GUILayoutOption[] options ) {
            var saveColor = GUI.color;
            if ( !enabled ) {
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            }
            EditorGUILayout.LabelField( content, style ?? GUI.skin.label, options );
            GUI.color = saveColor;
        }

        public static void DrawLabel( string title ) {
            DrawLabel( title, null, true );
        }

        public static void DrawLabel( string title, float width, GUIStyle style = null, bool enabled = true ) {
            DrawLabel( title, style, enabled, GUILayout.Width( width ) );
        }

        public static void DrawLabel( string title, float minWidth, float maxWidth, GUIStyle style = null, bool enabled = true ) {
            DrawLabel( title, style, enabled, GUILayout.MinWidth( minWidth ), GUILayout.MaxWidth( maxWidth ) );
        }

        public static void DrawLabel( string title, GUIStyle style, params GUILayoutOption[] options ) {
            DrawLabel( title, style, true, options );
        }

        public static void DrawLabel( string title, GUIStyle style, bool enabled, params GUILayoutOption[] options ) {
            var saveColor = GUI.color;
            if ( !enabled ) {
                GUI.color = new Color( 1f, 1f, 1f, 0.25f );
            }
            EditorGUILayout.LabelField( title, style ?? GUI.skin.label, options );
            GUI.color = saveColor;
        }
        #endregion


        public static void DrawInBox( Action body ) {
            GUILayout.BeginVertical( "Box" );
            body();
            GUILayout.EndVertical();
        }

        public static void DrawInBox( Color backgroundColor, Action body ) {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = backgroundColor;
            GUILayout.BeginVertical( "Box" );
            GUI.backgroundColor = color;
            body();
            GUI.backgroundColor = backgroundColor;
            GUILayout.EndVertical();
            GUI.backgroundColor = color;
        }

        public static void DrawInLine( Action body ) {
            GUILayout.BeginHorizontal();
            body();
            GUILayout.EndHorizontal();
        }

        public static void DrawInLine( Color backgroundColor, Action body ) {
            var color = GUI.backgroundColor;
            GUI.backgroundColor = backgroundColor;
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = color;
            body();
            GUI.backgroundColor = backgroundColor;
            GUILayout.EndHorizontal();
            GUI.backgroundColor = color;
        }

        public static T DrawDragAndDropArea<T>( string title, float width, float height ) where T : class {
            var currentEvent = Event.current;
            var area = GUILayoutUtility.GetRect( width, height, GUILayout.ExpandWidth( true ) );
            GUI.Box( area, title );
            if ( ( currentEvent.type.Equals( EventType.DragUpdated ) || currentEvent.type.Equals( EventType.DragPerform ) ) && area.Contains( currentEvent.mousePosition ) ) {
                var draggedObject = DragAndDrop.objectReferences[0];
                T result = null;
                if ( !( result = draggedObject as T ).IsNull() || ( ( draggedObject is GameObject ) && !( result = ( draggedObject as GameObject ).GetComponents<MonoBehaviour>().Find( component => component is T ) as T ).IsNull() ) ) {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if ( currentEvent.type.Equals( EventType.DragPerform ) ) {
                        DragAndDrop.AcceptDrag();
                        currentEvent.Use();
                        return result;
                    }
                }
            }
            return null;
        }


        #region Dialog
        public static void DrawQuestionDialogInBox( string question, Action yesAction, Action noAction, Color[] elementColors = null ) {
            DrawInBox( () => {
                var labelStyle = new GUIStyle( EditorStyles.label );
                labelStyle.alignment = TextAnchor.MiddleCenter;
                if ( !elementColors.IsNull() ) {
                    labelStyle.normal.textColor = elementColors[ 0 ];
                }
                DrawLabel( question, labelStyle );
                DrawInLine( () => {
                    var buttonStyle = new GUIStyle( EditorStyles.miniButtonLeft );
                    if ( !elementColors.IsNull() ) {
                        buttonStyle.normal.textColor = elementColors[ 2 ];
                    }
                    DrawButton( "No", noAction, buttonStyle );
                    buttonStyle = new GUIStyle( EditorStyles.miniButtonRight );
                    if ( !elementColors.IsNull() ) {
                        buttonStyle.normal.textColor = elementColors[ 1 ];
                    }
                    DrawButton( "Yes", yesAction, buttonStyle );
                } );
            } );
        }

        public static void DrawQuestionDialogInLine( string question, float questionMinWidth, Action yesAction, Action noAction, Color[] elementColors = null ) {
            DrawInLine( () => {
                var buttonStyle = new GUIStyle( EditorStyles.miniButtonLeft );
                if ( !elementColors.IsNull() ) {
                    buttonStyle.normal.textColor = elementColors[ 2 ];
                }
                DrawButton( "No", noAction, buttonStyle, 40f );
                var labelStyle = new GUIStyle( EditorStyles.miniButtonMid );
                if ( !elementColors.IsNull() ) {
                    labelStyle.normal.textColor = elementColors[ 0 ];
                }
                DrawButton( question, null, labelStyle, false, null, GUILayout.MinWidth( questionMinWidth ) );
                buttonStyle = new GUIStyle( EditorStyles.miniButtonRight );
                if ( !elementColors.IsNull() ) {
                    buttonStyle.normal.textColor = elementColors[ 1 ];
                }
                DrawButton( "Yes", yesAction, buttonStyle, 40f );
            } );
        }

        public static bool DrawQuestionDialogInFoldout( bool foldout, string question, Action yesAction, Action noAction, Color[] elementColors = null ) {
            DrawInLine( () => {
                var foldoutStyle = new GUIStyle( EditorStyles.foldout );
                foldoutStyle.alignment = TextAnchor.MiddleLeft;
                if ( !elementColors.IsNull() ) {
                    foldoutStyle.normal.textColor = elementColors[ 0 ];
                    foldoutStyle.onNormal.textColor = elementColors[ 0 ];
                }
                foldout = EditorGUILayout.Foldout( foldout, question, foldoutStyle );
                var buttonStyle = new GUIStyle( EditorStyles.miniButtonLeft );
                if ( !elementColors.IsNull() ) {
                    buttonStyle.normal.textColor = elementColors[ 2 ];
                }
                DrawButton( "No", noAction, buttonStyle, 40f );
                buttonStyle = new GUIStyle( EditorStyles.miniButtonRight );
                if ( !elementColors.IsNull() ) {
                    buttonStyle.normal.textColor = elementColors[ 1 ];
                }
                DrawButton( "Yes", yesAction, buttonStyle, 40f );
            } );
            return foldout;
        }

        public static void DrawEditListDialogInLine( int count, Action addCallback, Action removeLastCallback = null, Action removeAllCallbacl = null ) {
            DrawInLine( () => {
                DrawButton( "Add", addCallback, EditorStyles.miniButton, true, Color.green );
                if ( ( count > 1 ) && !removeLastCallback.IsNull() ) {
                    DrawButton( "Remove Last", removeLastCallback, EditorStyles.miniButton, true, Color.red );
                }
                if ( ( count > 0 ) && !removeAllCallbacl.IsNull() ) {
                    DrawButton( "Remove All", removeAllCallbacl, EditorStyles.miniButton, true, Color.red );
                }
            } );
        }
        #endregion


        public static void DrawSelector<T>( string title, Color elementColor, T[] variants, System.Converter<T, string> converter, System.Action<T> selectionCallback, params GUILayoutOption[] options ) {
            var values = new[] { title }.Concat( variants.ConvertAll( converter ) );
            var color = GUI.backgroundColor;
            GUI.backgroundColor = elementColor;
            var newIndex = EditorGUILayout.Popup( 0, values, options );
            if ( newIndex > 0 ) {
                selectionCallback.SafeInvoke( variants[ newIndex - 1 ] );
            }
            GUI.backgroundColor = color;
        }

        public static string FormatToPercent( float value, int countAfterDot = 0 ) {
            return string.Format( ( "{0,0:F" + countAfterDot + "}" ), ( value * 100 ) ).TrimEnd( '0' ).TrimEnd( '.' ) + " %";
        }

        public static string FormatToFloat( float value, int countAfterDot = 0 ) {
            return string.Format( ( "{0,0:F" + countAfterDot + "}" ), value ).TrimEnd( '0' ).TrimEnd( '.' );
        }

        public static string[] AllNamesPrefabsAtResources( string path = null ) {
            var prefabPaths = Directory.GetFiles( ( Directory.GetCurrentDirectory() + "/Assets/Resources/" + path.OrEmpty() ), "*.prefab" );
            ArrayTools.DoTimes( () => prefabPaths.Length, index => {
                prefabPaths[ index ] = prefabPaths[ index ].Replace( ( Directory.GetCurrentDirectory() + "/Assets/Resources/" + path.OrEmpty() ), string.Empty ).Replace( ".prefab", string.Empty );
            } );
            return prefabPaths;
        }

        public static string[] AllNamesCSVFilesAtResources( string path = null ) {
            var csvPaths = Directory.GetFiles( ( Directory.GetCurrentDirectory() + "/Assets/Resources/" + path.OrEmpty() ), "*.csv" );
            ArrayTools.DoTimes( () => csvPaths.Length, index => {
                csvPaths[ index ] = csvPaths[ index ].Replace( ( Directory.GetCurrentDirectory() + "/Assets/Resources/" + path.OrEmpty() ), string.Empty ).Replace( ".csv", string.Empty );
            } );
            return csvPaths;
        }


        #region Command
        public static void SaveAllAssets() {
            AssetDatabase.SaveAssets();
        }

        public static void SaveAllOpenScenes() {
            EditorSceneManager.SaveOpenScenes();
        }

        public static Scene ActiveScene {
            get { return SceneManager.GetActiveScene(); }
        }

        public static void ResetSelection() {
            Selection.activeGameObject = null;
            Selection.objects = new Object[ 0 ];
        }

        public static void SelectObject( GameObject target ) {
            Selection.activeGameObject = target;
            Selection.objects = new Object[] { target };
        }

        public static void SelectObjects( GameObject[] targets ) {
            Selection.activeGameObject = targets.First();
            Selection.objects = targets.ConvertAll( target => (Object) target );
        }

        public static void FrameSelectedObject( bool lockView = false ) {
            if ( lockView ) {
                SceneView.FrameLastActiveSceneViewWithLock();
            } else {
                SceneView.FrameLastActiveSceneView();
            }
        }

        [MenuItem( "File/Save All %&s" )]
        public static void SaveAll() {
            SaveAllAssets();
            SaveAllOpenScenes();
        }
        #endregion

    }
}
#endif