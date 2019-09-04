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
                case ChainTypes.FeeParameters.AccountCreateOperation:
                    return AccountCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountUpdateOperation:
                    return AccountUpdateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountWhitelistOperation:
                    return AccountWhitelistOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AccountTransferOperation:
                    return AccountTransferOperationFeeParametersData.Create(value.Last as JObject);
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
                case ChainTypes.FeeParameters.VestingBalanceCreateOperation:
                    return VestingBalanceCreateOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.VestingBalanceWithdrawOperation:
                    return VestingBalanceWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.BalanceClaimOperation:
                    return BalanceClaimOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.OverrideTransferOperation:
                    return OverrideTransferOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.AssetClaimFeesOperation:
                    return AssetClaimFeesOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.CreateContractOperation:
                //    return CreateContractOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ContractCallOperation:
                    return ContractCallOperationFeeParametersData.Create(value.Last as JObject);
                case ChainTypes.FeeParameters.ContractTransferOperation:
                    return ContractTransferOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainChangeConfigOperation:
                //    return SidechainChangeConfigOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.AccountAddressCreateOperation:
                //    return AccountAddressCreateOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.TransferToAddressOperation:
                //    return TransferToAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHCreateAddressOperation:
                //    return SidechainETHCreateAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHApproveAddressOperation:
                //    return SidechainETHApproveAddressOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHDepositETHOperation:
                //    return SidechainETHDepositETHOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHWithdrawOperation:
                //    return SidechainETHWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHApproveWithdrawOperation:
                //    return SidechainETHApproveWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractFundPoolOperation:
                //    return ContractFundPoolOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractWhitelistOperation:
                //    return ContractWhitelistOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHIssueOperation:
                //    return SidechainETHIssueOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainETHBurnOperation:
                //    return SidechainETHBurnOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainERC20RegisterTokenOperation:
                //    return SidechainERC20RegisterTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainERC20DepositTokenOperation:
                //    return SidechainERC20DepositTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainERC20WithdrawTokenOperation:
                //    return SidechainERC20WithdrawTokenOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.SidechainERC20ApproveTokenWithdrawOperation:
                //    return SidechainERC20ApproveTokenWithdrawOperationFeeParametersData.Create(value.Last as JObject);
                //case ChainTypes.FeeParameters.ContractUpdateOperation:
                //    return ContractUpdateOperationFeeParametersData.Create(value.Last as JObject);
                default:
                    CustomTools.Console.DebugError("Unexpected fee parameters type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(FeeParametersData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JObject.Parse(value.ToString()));
        }
    }
}