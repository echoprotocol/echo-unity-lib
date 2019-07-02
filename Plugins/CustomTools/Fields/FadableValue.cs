using CustomTools.Attributes;
using UnityEngine;


namespace CustomTools.Fields {

    [System.Serializable]
    public sealed class FadableValue {

        enum Direction {

            None,
            Out,
            In
        }


        [SerializeField, Label] float _value;

        [SerializeField] float maxValue;
        [SerializeField] AnimationCurve fadeOutValueFactor;
        [SerializeField] float fadeOutTime;
        [SerializeField] AnimationCurve fadeInValueFactor;
        [SerializeField] float fadeInTime;

        float elapsedTime;
        Direction fadeDirection = Direction.None;


        public FadableValue() {
            maxValue = 1f;
            fadeOutValueFactor = new AnimationCurve( new Keyframe( 0f, 0f ), new Keyframe( 1f, 1f ) );
            fadeOutTime = 1f;
            fadeInValueFactor = new AnimationCurve( new Keyframe( 0f, 1f ), new Keyframe( 1f, 0f ) );
            fadeInTime = 1f;
        }

        public void FadeOutSample() {
            elapsedTime = fadeDirection.Equals( Direction.Out ) ? Mathf.Min( fadeOutTime, elapsedTime + Time.deltaTime ) : 0f;
            fadeDirection = Direction.Out;
            _value = Value;
        }

        public void FadeInSample() {
            elapsedTime = fadeDirection.Equals( Direction.In ) ? Mathf.Min( fadeInTime, elapsedTime + Time.deltaTime ) : 0f;
            fadeDirection = Direction.In;
            _value = Value;
        }

        public float Value {
            get {
                switch ( fadeDirection ) {
                    case Direction.Out:
                        return maxValue * fadeOutValueFactor.Evaluate( elapsedTime / fadeOutTime );
                    case Direction.In:
                        return maxValue * fadeInValueFactor.Evaluate( elapsedTime / fadeInTime );
                    default:
                        return 0f;
                }
            }
        }
    }
}