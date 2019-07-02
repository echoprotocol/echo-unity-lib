namespace CustomTools.Extensions.Core.Enum {

	public static class EnumExtension {

		public static bool Contains( this System.Enum e, System.Enum value ) {
			if ( e.GetType() != value.GetType() ) {
				throw new System.ArgumentException( "Type mismatch" );
			}
			var typeCode = e.GetTypeCode();
			switch ( typeCode ) {
			case System.TypeCode.SByte:
                return (System.Convert.ToSByte( e ) & System.Convert.ToSByte( value )) != 0;
			case System.TypeCode.Int16:
                return (System.Convert.ToInt16( e ) & System.Convert.ToInt16( value )) != 0;
			case System.TypeCode.Int32:
                return (System.Convert.ToInt32( e ) & System.Convert.ToInt32( value )) != 0;
			case System.TypeCode.Int64:
				return (System.Convert.ToInt64( e ) & System.Convert.ToInt64( value )) != 0;
			case System.TypeCode.Byte:
                return (System.Convert.ToByte( e ) & System.Convert.ToByte( value )) != 0;
			case System.TypeCode.UInt16:
                return (System.Convert.ToUInt16( e ) & System.Convert.ToUInt16( value )) != 0;
			case System.TypeCode.UInt32:
                return (System.Convert.ToUInt32( e ) & System.Convert.ToUInt32( value )) != 0;
			case System.TypeCode.UInt64:
				return (System.Convert.ToUInt64( e ) & System.Convert.ToUInt64( value )) != 0;
			}
			throw new System.InvalidOperationException( string.Format( "The comparison of the type {0} is not implemented.", e.GetType().Name ) );
		}

		public static bool ContainsAny( this System.Enum e, params System.Enum[] values ) {
			if ( values.IsNull() ) {
				throw new System.NullReferenceException();
			}
			foreach ( var value in values ) {
				if ( e.Contains( value ) ) {
					return true;
				}
			}
			return false;
		}

		public static bool ContainsAll( this System.Enum e, params System.Enum[] values ) {
			if ( values.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var contains = true; 
			foreach ( var value in values ) {
				contains &= e.Contains( value );
			}
			return contains;
		}
	}
}