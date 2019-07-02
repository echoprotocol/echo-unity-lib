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
            ForceSettlement/*      */= 4,
            CommitteeMember/*      */= 5,
            LimitOrder/*           */= 6,
            CallOrder/*            */= 7,
            Custom/*               */= 8,
            Proposal/*             */= 9,
            OperationHistory/*     */= 10,
            WithdrawPermission/*   */= 11,
            VestingBalance/*       */= 12,
            Balance/*              */= 13,
            Contract/*             */= 14,
            ContractResult/*       */= 15,
            BlockResult/*          */= 16,
            ETHAddress/*           */= 17,
            DepositETH/*           */= 18,
            WithdrawETH/*          */= 19
        }


        public enum ImplementationType : byte
        {
            GlobalProperties/*                        */= 0,
            DynamicGlobalProperties/*                 */= 1,
            Reserved_0/*                              */= 2,
            AssetDynamicData/*                        */= 3,
            AssetBitassetData/*                       */= 4,
            AccountBalance/*                          */= 5,
            AccountStatistics/*                       */= 6,
            Transaction/*                             */= 7,
            BlockSummary/*                            */= 8,
            AccountTransactionHistory/*               */= 9,
            ChainProperty/*                           */= 10,
            BudgetRecord/*                            */= 11,
            SpecialAuthority/*                        */= 12,
            Buyback/*                                 */= 13,
            CollateralBid/*                           */= 14,
            ContractBalance/*                         */= 15,
            ContractHistory/*                         */= 16,
            ContractStatistics/*                      */= 17,
            AccountAddress/*                          */= 18,
            ContractPool/*                            */= 19
        }


        public enum VoteType : byte
        {
            Committee/*   */= 0
        }


        public enum Operation : int
        {
            Transfer/*                                */= 0,
            LimitOrderCreate/*                        */= 1,
            LimitOrderCancel/*                        */= 2,
            CallOrderUpdate/*                         */= 3,
            FillOrder/*                               */= 4, // VIRTUAL
            AccountCreate/*                           */= 5,
            AccountUpdate/*                           */= 6,
            AccountWhitelist/*                        */= 7,
            AccountUpgrade/*                          */= 8,
            AccountTransfer/*                         */= 9,
            AssetCreate/*                             */= 10,
            AssetUpdate/*                             */= 11,
            AssetUpdateBitasset/*                     */= 12,
            AssetUpdateFeedProducers/*                */= 13,
            AssetIssue/*                              */= 14,
            AssetReserve/*                            */= 15,
            AssetFundFeePool/*                        */= 16,
            AssetSettle/*                             */= 17,
            AssetGlobalSettle/*                       */= 18,
            AssetPublishFeed/*                        */= 19,
            ProposalCreate/*                          */= 20,
            ProposalUpdate/*                          */= 21,
            ProposalDelete/*                          */= 22,
            WithdrawPermissionCreate/*                */= 23,
            WithdrawPermissionUpdate/*                */= 24,
            WithdrawPermissionClaim/*                 */= 25,
            WithdrawPermissionDelete/*                */= 26,
            CommitteeMemberCreate/*                   */= 27,
            CommitteeMemberUpdate/*                   */= 28,
            CommitteeMemberUpdateGlobalParameters/*   */= 29,
            VestingBalanceCreate/*                    */= 30,
            VestingBalanceWithdraw/*                  */= 31,
            Custom/*                                  */= 32,
            Assert/*                                  */= 33,
            BalanceClaim/*                            */= 34,
            OverrideTransfer/*                        */= 35,
            AssetSettleCancel/*                       */= 36, // VIRTUAL
            AssetClaimFees/*                          */= 37,
            BidCollateral/*                           */= 38,
            ExecuteBid/*                              */= 39, // VIRTUAL
            CreateContract/*                          */= 40,
            CallContract/*                            */= 41,
            ContractTransfer/*                        */= 42,
            ChangeSidechainConfig/*                   */= 43, // temporary operation for tests
            AccountAddressCreate/*                    */= 44,
            TransferToAddress/*                       */= 45,
            GenerateETHAddress/*                      */= 46,
            CreateETHAddress/*                        */= 47,
            DepositETH/*                              */= 48,
            WithdrawETH/*                             */= 49,
            ApproveWithdrawETH/*                      */= 50,
            ContractFundPool/*                        */= 51,
            ContractWhitelist/*                       */= 52,
            SidechainIssue/*                          */= 53, // VIRTUAL
            SidechainBurn/*                           */= 54 // VIRTUAL
        }


        public enum FeeParameters : int
        {
            TransferOperation/*                               */= 0,
            LimitOrderCreateOperation/*                       */= 1,
            LimitOrderCancelOperation/*                       */= 2,
            CallOrderUpdateOperation/*                        */= 3,
            FillOrderOperation/*                              */= 4,
            AccountCreateOperation/*                          */= 5,
            AccountUpdateOperation/*                          */= 6,
            AccountWhitelistOperation/*                       */= 7,
            AccountUpgradeOperation/*                         */= 8,
            AccountTransferOperation/*                        */= 9,
            AssetCreateOperation/*                            */= 10,
            AssetUpdateOperation/*                            */= 11,
            AssetUpdateBitassetOperation/*                    */= 12,
            AssetUpdateFeedProducersOperation/*               */= 13,
            AssetIssueOperation/*                             */= 14,
            AssetReserveOperation/*                           */= 15,
            AssetFundFeePoolOperation/*                       */= 16,
            AssetSettleOperation/*                            */= 17,
            AssetGlobalSettleOperation/*                      */= 18,
            AssetPublishFeedOperation/*                       */= 19,
            ProposalCreateOperation/*                         */= 20,
            ProposalUpdateOperation/*                         */= 21,
            ProposalDeleteOperation/*                         */= 22,
            WithdrawPermissionCreateOperation/*               */= 23,
            WithdrawPermissionUpdateOperation/*               */= 24,
            WithdrawPermissionClaimOperation/*                */= 25,
            WithdrawPermissionDeleteOperation/*               */= 26,
            CommitteeMemberCreateOperation/*                  */= 27,
            CommitteeMemberUpdateOperation/*                  */= 28,
            CommitteeMemberUpdateGlobalParametersOperation/*  */= 29,
            VestingBalanceCreateOperation/*                   */= 30,
            VestingBalanceWithdrawOperation/*                 */= 31,
            CustomOperation/*                                 */= 32,
            AssertOperation/*                                 */= 33,
            BalanceClaimOperation/*                           */= 34,
            OverrideTransferOperation/*                       */= 35,
            AssetSettleCancelOperation/*                      */= 36,
            AssetClaimFeesOperation/*                         */= 37,
            BidCollateralOperation/*                          */= 38,
            ExecuteBidOperation/*                             */= 39,
            CreateContractOperation/*                         */= 40,
            CallContractOperation/*                           */= 41,
            ContractTransferOperation/*                       */= 42,
            ChangeSidechainConfigOperation/*                  */= 43,
            AccountAddressCreateOperation/*                   */= 44,
            TransferToAddressOperation/*                      */= 45,
            GenerateETHAddressOperation/*                     */= 46,
            CreateETHAddressOperation/*                       */= 47,
            DepositETHOperation/*                             */= 48,
            WithdrawETHOperation/*                            */= 49,
            ApproveWithdrawETHOperation/*                     */= 50,
            ContractFundPoolOperation/*                       */= 51,
            ContractWhitelistOperation/*                      */= 52,
            SidechainIssueOperation/*                         */= 53,
            SidechainBurnOperation/*                          */= 54
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