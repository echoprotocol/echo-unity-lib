using CustomTools.Extensions.Core.Transform;


namespace CustomTools.Extensions.Core.GameObject {

	public static class GameObjectExtension {

        public static void Activate( this UnityEngine.GameObject go ) {
            go.SetActive( true );
        }

        public static void Deactivate( this UnityEngine.GameObject go ) {
            go.SetActive( false );
        }

		public static void ActivateHierarchy( this UnityEngine.GameObject go ) {
			go.transform.DoForAllHierarchy( t => t.gameObject.SetActive( true ) );
		}

		public static void DeactivateHierarchy( this UnityEngine.GameObject go ) {
			go.transform.DoForAllHierarchy( t => t.gameObject.SetActive( false ) );
		}

		public static bool HasRigidbody( this UnityEngine.GameObject go ) {
			return !go.GetComponent<UnityEngine.Rigidbody>().IsNull();
		}

		public static bool HasCollider( this UnityEngine.GameObject go ) {
			return !go.GetComponent<UnityEngine.Collider>().IsNull();
		}

		public static bool HasAnimation( this UnityEngine.GameObject go ) {
			return !go.GetComponent<UnityEngine.Animation>().IsNull();
		}

		public static bool HasAnimator( this UnityEngine.GameObject go ) {
			return !go.GetComponent<UnityEngine.Animator>().IsNull();
		}
	}
}