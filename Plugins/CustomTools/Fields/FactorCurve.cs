using UnityEngine;


namespace CustomTools.Fields {
    
    [System.Serializable]
    public sealed class FactorCurve {
        
        [SerializeField] AnimationCurve curve;
        [SerializeField] float factor = 1f;


        public float Evaluate( float time ) => curve.Evaluate( time ) * factor;
    }
}