using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Enum;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {
    
    public sealed class FontSizeTweening : TweeningParameter {

    	const Type SUPPORT_TYPES = Type.Height;

        [SerializeField] Text target;
    	[SerializeField] int from;
        [SerializeField] int to;


        public override Type SupportTypes => SUPPORT_TYPES;

        public Text Target {
            get { return target.IsNull() ? (target = GetComponent<Text>()) : target; }
            set { target = value; }
        }

        public int From {
    		get { return from; }
    		set { from = value; }
    	}

        public int To {
    		get { return to; }
    		set { to = value; }
    	}

    	public override float Sample( float absolutFactor, bool isDone ) {
    		if ( ParameterType.Contains( Type.Height ) ) {
    			absolutFactor = tweenCurves[ 0 ].Evaluate( absolutFactor );
                Target.fontSize = Mathf.RoundToInt( from * (1f - absolutFactor) + to * absolutFactor );
    		}
    		return absolutFactor;
    	}


    #if UNITY_EDITOR
        public override Type[] UsedTypes {
            get { return new [] { Type.Height }; }
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
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					from = EditorGUILayout.IntField( from );
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
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					tweenCurves[ 0 ] = EditorGUILayout.CurveField( tweenCurves[ 0 ] );
    				}
    			} );
    			EditorTools.DrawInLine( () => {
    				EditorTools.DrawLabel( "To", 50f );
    				if ( ParameterType.Contains( Type.Height ) ) {
    					EditorTools.DrawLabel( char.ConvertFromUtf32( 0x2195 ), 10f );
    					to = EditorGUILayout.IntField( to );
    				}
                    EditorTools.DrawButton( "Apply", () => Sample( 1f, true ), EditorStyles.miniButton, Color.green, 40f );
    			} );
    		}
    	}
    #endif
    }
}