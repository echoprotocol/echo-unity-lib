using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class RectSizeTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Width | Type.Height;

        [SerializeField] RectTransform target;
    	[SerializeField] Vector2 from;
    	[SerializeField] Vector2 to;


    	public override Type SupportTypes => SUPPORT_TYPES;

        public RectTransform Target {
            get { return target.IsNull() ? (target = GetComponent<RectTransform>()) : target; }
            set { target = value; }
        }

    	public Vector2 From {
    		get { return from; }
    		set { from = value; }
    	}

    	public Vector2 To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		if ( ParameterType.Contains( Type.Width ) ) {
    			absolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
                Target.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, Mathf.RoundToInt( from.x * (1f - absolutFactor) + to.x * absolutFactor ) );
    		}
    		if ( ParameterType.Contains( Type.Height ) ) {
    			absolutFactor = tweenCurves[ 1 ].Evaluate( absolutFactor );
                Target.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, Mathf.RoundToInt( from.y * (1f - absolutFactor) + to.y * absolutFactor ) );
    		}
    		return absolutFactor;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Width, Type.Height }; }
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
    				if ( ParameterType.Contains( Type.Width ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2194 ), 15f );
    					from.x = EditorGUILayout.FloatField( from.x );
    				}
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					from.y = EditorGUILayout.FloatField( from.y );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 0f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    			EditorTools.DrawInLine( () => {
    				if ( tweenCurves.IsNull() ) {
    					tweenCurves = new [] {
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) ),
    						new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) )
    					};
    				}
    				EditorTools.DrawLabel( "Curve", 50f );
    				if ( ParameterType.Contains( Type.Width ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2194 ), 15f );
    					tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ] );
    				}
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					tweenCurves[ 1 ] = EditorGUILayout.CurveField( tweenCurves[ 1 ] );
    				}
    			} );
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "To", 50f );
    				if ( ParameterType.Contains( Type.Width ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2194 ), 15f );
    					to.x = EditorGUILayout.FloatField( to.x );
    				}
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					to.y = EditorGUILayout.FloatField( to.y );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}