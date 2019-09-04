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
        private readonly static string COMMITTEE_MEMBER =/*                */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.CommitteeMember);
        private readonly static string PROPOSAL =/*                        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Proposal);
        private readonly static string OPERATION_HISTORY =/*               */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.OperationHistory);
        private readonly static string VESTING_BALANCE =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.VestingBalance);
        private readonly static string BALANCE =/*                         */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Balance);
        private readonly static string CONTRACT =/*                        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Contract);
        private readonly static string CONTRACT_RESULT =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ContractResult);
        private readonly static string BLOCK_RESULT =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BlockResult);
        private readonly static string ETH_ADDRESS =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ETHAddress);
        private readonly static string DEPOSIT_ETH =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.DepositETH);
        private readonly static string WITHDRAW_ETH =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.WithdrawETH);
        private readonly static string ERC20_TOKEN =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ERC20Token);
        private readonly static string DEPOSIT_ERC20_TOKEN =/*             */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.DepositERC20Token);
        private readonly static string WITHDRAW_ERC20_TOKEN =/*            */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.WithdrawERC20Token);

        private readonly static string GLOBAL_PROPERTIES =/*               */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.GlobalProperties);
        private readonly static string DYNAMIC_GLOBAL_PROPERTIES =/*       */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.DynamicGlobalProperties);
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
        private readonly static string CONTRACT_BALANCE =/*                */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractBalance);
        private readonly static string CONTRACT_HISTORY =/*                */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractHistory);
        private readonly static string CONTRACT_STATISTICS =/*             */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractStatistics);
        private readonly static string ACCOUNT_ADDRESS =/*                 */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.AccountAddress);
        private readonly static string CONTRACT_POOL =/*                   */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.ContractPool);
        private readonly static string MALICIOUS_COMMITTEEMEN =/*          */string.Format("{0}.{1}", (int)Space.ImplementationIds, (int)ImplementationType.MaliciousCommitteemen);


        protected override Enum Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(Enum value) => ConvertTo(value);

        public static string ConvertTo(Enum value)
        {
            switch (value)
            {
                case Enum.Base:/*                                  */return BASE;
                case Enum.Account:/*                               */return ACCOUNT;
                case Enum.Asset:/*                                 */return ASSET;
                case Enum.CommitteeMember:/*                       */return COMMITTEE_MEMBER;
                case Enum.Proposal:/*                              */return PROPOSAL;
                case Enum.OperationHistory:/*                      */return OPERATION_HISTORY;
                case Enum.VestingBalance:/*                        */return VESTING_BALANCE;
                case Enum.Balance:/*                               */return BALANCE;
                case Enum.Contract:/*                              */return CONTRACT;
                case Enum.ContractResult:/*                        */return CONTRACT_RESULT;
                case Enum.BlockResult:/*                           */return BLOCK_RESULT;
                case Enum.ETHAddress:/*                            */return ETH_ADDRESS;
                case Enum.DepositETH:/*                            */return DEPOSIT_ETH;
                case Enum.WithdrawETH:/*                           */return WITHDRAW_ETH;
                case Enum.ERC20Token:/*                            */return ERC20_TOKEN;
                case Enum.DepositERC20Token:/*                     */return DEPOSIT_ERC20_TOKEN;
                case Enum.WithdrawERC20Token:/*                    */return WITHDRAW_ERC20_TOKEN;
                case Enum.GlobalProperties:/*                      */return GLOBAL_PROPERTIES;
                case Enum.DynamicGlobalProperties:/*               */return DYNAMIC_GLOBAL_PROPERTIES;
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
                case Enum.ContractBalance:/*                       */return CONTRACT_BALANCE;
                case Enum.ContractHistory:/*                       */return CONTRACT_HISTORY;
                case Enum.ContractStatistics:/*                    */return CONTRACT_STATISTICS;
                case Enum.AccountAddress:/*                        */return ACCOUNT_ADDRESS;
                case Enum.ContractPool:/*                          */return CONTRACT_POOL;
                case Enum.MaliciousCommitteemen:/*                 */return MALICIOUS_COMMITTEEMEN;
            }
            return string.Empty;
        }

        public static Enum ConvertFrom(string value)
        {
            if (BASE.Equals(value))/*                              */return Enum.Base;
            if (ACCOUNT.Equals(value))/*                           */return Enum.Account;
            if (ASSET.Equals(value))/*                             */return Enum.Asset;
            if (COMMITTEE_MEMBER.Equals(value))/*                  */return Enum.CommitteeMember;
            if (PROPOSAL.Equals(value))/*                          */return Enum.Proposal;
            if (OPERATION_HISTORY.Equals(value))/*                 */return Enum.OperationHistory;
            if (VESTING_BALANCE.Equals(value))/*                   */return Enum.VestingBalance;
            if (BALANCE.Equals(value))/*                           */return Enum.Balance;
            if (CONTRACT.Equals(value))/*                          */return Enum.Contract;
            if (CONTRACT_RESULT.Equals(value))/*                   */return Enum.ContractResult;
            if (BLOCK_RESULT.Equals(value))/*                      */return Enum.BlockResult;
            if (ETH_ADDRESS.Equals(value))/*                       */return Enum.ETHAddress;
            if (DEPOSIT_ETH.Equals(value))/*                       */return Enum.DepositETH;
            if (WITHDRAW_ETH.Equals(value))/*                      */return Enum.WithdrawETH;
            if (ERC20_TOKEN.Equals(value))/*                       */return Enum.ERC20Token;
            if (DEPOSIT_ERC20_TOKEN.Equals(value))/*               */return Enum.DepositERC20Token;
            if (WITHDRAW_ERC20_TOKEN.Equals(value))/*              */return Enum.WithdrawERC20Token;
            if (GLOBAL_PROPERTIES.Equals(value))/*                 */return Enum.GlobalProperties;
            if (DYNAMIC_GLOBAL_PROPERTIES.Equals(value))/*         */return Enum.DynamicGlobalProperties;
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
            if (CONTRACT_BALANCE.Equals(value))/*                  */return Enum.ContractBalance;
            if (CONTRACT_HISTORY.Equals(value))/*                  */return Enum.ContractHistory;
            if (CONTRACT_STATISTICS.Equals(value))/*               */return Enum.ContractStatistics;
            if (ACCOUNT_ADDRESS.Equals(value))/*                   */return Enum.AccountAddress;
            if (CONTRACT_POOL.Equals(value))/*                     */return Enum.ContractPool;
            if (MALICIOUS_COMMITTEEMEN.Equals(value))/*            */return Enum.MaliciousCommitteemen;
            return Enum.Unknown;
        }
    }
}