using System;
using Enum = Base.Config.ChainTypes.CodeDeposit;


namespace Base.Data.Json
{
    public sealed class CodeDepositEnumConverter : JsonCustomConverter<Enum, string>
    {
        private readonly static string NONE =/*       */"None";
        private readonly static string FAILED =/*     */"Failed";
        private readonly static string SUCCESS =/*    */"Success";


        protected override Enum Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(Enum value) => ConvertTo(value);

        public static string ConvertTo(Enum value)
        {
            switch (value)
            {
                case Enum.None:/*           */return NONE;
                case Enum.Failed:/*         */return FAILED;
                case Enum.Success:/*        */return SUCCESS;
            }
            return string.Empty;
        }

        public static Enum ConvertFrom(string value)
        {
            if (NONE.Equals(value))/*       */return Enum.None;
            if (FAILED.Equals(value))/*     */return Enum.Failed;
            if (SUCCESS.Equals(value))/*    */return Enum.Success;
            throw new InvalidOperationException(string.Format("Can't convert '{0}' value to {1} type.", value ?? "null", typeof(Enum).Name));
        }
    }
}