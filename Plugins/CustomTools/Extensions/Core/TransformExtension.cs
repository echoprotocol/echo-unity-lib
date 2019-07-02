using CustomTools.Extensions.Core.Array;


namespace CustomTools.Extensions.Core.Transform {

	public static class TransformExtension {

		public static void SetPositionX( this UnityEngine.Transform t, float x ) {
			var position = t.position;
			position.x = x;
			t.position = position;
		}

		public static void SetPositionY( this UnityEngine.Transform t, float y ) {
			var position = t.position;
			position.y = y;
			t.position = position;
		}

		public static void SetPositionZ( this UnityEngine.Transform t, float z ) {
			var position = t.position;
			position.z = z;
			t.position = position;
		}

		public static void SetPositionXY( this UnityEngine.Transform t, float x, float y ) {
			var position = t.position;
			position.x = x;
			position.x = y;
			t.position = position;
		}

		public static void SetLocalPositionX( this UnityEngine.Transform t, float x ) {
			var localPosition = t.localPosition;
			localPosition.x = x;
			t.localPosition = localPosition;
		}

		public static void SetLocalPositionY( this UnityEngine.Transform t, float y ) {
			var localPosition = t.localPosition;
			localPosition.y = y;
			t.localPosition = localPosition;
		}

		public static void SetLocalPositionZ( this UnityEngine.Transform t, float z ) {
			var localPosition = t.localPosition;
			localPosition.z = z;
			t.localPosition = localPosition;
		}

		public static void SetLocalPositionXY( this UnityEngine.Transform t, float x, float y ) {
			var localPosition = t.localPosition;
			localPosition.x = x;
			localPosition.x = y;
			t.localPosition = localPosition;
		}

		public static void DoForAllHierarchy( this UnityEngine.Transform root, System.Action<UnityEngine.Transform> action ) {
			if ( !action.IsNull() ) {
				action( root );
				foreach ( UnityEngine.Transform child in root ) {
					child.DoForAllHierarchy( action );
				}
			}
		}

		public static void DoForOnlyChilds( this UnityEngine.Transform root, System.Action<UnityEngine.Transform> action ) {
			if ( !action.IsNull() ) {
				foreach ( UnityEngine.Transform child in root ) {
					action( child );
					child.DoForOnlyChilds( action );
				}
			}
		}

        public static void DestroyAllChilds( this UnityEngine.Transform root ) {
            var childs = new UnityEngine.GameObject[ 0 ];
            foreach ( UnityEngine.Transform child in root ) {
                ArrayTools.Add( ref childs, child.gameObject );
            }
            childs.DoForAll( UnityEngine.Object.Destroy );
            ArrayTools.Clear( ref childs );
        }

        public static void ResetLocal( this UnityEngine.Transform t ) {
            t.localPosition = UnityEngine.Vector3.zero;
            t.localScale = UnityEngine.Vector3.one;
            t.localRotation = UnityEngine.Quaternion.identity;
		}

        public static void SetLayerForAllHierarchy( this UnityEngine.Transform root, UnityEngine.Transform layerSource, System.Type[] filter = null ) {
            if ( filter.IsNullOrEmpty() ) {
                root.gameObject.layer = layerSource.gameObject.layer;
            } else {
                foreach ( var type in filter ) {
                    if ( !root.GetComponent( type ).IsNull() ) {
                        root.gameObject.layer = layerSource.gameObject.layer;
                        break;
                    }
                }
            }
            foreach ( UnityEngine.Transform child in root ) {
                child.SetLayerForAllHierarchy( layerSource, filter );
            }
        }
    }
}