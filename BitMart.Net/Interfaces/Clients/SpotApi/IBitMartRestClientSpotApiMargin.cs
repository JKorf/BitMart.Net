using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart margin endpoints
    /// </summary>
    public interface IBitMartRestClientSpotApiMargin
    {
        /// <summary>
        /// Borrow an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#margin-borrow-isolated-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v1/margin/isolated/borrow
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to borrow</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartBorrowId>> BorrowAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#margin-repay-isolated-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v1/margin/isolated/repay
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartRepayId>> RepayAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get borrow history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-borrow-record-isolated-keyed" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/margin/isolated/borrow_record
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="borrowId">Filter by borrow id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BorrowRecord[]>> GetBorrowHistoryAsync(string symbol, string? borrowId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-repayment-record-isolated-keyed" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/margin/isolated/repay_record
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="repayId">Filter by repay id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<RepayRecord[]>> GetRepayHistoryAsync(string symbol, string? asset = null, string? repayId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow rate and quantity info
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-trading-pair-borrowing-rate-and-amount-keyed" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/margin/isolated/pairs
        /// </para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BorrowInfo[]>> GetBorrowInfoAsync(string? symbol = null, CancellationToken ct = default);

    }
}
