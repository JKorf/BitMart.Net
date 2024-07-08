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
    /// BitMart margin endpoints
    /// </summary>
    public interface IBitMartRestClientSpotApiMargin
    {
        /// <summary>
        /// Borrow an asset
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#margin-borrow-isolated-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to borrow</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartBorrowId>> BorrowAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay an asset
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#margin-repay-isolated-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartRepayId>> RepayAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get borrow history
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-borrow-record-isolated-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="borrowId">Filter by borrow id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BorrowRecord>>> GetBorrowHistoryAsync(string symbol, string? borrowId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-repayment-record-isolated-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="repayId">Filter by repay id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<RepayRecord>>> GetRepayHistoryAsync(string symbol, string? asset = null, string? repayId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow rate and quantity info
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-trading-pair-borrowing-rate-and-amount-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BorrowInfo>>> GetBorrowInfoAsync(string? symbol = null, CancellationToken ct = default);

    }
}
