using UnityEngine;


namespace CustomTools.Attributes {

    public class LabelAttribute : PropertyAttribute {

        public float UpdateInterval { get; private set; }

        public LabelAttribute( float updateInterval ) {
            UpdateInterval = updateInterval;
        }

        public LabelAttribute() : this( 0f ) { }
    }
}