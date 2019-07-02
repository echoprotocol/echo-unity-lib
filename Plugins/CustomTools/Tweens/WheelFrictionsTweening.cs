using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using CustomTools.Extensions.Core.Enum;
using CustomTools.Fields;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class WheelFrictionsTweening : TweeningParameter {

        const Type SUPPORT_TYPES = Type.Forward | Type.Sideways;

        [SerializeField] WheelCollider target;
        [SerializeField] WheelFrictions from = new WheelFrictions();
        [SerializeField] WheelFrictions to = new WheelFrictions();


        public override Type SupportTypes => SUPPORT_TYPES;

        public WheelCollider Target {
            get { return target.IsNull() ? (target = GetComponent<WheelCollider>()) : target; }
            set { target = value; }
    	}

        public WheelFrictions From {
    		get { return from; }
    		set { from = value; }
    	}

        public WheelFrictions To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
            if ( ParameterType.Contains( Type.Forward ) ) {
                absolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
                WheelFrictions.LerpForwardFriction( Target, from, to, absolutFactor );
            }
            if ( ParameterType.Contains( Type.Sideways ) ) {
                absolutFactor = tweenCurves[ 1 ].Evaluate( absolutFactor );
                WheelFrictions.LerpSidewaysFriction( Target, from, to, absolutFactor );
            }
            return absolutFactor;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Forward, Type.Sideways }; }
        }

    	public override void DrawInspector() {
    		base.DrawInspector();
            EditorTools.DrawInLine( () => {
                EditorTools.DrawLabel( "Target", 100f );
                target = EditorTools.DrawObjectField( target, true );
            } );
            if ( ParameterType.Contains( SupportTypes ) ) {
                var rowCount = 5;
                var rowHeightAtPixel = EditorGUIUtility.singleLineHeight * 1.25f;
                var spacingBetweenRowAtPixel = EditorGUIUtility.standardVerticalSpacing;
                var gridCount = 0;
                if ( ParameterType.Contains( Type.Forward ) ) {
                    gridCount++;
                }
                if ( ParameterType.Contains( Type.Sideways ) ) {
                    gridCount++;
                }

                var position = EditorGUILayout.GetControlRect( true, rowHeightAtPixel );
                var labelRect = new Rect( position ) { width = 40f };
                EditorGUI.LabelField( labelRect, "From" );
                position.x += labelRect.width;
                position.width -= labelRect.width;
                position.height = rowHeightAtPixel * rowCount;
                GUI.Box( position, string.Empty );
                var headerWidth = from.DrawHeader( position, rowHeightAtPixel );
                position.x += headerWidth;
                position.width -= headerWidth;
                position.width = position.width / gridCount;
                if ( ParameterType.Contains( Type.Forward ) ) {
                    from.Forward.DrawGrid( char.ConvertFromUtf32( 0x2195 ) + " Forward", position, rowHeightAtPixel );
                    position.x += position.width;
                }
                if ( ParameterType.Contains( Type.Sideways ) ) {
                    from.Sideways.DrawGrid( char.ConvertFromUtf32( 0x2194 ) + " Sideways", position, rowHeightAtPixel );
                }
                GUILayout.Space( position.height - rowHeightAtPixel );

                EditorTools.DrawInLine( () => {
                    if ( tweenCurves.IsNull() ) {
                        tweenCurves = new [] {
                            new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
                            new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
                        };
                    }
                    EditorTools.DrawLabel( "Curve", labelRect.width + headerWidth );
                    if ( ParameterType.Contains( Type.Forward ) ) {
                        EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 15f );
                        tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ] );
                    }
                    if ( ParameterType.Contains( Type.Sideways ) ) {
                        EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2194 ), 15f );
                        tweenCurves[ 1 ] = EditorGUILayout.CurveField( tweenCurves[ 1 ] );
                    }
                } );

                position = EditorGUILayout.GetControlRect( true, rowHeightAtPixel );
                labelRect = new Rect( position ) { width = 40f };
                EditorGUI.LabelField( labelRect, "To" );
                position.x += labelRect.width;
                position.width -= labelRect.width;
                position.height = rowHeightAtPixel * rowCount;
                GUI.Box( position, string.Empty );
                headerWidth = from.DrawHeader( position, rowHeightAtPixel );
                position.x += headerWidth;
                position.width -= headerWidth;
                position.width = position.width / gridCount;
                if ( ParameterType.Contains( Type.Forward ) ) {
                    from.Forward.DrawGrid( char.ConvertFromUtf32( 0x2195 ) + " Forward", position, rowHeightAtPixel );
                    position.x += position.width;
                }
                if ( ParameterType.Contains( Type.Sideways ) ) {
                    from.Sideways.DrawGrid( char.ConvertFromUtf32( 0x2194 ) + " Sideways", position, rowHeightAtPixel );
                }
                GUILayout.Space( position.height - rowHeightAtPixel );
            }
    	}
    #endif
    }
}