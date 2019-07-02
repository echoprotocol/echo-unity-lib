using CustomTools.Extensions.Core;


namespace CustomTools {

    public sealed class WrappedArray<T> {

		T[] array = new T[ 0 ];

		
		public WrappedArray() { }

		WrappedArray( T[] array ) {
			this.array = array;
		}
		
		public WrappedArray( int capacity ) {
			array = new T[ capacity ];
		}
		
		public WrappedArray( T fillValue ) {
			for ( var i = 0; i < array.Length; i++ ) {
				array[ i ] = fillValue;
			}
		}
		
		public int Length {
			get { return array.Length; }
		}

		public bool IsEmpty {
			get { return array.Length.Equals( 0 ); }
		}
		
		public T this[ int index ] {
			get {
				if ( (index < 0) || (index > array.Length) ) {
					throw new System.ArgumentOutOfRangeException();
				}
				return array[ index ];
			}
			set {
				if ( (index < 0) || (index > array.Length) ) {
					throw new System.ArgumentOutOfRangeException();
				}
				array[ index ] = value;
			}
		}
		
		public T Add( T item ) {
			System.Array.Resize( ref array, (array.Length + 1) );
			return array[ array.Length - 1 ] = item;
		}

		public void AddRange( T[] range ) {
			if ( range.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var start = array.Length;
			System.Array.Resize( ref array, (array.Length + range.Length) );
			for ( var i = start; i < (range.Length + start); i++ ) {
				array[ i ] = range[ i - start ];
			}
		}

		public void Insert( int index, T item ) {
			if ( (index < 0) || (index > array.Length) ) {
				throw new System.ArgumentOutOfRangeException();
			}
			System.Array.Resize( ref array, (array.Length + 1) );
			for ( var i = (array.Length - 1); i > index; i-- ) {
				array[ i ] = array[ i - 1 ];
			}
			array[ index ] = item;
		}

		public void InsertRange( int index, T[] range ) {
			if ( range.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( (index < 0) || (index > array.Length) ) {
				throw new System.ArgumentOutOfRangeException();
			}
			System.Array.Resize( ref array, (array.Length + range.Length) );
			for ( var i = (array.Length - 1); i > (index + range.Length); i-- ) {
				array[ i ] = array[ i - range.Length - 1 ];
			}
			for ( var i = index; i < (range.Length + index); i++ ) {
				array[ i ] = range[ i - index ];
			}
		}

		public void RemoveAt( int index ) {
			if ( (index < 0) || (index >= array.Length) ) {
				throw new System.ArgumentOutOfRangeException();
			}
			for ( var i = index; i < (array.Length - 1); i++ ) {
				array[ i ] = array[ i + 1 ];
			}
			if ( array.Length > 0 ) {
				System.Array.Resize( ref array, (array.Length - 1) );
			}
		}

		public bool Remove( T item ) {
			var index = System.Array.IndexOf( array, item );
			if ( index >= 0 ) {
                RemoveAt( index );
				return true;
			}
			return false;
		}

		public int RemoveAll( System.Predicate<T> match ) {
			if ( match.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var result = 0;
			for ( var i = 0; i < array.Length; i++ ) {
				if ( match.Invoke( array[ i ] ) ) {
					RemoveAt( i );
					result++;
					i--;
				}
			}
			return result;
		}

		public void Clear() {
			System.Array.Resize( ref array, 0 );
		}

		public WrappedArray<T> Fill( T value ) {
			for ( var i = 0; i < array.Length; i++ ) {
				array[ i ] = value;
			}
			return this;
		}

		public WrappedArray<T> Swap( int fromIndex, int toIndex ) {
			if ( (fromIndex < 0) || (fromIndex >= array.Length) ) {
				throw new System.ArgumentOutOfRangeException();
			}
			if ( (toIndex < 0) || (toIndex >= array.Length) ) {
				throw new System.ArgumentOutOfRangeException();
			}
			var temp = array[ fromIndex ];
			array[ fromIndex ] = array[ toIndex ];
			array[ toIndex ] = temp;
			return this;
		}

		public WrappedArray<T> Sort( System.Comparison<T> comparison = null ) {
			if ( comparison.IsNull() ) {
				System.Array.Sort( array );
			} else {
				System.Array.Sort( array, comparison );
			}
			return this;
		}

		public WrappedArray<T> Concat( params T[][] range ) {
			if ( range.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var newSize = array.Length;
			for ( var i = 0; i < range.Length; i++ ) {
				newSize += range[ i ].Or( new T[ 0 ] ).Length;
			}
			var result = new WrappedArray<T>( newSize );
			for ( var i = 0; i < array.Length; i++ ) {
				result.array[ i ] = array[ i ];
			}
			var offset = array.Length;
			for ( var i = 0; i < range.Length; i++ ) {
				for ( var j = 0; j < range[ i ].Or( new T[ 0 ] ).Length; j++ ) {
					result.array[ offset + j ] = range[ i ][ j ];
				}
				offset += range[ i ].Or( new T[ 0 ] ).Length;
			}
			return result;
		}

		public WrappedArray<T> Concat( params WrappedArray<T>[] range ) {
			if ( range.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var newSize = array.Length;
			for ( var i = 0; i < range.Length; i++ ) {
				newSize += range[ i ].Or( new WrappedArray<T>() ).array.Length;
			}
			var result = new WrappedArray<T>( newSize );
			for ( var i = 0; i < array.Length; i++ ) {
				result.array[ i ] = array[ i ];
			}
			var offset = array.Length;
			for ( var i = 0; i < range.Length; i++ ) {
				for ( var j = 0; j < range[ i ].Or( new WrappedArray<T>() ).array.Length; j++ ) {
					result.array[ offset + j ] = range[ i ].array[ j ];
				}
				offset += range[ i ].Or( new WrappedArray<T>() ).array.Length;
			}
			return result;
		}

		public WrappedArray<T> Slice( int fromIndex, int count ) {
			if ( (fromIndex < 0) || (fromIndex >= array.Length) ) {
				throw new System.ArgumentOutOfRangeException( "fromIndex" );
			}
			if ( (count <= 0) || (count > array.Length) ) {
				throw new System.ArgumentOutOfRangeException( "count" );
			}
			if ( fromIndex >= count ) {
				throw new System.ArgumentException( "fromIndex >= count" );
			}
			var result = new WrappedArray<T>( count - fromIndex );
			for ( var i = fromIndex; i < count; i++ ) {
				result.array[ i - fromIndex ] = array[ i ];
			}
			return result;
		}

		public WrappedArray<T> Slice( int fromIndex = 0 ) {
			return Slice( fromIndex, Length );
		}

		public WrappedArray<T> Copy() {
			var result = new WrappedArray<T>( array.Length );
			for ( var i = 0; i < array.Length; i++ ) {
				result.array[ i ] = array[ i ];
			}
			return result;
		}

		public WrappedArray<U> ConvertAll<U>( System.Converter<T, U> converter ) {
			return new WrappedArray<U>( (array.Length > 0) ? System.Array.ConvertAll( array, converter ) : new U[ 0 ] );
		}

		public bool Contains( T item ) {
			return System.Array.IndexOf( array, item ) >= 0;
		}

		public int IndexOf( T item ) {
			return System.Array.IndexOf( array, item );
		}

		public bool Exists( System.Predicate<T> match ) {
			if ( match.IsNull() ) {
				throw new System.NullReferenceException();
			}
			for ( var i = 0; i < array.Length; i++ ) {
				if ( match.Invoke( array[ i ] ) ) {
					return true;
				}
			}
			return false;
		}

		public T Find( System.Predicate<T> match ) {
			if ( match.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.Find( array, match );
		}

		public WrappedArray<T> FindAll( System.Predicate<T> match ) {
			if ( match.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return new WrappedArray<T>( System.Array.FindAll( array, match ) );
		}

		public int FindIndex( System.Predicate<T> match ) {
			if ( match.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.FindIndex( array, match );
		}

		public T First() {
			if ( IsEmpty ) {
				if ( typeof( T ).IsClass ) {
					return default( T );
				}
				throw new System.ArgumentOutOfRangeException();
			}
			return array[ 0 ];
		}

		public T Last() {
			if ( IsEmpty ) {
				if ( typeof( T ).IsClass ) {
					return default( T );
				}
				throw new System.ArgumentOutOfRangeException();
			}
			return array[ array.Length - 1 ];
		}

		public T Next( T item ) {
			var index = System.Array.IndexOf( array, item );
			if ( (index >= 0) && (index < (array.Length - 1)) ) {
				return array[ index + 1 ];
			}
			if ( typeof( T ).IsClass ) {
				return default( T );
			}
			throw new System.ArgumentOutOfRangeException();
		}

		public T NextLoop( T item ) {
			var index = System.Array.IndexOf( array, item );
			if ( index >= 0 ) {
				return array[ (index + 1) % array.Length ];
			}
			if ( typeof( T ).IsClass ) {
				return default( T );
			}
			throw new System.ArgumentOutOfRangeException();
		}

		public T Previous( T item ) {
			var index = System.Array.IndexOf( array, item );
			if ( index > 0 ) {
				return array[ index - 1 ];
			}
			if ( typeof( T ).IsClass ) {
				return default( T );
			}
			throw new System.ArgumentOutOfRangeException();
		}

		public T PreviousLoop( T item ) {
			var index = System.Array.IndexOf( array, item );
			if ( index >= 0 ) {
				return array[ (index - 1 + array.Length) % array.Length ];
			}
			if ( typeof( T ).IsClass ) {
				return default( T );
			}
			throw new System.ArgumentOutOfRangeException();
		}

		public string ToString( string separator = null ) {
			var builder = new System.Text.StringBuilder();
			for ( var i = 0; i < array.Length; i++ ) {
				if ( typeof( T ).IsClass && System.Collections.Generic.EqualityComparer<T>.Default.Equals( array[ i ], default( T ) ) ) {
					builder.Append( "null" );
				} else {
					builder.Append( array[ i ].ToString() );
				}
				if ( !separator.IsNullOrEmpty() && (i < (array.Length - 1)) ) {
					builder.Append( separator );
				}
			}
			return builder.ToString();
		}

		public void DoForAll( System.Action<T> action ) {
			if ( !action.IsNull() ) {
				for ( var i = 0; i < array.Length; i++ ) {
					action.Invoke( array[ i ] );
				}
			}
		}
	}
}