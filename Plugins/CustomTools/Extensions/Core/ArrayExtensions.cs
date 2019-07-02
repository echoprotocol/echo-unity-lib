namespace CustomTools.Extensions.Core.Array {

    public static class ArrayTools {

        public static void Add<T>( ref T[] a, T b ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            System.Array.Resize( ref a, (a.Length + 1) );
            a[ a.Length - 1 ] = b;
        }

        public static void AddRange<T>( ref T[] a, T[] b ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( b.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var start = a.Length;
            System.Array.Resize( ref a, (a.Length + b.Length) );
            for ( var i = start; i < (b.Length + start); i++ ) {
                a[ i ] = b[ i - start ];
            }
        }

        public static void Insert<T>( ref T[] a, int index, T b ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( (index < 0) || (index > a.Length) ) {
                throw new System.ArgumentOutOfRangeException( nameof( index ) );
            }
            System.Array.Resize( ref a, (a.Length + 1) );
            for ( var i = (a.Length - 1); i > index; i-- ) {
                a[ i ] = a[ i - 1 ]; 
            }
            a[ index ] = b;
        }

        public static void InsertRange<T>( ref T[] a, int index, T[] b ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( b.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( (index < 0) || (index > a.Length) ) {
                throw new System.ArgumentOutOfRangeException( nameof( index ) );
            }
            System.Array.Resize( ref a, (a.Length + b.Length) );
            for ( var i = (a.Length - 1); i > (index + b.Length); i-- ) {
                a[ i ] = a[ i - b.Length - 1 ]; 
            }
            for ( var i = index; i < (b.Length + index); i++ ) {
                a[ i ] = b[ i - index ];
            }
        }

        public static bool Remove<T>( ref T[] a, T b ) where T : System.IEquatable<T> {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            for ( var i = 0; i < a.Length; i++ ) {
                if ( a[ i ].Equals( b ) ) {
                    RemoveAt( ref a, i );
                    return true;
                }
            }
            return false;
        }

        public static void RemoveAll<T>( ref T[] a, System.Predicate<T> m ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( m.IsNull() ) {
                throw new System.NullReferenceException();
            }
            for ( var i = 0; i < a.Length; i++ ) {
                if ( m.Invoke( a[ i ] ) ) {
                    RemoveAt( ref a, i );
                    i--;
                }
            }
        }

        public static void RemoveAt<T>( ref T[] a, int index ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( (index < 0) || (index >= a.Length) ) {
                throw new System.ArgumentOutOfRangeException( nameof( index ) );
            }
            for ( var i = index; i < (a.Length - 1); i++ ) {
                a[ i ] = a[ i + 1 ];
            }
            if ( a.Length > 0 ) {
                System.Array.Resize( ref a, (a.Length - 1) );
            }
        }

        public static void Clear<T>( ref T[] a ) {
            System.Array.Resize( ref a, 0 );
        }

        public static void DoTimes( System.Func<int> times, System.Action<int> action, bool forwardOrder = true ) {
            if ( !times.IsNull() && !action.IsNull() ) {
                var start = forwardOrder ? 0 : (times.Invoke() - 1);
                var offset = forwardOrder ? 1 : -1;
                for ( var i = start; (i >= 0) && (i < times.Invoke()); i += offset ) {
                    action.Invoke( i );
                }
            }
        }

        public static void DoTimes( System.Func<int> times, System.Func<int, int> action, bool forwardOrder = true ) {
            if ( !times.IsNull() && !action.IsNull() ) {
                var start = forwardOrder ? 0 : (times.Invoke() - 1);
                var offset = forwardOrder ? 1 : -1;
                for ( var i = start; (i >= 0) && (i < times.Invoke()); i += offset ) {
                    i = action.Invoke( i );
                }
            }
        }
    }


	public static class ArrayExtensions {

        public static System.Collections.Generic.IList<T> ToList<T>( this System.Collections.Generic.IEnumerable<T> e ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            return new System.Collections.Generic.List<T>( e );
        }

        public static object[] ToArray( this System.Collections.IList l ) {
            if ( l.IsNull() ) {
                return new object[ 0 ];
            }
            var r = new object[ l.Count ];
            for ( var i = 0; i < r.Length; i++ ) {
                r[ i ] = l[ i ];
            }
            return r;
        }

        public static T[] ToArray<T>( this System.Collections.Generic.IEnumerable<T> e ) {
			if ( e.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var list = new System.Collections.Generic.List<T>( e );
			var result = list.ToArray();
			list.Clear();
			return result;
		}

        public static int Count<T>( this System.Collections.Generic.IEnumerable<T> e, System.Predicate<T> m = null ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var result = 0;
            var list = new System.Collections.Generic.List<T>( e );
            if ( m.IsNull() ) {
                result = list.Count;
            } else {
                for ( var i = 0; i < list.Count; i++ ) {
                    if ( m.Invoke( list[ i ] ) ) {
                        result++;
                    }
                }
            }
            list.Clear();
            return result;
        }

        public static bool Contains<T>( this System.Collections.Generic.IEnumerable<T> e, System.Predicate<T> m = null ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var list = new System.Collections.Generic.List<T>( e );
            var result = false;
            for ( var i = 0; (i < list.Count) && !result; i++ ) {
                result = m.Invoke( list[ i ] );
            }
            list.Clear();
            return result;
        }

        public static T Find<T>( this System.Collections.Generic.IEnumerable<T> e, System.Predicate<T> m = null ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var list = new System.Collections.Generic.List<T>( e );
            var result = list.Find( m );
            list.Clear();
            return result;
        }

        public static TOutput[] Convert<TInput, TOutput>( this System.Collections.Generic.IEnumerable<TInput> e, System.Converter<TInput, TOutput> c ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var list = new System.Collections.Generic.List<TInput>( e );
            var converted = list.ConvertAll( c );
            var result = converted.ToArray();
            list.Clear();
            converted.Clear();
            return result;
        }

        public static TOutput[] FindAndConvert<TInput, TOutput>( this System.Collections.Generic.IEnumerable<TInput> e, System.Predicate<TInput> m, System.Converter<TInput, TOutput> c ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( m.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var list = new System.Collections.Generic.List<TInput>( e );
            var finded = list.FindAll( m );
            var converted = finded.ConvertAll( c );
            var result = converted.ToArray();
            list.Clear();
            finded.Clear();
            converted.Clear();
            return result;
        }

        public static T[] Cast<T>( this System.Collections.IEnumerable e ) {
            if ( e.IsNull() ) {
                throw new System.NullReferenceException();
            }
            var list = new System.Collections.Generic.List<T>();
            foreach ( var value in e ) {
                list.Add( ( T )value );
            }
            var result = list.ToArray();
            list.Clear();
            return result;
        }

        public static T Any<T>( this System.Collections.Generic.IList<T> l ) {
            if ( l.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( l.Count.Equals( 0 ) ) {
                throw new System.IndexOutOfRangeException();
            }
            return l[ UnityEngine.Random.Range( 0, l.Count ) ];
        }

		public static bool IsNullOrEmpty( this System.Collections.ICollection c ) {
			return c.IsNull() || c.Count.Equals( 0 );
		}

        public static bool IsEmpty( this System.Collections.ICollection c ) {
            if ( c.IsNull() ) {
                throw new System.NullReferenceException();
            }
            return c.Count.Equals( 0 );
        }

		public static T[] OrEmpty<T>( this T[] a ) {
			return a.Or( new T[ 0 ] );
		}

		public static T[] Fill<T>( this T[] a, T b ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			for ( var i = 0; i < a.Length; i++ ) {
				a[ i ] = b;
			}
			return a;
		}

		public static T[] Swap<T>( this T[] a, int fromIndex, int toIndex ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( (fromIndex < 0) || (fromIndex >= a.Length) ) {
				throw new System.ArgumentOutOfRangeException( nameof( fromIndex ) );
			}
			if ( (toIndex < 0) || (toIndex >= a.Length) ) {
                throw new System.ArgumentOutOfRangeException( nameof( toIndex ) );
			}
			var temp = a[ fromIndex ];
			a[ fromIndex ] = a[ toIndex ];
			a[ toIndex ] = temp;
			return a;
		}

        public static T[] ShiftLeft<T>( this T[] a, T emptyValue, int offset = 1 ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( offset >= a.Length ) {
                throw new System.ArgumentOutOfRangeException( nameof( offset ) );
            }
            for ( var i = 0; i < a.Length; i++ ) {
                a[ i ] = ((i + offset) < a.Length) ? a[ i + offset ] : emptyValue;
            }
            return a;
        }

        public static T[] ShiftRight<T>( this T[] a, T emptyValue, int offset = 1 ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( offset >= a.Length ) {
                throw new System.ArgumentOutOfRangeException( nameof( offset ) );
            }
            for ( var i = (a.Length - 1); i >= 0; i-- ) {
                a[ i ] = ((i - offset) >= 0) ? a[ i - offset ] : emptyValue;
            }
            return a;
        }

		public static T[] Sort<T>( this T[] a, System.Comparison<T> comparison = null ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( comparison.IsNull() ) {
				System.Array.Sort( a );
			} else {
				System.Array.Sort( a, comparison );
			}
			return a;
		}

        public static T[] Reverse<T>( this T[] a ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            System.Array.Reverse( a );
            return a;
        }

		public static T[] Concat<T>( this T[] a, params T[][] b ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( b.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var newSize = a.Length;
			for ( var i = 0; i < b.Length; i++ ) {
				newSize += b[ i ].OrEmpty().Length;
			}
			var result = new T[ newSize ];
			for ( var i = 0; i < a.Length; i++ ) {
				result[ i ] = a[ i ];
			}
			var offset = a.Length;
			for ( var i = 0; i < b.Length; i++ ) {
				for ( var j = 0; j < b[ i ].OrEmpty().Length; j++ ) {
					result[ offset + j ] = b[ i ][ j ];
				}
				offset += b[ i ].OrEmpty().Length;
			}
			return result;
		}

		public static T[] Slice<T>( this T[] a, int fromIndex, int count ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( (fromIndex < 0) || (fromIndex >= a.Length) ) {
				throw new System.ArgumentOutOfRangeException( nameof( fromIndex ) );
			}
			if ( (count <= 0) || (count > a.Length) ) {
				throw new System.ArgumentOutOfRangeException( nameof( count ) );
			}
			if ( fromIndex >= count ) {
				throw new System.ArgumentException( "fromIndex >= count" );
			}
			var result = new T[ count - fromIndex ];
			for ( var i = fromIndex; i < count; i++ ) {
				result[ i - fromIndex ] = a[ i ];
			}
			return result;
		}

		public static T[] Slice<T>( this T[] a, int fromIndex = 0 ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return a.Slice( fromIndex, a.Length );
		}

        public static bool DeepEqual<T>( this T[] a, T[] b ) where T : struct, System.IEquatable<T> {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( b.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( a.Length != b.Length ) {
				return false;
			}
			for ( var i = 0; i < a.Length; i++ ) {
				if ( !a[ i ].Equals( b[ i ] ) ) {
					return false;
				}
			}
			return true;
		}

		public static T[] Copy<T>( this T[] a ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var result = new T[ a.Length ];
			for ( var i = 0; i < a.Length; i++ ) {
				result[ i ] = a[ i ];
			}
			return result;
		}

		public static TOutput[] ConvertAll<TInput, TOutput>( this TInput[] a, System.Converter<TInput, TOutput> c ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return (a.Length > 0) ? System.Array.ConvertAll( a, c ) : new TOutput[ 0 ];
		}

        public static bool Contains<T>( this T[] a, T o ) where T : System.IEquatable<T> {
			if ( a.IsNullOrEmpty() ) {
				return false;
			}
			foreach ( var item in a ) {
				if ( item.Equals( o ) ) {
					return true;
				}
			}
			return false;
		}

		public static int IndexOf<T>( this T[] a, T o ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.IndexOf( a, o );
		}

		public static bool Exists<T>( this T[] a, System.Predicate<T> m ) where T : class {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( m.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return !System.Array.Find( a, m ).IsNull();
		}

		public static T Find<T>( this T[] a, System.Predicate<T> m ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( m.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.Find( a, m );
		}

		public static T[] FindAll<T>( this T[] a, System.Predicate<T> m ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
            }
			if ( m.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.FindAll( a, m );
		}

		public static int FindIndex<T>( this T[] a, System.Predicate<T> m ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			if ( m.IsNull() ) {
				throw new System.NullReferenceException();
			}
			return System.Array.FindIndex( a, m );
		}

		public static T First<T>( this T[] a ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( a.IsEmpty() ) {
                throw new System.IndexOutOfRangeException();
            }
			return a[ 0 ];
		}

        public static T FirstOr<T>( this T[] a, T defaultValue ) {
            return a.IsNullOrEmpty() ? defaultValue : a[ 0 ];
        }

        public static T Last<T>( this T[] a ) {
            if ( a.IsNull() ) {
                throw new System.NullReferenceException();
            }
            if ( a.IsEmpty() ) {
                throw new System.IndexOutOfRangeException();
            }
			return a[ a.Length - 1 ];
		}

        public static T LastOr<T>( this T[] a, T defaultValue ) {
            return a.IsNullOrEmpty() ? defaultValue : a[ a.Length - 1 ];
        }

        public static T Next<T>( this T[] a, T o ) where T : class, System.IEquatable<T> {
			if ( a.IsNullOrEmpty() ) {
				return null;
			}
			for ( var i = 0; i < (a.Length - 1); i++ ) {
				if ( a[ i ].Equals( o ) ) {
					return a[ i + 1 ];
				}
			}
			return null;
		}

        public static T NextLoop<T>( this T[] a, T o ) where T : class, System.IEquatable<T> {
			if ( a.IsNullOrEmpty() ) {
				return null;
			}
			for ( var i = 0; i < a.Length; i++ ) {
				if ( a[ i ].Equals( o ) ) {
					return a[ (i + 1) % a.Length ];
				}
			}
			return null;
		}

        public static T Previous<T>( this T[] a, T o ) where T : class, System.IEquatable<T> {
			if ( a.IsNullOrEmpty() ) {
				return null;
			}
			for ( var i = 1; i < a.Length; i++ ) {
				if ( a[ i ].Equals( o ) ) {
					return a[ i - 1 ];
				}
			}
			return null;
		}

        public static T PreviousLoop<T>( this T[] a, T o ) where T : class, System.IEquatable<T> {
			if ( a.IsNullOrEmpty() ) {
				return null;
			}
			for ( var i = 0; i < a.Length; i++ ) {
				if ( a[ i ].Equals( o ) ) {
					return a[ (i - 1 + a.Length) % a.Length ];
				}
			}
			return null;
		}

		public static string ToString<T>( this T[] a, string separator = null ) {
			if ( a.IsNull() ) {
				throw new System.NullReferenceException();
			}
			var builder = new System.Text.StringBuilder();
			for ( var i = 0; i < a.Length; i++ ) {
				builder.Append( a[ i ].ToString() );
				if ( !separator.IsNullOrEmpty() && (i < (a.Length - 1)) ) {
					builder.Append( separator );
				}
			}
            return builder.ToString();
		}

        public static bool All<T>( this T[] a, System.Predicate<T> m ) {
            if ( !a.IsNull() && !m.IsNull() ) {
                for ( var i = 0; i < a.Length; i++ ) {
                    if ( !m.Invoke( a[ i ] ) ) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Any<T>( this T[] a, System.Predicate<T> m ) {
            if ( !a.IsNull() && !m.IsNull() ) {
                for ( var i = 0; i < a.Length; i++ ) {
                    if ( m.Invoke( a[ i ] ) ) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void DoForAll<T>( this T[] a, System.Action<T> action, bool forwardOrder = true ) {
            if ( !a.IsNull() && !action.IsNull() ) {
                var start = forwardOrder ? 0 : (a.Length - 1);
                var offset = forwardOrder ? 1 : -1;
                for ( var i = start; (i >= 0) && (i < a.Length); i += offset ) {
                    action.Invoke( a[ i ] );
                }
			}
		}

        public static void DoFor<T>( this T[] a, System.Predicate<T> m, System.Action<T> action, bool forwardOrder = true ) {
            if ( !a.IsNull() && !action.IsNull() && !m.IsNull() ) {
                var start = forwardOrder ? 0 : (a.Length - 1);
                var offset = forwardOrder ? 1 : -1;
                for ( var i = start; (i >= 0) && (i < a.Length); i += offset ) {
                    if ( m.Invoke( a[ i ] ) ) {
                        action.Invoke( a[ i ] );
                    }
                }
            }
        }
	}
}