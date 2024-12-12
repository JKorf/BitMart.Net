using BitMart.Net.Enums;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiAccount
    {
        /// <summary>
        /// Get account balances
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-contract-assets-keyed" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesBalance>>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account transfer history
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-transfer-list-signed" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset, for example `USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesTransfer>>> GetTransferHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between futures and spot account
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#transfer-signed" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `USDT`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="type">Transfer direction</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTransferResult>> TransferAsync(string asset, decimal quantity, FuturesTransferType type, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#submit-leverage-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="marginType">Open type, required at close position</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartLeverage>> SetLeverageAsync(string symbol, decimal leverage, MarginType marginType, CancellationToken ct = default);

        /// <summary>
        /// Get symbol trading fee
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-trade-fee-rate-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartFuturesFeeRate>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-transaction-history-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for Example `ETHUSDT`</param>
        /// <param name="flowType">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartFuturesTransaction>>> GetTransactionHistoryAsync(string? symbol = null, FlowType? flowType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
    }
}
