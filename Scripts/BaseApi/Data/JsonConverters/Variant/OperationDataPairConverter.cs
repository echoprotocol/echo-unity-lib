using System;
using Base.Config;
using Base.Data.Operations;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class OperationDataPairConverter : JsonCustomConverter<OperationData, JArray>
    {
        protected override OperationData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.Operation)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.Operation.Transfer:
                    return TransferOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.LimitOrderCreate:
                //case ChainTypes.Operation.LimitOrderCancel:
                //case ChainTypes.Operation.CallOrderUpdate:
                //case ChainTypes.Operation.FillOrder:
                case ChainTypes.Operation.AccountCreate:
                    return AccountCreateOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.AccountUpdate:
                //case ChainTypes.Operation.AccountWhitelist:
                //case ChainTypes.Operation.AccountUpgrade:
                //case ChainTypes.Operation.AccountTransfer:
                //case ChainTypes.Operation.AssetCreate:
                //case ChainTypes.Operation.AssetUpdate:
                //case ChainTypes.Operation.AssetUpdateBitasset:
                //case ChainTypes.Operation.AssetUpdateFeedProducers:
                case ChainTypes.Operation.AssetIssue:
                    return AssetIssueOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.AssetReserve:
                //case ChainTypes.Operation.AssetFundFeePool:
                //case ChainTypes.Operation.AssetSettle:
                //case ChainTypes.Operation.AssetGlobalSettle:
                //case ChainTypes.Operation.AssetPublishFeed:
                case ChainTypes.Operation.ProposalCreate:
                    return ProposalCreateOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.ProposalUpdate:
                //case ChainTypes.Operation.ProposalDelete:
                //case ChainTypes.Operation.WithdrawPermissionCreate:
                //case ChainTypes.Operation.WithdrawPermissionUpdate:
                //case ChainTypes.Operation.WithdrawPermissionClaim:
                //case ChainTypes.Operation.WithdrawPermissionDelete:
                //case ChainTypes.Operation.CommitteeMemberCreate:
                //case ChainTypes.Operation.CommitteeMemberUpdate:
                //case ChainTypes.Operation.CommitteeMemberUpdateGlobalParameters:
                //case ChainTypes.Operation.VestingBalanceCreate:
                //case ChainTypes.Operation.VestingBalanceWithdraw:
                //case ChainTypes.Operation.Custom:
                //case ChainTypes.Operation.Assert:
                //case ChainTypes.Operation.BalanceClaim:
                //case ChainTypes.Operation.OverrideTransfer:
                //case ChainTypes.Operation.AssetSettleCancel:
                //case ChainTypes.Operation.AssetClaimFees:
                //case ChainTypes.Operation.BidCollateral:
                //case ChainTypes.Operation.ExecuteBid:
                //case ChainTypes.Operation.CreateContract:
                case ChainTypes.Operation.CallContract: // todo ??
                    return CallContractOperationData.Create(value.Last as JObject);
                case ChainTypes.Operation.ContractTransfer:
                    return ContractTransferOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.ChangeSidechainConfig:
                //case ChainTypes.Operation.AccountAddressCreate:
                //case ChainTypes.Operation.TransferToAddress:
                //case ChainTypes.Operation.GenerateETHAddress:
                //case ChainTypes.Operation.CreateETHAddress:
                //case ChainTypes.Operation.DepositETH:
                //case ChainTypes.Operation.WithdrawETH:
                //case ChainTypes.Operation.ApproveWithdrawETH:
                //case ChainTypes.Operation.ContractFundPool:
                //case ChainTypes.Operation.ContractWhitelist:
                //case ChainTypes.Operation.SidechainIssue:
                //case ChainTypes.Operation.SidechainBurn:
                //case ChainTypes.Operation.RegisterERC20token:
                //case ChainTypes.Operation.DepositERC20Token:
                //case ChainTypes.Operation.WithdrawERC20token:
                //case ChainTypes.Operation.ApproveERC20TokenWithdraw:
                //case ChainTypes.Operation.ContractUpdate:
                default:
                    CustomTools.Console.DebugError("Unexpected operation type:", type, '\n', value);
                    return null;
            }
        }

        protected override JArray Serialize(OperationData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JObject.Parse(value.ToString()));
        }
    }
}