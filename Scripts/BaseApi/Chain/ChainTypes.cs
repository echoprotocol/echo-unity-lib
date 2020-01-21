namespace Base.Config
{
    public static class ChainTypes
    {
        public enum Space : byte
        {
            RelativeProtocolIds/*    */= 0,
            ProtocolIds/*            */= 1,
            ImplementationIds/*      */= 2
        }


        public enum ProtocolType : byte
        {
            Null/*                      */= 0,
            Base/*                      */= 1,
            Account/*                   */= 2,
            Asset/*                     */= 3,
            CommitteeMember/*           */= 4,
            Proposal/*                  */= 5,
            OperationHistory/*          */= 6,
            VestingBalance/*            */= 7,
            Balance/*                   */= 8,
            FrozenBalance/*             */= 9,
            CommitteeFrozenBalance/*    */= 10,
            Contract/*                  */= 11,
            ContractResult/*            */= 12,
            ETH_Address/*               */= 13,
            ETH_Deposit/*               */= 14,
            ETH_Withdraw/*              */= 15,
            ERC20_Token/*               */= 16,
            ERC20_DepositToken/*        */= 17,
            ERC20_WithdrawToken/*       */= 18,
            BTC_Address/*               */= 19,
            BTC_IntermediateDeposit/*   */= 20,
            BTC_Deposit/*               */= 21,
            BTC_Withdraw/*              */= 22,
            BTC_Aggregating/*           */= 23
        }


        public enum ImplementationType : byte
        {
            GlobalProperties/*             */= 0,
            DynamicGlobalProperties/*      */= 1,
            AssetDynamicData/*             */= 2,
            AssetBitassetData/*            */= 3,
            AccountBalance/*               */= 4,
            AccountStatistics/*            */= 5,
            Transaction/*                  */= 6,
            BlockSummary/*                 */= 7,
            AccountTransactionHistory/*    */= 8,
            ChainProperty/*                */= 9,
            SpecialAuthority/*             */= 10,
            ContractBalance/*              */= 11,
            ContractHistory/*              */= 12,
            ContractStatistics/*           */= 13,
            AccountAddress/*               */= 14,
            ContractPool/*                 */= 15,
            MaliciousCommitteemen/*        */= 16
        }


        public enum VoteType : byte
        {
            Committee/*   */= 0
        }


        public enum Operation : int
        {
            Transfer/*                                 */= 0,
            TransferToAddress/*                        */= 1,
            OverrideTransfer/*                         */= 2,
            AccountCreate/*                            */= 3,
            AccountUpdate/*                            */= 4,
            AccountWhitelist/*                         */= 5,
            AccountAddressCreate/*                     */= 6,
            AssetCreate/*                              */= 7,
            AssetUpdate/*                              */= 8,
            AssetUpdateBitasset/*                      */= 9,
            AssetUpdateFeedProducers/*                 */= 10,
            AssetIssue/*                               */= 11,
            AssetReserve/*                             */= 12,
            AssetFundFeePool/*                         */= 13,
            AssetPublishFeed/*                         */= 14,
            AssetClaimFees/*                           */= 15,
            ProposalCreate/*                           */= 16,
            ProposalUpdate/*                           */= 17,
            ProposalDelete/*                           */= 18,
            CommitteeMemberCreate/*                    */= 19,
            CommitteeMemberUpdate/*                    */= 20,
            CommitteeMemberUpdateGlobalParameters/*    */= 21,
            CommitteeMemberActivate/*                  */= 22,
            CommitteeMemberDeactivate/*                */= 23,
            CommitteeFrozenBalanceDeposit/*            */= 24,
            CommitteeFrozenBalanceWithdraw/*           */= 25,
            VestingBalanceCreate/*                     */= 26,
            VestingBalanceWithdraw/*                   */= 27,
            BalanceClaim/*                             */= 28,
            BalanceFreeze/*                            */= 29,
            BalanceUnfreeze/*                          */= 30, // VIRTUAL
            ContractCreate/*                           */= 31,
            ContractCall/*                             */= 32,
            ContractInternalCreate/*                   */= 33, // VIRTUAL
            ContractInternalCall/*                     */= 34, // VIRTUAL
            ContractSelfdestruct/*                     */= 35, // VIRTUAL
            ContractUpdate/*                           */= 36,
            ContractFundPool/*                         */= 37,
            ContractWhitelist/*                        */= 38,
            Sidechain_ETH_CreateAddress/*              */= 39,
            Sidechain_ETH_ApproveAddress/*             */= 40,
            Sidechain_ETH_Deposit/*                    */= 41,
            Sidechain_ETH_SendDeposit/*                */= 42,
            Sidechain_ETH_Withdraw/*                   */= 43,
            Sidechain_ETH_SendWithdraw/*               */= 44,
            Sidechain_ETH_ApproveWithdraw/*            */= 45,
            Sidechain_ETH_UpdateContractAddress/*      */= 46,
            SidechainIssue/*                           */= 47, // VIRTUAL
            SidechainBurn/*                            */= 48, // VIRTUAL
            Sidechain_ERC20_RegisterToken/*            */= 49,
            Sidechain_ERC20_DepositToken/*             */= 50,
            Sidechain_ERC20_SendDepositToken/*         */= 51,
            Sidechain_ERC20_WithdrawToken/*            */= 52,
            Sidechain_ERC20_SendWithdrawToken/*        */= 53,
            Sidechain_ERC20_ApproveTokenWithdraw/*     */= 54,
            Sidechain_ERC20_Issue/*                    */= 55, // VIRTUAL
            Sidechain_ERC20_Burn/*                     */= 56, // VIRTUAL
            Sidechain_BTC_CreateAddress/*              */= 57,
            Sidechain_BTC_CreateIntermediateDeposit/*  */= 58,
            Sidechain_BTC_IntermediateDeposit/*        */= 59,
            Sidechain_BTC_Deposit/*                    */= 60,
            Sidechain_BTC_Withdraw/*                   */= 61,
            Sidechain_BTC_Aggregate/*                  */= 62,
            Sidechain_BTC_ApproveAggregate/*           */= 63,
            BlockReward/*                              */= 64, // VIRTUAL
        }


        public enum FeeParameters : int
        {
            TransferOperation/*                                 */= 0,
            TransferToAddressOperation/*                        */= 1,
            OverrideTransferOperation/*                         */= 2,
            AccountCreateOperation/*                            */= 3,
            AccountUpdateOperation/*                            */= 4,
            AccountWhitelistOperation/*                         */= 5,
            AccountAddressCreateOperation/*                     */= 6,
            AssetCreateOperation/*                              */= 7,
            AssetUpdateOperation/*                              */= 8,
            AssetUpdateBitassetOperation/*                      */= 9,
            AssetUpdateFeedProducersOperation/*                 */= 10,
            AssetIssueOperation/*                               */= 11,
            AssetReserveOperation/*                             */= 12,
            AssetFundFeePoolOperation/*                         */= 13,
            AssetPublishFeedOperation/*                         */= 14,
            AssetClaimFeesOperation/*                           */= 15,
            ProposalCreateOperation/*                           */= 16,
            ProposalUpdateOperation/*                           */= 17,
            ProposalDeleteOperation/*                           */= 18,
            CommitteeMemberCreateOperation/*                    */= 19,
            CommitteeMemberUpdateOperation/*                    */= 20,
            CommitteeMemberUpdateGlobalParametersOperation/*    */= 21,
            CommitteeMemberActivateOperation/*                  */= 22,
            CommitteeMemberDeactivateOperation/*                */= 23,
            CommitteeFrozenBalanceDepositOperation/*            */= 24,
            CommitteeFrozenBalanceWithdrawOperation/*           */= 25,
            VestingBalanceCreateOperation/*                     */= 26,
            VestingBalanceWithdrawOperation/*                   */= 27,
            BalanceClaimOperation/*                             */= 28,
            BalanceFreezeOperation/*                            */= 29,
            BalanceUnfreezeOperation/*                          */= 30,
            ContractCreateOperation/*                           */= 31,
            ContractCallOperation/*                             */= 32,
            ContractInternalCreateOperation/*                   */= 33,
            ContractInternalCallOperation/*                     */= 34,
            ContractSelfdestructOperation/*                     */= 35,
            ContractUpdateOperation/*                           */= 36,
            ContractFundPoolOperation/*                         */= 37,
            ContractWhitelistOperation/*                        */= 38,
            Sidechain_ETH_CreateAddressOperation/*              */= 39,
            Sidechain_ETH_ApproveAddressOperation/*             */= 40,
            Sidechain_ETH_DepositOperation/*                    */= 41,
            Sidechain_ETH_SendDepositOperation/*                */= 42,
            Sidechain_ETH_WithdrawOperation/*                   */= 43,
            Sidechain_ETH_SendWithdrawOperation/*               */= 44,
            Sidechain_ETH_ApproveWithdrawOperation/*            */= 45,
            Sidechain_ETH_UpdateContractAddressOperation/*      */= 46,
            SidechainIssueOperation/*                           */= 47,
            SidechainBurnOperation/*                            */= 48,
            Sidechain_ERC20_RegisterTokenOperation/*            */= 49,
            Sidechain_ERC20_DepositTokenOperation/*             */= 50,
            Sidechain_ERC20_SendDepositTokenOperation/*         */= 51,
            Sidechain_ERC20_WithdrawTokenOperation/*            */= 52,
            Sidechain_ERC20_SendWithdrawTokenOperation/*        */= 53,
            Sidechain_ERC20_ApproveTokenWithdrawOperation/*     */= 54,
            Sidechain_ERC20_IssueOperation/*                    */= 55,
            Sidechain_ERC20_BurnOperation/*                     */= 56,
            Sidechain_BTC_CreateAddressOperation/*              */= 57,
            Sidechain_BTC_CreateIntermediateDepositOperation/*  */= 58,
            Sidechain_BTC_IntermediateDepositOperation/*        */= 59,
            Sidechain_BTC_DepositOperation/*                    */= 60,
            Sidechain_BTC_WithdrawOperation/*                   */= 61,
            Sidechain_BTC_AggregateOperation/*                  */= 62,
            Sidechain_BTC_ApproveAggregateOperation/*           */= 63,
            BlockRewardOperation/*                              */= 64,
        }


        public enum OperationResult : int
        {
            Void/*            */= 0,
            SpaceTypeId/*     */= 1,
            Asset/*           */= 2
        }


        public enum ContractResult : int
        {
            Ethereum/*        */= 0,
            X86_64/*          */= 1
        }


        public enum SpecialAuthority : int
        {
            No/*              */= 0,
            TopHolders/*      */= 1
        }


        public enum VestingPolicy : int
        {
            Linear/*          */= 0,
            Cdd/*             */= 1
        }


        public enum Worker : int
        {
            Refund/*          */= 0,
            VestingBalance/*  */= 1,
            Burn/*            */= 2
        }


        public enum Predicate : int
        {
            AccountName/*     */= 0,
            AssetSymbol/*     */= 1,
            BlockId/*         */= 2
        }


        public enum TransactionException : int
        {
            None/*                                */= 0,
            Unknown/*                             */= 1,
            BadRLP/*                              */= 2,
            InvalidFormat/*                       */= 3,
            OutOfGasIntrinsic/*                   */= 4,  // Too little gas to pay for the base transaction cost.
            InvalidSignature/*                    */= 5,
            InvalidNonce/*                        */= 6,
            NotEnoughCash/*                       */= 7,
            OutOfGasBase/*                        */= 8,  // Too little gas to pay for the base transaction cost.
            BlockGasLimitReached/*                */= 9,
            BadInstruction/*                      */= 10,
            BadJumpDestination/*                  */= 11,
            OutOfGas/*                            */= 12, // Ran out of gas executing code of the transaction.
            OutOfStack/*                          */= 13, // Ran out of stack executing code of the transaction.
            StackUnderflow/*                      */= 14,
            RevertInstruction/*                   */= 15,
            InvalidZeroSignatureFormat/*          */= 16,
            AddressAlreadyUsed/*                  */= 17
        }

        public enum ExecutionException : int
        {
            None/*                                */= 0,
            Unknown/*                             */= 1,
            InvalidRegister/*                     */= 2,
            ContractError/*                       */= 3,
            UnsupportedInstruction/*              */= 4,
            DivisionByZero/*                      */= 5,
            LogLimitExceeded/*                    */= 6,
            UnexpectedOperation/*                 */= 7,
            MemoryInvalidAccess/*                 */= 8,
            ZeroSizeAllocation/*                  */= 9,
            OperandInvalidAccess/*                */= 10,
            OutputLimitExceeded/*                 */= 11,
            UnsupportedModrmSib/*                 */= 12,
            NoAvailableMemory/*                   */= 13,
            OutOfGas/*                            */= 14,
            NotHeapMemory/*                       */= 15,
            IncorrectParameters/*                 */= 16,
            InvalidChainCall/*                    */= 17,
            IncorrectEmulatorLoad/*               */= 18,
        }

        public enum CodeDeposit : int
        {
            None/*            */= 0,
            Failed/*          */= 1,
            Success/*         */= 3,
        }
    }
}