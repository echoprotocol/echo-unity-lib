using CustomTools.Extensions.Core.Array;


namespace CustomTools.Extensions.Core.KeyCode {

    public static class KeyCodeExtension {

        public static bool AllKey( this UnityEngine.KeyCode[] keys ) {
            return keys.All( UnityEngine.Input.GetKey );
		}

        public static bool AllKeyUp( this UnityEngine.KeyCode[] keys ) {
            return keys.All( UnityEngine.Input.GetKeyUp );
        }

        public static bool AllKeyDown( this UnityEngine.KeyCode[] keys ) {
            return keys.All( UnityEngine.Input.GetKeyDown );
        }

        public static bool AnyKey( this UnityEngine.KeyCode[] keys ) {
            return keys.Any( UnityEngine.Input.GetKey );
        }

        public static bool AnyKeyUp( this UnityEngine.KeyCode[] keys ) {
            return keys.Any( UnityEngine.Input.GetKeyUp );
        }

        public static bool AnyKeyDown( this UnityEngine.KeyCode[] keys ) {
            return keys.Any( UnityEngine.Input.GetKeyDown );
        }
	}
}