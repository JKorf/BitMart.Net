using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
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
        /// <param name="asset">Filter on asset</param>
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
        /// <param name="asset">The asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default);
        
        /// <summary>
        /// Get withdrawal quotas
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#withdraw-quota-keyed" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartWithdrawalQuota>> GetWithdrawalQuotaAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#withdraw-signed" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="targetAddress">Target blockchain address</param>
        /// <param name="memo">Memo</param>
        /// <param name="remark">Remark</param>
        /// <param name="accountDestType">Account destination type for internal withdrawal</param>
        /// <param name="targetAccount">Target account</param>
        /// <param name="areaCode">Area phone code</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartWithdrawId>> WithdrawAsync(string asset, decimal quantity, string? targetAddress = null, string? memo = null, string? remark = null, string? accountDestType = null, string? targetAccount = null, string? areaCode = null, CancellationToken ct = default);

    }
}