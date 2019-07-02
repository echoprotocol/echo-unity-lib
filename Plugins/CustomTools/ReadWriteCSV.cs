using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace CustomTools.ReadWriteCsv {

	/// <summary>
	/// Class to store one CSV row
	/// </summary>
	public class CsvRow : List<string> {

		public string LineText { get; set; }

		public CsvRow Clone() {
			var clone = new CsvRow();
			clone.LineText = LineText;
			clone.AddRange( this );
			return clone;
		}

		public string ToString( string separator ) {
			return ToArray().ToString( separator );
		}

		public override string ToString() {
			return ToString( "," );
		}
	}


	/// <summary>
	/// Class to write data to a CSV file
	/// </summary>
	public class CsvFileWriter : StreamWriter {

		public CsvFileWriter( Stream stream ) : base( stream ) { }

		public CsvFileWriter( string filename ) : base( filename ) { }

		/// <summary>
		/// Writes a single row to a CSV file.
		/// </summary>
		/// <param name="row">The row to be written</param>
		public void WriteRow( CsvRow row ) {
			var builder = new StringBuilder();
			var firstColumn = true;
			foreach ( var value in row ) {
				// Add separator if this isn't the first value
				if ( !firstColumn ) {
					builder.Append( ',' );
				}
				// Implement special handling for values that contain comma or quote
				// Enclose in quotes and double up any double quotes
				if ( value.IndexOfAny( new [] { '"', ',' } ) != -1 ) {
					builder.AppendFormat( "\"{0}\"", value.Replace( "\"", "\"\"" ) );
				} else {
					builder.Append( value );
				}
				firstColumn = false;
			}
			row.LineText = builder.ToString();
			WriteLine( row.LineText );
		}
	}


	/// <summary>
	/// Class to read data from a CSV file
	/// </summary>
	public class CsvFileReader : StreamReader {
		
		public CsvFileReader( Stream stream ) : base( stream ) { }

		public CsvFileReader( string filename ) : base( filename ) { }

		/// <summary>
		/// Reads a row of data from a CSV file
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool ReadRow( CsvRow row ) {
			row.LineText = ReadLine();
			if ( String.IsNullOrEmpty( row.LineText ) ) {
				return false;
			}
			var position = 0;
			var rows = 0;
			while ( position < row.LineText.Length ) {
				var value = string.Empty;
				// Special handling for quoted field
				if ( row.LineText[ position ].Equals( '"' ) ) {
					// Skip initial quote
					position++;
					// Parse quoted value
					var start = position;
					while ( position < row.LineText.Length ) {
						// Test for quote character
						if ( row.LineText[ position ].Equals( '"' ) ) {
							// Found one
							position++;
							// If two quotes together, keep one
							// Otherwise, indicates end of value
							if ( (position >= row.LineText.Length) || !row.LineText[ position ].Equals( '"' ) ) {
								position--;
								break;
							}
						}
						position++;
					}
					value = row.LineText.Substring( start, (position - start) );
					value = value.Replace( "\"\"", "\"" );
				} else {
					// Parse unquoted value
					var start = position;
					while ( (position < row.LineText.Length) && !row.LineText[ position ].Equals( ',' ) ) {
						position++;
					}
					value = row.LineText.Substring( start, (position - start) );
				}
				// Add field to list
				if ( rows < row.Count ) {
					row[ rows ] = value;
				} else {
					row.Add( value );
				}
				rows++;
				// Eat up to and including next comma
				while ( (position < row.LineText.Length) && !row.LineText[ position ].Equals( ',' ) ) {
					position++;
				}
				if ( position < row.LineText.Length ) {
					position++;
				}
			}
			// Delete any unused items
			while ( row.Count > rows ) {
				row.RemoveAt( rows );
			}
			// Return true if any columns read
			return (row.Count > 0);
		}
	}


	public static class CsvLoader {
		
		public static byte[] LoadCsvFile( string fileName ) {
			var data = new byte[ 0 ];
			var pathParts = fileName.Split( '/' );
			var fullFileName = Application.persistentDataPath + "/" + pathParts[ pathParts.Length - 1 ] + ".csv";
			if ( File.Exists( fullFileName ) ) {
				using ( var fileStream = new FileStream( fullFileName, FileMode.Open, FileAccess.Read ) ) {
					using ( var binaryReader = new BinaryReader( fileStream ) ) {
						data = binaryReader.ReadBytes( ( int )new FileInfo( fullFileName ).Length );
					}
				}
			} else {
				var textAsset = Resources.Load<TextAsset>( fileName );
				if ( !textAsset.IsNull() ) { 
					data = textAsset.bytes;
					Resources.UnloadAsset( textAsset );
				}
			}
			return data;
		}

		public static bool SaveCsvFile( string fileName, byte[] data ) {
			var result = false;
			if ( data.IsNullOrEmpty() ) {
				Console.DebugWarning( "PreferencesLoader", string.Format( "SaveCsvFile( {0}, data )", fileName ), "Data is empty" );
				return result;
			}
			var pathParts = fileName.Split( '/' );
			var fullFileName = Application.persistentDataPath + "/" + pathParts[ pathParts.Length - 1 ] + ".csv";
			try {
				using ( var fileStream = new FileStream( fullFileName, FileMode.Create, FileAccess.Write ) ) {
					using ( var binaryWriter = new BinaryWriter( fileStream ) ) {
						binaryWriter.Write( data );
						result = true;
						Console.DebugLog( "PreferencesLoader", string.Format( "SaveCsvFile( {0}, data )", fileName ), "Save done" );
					}
				}
			} catch ( Exception error ) {
				Console.DebugError( "PreferencesLoader", string.Format( "SaveCsvFile( {0}, data )", fileName ), error.Message );
			}
			return result;
		}
	}
}