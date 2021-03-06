﻿using UnityEngine;


namespace CustomTools.Attributes {

    public class ButtonAttribute : PropertyAttribute {

        public string ActionName { get; private set; }
        public string Title { get; private set; }

        public ButtonAttribute( string actionName, string title = null ) {
            ActionName = actionName;
            Title = title;
        }
    }
}