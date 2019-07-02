using System;
using Enum = Base.Data.SpaceType;
using Space = Base.Config.ChainTypes.Space;
using ProtocolType = Base.Config.ChainTypes.ProtocolType;
using ImplementationType = Base.Config.ChainTypes.ImplementationType;


namespace Base.Data.Json
{
    public sealed class SpaceTypeEnumConverter : JsonCustomConverter<Enum, string>
    {
        private readonly static string BASE =/*                            */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Base);
        private readonly static string ACCOUNT =/*                         */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Account);
        private readonly static string ASSET =/*                           */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Asset);
        private readonly static string FORCE_SETTLEMENT =/*                */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ForceSettlement);
        private readonly static string COMMITTEE_MEMBER =/*                */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.CommitteeMember);
        private readonly static string LIMIT_ORDER =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.LimitOrder);
        private readonly static string CALL_ORDER =/*                      */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.CallOrder);
        private readonly static string CUSTOM =/*                          */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Custom);
        private readonly static string PROPOSAL =/*                        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Proposal);
        private readonly static string OPERATION_HISTORY =/*               */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.OperationHistory);
        private readonly static string WITHDRAW_PERMISSION =/*             */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.WithdrawPermission);
        private readonly static string VESTING_BALANCE =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.VestingBalance);
        private readonly static string BALANCE =/*                         */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Balance);
        private readonly static string CONTRACT =/*                        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Contract);
        private readonly static string CONTRACT_RESULT =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ContractResult);
        private readonly static string BLOCK_RESULT =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BlockResult);
        private readonly static string ETH_ADDRESS =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ETHAddress);
        private readonly static string DEPOSIT_ETH =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.DepositETH);
        private readonly static string WITHDRAW_ETH =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.WithdrawETH);

        private readonly static string GLOBAL_PROPERTIES =/*               */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.GlobalProperties);
        private readonly static string DYNAMIC_GLOBAL_PROPERTIES =/*       */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.DynamicGlobalProperties);
        private readonly static string RESERVED_0 =/*                      */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.Reserved_0); // todo
        private readonly static string ASSET_DYNAMIC_DATA =/*              */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AssetDynamicData);
        private readonly static string ASSET_BITASSET_DATA =/*             */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AssetBitassetData);
        private readonly static string ACCOUNT_BALANCE =/*                 */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AccountBalance);
        private readonly static string ACCOUNT_STATISTICS =/*              */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AccountStatistics);
        private readonly static string TRANSACTION =/*                     */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.Transaction);
        private readonly static string BLOCK_SUMMARY =/*                   */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.BlockSummary);
        private readonly static string ACCOUNT_TRANSACTION_HISTORY =/*     */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AccountTransactionHistory);
        private readonly static string CHAIN_PROPERTY =/*                  */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ChainProperty);
        private readonly static string BUDGET_RECORD =/*                   */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.BudgetRecord);
        private readonly static string SPECIAL_AUTHORITY =/*               */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.SpecialAuthority);
        private readonly static string BUYBACK =/*                         */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.Buyback);
        private readonly static string COLLATERAL_BID =/*                  */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.CollateralBid);
        private readonly static string CONTRACT_BALANCE =/*                */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractBalance);
        private readonly static string CONTRACT_HISTORY =/*                */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractHistory);
        private readonly static string CONTRACT_STATISTICS =/*             */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractStatistics);
        private readonly static string ACCOUNT_ADDRESS =/*                 */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AccountAddress);
        private readonly static string CONTRACT_POOL =/*                   */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractPool);


        protected override Enum Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(Enum value) => ConvertTo(value);

        public static string ConvertTo(Enum value)
        {
            switch (value)
            {
                case Enum.Base:/*                                  */return BASE;
                case Enum.Account:/*                               */return ACCOUNT;
                case Enum.Asset:/*                                 */return ASSET;
                case Enum.ForceSettlement:/*                       */return FORCE_SETTLEMENT;
                case Enum.CommitteeMember:/*                       */return COMMITTEE_MEMBER;
                case Enum.LimitOrder:/*                            */return LIMIT_ORDER;
                case Enum.CallOrder:/*                             */return CALL_ORDER;
                case Enum.Custom:/*                                */return CUSTOM;
                case Enum.Proposal:/*                              */return PROPOSAL;
                case Enum.OperationHistory:/*                      */return OPERATION_HISTORY;
                case Enum.WithdrawPermission:/*                    */return WITHDRAW_PERMISSION;
                case Enum.VestingBalance:/*                        */return VESTING_BALANCE;
                case Enum.Balance:/*                               */return BALANCE;
                case Enum.Contract:/*                              */return CONTRACT;
                case Enum.ContractResult:/*                        */return CONTRACT_RESULT;
                case Enum.BlockResult:/*                           */return BLOCK_RESULT;
                case Enum.ETHAddress:/*                            */return ETH_ADDRESS;
                case Enum.DepositETH:/*                            */return DEPOSIT_ETH;
                case Enum.WithdrawETH:/*                           */return WITHDRAW_ETH;
                case Enum.GlobalProperties:/*                      */return GLOBAL_PROPERTIES;
                case Enum.DynamicGlobalProperties:/*               */return DYNAMIC_GLOBAL_PROPERTIES;
                case Enum.Reserved_0:/*                            */return RESERVED_0;
                case Enum.AssetDynamicData:/*                      */return ASSET_DYNAMIC_DATA;
                case Enum.AssetBitassetData:/*                     */return ASSET_BITASSET_DATA;
                case Enum.AccountBalance:/*                        */return ACCOUNT_BALANCE;
                case Enum.AccountStatistics:/*                     */return ACCOUNT_STATISTICS;
                case Enum.Transaction:/*                           */return TRANSACTION;
                case Enum.BlockSummary:/*                          */return BLOCK_SUMMARY;
                case Enum.AccountTransactionHistory:/*             */return ACCOUNT_TRANSACTION_HISTORY;
                case Enum.ChainProperty:/*                         */return CHAIN_PROPERTY;
                case Enum.BudgetRecord:/*                          */return BUDGET_RECORD;
                case Enum.SpecialAuthority:/*                      */return SPECIAL_AUTHORITY;
                case Enum.Buyback:/*                               */return BUYBACK;
                case Enum.CollateralBid:/*                         */return COLLATERAL_BID;
                case Enum.ContractBalance:/*                       */return CONTRACT_BALANCE;
                case Enum.ContractHistory:/*                       */return CONTRACT_HISTORY;
                case Enum.ContractStatistics:/*                    */return CONTRACT_STATISTICS;
                case Enum.AccountAddress:/*                        */return ACCOUNT_ADDRESS;
                case Enum.ContractPool:/*                          */return CONTRACT_POOL;
            }
            return string.Empty;
        }

        public static Enum ConvertFrom(string value)
        {
            if (BASE.Equals(value))/*                              */return Enum.Base;
            if (ACCOUNT.Equals(value))/*                           */return Enum.Account;
            if (ASSET.Equals(value))/*                             */return Enum.Asset;
            if (FORCE_SETTLEMENT.Equals(value))/*                  */return Enum.ForceSettlement;
            if (COMMITTEE_MEMBER.Equals(value))/*                  */return Enum.CommitteeMember;
            if (LIMIT_ORDER.Equals(value))/*                       */return Enum.LimitOrder;
            if (CALL_ORDER.Equals(value))/*                        */return Enum.CallOrder;
            if (CUSTOM.Equals(value))/*                            */return Enum.Custom;
            if (PROPOSAL.Equals(value))/*                          */return Enum.Proposal;
            if (OPERATION_HISTORY.Equals(value))/*                 */return Enum.OperationHistory;
            if (WITHDRAW_PERMISSION.Equals(value))/*               */return Enum.WithdrawPermission;
            if (VESTING_BALANCE.Equals(value))/*                   */return Enum.VestingBalance;
            if (BALANCE.Equals(value))/*                           */return Enum.Balance;
            if (CONTRACT.Equals(value))/*                          */return Enum.Contract;
            if (CONTRACT_RESULT.Equals(value))/*                   */return Enum.ContractResult;
            if (BLOCK_RESULT.Equals(value))/*                      */return Enum.BlockResult;
            if (ETH_ADDRESS.Equals(value))/*                       */return Enum.ETHAddress;
            if (DEPOSIT_ETH.Equals(value))/*                       */return Enum.DepositETH;
            if (WITHDRAW_ETH.Equals(value))/*                      */return Enum.WithdrawETH;
            if (GLOBAL_PROPERTIES.Equals(value))/*                 */return Enum.GlobalProperties;
            if (DYNAMIC_GLOBAL_PROPERTIES.Equals(value))/*         */return Enum.DynamicGlobalProperties;
            if (RESERVED_0.Equals(value))/*                        */return Enum.Reserved_0;
            if (ASSET_DYNAMIC_DATA.Equals(value))/*                */return Enum.AssetDynamicData;
            if (ASSET_BITASSET_DATA.Equals(value))/*               */return Enum.AssetBitassetData;
            if (ACCOUNT_BALANCE.Equals(value))/*                   */return Enum.AccountBalance;
            if (ACCOUNT_STATISTICS.Equals(value))/*                */return Enum.AccountStatistics;
            if (TRANSACTION.Equals(value))/*                       */return Enum.Transaction;
            if (BLOCK_SUMMARY.Equals(value))/*                     */return Enum.BlockSummary;
            if (ACCOUNT_TRANSACTION_HISTORY.Equals(value))/*       */return Enum.AccountTransactionHistory;
            if (CHAIN_PROPERTY.Equals(value))/*                    */return Enum.ChainProperty;
            if (BUDGET_RECORD.Equals(value))/*                     */return Enum.BudgetRecord;
            if (SPECIAL_AUTHORITY.Equals(value))/*                 */return Enum.SpecialAuthority;
            if (BUYBACK.Equals(value))/*                           */return Enum.Buyback;
            if (COLLATERAL_BID.Equals(value))/*                    */return Enum.CollateralBid;
            if (CONTRACT_BALANCE.Equals(value))/*                  */return Enum.ContractBalance;
            if (CONTRACT_HISTORY.Equals(value))/*                  */return Enum.ContractHistory;
            if (CONTRACT_STATISTICS.Equals(value))/*               */return Enum.ContractStatistics;
            if (ACCOUNT_ADDRESS.Equals(value))/*                   */return Enum.AccountAddress;
            if (CONTRACT_POOL.Equals(value))/*                     */return Enum.ContractPool;
            return Enum.Unknown;
        }
    }
}