using System;


namespace Tools.Assert
{
    public class Assert
    {
        public static void Equal<T>(T a, T b, string alertMessage)
        {
            if (!a.Equals(b))
            {
                throw new ArgumentException(alertMessage);
            }
        }

        public static void Check(bool condition, string message)
        {
            CustomTools.Console.Assert(condition, message);
        }
    }
}