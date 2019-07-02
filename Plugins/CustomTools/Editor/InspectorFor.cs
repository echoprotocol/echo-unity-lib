namespace CustomTools.Editor {

	public abstract class InspectorFor<T> : UnityEditor.Editor where T : ICustomInspector {

		public override void OnInspectorGUI() => (target as ICustomInspector).DrawInspector();
	}
}