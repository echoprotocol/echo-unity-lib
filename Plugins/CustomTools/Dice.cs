using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace CustomTools {

	public class Dice {

		public interface IWeight {

			float Value { get; }
		}

		
		public static bool ThrowDice( float percent ) {
			percent = Mathf.Clamp01( percent );
			var count = ( int )(percent * 100f + 0.5f);
			var selection = new bool[ 100 ];
            ArrayTools.DoTimes( () => selection.Length, index => {
                selection[ index ] = index < count;
            } );
			return selection[ Random.Range( 0, selection.Length ) ];
		}

		public static T ThrowDice<T>( T[] weights ) where T : IWeight {
			var sum = 0f;
			foreach ( T weight in weights ) {
				sum += weight.Value;
			}
			var selection = new T[ 0 ];
			foreach ( T weight in weights ) {
				var count = ( int )(weight.Value * 100f / sum + 0.5f);
                ArrayTools.DoTimes( () => count, index => {
                    ArrayTools.Add( ref selection, weight );
                } );
			}
			return selection[ Random.Range( 0, selection.Length ) ];
		}

		public static int RandomValue( int min, int max, params int[] filter ) {
			if ( filter.Length >= Mathf.Abs( max - min ) || (min == max) ) {
				return min;
			}
			while ( true ) {
				var value = Random.Range( min, max );
				foreach ( var exception in filter ) {
					if ( value == exception ) {
						continue;
					}
				}
				return value;
			}
		}

		public static float RandomValue( float min, float max, params float[] filter ) {
			if ( Mathf.Abs( min - max ) < 0.0001f ) {
				return min;
			}
			while ( true ) {
				var value = Random.Range( min, max );
				foreach ( var exception in filter ) {
					if ( Mathf.Abs( value - exception ) < 0.0001f ) {
						continue;
					}
				}
				return value;
			}
		}

		public static bool[] GetSequenceOfEvents( float percentage, float amountEvents ) {
			var arrayEvents = new bool[ ( int )amountEvents ];
			percentage = Mathf.Min( 1f, Mathf.Abs( percentage ) );
			var tmpAmount = Mathf.Round( percentage * amountEvents );
			var isPositive = true;
			if ( tmpAmount > (amountEvents / 2f) ) {
				tmpAmount = amountEvents - tmpAmount;
				isPositive = false;
			}
			var interval = ( int )(amountEvents / tmpAmount);
			var amountBigInterv = ( int )(amountEvents - tmpAmount * interval);
			var currentIndex = 0;
			var randInt = 0;
			var currentInterval = 0;
			for ( var i = 0; i < amountEvents; i++ ) {
				arrayEvents[ i ] = !isPositive;
			}
			for ( var i = 1; i <= tmpAmount; i++ ) {
				if ( i <= (tmpAmount - amountBigInterv) ) {
					currentInterval = interval;
				} else {
					currentInterval = interval + 1;
				}
				randInt = Random.Range( 1, currentInterval );
				arrayEvents[ currentIndex + randInt ] = !arrayEvents[ currentIndex + randInt ];
				currentIndex += currentInterval;
			}
			return arrayEvents;
		}

		public static bool[] GetSequenceOfEvents( int percentage, float amountEvents ) {
			return GetSequenceOfEvents( (percentage / 100f), amountEvents );
		}

		public static string GetRandomKey( int size ) {
			var builder = new System.Text.StringBuilder();
            ArrayTools.DoTimes( () => size, index => {
                builder.Append( System.Convert.ToChar( System.Convert.ToInt32( Mathf.Floor( 26f * Random.Range( 0.0f, 1.0f ) + 65f ) ) ) );
            } );
			return builder.ToString();
		}
	}
}
