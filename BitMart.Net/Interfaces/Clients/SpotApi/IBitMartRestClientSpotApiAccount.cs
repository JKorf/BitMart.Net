using BitMart.Net.Enums;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitMartRestClientSpotApiAccount
    {
        /// <summary>
        /// Get funding account balances
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-account-balance-keyed" /></para>
        /// </summary>
        /// <param name="asset">Filter on asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartBalance>>> GetFundingBalancesAsync(string? asset = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get spot account balances
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-spot-wallet-balance-keyed" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartSpotBalance>>> GetSpotBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit address
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#deposit-address-keyed" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal quotas
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#withdraw-quota-keyed" /></para>
        /// </summary>
        /// <param name="asset">Asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartWithdrawalQuota>> GetWithdrawalQuotaAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#withdraw-signed" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="targetAddress">Target blockchain address</param>
        /// <param name="memo">Memo</param>
        /// <param name="remark">Remark</param>
        /// <param name="accountDestType">Account destination type for internal withdrawal</param>
        /// <param name="targetAccount">Target account</param>
        /// <param name="areaCode">Area phone code</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartWithdrawId>> WithdrawAsync(string asset, decimal quantity, string? targetAddress = null, string? memo = null, string? remark = null, string? accountDestType = null, string? targetAccount = null, string? areaCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-deposit-and-withdraw-history-keyed" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartDepositWithdrawal>>> GetDepositHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-deposit-and-withdraw-history-keyed" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartDepositWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get a specific withdrawal or deposit
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-a-deposit-or-withdraw-detail-keyed" /></para>
        /// </summary>
        /// <param name="id">The deposit or withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartDepositWithdrawal>> GetDepositWithdrawalAsync(string id, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account info
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-margin-account-details-isolated-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartIsolatedMarginAccount>>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between Spot and Isolated Margin account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#margin-asset-transfer-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="direction">Direction</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTransferId>> IsolatedMarginTransferAsync(string symbol, string asset, decimal quantity, TransferDirection direction, CancellationToken ct = default);

        /// <summary>
        /// Get base trading fees
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-basic-fee-rate-keyed" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFeeRate>> GetBaseTradeFeesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get trading fees for a specific symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-actual-trade-fee-rate-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartSymbolTradeFee>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal addresses
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#withdraw-address-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartWithdrawalAddress>>> GetWithdrawalAddressesAsync(CancellationToken ct = default);
    }
}
