using System;
using Enum = Base.Config.ChainTypes.ExecutionException;


namespace Base.Data.Json
{
    public sealed class ExecutionExceptionEnumConverter : JsonCustomConverter<Enum, string>
    {
        private readonly static string NONE =/*                             */"none";
        private readonly static string UNKNOWN =/*                          */"unknown";
        private readonly static string INVALID_REGISTER =/*                 */"invalid_register";
        private readonly static string CONTRACT_ERROR =/*                   */"contract_error";
        private readonly static string UNSUPPORTED_INSTRUCTION =/*          */"unsupported_instruction";
        private readonly static string DIVISION_BY_ZERO =/*                 */"division_by_zero";
        private readonly static string LOG_LIMIT_EXCEEDED =/*               */"log_limit_exceeded";
        private readonly static string UNEXPECTED_OPERATION =/*             */"unexpected_operation";
        private readonly static string MEMORY_INVALID_ACCESS =/*            */"memory_invalid_access";
        private readonly static string ZERO_SIZE_ALLOCATION =/*             */"zero_size_allocation";
        private readonly static string OPERAND_INVALID_ACCESS =/*           */"operand_invalid_access";
        private readonly static string OUTPUT_LIMIT_EXCEEDED =/*            */"output_limit_exceeded";
        private readonly static string UNSUPPORTED_MODRM_SIB =/*            */"unsupported_modrm_sib";
        private readonly static string NO_AVAILABLE_MEMORY =/*              */"no_available_memory";
        private readonly static string OUT_OF_GAS =/*                       */"out_of_gas";
        private readonly static string NOT_HEAP_MEMORY =/*                  */"not_heap_memory";
        private readonly static string INCORRECT_PARAMETERS =/*             */"incorrect_parameters";
        private readonly static string INVALID_CHAIN_CALL =/*               */"invalid_chain_call";
        private readonly static string INCORRECT_EMULATOR_LOAD =/*          */"incorrect_emulator_load";


        protected override Enum Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(Enum value) => ConvertTo(value);

        public static string ConvertTo(Enum value)
        {
            switch (value)
            {
                case Enum.None:/*                                      */return NONE;
                case Enum.Unknown:/*                                   */return UNKNOWN;
                case Enum.InvalidRegister:/*                           */return INVALID_REGISTER;
                case Enum.ContractError:/*                             */return CONTRACT_ERROR;
                case Enum.UnsupportedInstruction:/*                    */return UNSUPPORTED_INSTRUCTION;
                case Enum.DivisionByZero:/*                            */return DIVISION_BY_ZERO;
                case Enum.LogLimitExceeded:/*                          */return LOG_LIMIT_EXCEEDED;
                case Enum.UnexpectedOperation:/*                       */return UNEXPECTED_OPERATION;
                case Enum.MemoryInvalidAccess:/*                       */return MEMORY_INVALID_ACCESS;
                case Enum.ZeroSizeAllocation:/*                        */return ZERO_SIZE_ALLOCATION;
                case Enum.OperandInvalidAccess:/*                      */return OPERAND_INVALID_ACCESS;
                case Enum.OutputLimitExceeded:/*                       */return OUTPUT_LIMIT_EXCEEDED;
                case Enum.UnsupportedModrmSib:/*                       */return UNSUPPORTED_MODRM_SIB;
                case Enum.NoAvailableMemory:/*                         */return NO_AVAILABLE_MEMORY;
                case Enum.OutOfGas:/*                                  */return OUT_OF_GAS;
                case Enum.NotHeapMemory:/*                             */return NOT_HEAP_MEMORY;
                case Enum.IncorrectParameters:/*                       */return INCORRECT_PARAMETERS;
                case Enum.InvalidChainCall:/*                          */return INVALID_CHAIN_CALL;
                case Enum.IncorrectEmulatorLoad:/*                     */return INCORRECT_EMULATOR_LOAD;
            }
            return string.Empty;
        }

        public static Enum ConvertFrom(string value)
        {
            if (NONE.Equals(value))/*                                  */return Enum.None;
            if (UNKNOWN.Equals(value))/*                               */return Enum.Unknown;
            if (INVALID_REGISTER.Equals(value))/*                      */return Enum.InvalidRegister;
            if (CONTRACT_ERROR.Equals(value))/*                        */return Enum.ContractError;
            if (UNSUPPORTED_INSTRUCTION.Equals(value))/*               */return Enum.UnsupportedInstruction;
            if (DIVISION_BY_ZERO.Equals(value))/*                      */return Enum.DivisionByZero;
            if (LOG_LIMIT_EXCEEDED.Equals(value))/*                    */return Enum.LogLimitExceeded;
            if (UNEXPECTED_OPERATION.Equals(value))/*                  */return Enum.UnexpectedOperation;
            if (MEMORY_INVALID_ACCESS.Equals(value))/*                 */return Enum.MemoryInvalidAccess;
            if (ZERO_SIZE_ALLOCATION.Equals(value))/*                  */return Enum.ZeroSizeAllocation;
            if (OPERAND_INVALID_ACCESS.Equals(value))/*                */return Enum.OperandInvalidAccess;
            if (OUTPUT_LIMIT_EXCEEDED.Equals(value))/*                 */return Enum.OutputLimitExceeded;
            if (UNSUPPORTED_MODRM_SIB.Equals(value))/*                 */return Enum.UnsupportedModrmSib;
            if (NO_AVAILABLE_MEMORY.Equals(value))/*                   */return Enum.NoAvailableMemory;
            if (OUT_OF_GAS.Equals(value))/*                            */return Enum.OutOfGas;
            if (NOT_HEAP_MEMORY.Equals(value))/*                       */return Enum.NotHeapMemory;
            if (INCORRECT_PARAMETERS.Equals(value))/*                  */return Enum.IncorrectParameters;
            if (INVALID_CHAIN_CALL.Equals(value))/*                    */return Enum.InvalidChainCall;
            if (INCORRECT_EMULATOR_LOAD.Equals(value))/*               */return Enum.IncorrectEmulatorLoad;
            throw new InvalidOperationException(string.Format("Can't convert '{0}' value to {1} type.", value ?? "null", typeof(Enum).Name));
        }
    }
}