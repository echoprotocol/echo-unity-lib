using UnityEngine;


namespace CustomTools.Extensions.Core.Vector {

	public static class VectorExtension {

		public static Vector3 ToVector3( this Vector2 v, float z = 0f ) {
			var cast = ( Vector3 )v;
			cast.z = z;
			return cast;
		}

		public static Vector4 ToVector4( this Vector2 v, float z = 0f, float w = 0f ) {
			var cast = ( Vector4 )v;
			cast.z = z;
			cast.w = w;
			return cast;
		}

		public static Vector2 ToVector2( this Vector3 v ) {
			return v;
		}

		public static Vector4 ToVector4( this Vector3 v, float w = 0f ) {
			var cast = ( Vector4 )v;
			cast.w = w;
			return cast;
		}

		public static Vector2 ToVector2( this Vector4 v ) {
			return v;
		}

		public static Vector3 ToVector3( this Vector4 v ) {
			return v;
		}

		public static Vector2 GetSwap( this Vector2 v ) {
			return new Vector2( v.y, v.x );
		}
	}
}