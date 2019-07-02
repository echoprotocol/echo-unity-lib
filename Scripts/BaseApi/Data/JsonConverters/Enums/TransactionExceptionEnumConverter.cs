using System;
using Enum = Base.Config.ChainTypes.TransactionException;


namespace Base.Data.Json
{
    public sealed class TransactionExceptionEnumConverter : JsonCustomConverter<Enum, string>
    {
        private readonly static string NONE =/*                             */"None";
        private readonly static string UNKNOWN =/*                          */"Unknown";
        private readonly static string BAD_RLP =/*                          */"BadRLP";
        private readonly static string INVALID_FORMAT =/*                   */"InvalidFormat";
        private readonly static string OUT_OF_GAS_INTRINSIC =/*             */"OutOfGasIntrinsic";
        private readonly static string INVALID_SIGNATURE =/*                */"InvalidSignature";
        private readonly static string INVALID_NONCE =/*                    */"InvalidNonce";
        private readonly static string NOT_ENOUGH_CASH =/*                  */"NotEnoughCash";
        private readonly static string OUT_OF_GAS_BASE =/*                  */"OutOfGasBase";
        private readonly static string BLOCK_GAS_LIMIT_REACHED =/*          */"BlockGasLimitReached";
        private readonly static string BAD_INSTRUCTION =/*                  */"BadInstruction";
        private readonly static string BAD_JUMP_DESTINATION =/*             */"BadJumpDestination";
        private readonly static string OUT_OF_GAS =/*                       */"OutOfGas";
        private readonly static string OUT_OF_STACK =/*                     */"OutOfStack";
        private readonly static string STACK_UNDERFLOW =/*                  */"StackUnderflow";
        private readonly static string REVERT_INSTRUCTION =/*               */"RevertInstruction";
        private readonly static string INVALID_ZERO_SIGNATURE_FORMAT =/*    */"InvalidZeroSignatureFormat";
        private readonly static string ADDRESS_ALREADY_USED =/*             */"AddressAlreadyUsed";


        protected override Enum Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(Enum value) => ConvertTo(value);

        public static string ConvertTo(Enum value)
        {
            switch (value)
            {
                case Enum.None:/*                                      */return NONE;
                case Enum.Unknown:/*                                   */return UNKNOWN;
                case Enum.BadRLP:/*                                    */return BAD_RLP;
                case Enum.InvalidFormat:/*                             */return INVALID_FORMAT;
                case Enum.OutOfGasIntrinsic:/*                         */return OUT_OF_GAS_INTRINSIC;
                case Enum.InvalidSignature:/*                          */return INVALID_SIGNATURE;
                case Enum.InvalidNonce:/*                              */return INVALID_NONCE;
                case Enum.NotEnoughCash:/*                             */return NOT_ENOUGH_CASH;
                case Enum.OutOfGasBase:/*                              */return OUT_OF_GAS_BASE;
                case Enum.BlockGasLimitReached:/*                      */return BLOCK_GAS_LIMIT_REACHED;
                case Enum.BadInstruction:/*                            */return BAD_INSTRUCTION;
                case Enum.BadJumpDestination:/*                        */return BAD_JUMP_DESTINATION;
                case Enum.OutOfGas:/*                                  */return OUT_OF_GAS;
                case Enum.OutOfStack:/*                                */return OUT_OF_STACK;
                case Enum.StackUnderflow:/*                            */return STACK_UNDERFLOW;
                case Enum.RevertInstruction:/*                         */return REVERT_INSTRUCTION;
                case Enum.InvalidZeroSignatureFormat:/*                */return INVALID_ZERO_SIGNATURE_FORMAT;
                case Enum.AddressAlreadyUsed:/*                        */return ADDRESS_ALREADY_USED;
            }
            return string.Empty;
        }

        public static Enum ConvertFrom(string value)
        {
            if (NONE.Equals(value))/*                                  */return Enum.None;
            if (UNKNOWN.Equals(value))/*                               */return Enum.Unknown;
            if (BAD_RLP.Equals(value))/*                               */return Enum.BadRLP;
            if (INVALID_FORMAT.Equals(value))/*                        */return Enum.InvalidFormat;
            if (OUT_OF_GAS_INTRINSIC.Equals(value))/*                  */return Enum.OutOfGasIntrinsic;
            if (INVALID_SIGNATURE.Equals(value))/*                     */return Enum.InvalidSignature;
            if (INVALID_NONCE.Equals(value))/*                         */return Enum.InvalidNonce;
            if (NOT_ENOUGH_CASH.Equals(value))/*                       */return Enum.NotEnoughCash;
            if (OUT_OF_GAS_BASE.Equals(value))/*                       */return Enum.OutOfGasBase;
            if (BLOCK_GAS_LIMIT_REACHED.Equals(value))/*               */return Enum.BlockGasLimitReached;
            if (BAD_INSTRUCTION.Equals(value))/*                       */return Enum.BadInstruction;
            if (BAD_JUMP_DESTINATION.Equals(value))/*                  */return Enum.BadJumpDestination;
            if (OUT_OF_GAS.Equals(value))/*                            */return Enum.OutOfGas;
            if (OUT_OF_STACK.Equals(value))/*                          */return Enum.OutOfStack;
            if (STACK_UNDERFLOW.Equals(value))/*                       */return Enum.StackUnderflow;
            if (REVERT_INSTRUCTION.Equals(value))/*                    */return Enum.RevertInstruction;
            if (INVALID_ZERO_SIGNATURE_FORMAT.Equals(value))/*         */return Enum.InvalidZeroSignatureFormat;
            if (ADDRESS_ALREADY_USED.Equals(value))/*                  */return Enum.AddressAlreadyUsed;
            throw new InvalidOperationException(string.Format("Can't convert '{0}' value to {1} type.", value ?? "null", typeof(Enum).Name));
        }
    }
}