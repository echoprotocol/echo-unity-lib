using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class ActiveTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Active;

    	[SerializeField] GameObject target;
    	[SerializeField] bool from;
    	[SerializeField] bool to;


    	public override Type SupportTypes => SUPPORT_TYPES;

        public GameObject Target {
            get { return target.IsNull() ? (target = gameObject) : target; }
            set { target = value; }
        }

    	public bool From {
    		get { return from; }
    		set { from = value; }
    	}

    	public bool To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		absolutFactor = tweenCurves.First().Evaluate( absolutFactor );
    		if ( ParameterType.Contains( Type.Active ) ) {
    			Target.SetActive( (absolutFactor > 0f) ? to : from );
    		}
    		return absolutFactor;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Active }; }
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
    				if ( ParameterType.Contains( Type.Active ) ) {
    					from = EditorGUILayout.Toggle( from, GUILayout.MinWidth( 80f ) );
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
    				if ( ParameterType.Contains( Type.Active ) ) {
                        to = EditorGUILayout.Toggle( to, GUILayout.MinWidth( 80f ) );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}