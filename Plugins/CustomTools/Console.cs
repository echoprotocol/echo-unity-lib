using System.Text;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace CustomTools {

	public enum LogEnum {
		
		Message,
		Warning,
		Error
	}


    public sealed class Console : ILogHandler {

        const string NULL_TAG = "null";


        public class ColoredLog : System.IDisposable {

            const string COLOR_TAG_PREFIX = "<color={0}>";
            const string COLOR_TAG_POSTFIX = "</color>";
            const string HEX_FORMAT = "X2";
            const string COLOR_FORMAT = "#{0}{1}{2}";

            Color color;
            char separator;
            object[] messages;


            internal ColoredLog( Color color, char separator, params object[] messages ) {
                this.color = color;
                this.separator = separator;
                this.messages = messages;
            }

            public void Dispose() {
                messages = null;
            }

            public StringBuilder Build( StringBuilder builder = null ) {
                builder = builder ?? new StringBuilder();
                if ( !messages.IsNullOrEmpty() ) {
#if UNITY_EDITOR
                    builder.Append( string.Format( COLOR_TAG_PREFIX, ColorToHex( color ) ) );
#endif
                    var times = messages.Length;
                    ArrayTools.DoTimes( () => times, index => {
                        ((index > 0) ? builder.Append( separator ) : builder).Append( messages[ index ].IsNull() ? NULL_TAG : messages[ index ].ToString() );
                    } );
#if UNITY_EDITOR
                    builder.Append( COLOR_TAG_POSTFIX );
#endif
                }
                return builder;
            }

            static string ColorToHex( Color32 color ) {
                return string.Format( COLOR_FORMAT, color.r.ToString( HEX_FORMAT ), color.g.ToString( HEX_FORMAT ), color.b.ToString( HEX_FORMAT ) );
            }
        }


        #region class
        ILogHandler defaultLogHandler = Debug.unityLogger.logHandler;

		Console() { }


		public void LogFormat( LogType logType, Object context, string format, params object[] args ) {
			defaultLogHandler.LogFormat( logType, context, format, args );
		}

		public void LogException( System.Exception exception, Object context ) {
			defaultLogHandler.LogException( exception, context );
		}
		#endregion


		public static void Set() {
			if ( Debug.unityLogger.logHandler is Console ) {
				return;
			}
			Debug.unityLogger.logHandler = new Console();
		}

		public static void UnSet() {
			if ( Debug.unityLogger.logHandler is Console ) {
				Debug.unityLogger.logHandler = (Debug.unityLogger.logHandler as Console).defaultLogHandler;
			}
		}

		public static ColoredLog LogRedColor( params object[] source ) {
			return new ColoredLog( Color.red, ' ', source );
		}

		public static ColoredLog LogGreenColor( params object[] source ) {
			return new ColoredLog( Color.green, ' ', source );
		}

		public static ColoredLog LogBlueColor( params object[] source ) {
			return new ColoredLog( Color.blue, ' ', source );
		}

		public static ColoredLog LogCyanColor( params object[] source ) {
			return new ColoredLog( Color.cyan, ' ', source );
		}

		public static ColoredLog LogMagentaColor( params object[] source ) {
			return new ColoredLog( Color.magenta, ' ', source );
		}

		public static ColoredLog LogYellowColor( params object[] source ) {
			return new ColoredLog( Color.yellow, ' ', source );
		}

		public static ColoredLog LogWhiteColor( params object[] source ) {
			return new ColoredLog( Color.white, ' ', source );
		}

		public static ColoredLog LogBlackColor( params object[] source ) {
			return new ColoredLog( Color.black, ' ', source );
		}

		public static ColoredLog LogGrayColor( params object[] source ) {
			return new ColoredLog( Color.gray, ' ', source );
		}

		public static void DebugLog( params object[] messages ) => DebugLog( LogEnum.Message, ' ', messages );

		public static void DebugWarning( params object[] messages ) => DebugLog( LogEnum.Warning, ' ', messages );

		public static void DebugError( params object[] messages ) => DebugLog( LogEnum.Error, ' ', messages );

		static void DebugLog( LogEnum type, char separator, params object[] messages ) {
#if ECHO_DEBUG
			var builder = new StringBuilder();
			builder.Append( "DEBUG:" );
            var times = messages.Length;
            ArrayTools.DoTimes( () => times, index => {
                var message = messages[ index ];
                if ( message.IsNull() ) {
                    builder.Append( separator ).Append( NULL_TAG );
                } else if ( message is ColoredLog ) {
                    var coloredMessage = message as ColoredLog;
                    coloredMessage.Build( builder.Append( separator ) );
                    coloredMessage.Dispose();
                } else {
                    builder.Append( separator ).Append( message.ToString() );
                }
            } );
			switch ( type ) {
			case LogEnum.Message:
				Debug.Log( builder.ToString() );
				break;
			case LogEnum.Warning:
				Debug.LogWarning( builder.ToString() );
				break;
			case LogEnum.Error:
				Debug.LogError( builder.ToString() );
				break;
			}
            builder.Clear();
#endif
        }

        public static void Log( params object[] messages ) {
			Log( LogEnum.Message, ' ', messages );
		}

		public static void Warning( params object[] messages ) {
			Log( LogEnum.Warning, ' ', messages );
		}

		public static void Error( params object[] messages ) {
			Log( LogEnum.Error, ' ', messages );
		}

		public static void Assert( bool condition, string message ) {
			Debug.AssertFormat( condition, "{0}", message );
		}

		static void Log( LogEnum type, char separator, params object[] messages ) {
			var builder = new StringBuilder();
            var times = messages.Length;
            ArrayTools.DoTimes( () => times, index => {
                var message = messages[ index ];
                if ( message.IsNull() ) {
                    ((index > 0) ? builder.Append( separator ) : builder).Append( NULL_TAG );
                } else if ( message is ColoredLog ) {
                    var coloredMessage = message as ColoredLog;
                    coloredMessage.Build( (index > 0) ? builder.Append( separator ) : builder );
                    coloredMessage.Dispose();
                } else {
                    ((index > 0) ? builder.Append( separator ) : builder).Append( message.ToString() );
                }
            } );
			switch ( type ) {
			case LogEnum.Message:
				Debug.Log( builder.ToString() );
				break;
			case LogEnum.Warning:
				Debug.LogWarning( builder.ToString() );
				break;
			case LogEnum.Error:
				Debug.LogError( builder.ToString() );
				break;
			}
            builder.Clear();
        }
	}
}