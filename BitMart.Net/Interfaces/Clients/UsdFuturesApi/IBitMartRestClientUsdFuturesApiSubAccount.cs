using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures sub-account endpoints
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiSubAccount
    {
        /// <summary>
        /// Transfer from sub futures account to main account, for main account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#sub-account-to-main-account-for-main-account-signed" /><br />
        /// Endpoint:<br />
        /// POST /account/contract/sub-account/main/v1/sub-to-main
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `USDT`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferSubToMainForMainAsync(string asset, decimal quantity, string subAccount, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Transfer from main account to sub futures acount
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#main-account-to-sub-account-for-main-account-signed" /><br />
        /// Endpoint:<br />
        /// POST /account/contract/sub-account/main/v1/main-to-sub
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `USDT`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferMainToSubForMainAsync(string asset, decimal quantity, string subAccount, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Transfer from sub futures account to main account, for sub account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#sub-account-to-main-account-for-sub-account-signed" /><br />
        /// Endpoint:<br />
        /// POST /account/contract/sub-account/sub/v1/sub-to-main
        /// </para>
        /// </summary>
        /// <param name="asset">The asset, for example `USDT`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferSubToMainForSubAsync(string asset, decimal quantity, string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Get sub account futures balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-sub-account-futures-wallet-balance-for-main-account-keyed" /><br />
        /// Endpoint:<br />
        /// GET /account/contract/sub-account/main/v1/wallet
        /// </para>
        /// </summary>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="asset">The asset, for example `USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartSubAccountBalance[]>> GetSubAcccountBalanceAsync(string subAccount, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get sub account transfer history, for main account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-sub-account-transfer-history-for-main-account-keyed" /><br />
        /// Endpoint:<br />
        /// GET /account/contract/sub-account/main/v1/transfer-list
        /// </para>
        /// </summary>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<SubAccountTransfer[]>> GetSubAccountTransferHistoryForMainAsync(string subAccount, int limit, CancellationToken ct = default);

        /// <summary>
        /// Get sub account transfer history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#get-account-futures-asset-transfer-history-for-main-sub-account-keyed" /><br />
        /// Endpoint:<br />
        /// GET /account/contract/sub-account/v1/transfer-history
        /// </para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<SubAccountTransfer[]>> GetSubAccountTransferHistoryAsync(int limit, CancellationToken ct = default);

    }
}
