namespace CustomTools.Extensions.Core.Action {

	public static class ActionExtension {

		public static bool SafeInvoke( this System.Action h ) {
			if ( !h.IsNull() ) {
				h.Invoke();
				return true;
			}
			return false;
		}

		public static bool SafeInvoke<T>( this System.Action<T> h, T arg ) {
			if ( !h.IsNull() ) {
				h.Invoke( arg );
				return true;
			}
			return false;
		}

		public static bool SafeInvoke<T1, T2>( this System.Action<T1, T2> h, T1 arg1, T2 arg2 ) {
			if ( !h.IsNull() ) {
				h.Invoke( arg1, arg2 );
				return true;
			}
			return false;
		}

		public static bool SafeInvoke<T1, T2, T3>( this System.Action<T1, T2, T3> h, T1 arg1, T2 arg2, T3 arg3 ) {
			if ( !h.IsNull() ) {
				h.Invoke( arg1, arg2, arg3 );
				return true;
			}
			return false;
		}

		public static bool SafeInvoke<T1, T2, T3, T4>( this System.Action<T1, T2, T3, T4> h, T1 arg1, T2 arg2, T3 arg3, T4 arg4 ) {
			if ( !h.IsNull() ) {
				h.Invoke( arg1, arg2, arg3, arg4 );
				return true;
			}
			return false;
		}

		public static TResult SafeInvoke<TResult>( this System.Func<TResult> h, TResult defaultValue ) {
			return !h.IsNull() ? h.Invoke() : defaultValue;
		}

		public static TResult SafeInvoke<T, TResult>( this System.Func<T, TResult> h, T arg, TResult defaultValue ) {
			return !h.IsNull() ? h.Invoke( arg ) : defaultValue;
		}

		public static TResult SafeInvoke<T1, T2, TResult>( this System.Func<T1, T2, TResult> h, T1 arg1, T2 arg2, TResult defaultValue ) {
			return !h.IsNull() ? h.Invoke( arg1, arg2 ) : defaultValue;
		}
		public static TResult SafeInvoke<T1, T2, T3, TResult>( this System.Func<T1, T2, T3, TResult> h, T1 arg1, T2 arg2, T3 arg3, TResult defaultValue ) {
			return !h.IsNull() ? h.Invoke( arg1, arg2, arg3 ) : defaultValue;
		}

		public static TResult SafeInvoke<T1, T2, T3, T4, TResult>( this System.Func<T1, T2, T3, T4, TResult> h, T1 arg1, T2 arg2, T3 arg3, T4 arg4, TResult defaultValue ) {
			return !h.IsNull() ? h.Invoke( arg1, arg2, arg3, arg4 ) : defaultValue;
		}
	}
}