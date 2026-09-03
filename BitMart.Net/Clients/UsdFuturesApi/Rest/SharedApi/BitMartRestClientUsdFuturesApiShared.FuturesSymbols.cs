using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;
using Microsoft.Extensions.Logging;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartRestClientUsdFuturesSharedApi
    {
        #region Futures Symbol client
        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicId, _api.EnvironmentName, null);

        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var contracts = await _api.ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!contracts.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(contracts);

            var data = contracts.Data
                .Select(x => ParseSymbol(x))
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(contracts, SharedUtils.ApplySymbolFilter(data, request));
        }

        private SharedFuturesSymbol ParseSymbol(BitMartContract s)
        {
            var result = new SharedFuturesSymbol(
                s.ProductType == ContractType.Perpetual ? TradingMode.PerpetualLinear : TradingMode.DeliveryLinear,
                s.BaseAsset,
                s.QuoteAsset,
                s.Symbol,
                true)
            {
                MinTradeQuantity = s.MinQuantity,
                DeliveryTime = s.SettleTime,
                PriceStep = s.PricePrecision,
                QuantityStep = s.QuantityPrecision,
                ContractSize = s.ContractQuantity,
                MaxTradeQuantity = s.MaxQuantity,
                MaxLongLeverage = s.MaxLeverage,
                MaxShortLeverage = s.MaxLeverage,
                QuoteAssetType = SharedAssetType.Crypto,
                QuoteAssetSubType = SharedAssetSubType.StableCoin,
                DisplayName = s.Symbol
            };

            if (s.TradfiInfo == null)
            {
                if (LibraryHelpers.IsCommodity(result.BaseAsset, "SLVON"))
                {
                    // Some symbols like PAXGUSDT or NGASUSDT aren't considered Tradfi by the API
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
                }

                result.BaseAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(s.BaseAsset))
                    result.BaseAssetSubType = SharedAssetSubType.StableCoin;
            }
            else
            {
                if (s.TradfiInfo.MarketGroup == TradFiGroup.UsMarket
                    || s.TradfiInfo.MarketGroup == TradFiGroup.HkStock)
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Equity;
                }
                else if (s.TradfiInfo.MarketGroup == TradFiGroup.AuIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.DeIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.HkIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.JpIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.KrIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.TwIndex
                    || s.TradfiInfo.MarketGroup == TradFiGroup.UkIndex)
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Equity;
                }
                else if (s.TradfiInfo.MarketGroup == TradFiGroup.MetalLme
                    || s.TradfiInfo.MarketGroup == TradFiGroup.CommodityCme
                    || s.TradfiInfo.MarketGroup == TradFiGroup.CommodityIce)
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                    result.BaseAssetSubType = SharedAssetSubType.Commodity;
                }
                else if (s.TradfiInfo.MarketGroup == TradFiGroup.Forex)
                {
                    result.BaseAssetType = SharedAssetType.Fiat;
                }
                else if (s.TradfiInfo.MarketGroup == TradFiGroup.PreListing)
                {
                    result.BaseAssetType = SharedAssetType.TradFi;
                }
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, _api.EnvironmentName, null, symbolName));
        }
        #endregion
    }
}
