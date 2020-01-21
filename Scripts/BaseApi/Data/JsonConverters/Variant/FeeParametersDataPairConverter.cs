using System;
using Base.Config;
using Base.Data.Operations.Fee;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class FeeParametersDataPairConverter : JsonCustomConverter<FeeParametersData, JArray>
    {
        protected override FeeParametersData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.FeeParameters)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.FeeParameters.TransferOperation:
                    return TransferOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.TransferToAddressOperation:
                //    return TransferToAddressOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.OverrideTransferOperation:
                    return OverrideTransferOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountCreateOperation:
                    return AccountCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountUpdateOperation:
                    return AccountUpdateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountWhitelistOperation:
                    return AccountWhitelistOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.AccountAddressCreateOperation:
                //    return AccountAddressCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetCreateOperation:
                    return AssetCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetUpdateOperation:
                    return AssetUpdateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetUpdateBitassetOperation:
                    return AssetUpdateBitassetOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetUpdateFeedProducersOperation:
                    return AssetUpdateFeedProducersOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetIssueOperation:
                    return AssetIssueOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetReserveOperation:
                    return AssetReserveOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetFundFeePoolOperation:
                    return AssetFundFeePoolOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetPublishFeedOperation:
                    return AssetPublishFeedOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetClaimFeesOperation:
                    return AssetClaimFeesOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ProposalCreateOperation:
                    return ProposalCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ProposalUpdateOperation:
                    return ProposalUpdateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ProposalDeleteOperation:
                    return ProposalDeleteOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.CommitteeMemberCreateOperation:
                    return CommitteeMemberCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.CommitteeMemberUpdateOperation:
                    return CommitteeMemberUpdateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.CommitteeMemberUpdateGlobalParametersOperation:
                    return CommitteeMemberUpdateGlobalParametersOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.CommitteeMemberActivateOperation:
                //    return CommitteeMemberActivateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.CommitteeMemberDeactivateOperation:
                //    return CommitteeMemberDeactivateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.CommitteeFrozenBalanceDepositOperation:
                //    return CommitteeFrozenBalanceDepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.CommitteeFrozenBalanceWithdrawOperation:
                //    return CommitteeFrozenBalanceWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.VestingBalanceCreateOperation:
                    return VestingBalanceCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.VestingBalanceWithdrawOperation:
                    return VestingBalanceWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.BalanceClaimOperation:
                    return BalanceClaimOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.BalanceFreezeOperation:
                //    return BalanceFreezeOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.BalanceUnfreezeOperation:
                //    return BalanceUnfreezeOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractCreateOperation:
                //    return ContractCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ContractCallOperation:
                    return ContractCallOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractInternalCreateOperation:
                //    return ContractInternalCreateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractInternalCallOperation:
                //    return ContractInternalCallOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractSelfdestructOperation:
                //    return ContractSelfdestructOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractUpdateOperation:
                //    return ContractUpdateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractFundPoolOperation:
                //    return ContractFundPoolOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractWhitelistOperation:
                //    return ContractWhitelistOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_CreateAddressOperation:
                //    return Sidechain_ETH_CreateAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_ApproveAddressOperation:
                //    return Sidechain_ETH_ApproveAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_DepositOperation:
                //    return Sidechain_ETH_DepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_SendDepositOperation:
                //    return Sidechain_ETH_SendDepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_WithdrawOperation:
                //    return Sidechain_ETH_WithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_SendWithdrawOperation:
                //    return Sidechain_ETH_SendWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_ApproveWithdrawOperation:
                //    return Sidechain_ETH_ApproveWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ETH_UpdateContractAddressOperation:
                //    return Sidechain_ETH_UpdateContractAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainIssueOperation:
                //    return SidechainIssueOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainBurnOperation:
                //    return SidechainBurnOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_RegisterTokenOperation:
                //    return Sidechain_ERC20_RegisterTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_DepositTokenOperation:
                //    return Sidechain_ERC20_DepositTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_SendDepositTokenOperation:
                //    return Sidechain_ERC20_SendDepositTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_WithdrawTokenOperation:
                //    return Sidechain_ERC20_WithdrawTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_SendWithdrawTokenOperation:
                //    return Sidechain_ERC20_SendWithdrawTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_ApproveTokenWithdrawOperation:
                //    return Sidechain_ERC20_ApproveTokenWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_IssueOperation:
                //    return Sidechain_ERC20_IssueOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_ERC20_BurnOperation:
                //    return Sidechain_ERC20_BurnOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_CreateAddressOperation:
                //    return Sidechain_BTC_CreateAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_CreateIntermediateDepositOperation:
                //    return Sidechain_BTC_CreateIntermediateDepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_IntermediateDepositOperation:
                //    return Sidechain_BTC_IntermediateDepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_DepositOperation:
                //    return Sidechain_BTC_DepositOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_WithdrawOperation:
                //    return Sidechain_BTC_WithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_AggregateOperation:
                //    return Sidechain_BTC_AggregateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.Sidechain_BTC_ApproveAggregateOperation:
                //    return Sidechain_BTC_ApproveAggregateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.BlockRewardOperation:
                //    return BlockRewardOperationFeeParametersData.Create(value.Last as JObject);
                default:
                //    CustomTools.Console.DebugError("Unexpected fee parameters type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(FeeParametersData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JObject.Parse(value.ToString()));
        }
    }
}