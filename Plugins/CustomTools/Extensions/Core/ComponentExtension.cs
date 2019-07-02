namespace CustomTools.Extensions.Core.Component {

	public static class ComponentExtension {

        public static T AddComponent<T>( this UnityEngine.Component c ) where T : UnityEngine.Component {
            return c.gameObject.AddComponent<T>();
        }

		public static T GetOrAddComponent<T>( this UnityEngine.Component c ) where T : UnityEngine.Component {
			return c.GetComponent<T>() ?? c.gameObject.AddComponent<T>();
		}

        public static void Activate( this UnityEngine.Component c ) {
            c.gameObject.SetActive( true );
        }

        public static void Deactivate( this UnityEngine.Component c ) {
            c.gameObject.SetActive( false );
        }

        public static bool ExistComponent<T>( this UnityEngine.Component c ) where T : UnityEngine.Component {
            return !c.GetComponent<T>().IsNull();
        }

        public static bool ExistComponentInParent<T>( this UnityEngine.Component c ) where T : UnityEngine.Component {
            return !c.GetComponentInParent<T>().IsNull();
        }

        public static bool ExistComponentInChildren<T>( this UnityEngine.Component c ) where T : UnityEngine.Component {
            return !c.GetComponentInChildren<T>().IsNull();
        }
	}
}