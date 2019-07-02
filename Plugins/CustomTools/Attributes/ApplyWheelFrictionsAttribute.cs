using UnityEngine;


namespace CustomTools.Attributes {

    public class ApplyWheelFrictionsAttribute : PropertyAttribute {

        public string ActionName { get; private set; }
        public string Title { get; private set; }

        public ApplyWheelFrictionsAttribute( string actionName, string title = null ) {
            ActionName = actionName;
            Title = title;
        }
    }
}