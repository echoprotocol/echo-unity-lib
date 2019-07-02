namespace CustomTools.Extensions.Core {

	public static class CoreExtension {

		public static bool IsNull<T>( this T n ) where T : class {
            return n == null || n.Equals( null );
		}

		public static string ToNullableString<T>( this T n ) where T : class {
			return n.IsNull() ? null : n.ToString();
		}

		public static T Or<T>( this T s, T defaultValue ) where T : class {
            return s.IsNull() ? defaultValue : s;
		}

		public static bool IsNullOrEmpty( this string s ) {
			return string.IsNullOrEmpty( s );
		}

		public static string OrEmpty( this string s ) {
			return s.Or( string.Empty );
		}

        public static bool IsArray( this object o ) {
            if ( o.IsNull() ) {
                throw new System.NullReferenceException();
            }
            return o.GetType().IsArray;
        }

        public static byte[] Clear( this byte[] a ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            for ( var i = 0; i < a.Length; i++ ) {
                a[ i ] = 0;
            }
            return a;
        }

        public static bool IsNumeric( this System.Type t ) {
            switch ( System.Type.GetTypeCode( t ) ) {
            case System.TypeCode.Byte:
            case System.TypeCode.SByte:
            case System.TypeCode.UInt16:
            case System.TypeCode.UInt32:
            case System.TypeCode.UInt64:
            case System.TypeCode.Int16:
            case System.TypeCode.Int32:
            case System.TypeCode.Int64:
            case System.TypeCode.Decimal:
            case System.TypeCode.Double:
            case System.TypeCode.Single:
                return true;
            default:
                return false;
            }
        }

        public static bool IsFloating( this System.Type t ) {
            switch ( System.Type.GetTypeCode( t ) ) {
            case System.TypeCode.Double:
            case System.TypeCode.Single:
                return true;
            default:
                return false;
            }
        }
    }
}