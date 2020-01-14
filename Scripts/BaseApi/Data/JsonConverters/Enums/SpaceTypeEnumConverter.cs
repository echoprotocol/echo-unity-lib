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
        private readonly static string FROZEN_BALANCE =/*                  */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.FrozenBalance);
        private readonly static string COMMITTEE_FROZEN_BALANCE =/*        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.CommitteeFrozenBalance);
        private readonly static string CONTRACT =/*                        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.Contract);
        private readonly static string CONTRACT_RESULT =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ContractResult);
        private readonly static string ETH_ADDRESS =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ETH_Address);
        private readonly static string ETH_DEPOSIT =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ETH_Deposit);
        private readonly static string ETH_WITHDRAW =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ETH_Withdraw);
        private readonly static string ERC20_TOKEN =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ERC20_Token);
        private readonly static string ERC20_DEPOSIT_TOKEN =/*             */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ERC20_DepositToken);
        private readonly static string ERC20_WITHDRAW_TOKEN =/*            */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.ERC20_WithdrawToken);
        private readonly static string BTC_ADDRESS =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BTC_Address);
        private readonly static string BTC_INTERMEDIATE_DEPOSIT =/*        */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BTC_IntermediateDeposit);
        private readonly static string BTC_DEPOSIT =/*                     */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BTC_Deposit);
        private readonly static string BTC_WITHDRAW =/*                    */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BTC_Withdraw);
        private readonly static string BTC_AGGREGATING =/*                 */string.Format("{0}.{1}", (int)Space.ProtocolIds, (int)ProtocolType.BTC_Aggregating);

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
                case Enum.FrozenBalance:/*                         */return FROZEN_BALANCE;
                case Enum.CommitteeFrozenBalance:/*                */return COMMITTEE_FROZEN_BALANCE;
                case Enum.Contract:/*                              */return CONTRACT;
                case Enum.ContractResult:/*                        */return CONTRACT_RESULT;
                case Enum.ETH_Address:/*                           */return ETH_ADDRESS;
                case Enum.ETH_Deposit:/*                           */return ETH_DEPOSIT;
                case Enum.ETH_Withdraw:/*                          */return ETH_WITHDRAW;
                case Enum.ERC20_Token:/*                           */return ERC20_TOKEN;
                case Enum.ERC20_DepositToken:/*                    */return ERC20_DEPOSIT_TOKEN;
                case Enum.ERC20_WithdrawToken:/*                   */return ERC20_WITHDRAW_TOKEN;
                case Enum.BTC_Address:/*                           */return BTC_ADDRESS;
                case Enum.BTC_IntermediateDeposit:/*               */return BTC_INTERMEDIATE_DEPOSIT;
                case Enum.BTC_Deposit:/*                           */return BTC_DEPOSIT;
                case Enum.BTC_Withdraw:/*                          */return BTC_WITHDRAW;
                case Enum.BTC_Aggregating:/*                       */return BTC_AGGREGATING;
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
            if (FROZEN_BALANCE.Equals(value))/*                    */return Enum.FrozenBalance;
            if (COMMITTEE_FROZEN_BALANCE.Equals(value))/*          */return Enum.CommitteeFrozenBalance;
            if (CONTRACT.Equals(value))/*                          */return Enum.Contract;
            if (CONTRACT_RESULT.Equals(value))/*                   */return Enum.ContractResult;
            if (ETH_ADDRESS.Equals(value))/*                       */return Enum.ETH_Address;
            if (ETH_DEPOSIT.Equals(value))/*                       */return Enum.ETH_Deposit;
            if (ETH_WITHDRAW.Equals(value))/*                      */return Enum.ETH_Withdraw;
            if (ERC20_TOKEN.Equals(value))/*                       */return Enum.ERC20_Token;
            if (ERC20_DEPOSIT_TOKEN.Equals(value))/*               */return Enum.ERC20_DepositToken;
            if (ERC20_WITHDRAW_TOKEN.Equals(value))/*              */return Enum.ERC20_WithdrawToken;
            if (BTC_ADDRESS.Equals(value))/*                       */return Enum.BTC_Address;
            if (BTC_INTERMEDIATE_DEPOSIT.Equals(value))/*          */return Enum.BTC_IntermediateDeposit;
            if (BTC_DEPOSIT.Equals(value))/*                       */return Enum.BTC_Deposit;
            if (BTC_WITHDRAW.Equals(value))/*                      */return Enum.BTC_Withdraw;
            if (BTC_AGGREGATING.Equals(value))/*                   */return Enum.BTC_Aggregating;
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