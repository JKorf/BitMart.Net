using BitMart.Net.Enums;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-contract-assets-keyed" /><br />
        /// Endpoint:<br />
        /// GET /contract/private/assets-detail
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-transfer-list-signed" /><br />
        /// Endpoint:<br />
        /// POST /account/v1/transfer-contract-list
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Filter by asset, for example `USDT`</param>
        /// <param name="startTime">["<c>time_start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>time_end</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page number</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesTransfer[]>> GetTransferHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between futures and spot account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#transfer-signed" /><br />
        /// Endpoint:<br />
        /// POST /account/v1/transfer-contract
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `USDT`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="type">["<c>type</c>"] Transfer direction</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTransferResult>> TransferAsync(string asset, decimal quantity, FuturesTransferType type, CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#submit-leverage-signed" /><br />
        /// Endpoint:<br />
        /// POST /contract/private/submit-leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="marginType">["<c>open_type</c>"] Open type, required at close position</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartLeverage>> SetLeverageAsync(string symbol, decimal leverage, MarginType marginType, CancellationToken ct = default);

        /// <summary>
        /// Get symbol trading fee
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-trade-fee-rate-keyed" /><br />
        /// Endpoint:<br />
        /// GET /contract/private/trade-fee-rate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartFuturesFeeRate>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get transaction history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-transaction-history-keyed" /><br />
        /// Endpoint:<br />
        /// GET /contract/private/transaction-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for Example `ETHUSDT`</param>
        /// <param name="flowType">["<c>flow_type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>time_start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>time_end</c>"] Filter by end time</param>
        /// <param name="limit">["<c>page_size</c>"] Max number of results, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartFuturesTransaction[]>> GetTransactionHistoryAsync(string? symbol = null, FlowType? flowType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Set the position mode of the account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#set-position-mode-signed" /><br />
        /// Endpoint:<br />
        /// POST /contract/private/set-position-mode
        /// </para>
        /// </summary>
        /// <param name="positionMode">["<c>position_mode</c>"] Position mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default);

        /// <summary>
        /// Get the current position  mode of the account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-position-mode-keyed" /><br />
        /// Endpoint:<br />
        /// GET /contract/private/get-position-mode
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartPositionMode>> GetPositionModeAsync(CancellationToken ct = default);
    }
}
