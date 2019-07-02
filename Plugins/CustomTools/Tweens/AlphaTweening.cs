using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class AlphaTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Alpha;

        [SerializeField] CanvasGroup target;
    	[SerializeField] float from;
    	[SerializeField] float to;


    	public override Type SupportTypes => SUPPORT_TYPES;

        public CanvasGroup Target {
            get { return target.IsNull() ? (target = GetComponent<CanvasGroup>()) : target; }
    		set { target = value; }
    	}

    	public float From {
    		get { return from; }
    		set { from = value; }
    	}

    	public float To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		absolutFactor = tweenCurves.First().Evaluate( absolutFactor );
    		if ( !Target.IsNull() && ParameterType.Contains( Type.Alpha ) ) {
    			Target.alpha = Mathf.Lerp( from, to, absolutFactor );
    		}
    		return absolutFactor;
    	}

        public static AlphaTweening PlayAlpha( CanvasGroup target, float duration, float toAlpha ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<AlphaTweening>( Type.Alpha );
    		result.parameterType = Type.Alpha;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from = target.alpha;
    		result.to = toAlpha;
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
            get { return new [] { Type.Alpha }; }
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
    				if ( ParameterType.Contains( Type.Alpha ) ) {
    					from = EditorGUILayout.Slider( from, 0f, 1f, GUILayout.MinWidth( 80f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 0f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    			EditorTools.DrawInLine( () => {
    				if ( tweenCurves.IsNull() ) {
    					tweenCurves = new [] {
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    					};
    				}
    				EditorTools.DrawLabel( "Curve", 50f );
    				tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ] );
    			} );
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "To", 50f );
    				if ( ParameterType.Contains( Type.Alpha ) ) {
    					to = EditorGUILayout.Slider( to, 0f, 1f, GUILayout.MinWidth( 80f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
                } );
    		}
    	}
    #endif
    }
}