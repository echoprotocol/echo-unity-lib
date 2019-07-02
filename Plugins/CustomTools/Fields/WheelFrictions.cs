using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace CustomTools.Fields {

    [System.Serializable]
    public sealed class WheelFrictions {

        [System.Serializable]
        public sealed class Friction {

            [SerializeField] float extremumSlip;
            [SerializeField] float extremumValue;
            [SerializeField] float asymptoteSlip;
            [SerializeField] float asymptoteValue;
            [SerializeField] float stiffness;


            Friction() { }
                
            public Friction( WheelFrictionCurve curve ) {
                extremumSlip = curve.extremumSlip;
                extremumValue = curve.extremumValue;
                asymptoteSlip = curve.asymptoteSlip;
                asymptoteValue = curve.asymptoteValue;
                stiffness = curve.stiffness;
            }

            public WheelFrictionCurve Apply( ref WheelFrictionCurve curve ) {
                curve.extremumSlip = extremumSlip;
                curve.extremumValue = extremumValue;
                curve.asymptoteSlip = asymptoteSlip;
                curve.asymptoteValue = asymptoteValue;
                curve.stiffness = stiffness;
                return curve;
            }

            public static WheelFrictionCurve LerpFriction( ref WheelFrictionCurve curve, Friction from, Friction to, float factor ) {
                curve.extremumSlip = Mathf.Lerp( from.extremumSlip, to.extremumSlip, factor );
                curve.extremumValue = Mathf.Lerp( from.extremumValue, to.extremumValue, factor );
                curve.asymptoteSlip = Mathf.Lerp( from.asymptoteSlip, to.asymptoteSlip, factor );
                curve.asymptoteValue = Mathf.Lerp( from.asymptoteValue, to.asymptoteValue, factor );
                curve.stiffness = Mathf.Lerp( from.stiffness, to.stiffness, factor );
                return curve;
            }

            public static Friction CreateForward() {
                return new Friction {
                    extremumSlip = 0.4f,
                    extremumValue = 1f,
                    asymptoteSlip = 0.8f,
                    asymptoteValue = 0.5f,
                    stiffness = 1f
                };
            }

            public static Friction CreateSideways() {
                return new Friction {
                    extremumSlip = 0.2f,
                    extremumValue = 1f,
                    asymptoteSlip = 0.5f,
                    asymptoteValue = 0.75f,
                    stiffness = 1f
                };
            }


        #if UNITY_EDITOR
            public void DrawGrid( string title, Rect gridSize, float rowHeight ) {
                var style = new GUIStyle( GUI.skin.box ) { alignment = TextAnchor.MiddleCenter };
                var cellRect = new Rect( gridSize ) { height = rowHeight };
                EditorGUI.LabelField( cellRect, new GUIContent( title ), new GUIStyle( style ) { fontStyle = FontStyle.Bold } );
                cellRect.y += rowHeight;
                cellRect.width = gridSize.width / 2f;
                EditorGUI.LabelField( cellRect, new GUIContent( "Force" ), style );
                cellRect.x += gridSize.width / 2f;
                EditorGUI.LabelField( cellRect, new GUIContent( "Slip" ), style );
                cellRect.y += rowHeight;
                cellRect.x -= gridSize.width / 2f;
                extremumValue = EditorGUI.FloatField( cellRect, extremumValue, style );
                cellRect.x += gridSize.width / 2f;
                extremumSlip = EditorGUI.FloatField( cellRect, extremumSlip, style );
                cellRect.y += rowHeight;
                cellRect.x -= gridSize.width / 2f;
                asymptoteValue = EditorGUI.FloatField( cellRect, asymptoteValue, style );
                cellRect.x += gridSize.width / 2f;
                asymptoteSlip = EditorGUI.FloatField( cellRect, asymptoteSlip, style );
                cellRect.y += rowHeight;
                cellRect.x -= gridSize.width / 2f;
                cellRect.width = gridSize.width;
                stiffness = EditorGUI.FloatField( cellRect, stiffness, style );
            }
        #endif
        }


        [SerializeField] Friction forward = Friction.CreateForward();
        [SerializeField] Friction sideways = Friction.CreateSideways();


        public Friction Forward => forward ?? (forward = Friction.CreateForward());

        public Friction Sideways => sideways ?? (sideways = Friction.CreateSideways());

        public WheelFrictions() { }

        public WheelFrictions( WheelCollider wheel ) {
            forward = new Friction( wheel.forwardFriction );
            sideways = new Friction( wheel.sidewaysFriction );
        } 

        public void Apply( WheelCollider destination ) {
            var friction = destination.forwardFriction;
            forward.Apply( ref friction );
            destination.forwardFriction = friction;
            friction = destination.sidewaysFriction;
            sideways.Apply( ref friction );
            destination.sidewaysFriction = friction;
        }

        public static void LerpFrictions( WheelCollider wheel, WheelFrictions from, WheelFrictions to, float factor ) {
            LerpForwardFriction( wheel, from, to, factor );
            LerpSidewaysFriction( wheel, from, to, factor );
        }

        public static void LerpForwardFriction( WheelCollider wheel, WheelFrictions from, WheelFrictions to, float factor ) {
            var friction = wheel.forwardFriction;
            Friction.LerpFriction( ref friction, from.forward, to.forward, factor );
            wheel.forwardFriction = friction;
        }

        public static void LerpSidewaysFriction( WheelCollider wheel, WheelFrictions from, WheelFrictions to, float factor ) {
            var friction = wheel.sidewaysFriction;
            Friction.LerpFriction( ref friction, from.sideways, to.sideways, factor );
            wheel.sidewaysFriction = friction;
        }


    #if UNITY_EDITOR
        public float DrawHeader( Rect fullGridRect, float rowHeight ) {
            var style = new GUIStyle( GUI.skin.box ) { alignment = TextAnchor.MiddleLeft };
            var extremumTitle = new GUIContent( "Extremum" );
            var asymptoteTitle = new GUIContent( "Asymptote" );
            var stiffnessTitle = new GUIContent( "Stiffness" );
            var cellWidth = Mathf.Max(
                Mathf.Max( style.CalcSize( extremumTitle ).x, style.CalcSize( asymptoteTitle ).x ),
                style.CalcSize( stiffnessTitle ).x
            );
            var cellRect = new Rect( fullGridRect ) { height = rowHeight * 2f, width = cellWidth };
            GUI.Box( cellRect, string.Empty );
            cellRect.height /= 2f;
            cellRect.y += rowHeight * 2f;
            EditorGUI.LabelField( cellRect, extremumTitle, style );
            cellRect.y += rowHeight;
            EditorGUI.LabelField( cellRect, asymptoteTitle, style );
            cellRect.y += rowHeight;
            EditorGUI.LabelField( cellRect, stiffnessTitle, style );
            return cellRect.width;
        }
    #endif
    }
}