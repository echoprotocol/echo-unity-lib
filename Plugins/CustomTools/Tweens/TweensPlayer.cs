using System;
using System.Collections;
using CustomTools.CachedBehaviour;
using CustomTools.Extensions.Core.Action;
using CustomTools.Extensions.Core.Array;
using UnityEngine;


#if UNITY_EDITOR
using CustomTools.Editor;
using UnityEditor;
#endif


namespace CustomTools.Tweens {

    public abstract class TweeningParameter : MonoBehaviour {

    	[Flags]
    	public enum Type {

    		Position/*    */ = 1,
    		Rotation/*    */ = 1 << 1,
    		Scale/*       */ = 1 << 2,
    		Color/*       */ = 1 << 3,
    		Alpha/*       */ = 1 << 4,
    		Height/*      */ = 1 << 5,
    		Width/*       */ = 1 << 6,
    		X/*           */ = 1 << 7,
    		Y/*           */ = 1 << 8,
    		Z/*           */ = 1 << 9,
    		Active/*      */ = 1 << 10,
            Forward/*     */ = 1 << 11,
            Sideways/*    */ = 1 << 12
    	}


    	[SerializeField] protected Type parameterType;
    	[SerializeField] protected AnimationCurve[] tweenCurves;


    	public abstract Type SupportTypes { get; }

    	protected virtual Type BaseType {
    		get { return 0; }
    	}

    	public Type ParameterType {
    		get { return parameterType; }
    	}

    	public virtual float Sample( float absolutFactor, bool isDone ) {
    		return absolutFactor;
    	}

    	public void SetSupportTypes() {
    		parameterType = SupportTypes;
    	}


    #if UNITY_EDITOR
    	[NonSerialized] public bool needRemove;


        public abstract Type[] UsedTypes { get; }

    	public virtual void DrawTitle() {
            EditorTools.DrawLabel( GetType().Name );
    	}

    	public virtual void DrawInspector() {
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Parameters", 100f );
                parameterType = ( Type )EditorGUILayout.EnumFlagsField( parameterType );
    			if ( parameterType < 0 ) {
    				parameterType = SupportTypes;
    			}
    			parameterType |= BaseType;
    			parameterType &= SupportTypes;
    		} );
    	}
    #endif
    }


    public sealed class TweensPlayer : CachedMonoBehaviour, ICustomInspector {

    	public enum PlayDirection {

    		Forward,
    		Reverse
    	}


    	public enum Method {

    		Linear,
    		EaseIn,
    		EaseOut,
    		EaseInOut
    	}


    	public enum Style {

    		Once,
    		Loop,
    		PingPong
    	}


    	public event Action<TweensPlayer> OnFinished;

    	[SerializeField] Method tweenMethod = Method.Linear;
    	[SerializeField] Style tweenStyle = Style.Once;
    	[SerializeField] int cycleCount = 0;
    	[SerializeField] float tweenDuration = 1f;
    	[SerializeField] float startDelay = 0f;

    	TweeningParameter[] parameters = new TweeningParameter[ 0 ];
    	float factor = 0f;
    	float? amountPerDelta;
    	bool started = false;
    	bool paused = false;
    	float startTime;
    	int elapsedСycle = 0;


    	TweeningParameter[] Parameters {
    		get {
    			if ( parameters.IsNullOrEmpty() ) {
    				parameters = GetComponents<TweeningParameter>();
    			}
    			return parameters;
    		}
    	}

    	float AmountPerDelta {
    		get {
    			if ( !amountPerDelta.HasValue ) {
    				amountPerDelta = (tweenDuration > 0f) ? (1f / tweenDuration) : 1000f;
    			}
    			return amountPerDelta.Value;
    		}
    	}

    	public float Duration {
    		get { return tweenDuration; }
    		set {
    			value = Mathf.Max( 0f, value );
    			if ( tweenDuration != value ) {
    				var sign = Mathf.Sign( AmountPerDelta );
    				tweenDuration = value;
    				amountPerDelta = ((tweenDuration > 0f) ? (1f / tweenDuration) : 1000f) * sign;
    			}
    		}
    	}

    	public PlayDirection Direction => (AmountPerDelta < 0f) ? PlayDirection.Reverse : PlayDirection.Forward;

    	public float Delay {
    		get { return startDelay; }
    		set { startDelay = value; }
    	}

    	public void ResetDelay() {
    		startDelay = 0f;
    		startTime = Time.time;
    	}

    	public Method TweenMethod {
    		get { return tweenMethod; }
    		set { tweenMethod = value; }
    	}

    	public Style TweenStyle {
    		get { return tweenStyle; }
    		set { tweenStyle = value; }
    	}

    	public int CycleCount {
    		get { return cycleCount; }
    		set {
    			value = Mathf.Max( 0, value );
    			if ( cycleCount != value ) {
    				cycleCount = value;
    				elapsedСycle = 0;
    			}
    		}
    	}

        public bool IsWork => enabled && started && !paused;

    	public bool IsPaused {
    		get { return paused; }
    		set { paused = value; }
    	}

    	public float TweenFactor {
    		get { return factor; }
    		set { factor = Mathf.Clamp01( value ); }
    	}

    	public TweeningParameter[] this[ TweeningParameter.Type filter ] {
    		get {
    			var result = new TweeningParameter[ 0 ];
    			foreach ( var parameter in Parameters ) {
    				if ( parameter.ParameterType.Equals( filter ) ) {
    					ArrayTools.Add( ref result, parameter );
    				}
    			}
    			return result;
    		}
    	}

    	public T GetOrAddParameter<T>( TweeningParameter.Type filter ) where T : TweeningParameter {
    		foreach ( var parameter in Parameters ) {
    			if ( (parameter is T) && parameter.ParameterType.Equals( filter ) ) {
    				return ( T )parameter;
    			}
    		}
    		return AddParameter<T>();
    	}

    	public T AddParameter<T>() where T : TweeningParameter {
    		var result = gameObject.AddComponent<T>();
    		result.hideFlags = HideFlags.HideInInspector;
    		result.SetSupportTypes();
    		ArrayTools.Clear( ref parameters );
    		return result;
    	}

    	public TweeningParameter AddParameter( Type type ) {
    		var newParameter = ( TweeningParameter )gameObject.AddComponent( type );
    		newParameter.hideFlags = HideFlags.HideInInspector;
    		newParameter.SetSupportTypes();
            ArrayTools.Clear( ref parameters );
    		return newParameter;
    	}

    	void Update() {
    		if ( paused ) {
    			return;
    		}
    		var delta = Time.deltaTime;
    		var time = Time.time;
    		if ( !started ) {
    			started = true;
    			startTime = time + startDelay;
    		}
    		if ( time < startTime ) {
    			return;
    		}
    		factor += AmountPerDelta * delta;
    		switch ( tweenStyle ) {
    		case Style.Loop:
    			if ( factor > 1f ) {
    				factor -= Mathf.Floor( factor );
    				elapsedСycle++;
    			}
    			break;
    		case Style.PingPong:
    			if ( factor > 1f ) {
    				factor = 1f - (factor - Mathf.Floor( factor ));
    				amountPerDelta = -AmountPerDelta;
    				elapsedСycle++;
    				break;
    			} 
    			if ( factor < 0f ) {
    				factor = -factor;
    				factor -= Mathf.Floor( factor );
    				amountPerDelta = -AmountPerDelta;
    				elapsedСycle++;
    				break;
    			}
    			break;
    		}
            var isDone = (tweenDuration == 0f) || (tweenStyle.Equals( Style.Once ) && ((factor > 1f) || (factor < 0f))) || (!tweenStyle.Equals( Style.Once ) && ((cycleCount > 0) && (elapsedСycle >= cycleCount)));
    		if ( isDone ) {
    			elapsedСycle = 0;
    			if ( (factor < 1f) && (factor > 0f) ) {
    				factor = Mathf.Round( factor );
    			} else {
    				factor = Mathf.Clamp01( factor );
    			}
    			enabled = started = paused = false;
    		}
    		Sample( factor, isDone );
    		if ( isDone ) {
    			OnFinished.SafeInvoke( this );
    		}
    	}

    	void Sample( float factorValue, bool isDone ) {
    		var value = Mathf.Clamp01( factorValue );
    		switch ( tweenMethod ) {
    		case Method.EaseIn:
    			value = 1f - Mathf.Sin( 0.5f * Mathf.PI * (1f - value) );
    			value *= value;
    			break;
    		case Method.EaseOut:
    			value = Mathf.Sin( 0.5f * Mathf.PI * value );
    			value = 1f - value;
    			value = 1f - value * value;
    			break;
    		case Method.EaseInOut:
    			const float PIx2 = Mathf.PI * 2f;
    			value = value - Mathf.Sin( value * PIx2 ) / PIx2;
    			value = value * 2f - 1f;
    			var sign = Mathf.Sign( value );
    			value = 1f - Mathf.Abs( value );
    			value = 1f - value * value;
    			value = sign * value * 0.5f + 0.5f;
    			break;
    		}
    		foreach ( var parameter in Parameters ) {
    			parameter.Sample( value, isDone );
    		}
    	}

    	public void Play( PlayDirection direction ) {
    		amountPerDelta = Mathf.Abs( AmountPerDelta );
    		if ( direction.Equals( PlayDirection.Reverse ) ) {
    			amountPerDelta = -AmountPerDelta;
    		}
    		enabled = true;
    		Update();
    	}

    	public void PlayForward() => Play( PlayDirection.Forward );

    	public void PlayReverse() => Play( PlayDirection.Reverse );

        public IEnumerator PlayForwardAndWaitDone() {
            var isDone = false;
            Action<TweensPlayer> done = t => isDone = true;
            OnFinished += done;
            PlayForward();
            yield return new WaitWhile( () => !isDone );
            OnFinished -= done;
        }

        public IEnumerator PlayReverseAndWaitDone() {
            var isDone = false;
            Action<TweensPlayer> done = t => isDone = true;
            OnFinished += done;
            PlayReverse();
            yield return new WaitWhile( () => !isDone );
            OnFinished -= done;
        }

        public IEnumerator WaitDone()
        {
            var isDone = false;
            Action<TweensPlayer> done = t => isDone = true;
            OnFinished += done;
            yield return new WaitWhile(() => !isDone);
            OnFinished -= done;
        }

    	public void ResetForPlaying( PlayDirection direction ) {
    		if ( direction.Equals( PlayDirection.Forward ) ) {
    			ResetForForwardPlaying();
    			return;
    		}
    		if ( direction.Equals( PlayDirection.Reverse ) ) {
    			ResetForReversePlaying();
    			return;
    		}
    	}

    	public void ResetForForwardPlaying() {
    		amountPerDelta = Mathf.Abs( AmountPerDelta );
    		factor = 0f;
    		enabled = started = paused = false;
    		Sample( factor, false );
    	}

    	public void ResetForReversePlaying() {
    		amountPerDelta = -Mathf.Abs( AmountPerDelta );
    		factor = 1f;
    		enabled = started = paused = false;
    		Sample( factor, false );
    	}

    	public static TweensPlayer Get( GameObject target, float duration ) {
    		var player = target.GetComponent<TweensPlayer>() ?? target.AddComponent<TweensPlayer>();
    		player.tweenDuration = duration;
    		player.tweenStyle = Style.Once;
    		return player;
    	}


    #if UNITY_EDITOR
    	public void DrawInspector() {
            if ( enabled && !Application.isPlaying ) {
                EditorTools.DrawInBox( Color.yellow, () => {
                    var style = new GUIStyle( EditorStyles.boldLabel ) {
                        alignment = TextAnchor.MiddleCenter,
                        wordWrap = true
                    };
                    EditorTools.DrawLabel( "It will playing automatically after load scene in play mode! (Uncheck enable if you need)", style );
                } );
            }
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Method", 70f );
    			TweenMethod = ( Method )EditorGUILayout.EnumPopup( tweenMethod );
    		} );
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Style", 70f );
    			TweenStyle = ( Style )EditorGUILayout.EnumPopup( tweenStyle );
    			if ( tweenStyle != Style.Once ) {
    				EditorTools.DrawLabel( "Cycles", 50f );
    				CycleCount = EditorGUILayout.IntField( cycleCount );
    			}
    		} );
    		EditorTools.DrawInLine( () => {
    			EditorTools.DrawLabel( "Duration", 70f );
    			Duration = EditorGUILayout.FloatField( tweenDuration );
    			EditorTools.DrawLabel( "Delay", 50f );
    			startDelay = EditorGUILayout.FloatField( startDelay );
    		} );
    		EditorTools.DrawInLine( () => {
    			var parametersForCreate = new [] {
    				typeof( TransformTweening ),
    				typeof( ColorTweening ),
    				typeof( AlphaTweening ),
    				typeof( RectSizeTweening ),
                    typeof( FontSizeTweening ),
    				typeof( ScaleTweening ),
    				typeof( PositionTweening ),
    				typeof( ActiveTweening ),
                    typeof( WheelFrictionsTweening )
    			};
    			EditorTools.DrawLabel( "New Parameter", 100f );
                EditorTools.DrawSelector( "Select for Add...", Color.green, parametersForCreate, parameter => parameter.Name, selected => AddParameter( selected ) );
    		} );
    		parameters = GetComponents<TweeningParameter>();
            ArrayTools.DoTimes( () => parameters.Length, index => {
                parameters[ index ].hideFlags = HideFlags.HideInInspector;
    			EditorTools.DrawInBox( () => {
                    if ( parameters[ index ].needRemove ) {
                        var colors = new[] { EditorGUIUtility.isProSkin ? Color.yellow : Color.black, Color.red, Color.green };
    					EditorTools.DrawQuestionDialogInLine( "Remove this parameter?", 110f, () => {
                            DestroyImmediate( parameters[ index ] );
                            ArrayTools.RemoveAt( ref parameters, index );
                            index--;
    					}, () => {
                            parameters[ index ].needRemove = false;
                        }, colors );
    				} else {
    					EditorTools.DrawInLine( () => {
                            EditorTools.DrawButton( "R", "Remove", () => parameters[ index ].needRemove = true, EditorStyles.miniButton, Color.red, 20f );
                            parameters[ index ].DrawTitle();
    					} );
                        parameters[ index ].DrawInspector();
    				}
    			} );
                return index;
            } );
    	}
    #endif
    }
}