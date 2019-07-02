using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class ColorTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Color | Type.Alpha;

        [SerializeField] Graphic target;
    	[SerializeField] Color from;
    	[SerializeField] Color to;


    	public override Type SupportTypes => SUPPORT_TYPES;

        public Graphic Target {
            get { return target.IsNull() ? (target = GetComponent<Graphic>()) : target; }
            set { target = value; }
    	}

    	public Color From {
    		get { return from; }
    		set { from = value; }
    	}

    	public Color To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		absolutFactor = tweenCurves.First().Evaluate( absolutFactor );
    		if ( ParameterType.Contains( Type.Color ) ) {
                var color = Color.Lerp( from, to, absolutFactor );
                color.a = Target.color.a;
                Target.color = color;
    		}
    		if ( ParameterType.Contains( Type.Alpha ) ) {
                var color = Target.color;
                color.a = Mathf.Lerp( from.a, to.a, absolutFactor );
                Target.color = color;
    		}
    		return absolutFactor;
    	}

        public static ColorTweening Play( Graphic target, float duration, Color toColor ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<ColorTweening>( SUPPORT_TYPES );
    		result.parameterType = SUPPORT_TYPES;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from = target.color;
    		result.to = toColor;
    		if ( duration > 0f ) {
    			player.ResetForForwardPlaying();
    			player.PlayForward();
    		} else {
    			player.ResetForReversePlaying();
    		}
    		return result;
    	}

        public static ColorTweening PlayAlpha( Graphic target, float duration, float toAlpha ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<ColorTweening>( Type.Alpha );
    		result.parameterType = Type.Alpha;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from.a = target.color.a;
    		result.to.a = toAlpha;
    		if ( duration > 0f ) {
    			player.ResetForForwardPlaying();
    			player.PlayForward();
    		} else {
    			player.ResetForReversePlaying();
    		}
    		return result;
    	}

        public static ColorTweening PlayColor( Graphic target, float duration, Color toColor ) {
    		var player = TweensPlayer.Get( target.gameObject, duration );
    		var result = player.GetOrAddParameter<ColorTweening>( Type.Color );
    		result.parameterType = Type.Color;
    		result.tweenCurves = new [] {
    			new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    		};
    		result.target = target;
    		result.from = target.color;
    		result.to = toColor;
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
            get { return new [] { Type.Color, Type.Alpha }; }
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
    				if ( ParameterType.Contains( Type.Color ) ) {
                        from = EditorGUILayout.ColorField( from );
    				}
    				if ( ParameterType.Contains( Type.Alpha ) ) {
    					from.a = EditorGUILayout.Slider( from.a, 0f, 1f, GUILayout.MinWidth( 80f ) );
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
    				if ( ParameterType.Contains( Type.Color ) ) {
    					to = EditorGUILayout.ColorField( to );
    				}
    				if ( ParameterType.Contains( Type.Alpha ) ) {
    					to.a = EditorGUILayout.Slider( to.a, 0f, 1f, GUILayout.MinWidth( 80f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}