namespace CustomTools {

	public interface ICustomInspector {
#if UNITY_EDITOR
		void DrawInspector();
#endif
	}
}