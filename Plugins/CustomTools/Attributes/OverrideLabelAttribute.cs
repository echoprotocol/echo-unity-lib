using UnityEngine;


namespace CustomTools.Attributes {

    public class OverrideLabelAttribute : PropertyAttribute {

        public string Title { get; private set; }

        public OverrideLabelAttribute( string title ) {
            Title = title;
        }
    }
}