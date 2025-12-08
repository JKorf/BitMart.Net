using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart subaccount endpoints
    /// </summary>
    public interface IBitMartRestClientSpotApiSubAccount
    {
        /// <summary>
        /// Transfer from sub account to main account, usable from main account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#sub-account-to-main-account-for-main-account-signed" /></para>
        /// </summary>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="subAccount">Sub account user name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferSubToMainForMainAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default);

        /// <summary>
        /// Transfer from sub account to main account, usable from sub account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#sub-account-to-main-account-for-sub-account-signed" /></para>
        /// </summary>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferSubToMainForSubAsync(string clientOrderId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer from main account to a sub account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#main-account-to-sub-account-for-main-account-signed" /></para>
        /// </summary>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferMainToSubAccountAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default);

        /// <summary>
        /// Transfer from sub account to another sub account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#sub-account-to-sub-account-for-main-account-signed" /></para>
        /// </summary>
        /// <param name="clientOrderId">Unique identifier</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="fromAccount">Source sub account name</param>
        /// <param name="toAccount">Target sub account name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> TransferSubAccountToSubAccountAsync(string clientOrderId, decimal quantity, string asset, string fromAccount, string toAccount, CancellationToken ct = default);

        /// <summary>
        /// Get sub account transfer history for main account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-sub-account-transfer-history-for-main-account-keyed" /></para>
        /// </summary>
        /// <param name="account">Filter by account name</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryForMainAsync(int limit, string? account = null, CancellationToken ct = default);

        /// <summary>
        /// Get sub accont transfer history 
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-account-spot-asset-transfer-history-for-main-sub-account-keyed" /></para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryAsync(int limit, CancellationToken ct = default);

        /// <summary>
        /// Get the balance of a sub account
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-sub-account-spot-wallet-balance-for-main-account-keyed" /></para>
        /// </summary>
        /// <param name="subAccount">Sub account name</param>
        /// <param name="asset">Filter by asset, for example `ETH`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartSubAccountBalance[]>> GetSubAcccountBalanceAsync(string subAccount, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of sub accounts
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-sub-account-list-for-main-account-keyed" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartSubAccount[]>> GetSubAccountListAsync(CancellationToken ct = default);

    }
}
