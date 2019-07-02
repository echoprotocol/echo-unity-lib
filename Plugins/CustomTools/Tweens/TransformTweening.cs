using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class TransformTweening : TweeningParameter {

    	public class TransformFrame {

    		public Vector3 position;
    		public Quaternion rotation;
    		public Vector3 scale;
    	}


    	const Type SUPPORT_TYPES = Type.Position | Type.Scale | Type.Rotation;

    	[SerializeField] Transform target;
    	[SerializeField] Transform from;
    	[SerializeField] Transform to;

    	TransformFrame fromCurrent;


    	TransformFrame FromCurrent {
    		get { return fromCurrent ?? (fromCurrent = new TransformFrame { position = Target.position, rotation = Target.rotation, scale = Target.localScale }); }
    	}

        public override Type SupportTypes => SUPPORT_TYPES;

        public Transform Target {
            get { return target.IsNull() ? (target = transform) : target; }
            set { target = value; }
        }

    	public Transform From {
    		get { return from; }
    		set {
    			from = value;
    			ResetFromCurrent();
    		}
    	}

    	public Transform To {
    		get { return to; }
    		set { to = value; }
    	}

    	public void ResetFromCurrent() => fromCurrent = null;

    	public override float Sample( float absolutFactor, bool isDone ) {
    		if ( !to.IsNull() ) {
    			if ( from != null ) {
    				if ( ParameterType.Contains( Type.Position ) ) {
    					var position = from.position;
    					var positionAbsolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
    					position.x = from.position.x * (1f - positionAbsolutFactor) + to.position.x * positionAbsolutFactor;
    					positionAbsolutFactor = tweenCurves[ 1 ].Evaluate( absolutFactor );
    					position.y = from.position.y * (1f - positionAbsolutFactor) + to.position.y * positionAbsolutFactor;
    					positionAbsolutFactor = tweenCurves[ 2 ].Evaluate( absolutFactor );
    					position.z = from.position.z * (1f - positionAbsolutFactor) + to.position.z * positionAbsolutFactor;
    					Target.position = position;
    				}
    				if ( ParameterType.Contains( Type.Rotation ) ) {
    					var rotationAbsolutFactor = tweenCurves[ 3 ].Evaluate( absolutFactor );
    					Target.rotation = Quaternion.Slerp( from.rotation, to.rotation, rotationAbsolutFactor );
    				}
    				if ( ParameterType.Contains( Type.Scale ) ) {
    					var scale = from.localScale;
    					var scaleAbsolutFactor = tweenCurves[ 4 ].Evaluate( absolutFactor );
    					scale.x = from.localScale.x * (1f - scaleAbsolutFactor) + to.localScale.x * scaleAbsolutFactor;
    					scaleAbsolutFactor = tweenCurves[ 5 ].Evaluate( absolutFactor );
    					scale.y = from.localScale.y * (1f - scaleAbsolutFactor) + to.localScale.y * scaleAbsolutFactor;
    					scaleAbsolutFactor = tweenCurves[ 6 ].Evaluate( absolutFactor );
    					scale.z = from.localScale.z * (1f - scaleAbsolutFactor) + to.localScale.z * scaleAbsolutFactor;
    					Target.localScale = scale;
    				}
    			} else {
    				if ( ParameterType.Contains( Type.Position ) ) {
    					var position = FromCurrent.position;
    					var positionAbsolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
    					position.x = FromCurrent.position.x * (1f - positionAbsolutFactor) + to.position.x * positionAbsolutFactor;
    					positionAbsolutFactor = tweenCurves[ 1 ].Evaluate( absolutFactor );
    					position.y = FromCurrent.position.y * (1f - positionAbsolutFactor) + to.position.y * positionAbsolutFactor;
    					positionAbsolutFactor = tweenCurves[ 2 ].Evaluate( absolutFactor );
    					position.z = FromCurrent.position.z * (1f - positionAbsolutFactor) + to.position.z * positionAbsolutFactor;
    					Target.position = position;
    				}
    				if ( ParameterType.Contains( Type.Rotation ) ) {
    					var rotationAbsolutFactor = tweenCurves[ 3 ].Evaluate( absolutFactor );
    					Target.rotation = Quaternion.Slerp( FromCurrent.rotation, to.rotation, rotationAbsolutFactor );
    				}
    				if ( ParameterType.Contains( Type.Scale ) ) {
    					var scale = FromCurrent.scale;
    					var scaleAbsolutFactor = tweenCurves[ 4 ].Evaluate( absolutFactor );
    					scale.x = FromCurrent.scale.x * (1f - scaleAbsolutFactor) + to.localScale.x * scaleAbsolutFactor;
    					scaleAbsolutFactor = tweenCurves[ 5 ].Evaluate( absolutFactor );
    					scale.y = FromCurrent.scale.y * (1f - scaleAbsolutFactor) + to.localScale.y * scaleAbsolutFactor;
    					scaleAbsolutFactor = tweenCurves[ 6 ].Evaluate( absolutFactor );
    					scale.z = FromCurrent.scale.z * (1f - scaleAbsolutFactor) + to.localScale.z * scaleAbsolutFactor;
    					Target.localScale = scale;
    				}
    			}
    		}
    		if ( isDone ) {
    			ResetFromCurrent();
    		}
    		return absolutFactor;
    	}

    	public static TransformTweening Play( Transform target, float duration, Transform toTransform ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<TransformTweening>( SUPPORT_TYPES );
    		result.parameterType = SUPPORT_TYPES;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from = null;
    		result.to = toTransform;
    		if ( duration > 0f ) {
    			player.ResetForForwardPlaying();
    			player.PlayForward();
    		} else {
    			player.ResetForReversePlaying();
    		}
    		return result;
    	}

    	public static TransformTweening PlayPosition( Transform target, float duration, Transform toTransform ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<TransformTweening>( Type.Position );
    		result.parameterType = Type.Position;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from = null;
    		result.to = toTransform;
    		player.ResetForForwardPlaying();
    		if ( duration > 0f ) {
    			player.ResetForForwardPlaying();
    			player.PlayForward();
    		} else {
    			player.ResetForReversePlaying();
    		}
    		return result;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Position, Type.Scale, Type.Rotation }; }
        }

    	public override void DrawInspector() {
    		base.DrawInspector();
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Target", 100f );
                target = EditorTools.DrawObjectField( target, true );
    		} );
    		if ( ParameterType.Contains( SupportTypes ) ) {
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "From", 50f );
                    from = EditorTools.DrawObjectField( from, true );
                    EditorTools.DrawButton( "Apply", () => Sample( 0f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    			if ( tweenCurves.IsNull() ) {
    				tweenCurves = new [] {
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    					new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    				};
    			}
    			if ( ParameterType.Contains( Type.Position ) ) {
    				EditorTools.DrawInLine( () => {
    					EditorTools.DrawLabel( "Curve P", 50f );
    					EditorTools.DrawLabel( "X", 12.5f );
    					tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ], GUILayout.MinWidth( 45f ) );
    					EditorTools.DrawLabel( "Y", 12.5f );
    					tweenCurves[ 1 ] = EditorGUILayout.CurveField( tweenCurves[ 1 ], GUILayout.MinWidth( 45f ) );
    					EditorTools.DrawLabel( "Z", 12.5f );
    					tweenCurves[ 2 ] = EditorGUILayout.CurveField( tweenCurves[ 2 ], GUILayout.MinWidth( 45f ) );
    				} );
    			}
    			if ( ParameterType.Contains( Type.Rotation ) ) {
    				EditorTools.DrawInLine( () => {
    					EditorTools.DrawLabel( "Curve R", 50f );
    					tweenCurves[ 3 ] = EditorGUILayout.CurveField( tweenCurves[ 3 ] );
    				} );
    			}
    			if ( ParameterType.Contains( Type.Scale ) ) {
    				EditorTools.DrawInLine( () => {
    					EditorTools.DrawLabel( "Curve S", 50f );
    					EditorTools.DrawLabel( "X", 12.5f );
    					tweenCurves[ 4 ] = EditorGUILayout.CurveField( tweenCurves[ 4 ], GUILayout.MinWidth( 45f ) );
    					EditorTools.DrawLabel( "Y", 12.5f );
    					tweenCurves[ 5 ] = EditorGUILayout.CurveField( tweenCurves[ 5 ], GUILayout.MinWidth( 45f ) );
    					EditorTools.DrawLabel( "Z", 12.5f );
    					tweenCurves[ 6 ] = EditorGUILayout.CurveField( tweenCurves[ 6 ], GUILayout.MinWidth( 45f ) );
    				} );
    			}
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "To", 50f );
                    to = EditorTools.DrawObjectField( to, true );
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}