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
                //case ChainTypes.Operation.TransferToAddress:
                //case ChainTypes.Operation.OverrideTransfer:
                case ChainTypes.Operation.AccountCreate:
                    return AccountCreateOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.AccountUpdate:
                //case ChainTypes.Operation.AccountWhitelist:
                //case ChainTypes.Operation.AccountAddressCreate:
                //case ChainTypes.Operation.AssetCreate:
                //case ChainTypes.Operation.AssetUpdate:
                //case ChainTypes.Operation.AssetUpdateBitasset:
                //case ChainTypes.Operation.AssetUpdateFeedProducers:
                case ChainTypes.Operation.AssetIssue:
                    return AssetIssueOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.AssetReserve:
                //case ChainTypes.Operation.AssetFundFeePool:
                //case ChainTypes.Operation.AssetPublishFeed:
                //case ChainTypes.Operation.AssetClaimFees:
                case ChainTypes.Operation.ProposalCreate:
                    return ProposalCreateOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.ProposalUpdate:
                //case ChainTypes.Operation.ProposalDelete:
                //case ChainTypes.Operation.CommitteeMemberCreate:
                //case ChainTypes.Operation.CommitteeMemberUpdate:
                //case ChainTypes.Operation.CommitteeMemberUpdateGlobalParameters:
                //case ChainTypes.Operation.CommitteeMemberActivate:
                //case ChainTypes.Operation.CommitteeMemberDeactivate:
                //case ChainTypes.Operation.CommitteeFrozenBalanceDeposit:
                //case ChainTypes.Operation.CommitteeFrozenBalanceWithdraw:
                //case ChainTypes.Operation.VestingBalanceCreate:
                //case ChainTypes.Operation.VestingBalanceWithdraw:
                //case ChainTypes.Operation.BalanceClaim:
                //case ChainTypes.Operation.BalanceFreeze:
                //case ChainTypes.Operation.BalanceUnfreeze:
                //case ChainTypes.Operation.ContractCreate:
                case ChainTypes.Operation.ContractCall:
                    return ContractCallOperationData.Create(value.Last as JObject);
                //case ChainTypes.Operation.ContractInternalCreate:
                //case ChainTypes.Operation.ContractInternalCall:
                //case ChainTypes.Operation.ContractSelfdestruct:
                //case ChainTypes.Operation.ContractUpdate:
                //case ChainTypes.Operation.ContractFundPool:
                //case ChainTypes.Operation.ContractWhitelist:
                //case ChainTypes.Operation.Sidechain_ETH_CreateAddress:
                //case ChainTypes.Operation.Sidechain_ETH_ApproveAddress:
                //case ChainTypes.Operation.Sidechain_ETH_Deposit:
                //case ChainTypes.Operation.Sidechain_ETH_SendDeposit:
                //case ChainTypes.Operation.Sidechain_ETH_Withdraw:
                //case ChainTypes.Operation.Sidechain_ETH_SendWithdraw:
                //case ChainTypes.Operation.Sidechain_ETH_ApproveWithdraw:
                //case ChainTypes.Operation.Sidechain_ETH_UpdateContractAddress:
                //case ChainTypes.Operation.SidechainIssue:
                //case ChainTypes.Operation.SidechainBurn:
                //case ChainTypes.Operation.Sidechain_ERC20_RegisterToken:
                //case ChainTypes.Operation.Sidechain_ERC20_DepositToken:
                //case ChainTypes.Operation.Sidechain_ERC20_SendDepositToken:
                //case ChainTypes.Operation.Sidechain_ERC20_WithdrawToken:
                //case ChainTypes.Operation.Sidechain_ERC20_SendWithdrawToken:
                //case ChainTypes.Operation.Sidechain_ERC20_ApproveTokenWithdraw:
                //case ChainTypes.Operation.Sidechain_ERC20_Issue:
                //case ChainTypes.Operation.Sidechain_ERC20_Burn:
                //case ChainTypes.Operation.Sidechain_BTC_CreateAddress:
                //case ChainTypes.Operation.Sidechain_BTC_CreateIntermediateDeposit:
                //case ChainTypes.Operation.Sidechain_BTC_IntermediateDeposit:
                //case ChainTypes.Operation.Sidechain_BTC_Deposit:
                //case ChainTypes.Operation.Sidechain_BTC_Withdraw:
                //case ChainTypes.Operation.Sidechain_BTC_Aggregate:
                //case ChainTypes.Operation.Sidechain_BTC_ApproveAggregate:
                //case ChainTypes.Operation.BlockReward:
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