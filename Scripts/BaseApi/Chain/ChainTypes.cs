namespace Base.Config
{
    public static class ChainTypes
    {
        public enum Space : byte
        {
            RelativeProtocolIds/* */= 0,
            ProtocolIds/*         */= 1,
            ImplementationIds/*   */= 2
        }


        public enum ProtocolType : byte
        {
            Null/*                 */= 0,
            Base/*                 */= 1,
            Account/*              */= 2,
            Asset/*                */= 3,
            CommitteeMember/*      */= 4,
            Proposal/*             */= 5,
            OperationHistory/*     */= 6,
            VestingBalance/*       */= 7,
            Balance/*              */= 8,
            Contract/*             */= 9,
            ContractResult/*       */= 10,
            BlockResult/*          */= 11,
            ETHAddress/*           */= 12,
            DepositETH/*           */= 13,
            WithdrawETH/*          */= 14,
            ERC20Token/*           */= 15,
            DepositERC20Token/*    */= 16,
            WithdrawERC20Token/*   */= 17
        }


        public enum ImplementationType : byte
        {
            GlobalProperties/*                        */= 0,
            DynamicGlobalProperties/*                 */= 1,
            AssetDynamicData/*                        */= 2,
            AssetBitassetData/*                       */= 3,
            AccountBalance/*                          */= 4,
            AccountStatistics/*                       */= 5,
            Transaction/*                             */= 6,
            BlockSummary/*                            */= 7,
            AccountTransactionHistory/*               */= 8,
            ChainProperty/*                           */= 9,
            BudgetRecord/*                            */= 10,
            SpecialAuthority/*                        */= 11,
            ContractBalance/*                         */= 12,
            ContractHistory/*                         */= 13,
            ContractStatistics/*                      */= 14,
            AccountAddress/*                          */= 15,
            ContractPool/*                            */= 16,
            MaliciousCommitteemen/*                   */= 17
        }


        public enum VoteType : byte
        {
            Committee/*   */= 0
        }


        public enum Operation : int
        {
            Transfer/*                                */= 0,
            AccountCreate/*                           */= 1,
            AccountUpdate/*                           */= 2,
            AccountWhitelist/*                        */= 3,
            AccountTransfer/*                         */= 4,
            AssetCreate/*                             */= 5,
            AssetUpdate/*                             */= 6,
            AssetUpdateBitasset/*                     */= 7,
            AssetUpdateFeedProducers/*                */= 8,
            AssetIssue/*                              */= 9,
            AssetReserve/*                            */= 10,
            AssetFundFeePool/*                        */= 11,
            AssetPublishFeed/*                        */= 12,
            ProposalCreate/*                          */= 13,
            ProposalUpdate/*                          */= 14,
            ProposalDelete/*                          */= 15,
            CommitteeMemberCreate/*                   */= 16,
            CommitteeMemberUpdate/*                   */= 17,
            CommitteeMemberUpdateGlobalParameters/*   */= 18,
            VestingBalanceCreate/*                    */= 19,
            VestingBalanceWithdraw/*                  */= 20,
            BalanceClaim/*                            */= 21,
            OverrideTransfer/*                        */= 22,
            AssetClaimFees/*                          */= 23,
            ContractCreate/*                          */= 24,
            ContractCall/*                            */= 25,
            ContractTransfer/*                        */= 26,
            SidechainChangeConfig/*                   */= 27, // temporary operation for tests
            AccountAddressCreate/*                    */= 28,
            TransferToAddress/*                       */= 29,
            SidechainETHCreateAddress/*               */= 30,
            SidechainETHApproveAddress/*              */= 31,
            SidechainETHDeposit/*                     */= 32,
            SidechainETHWithdraw/*                    */= 33,
            SidechainETHApproveWithdraw/*             */= 34,
            ContractFundPool/*                        */= 35,
            ContractWhitelist/*                       */= 36,
            SidechainETHIssue/*                       */= 37, // VIRTUAL
            SidechainETHBurn/*                        */= 38, // VIRTUAL
            SidechainERC20RegisterToken/*             */= 39,
            SidechainERC20DepositToken/*              */= 40,
            SidechainERC20WithdrawToken/*             */= 41,
            SidechainERC20ApproveTokenWithdraw/*      */= 42,
            ContractUpdate/*                          */= 43
        }
        //base_contract_operation -> contract_base_operation

        public enum FeeParameters : int
        {
            TransferOperation/*                               */= 0,
            AccountCreateOperation/*                          */= 1,
            AccountUpdateOperation/*                          */= 2,
            AccountWhitelistOperation/*                       */= 3,
            AccountTransferOperation/*                        */= 4,
            AssetCreateOperation/*                            */= 5,
            AssetUpdateOperation/*                            */= 6,
            AssetUpdateBitassetOperation/*                    */= 7,
            AssetUpdateFeedProducersOperation/*               */= 8,
            AssetIssueOperation/*                             */= 9,
            AssetReserveOperation/*                           */= 10,
            AssetFundFeePoolOperation/*                       */= 11,
            AssetPublishFeedOperation/*                       */= 12,
            ProposalCreateOperation/*                         */= 13,
            ProposalUpdateOperation/*                         */= 14,
            ProposalDeleteOperation/*                         */= 15,
            CommitteeMemberCreateOperation/*                  */= 16,
            CommitteeMemberUpdateOperation/*                  */= 17,
            CommitteeMemberUpdateGlobalParametersOperation/*  */= 18,
            VestingBalanceCreateOperation/*                   */= 19,
            VestingBalanceWithdrawOperation/*                 */= 20,
            BalanceClaimOperation/*                           */= 21,
            OverrideTransferOperation/*                       */= 22,
            AssetClaimFeesOperation/*                         */= 23,
            ContractCreateOperation/*                         */= 24,
            ContractCallOperation/*                           */= 25,
            ContractTransferOperation/*                       */= 26,
            SidechainChangeConfigOperation/*                  */= 27,
            AccountAddressCreateOperation/*                   */= 28,
            TransferToAddressOperation/*                      */= 29,
            SidechainETHCreateAddressOperation/*              */= 30,
            SidechainETHApproveAddressOperation/*             */= 31,
            SidechainETHDepositETHOperation/*                 */= 32,
            SidechainETHWithdrawOperation/*                   */= 33,
            SidechainETHApproveWithdrawOperation/*            */= 34,
            ContractFundPoolOperation/*                       */= 35,
            ContractWhitelistOperation/*                      */= 36,
            SidechainETHIssueOperation/*                      */= 37,
            SidechainETHBurnOperation/*                       */= 38,
            SidechainERC20RegisterTokenOperation/*            */= 39,
            SidechainERC20DepositTokenOperation/*             */= 40,
            SidechainERC20WithdrawTokenOperation/*            */= 41,
            SidechainERC20ApproveTokenWithdrawOperation/*     */= 42,
            ContractUpdateOperation/*                         */= 43
        }


        public enum OperationResult : int
        {
            Void/*            */= 0,
            SpaceTypeId/*     */= 1,
            Asset/*           */= 2
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


        public enum CodeDeposit : int
        {
            None/*            */= 0,
            Failed/*          */= 1,
            Success/*         */= 3,
        }
    }
}