using CustomTools.Fields;


namespace CustomTools.Extensions.Core.WheelCollider {

    public static class WheelColliderExtension {

        public static void LoadFrictions( this UnityEngine.WheelCollider wheel, WheelFrictions frictions ) => frictions.Apply( wheel );

        public static WheelFrictions SaveFrictions( this UnityEngine.WheelCollider wheel ) => new WheelFrictions( wheel );

        public static void LerpFrictions( this UnityEngine.WheelCollider wheel, WheelFrictions from, WheelFrictions to, float factor ) => WheelFrictions.LerpFrictions( wheel, from, to, factor );
	}
}