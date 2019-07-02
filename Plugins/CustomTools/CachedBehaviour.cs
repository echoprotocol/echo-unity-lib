using CustomTools.Extensions.Core;
using UnityEngine;


namespace CustomTools.CachedBehaviour {

    public interface ICachedBehaviour {

        Rigidbody CachedRigidbody { get; }
        Collider CachedCollider { get; }
        Transform CachedTransform { get; }
    }


    public abstract class CachedMonoBehaviour : MonoBehaviour, ICachedBehaviour {

		Rigidbody cachedRigidbody;
		Collider cachedCollider;
		Transform cachedTransform;


		public Rigidbody CachedRigidbody {
			get {
                if ( cachedRigidbody.IsNull() ) {
					cachedRigidbody = GetComponent<Rigidbody>();
				}
				return cachedRigidbody;
			}
		}

		public Collider CachedCollider {
			get {
                if ( cachedCollider.IsNull() ) {
					cachedCollider = GetComponent<Collider>();
				}
				return cachedCollider;
			}
		}

		public Transform CachedTransform {
			get {
				if ( cachedTransform.IsNull() ) {
					cachedTransform = GetComponent<Transform>();
				}
				return cachedTransform;
			}
		}

		public Transform Parent {
			get { return CachedTransform.parent; }
			set { CachedTransform.parent = value; }
		}

        public Vector3 Forward {
            get { return CachedTransform.forward; }
        }

		public Vector3 Position {
			get { return CachedTransform.position; }
			set { CachedTransform.position = value; }
		}

		public Vector3 LocalPosition {
			get { return CachedTransform.localPosition; }
			set { CachedTransform.localPosition = value; }
		}

		public Quaternion Rotation {
			get { return CachedTransform.rotation; }
			set { CachedTransform.rotation = value; }
		}

		public Quaternion LocalRotation {
			get { return CachedTransform.localRotation; }
			set { CachedTransform.localRotation = value; }
		}

		public Vector3 LocalScale {
			get { return CachedTransform.localScale; }
			set { CachedTransform.localScale = value; }
		}

		public void ResetLocal() {
			LocalPosition = Vector3.zero;
			LocalScale = Vector3.one;
			LocalRotation = Quaternion.identity;
		}
	}
}