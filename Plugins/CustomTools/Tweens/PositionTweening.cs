using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class PositionTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Position | Type.X | Type.Y | Type.Z;

    	[SerializeField] Transform target;
    	[SerializeField] Vector3 from;
    	[SerializeField] Vector3 to;
    	[SerializeField] bool useLocal;


    	public override Type SupportTypes => SUPPORT_TYPES;

    	protected override Type BaseType => Type.Position;

    	public Transform Target {
            get { return target.IsNull() ? (target = transform) : target; }
            set { target = value; }
    	}

    	public Vector3 From {
    		get { return from; }
    		set { from = value; }
    	}

    	public Vector3 To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		var position = useLocal ? Target.localPosition : Target.position;
    		if ( ParameterType.Contains( Type.X ) ) {
    			var axisAbsolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
    			position.x = from.x * (1f - axisAbsolutFactor) + to.x * axisAbsolutFactor;
    		}
    		if ( ParameterType.Contains( Type.Y ) ) {
    			var axisAbsolutFactor = tweenCurves[ 1 ].Evaluate( absolutFactor );
    			position.y = from.y * (1f - axisAbsolutFactor) + to.y * axisAbsolutFactor;
    		}
    		if ( ParameterType.Contains( Type.Z ) ) {
    			var axisAbsolutFactor = tweenCurves[ 2 ].Evaluate( absolutFactor );
    			position.z = from.z * (1f - axisAbsolutFactor) + to.z * axisAbsolutFactor;
    		}
    		if ( useLocal ) {
    			Target.localPosition = position;
    		} else {
    			Target.position = position;
    		}
    		return absolutFactor;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Position, Type.X, Type.Y, Type.Z }; }
        }

    	public override void DrawTitle() {
    		EditorTools.DrawLabel( GetType().ToString(), 100f );
    		useLocal = EditorGUILayout.Popup( useLocal ? 1 : 0, new [] { "Global", "Local" }, GUILayout.Width( 50f ) ) > 0;
    	}

    	public override void DrawInspector() {
    		base.DrawInspector();
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Target", 100f );
                target = EditorTools.DrawObjectField( target, true );
    		} );
    		if ( ParameterType.Contains( SupportTypes ^ BaseType ) ) {
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "From", 50f );
    				if ( ParameterType.Contains( Type.X ) ) {
    					EditorTools.DrawLabel( "X", 12.5f );
    					from.x = EditorGUILayout.FloatField( from.x, GUILayout.MinWidth( 30f ) );
    				}
    				if ( ParameterType.Contains( Type.Y ) ) {
    					EditorTools.DrawLabel( "Y", 12.5f );
    					from.y = EditorGUILayout.FloatField( from.y, GUILayout.MinWidth( 30f ) );
    				}
    				if ( ParameterType.Contains( Type.Z ) ) {
    					EditorTools.DrawLabel( "Z", 12.5f );
    					from.z = EditorGUILayout.FloatField( from.z, GUILayout.MinWidth( 30f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 0f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    			EditorTools.DrawInLine( () => {
    				if ( tweenCurves.IsNull() ) {
    					tweenCurves = new [] {
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    					};
    				}
    				EditorTools.DrawLabel( "Curve", 50f );
    				if ( ParameterType.Contains( Type.X ) ) {
    					EditorTools.DrawLabel( "X", 12.5f );
    					tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ], GUILayout.MinWidth( 45f ) );
    				}
    				if ( ParameterType.Contains( Type.Y ) ) {
    					EditorTools.DrawLabel( "Y", 12.5f );
    					tweenCurves[ 1 ] = EditorGUILayout.CurveField( tweenCurves[ 1 ], GUILayout.MinWidth( 45f ) );
    				}
    				if ( ParameterType.Contains( Type.Z ) ) {
    					EditorTools.DrawLabel( "Z", 12.5f );
    					tweenCurves[ 2 ] = EditorGUILayout.CurveField( tweenCurves[ 2 ], GUILayout.MinWidth( 45f ) );
    				}
    			} );
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "To", 50f );
    				if ( ParameterType.Contains( Type.X ) ) {
    					EditorTools.DrawLabel( "X", 12.5f );
    					to.x = EditorGUILayout.FloatField( to.x, GUILayout.MinWidth( 30f ) );
    				}
    				if ( ParameterType.Contains( Type.Y ) ) {
    					EditorTools.DrawLabel( "Y", 12.5f );
    					to.y = EditorGUILayout.FloatField( to.y, GUILayout.MinWidth( 30f ) );
    				}
    				if ( ParameterType.Contains( Type.Z ) ) {
    					EditorTools.DrawLabel( "Z", 12.5f );
    					to.z = EditorGUILayout.FloatField( to.z, GUILayout.MinWidth( 30f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}