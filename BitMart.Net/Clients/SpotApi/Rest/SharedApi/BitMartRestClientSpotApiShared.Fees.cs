using BitMart.Net.Enums;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartRestClientSpotSharedApi
    {
        #region Get Fees

        public GetFeeOptions GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        async Task<ICallResult<SharedFee>> IGetFees.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
            => await GetFeesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedFee>> GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await _api.Account.GetSymbolTradeFeeAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            // Return
            return HttpResult.Ok(result, new SharedFee(result.Data.BuyMakerFeeRate * 100, result.Data.BuyTakerFeeRate * 100));
        }

        #endregion
    }
}
