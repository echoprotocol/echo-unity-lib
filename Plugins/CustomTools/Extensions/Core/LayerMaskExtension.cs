namespace CustomTools.Extensions.Core.LayerMask {

	public static class LayerMaskExtension {

		public static bool Contains( this UnityEngine.LayerMask mask, int layer ) {
			return ((1 << layer) & mask.value) != 0;
		}

		public static bool Contains( this UnityEngine.LayerMask mask, string layerName ) {
			return mask.Contains( UnityEngine.LayerMask.NameToLayer( layerName ) );
		}
	}
}