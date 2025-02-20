# ![.BitMart.Net](https://raw.githubusercontent.com/JKorf/BitMart.Net/main/BitMart.Net/Icon/icon.png) BitMart.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/BitMart.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/BitMart.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/BitMart.Net?style=for-the-badge)

BitMart.Net is a client library for accessing the [BitMart REST and Websocket API](https://developer-pro.bitmart.com/).

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/BitMart.net.svg?style=for-the-badge)](https://www.nuget.org/packages/BitMart.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/BitMart.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/BitMart.Net)

	dotnet add package BitMart.Net
	
### GitHub packages
BitMart.Net is available on [GitHub packages](https://github.com/JKorf/BitMart.Net/pkgs/nuget/BitMart.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/BitMart.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/BitMart.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/BitMart.Net/releases).

	
## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USDT ticker via rest request
	var restClient = new BitMartRestClient();
	var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT");
	var lastPrice = tickerResult.Data.LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USDT ticker updates via the websocket API
	var socketClient = new BitMartSocketClient();
	var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", (update) => 
	{
	  var lastPrice = update.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples [here](https://github.com/JKorf/BitMart.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
BitMart.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

### Spot Rest
|API|Supported|Location|
|--|--:|--|
|System Status|✓|`restClient.SpotApi.ExchangeData`|
|Public Market Data|✓|`restClient.SpotApi.ExchangeData`|
|Funding Account|✓|`restClient.SpotApi.Account`|
|Spot/Margin Trading|✓|`restClient.SpotApi.Trading`|
|Margin Loan|✓|`restClient.SpotApi.Margin`|
|Sub Account|✓|`restClient.SpotApi.SubAccount`|

### Spot Websocket
|API|Supported|Location|
|--|--:|--|
|Public|✓|`socketClient.SpotApi`|
|Private|✓|`socketClient.SpotApi`|

### Futures
|API|Supported|Location|
|--|--:|--|
|Futures Market Data|✓|`restClient.FuturesApi.ExchangeData`|
|Futures Account Data|✓|`restClient.FuturesApi.Account`|
|Futures Trading|✓|`restClient.FuturesApi.Trading`|
|Sub Account|✓|`restClient.FuturesApi.SubAccount`|

### Spot Websocket
|API|Supported|Location|
|--|--:|--|
|Public|✓|`socketClient.FuturesApi`|
|Private|✓|`socketClient.FuturesApi`|

### API Broker
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
Any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd 

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 1.14.0 - 20 Feb 2025
    * Added restClient.SpotApi.Account.GetWithdrawalAddressesAsync endpoint
    * Added startTime/endTime filter to restClient.SpotApi.Account.GetDepositHistoryAsync and GetWithdrawalHistoryAsync
    * Added needUsdValuation to restClient.SpotApi.Account.GetFundingBalancesAsync endpoint
    * Added asset parameter to restClient.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync endpoint
    * Added restClient.UsdFuturesApi.ExchangeData.GetMarkKlinesAsync endpoint
    * Added socketClient.UsdFuturesApi.SubscribeToMarkKlineUpdatesAsync subscription
    * Added symbol specific overloads for socketClient.UsdFuturesApi.SubscribeToTickerUpdatesAsync subscription

* Version 1.13.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Fix Mono runtime exception on rest client construction using DI

* Version 1.12.2 - 22 Jan 2025
    * Added BuyerIsMaker property to socketClient.UsdFuturesApi.SubscribeToTradeUpdatesAsync update model
    * Added Side to futures API shared interfaces trade subscription

* Version 1.12.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to BitMartExchange class

* Version 1.12.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Added restClient.UsdFuturesApi.Trading.PlaceTrailingOrderAsync endpoint
    * Added restClient.UsdFuturesApi.Trading.CancelTrailingOrderAsync endpoint
    * Improved websocket disconnect detection
    * Removed trailing stop parameters from restClient.UsdFuturesApi.Trading.PlaceOrderAsync

* Version 1.11.0 - 12 Dec 2024
    * Added socketClient.UsdFuturesApi.SubscribeToBookTickerUpdatesAsync stream subscription
    * Added restClient.UsdFuturesApi.ExchangeData.GetFundingRateHistoryAsync endpoint
    * Added restClient.UsdFuturesApi.Account.GetTransactionHistoryAsync endpoint

* Version 1.10.1 - 05 Dec 2024
    * Fixed clientOrderId parameter serialization in restClient.SpotApi.Trading.PlaceMultipleOrdersAsync

* Version 1.10.0 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added socketClient.UsdFuturesApi.SubscribeToFundingRateUpdatesAsync stream
    * Added Approval enum mapping for Status property on socketClient.UsdFuturesApi.SubscribeToOrderUpdatesAsync update model
    * Removed clientOrderId parameter from restCLient.UsdFuturesApi.Trading.EditTpSlOrderAsync
    * Fixed orderbook creation via BitMartOrderBookFactory

* Version 1.9.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated PlaceMarginOrderAsync ratelimit from 1 per second per key to 20
    * Updated BitMartOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 1.8.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to BitMartExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant BitMartSocketClient constructor

* Version 1.7.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 1.6.0 - 04 Nov 2024
    * Added socketClient.UsdFuturesApi.SubscribeToOrderBookSnapshotUpdatesAsync subscription
    * Added socketClient.UsdFuturesApi.SubscribeToOrderBookIncrementalUpdatesAsync subscription
    * Added IOrderBookSocketClient to UsdFuturesApi Shared socket implementations
    * Added MaxMarketOrderQuantity to BitMartContract response model

* Version 1.5.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to BitMartExchange class
    * Added support Side setting on SharedTrade model
    * Added BitMartTrackerFactory for creating trackers
    * Added overload to Create method on BitMartOrderBookFactory support SharedSymbol parameter

* Version 1.4.0 - 21 Oct 2024
    * Added restClient.UsdFuturesApi.Account.GetSymbolTradeFeeAsync endpoint
    * Added TakerFeeRateD and MakerFeeRateD properties to restClient.SpotApi.Account.GetBaseTradeFeesAsync response model
    * Added FundingIntervalHours to restClient.UsdFuturesApi.ExchangeData.GetContractsAsync response model

* Version 1.3.2 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization

* Version 1.3.1 - 14 Oct 2024
    * Fixed symbol formatting order book factory
    * Fixed IBitMartOrderBookFactory DI lifetime

* Version 1.3.0 - 08 Oct 2024
    * Added UsdFuturesApi.Trading.PlaceTpSlOrderAsync endpoint
    * Added UsdFuturesApi.Trading.EditTpSlOrderAsync endpoint
    * Added UsdFuturesApi.Trading.EditTriggerOrderAsync endpoint
    * Added UsdFuturesApi.Trading.EditPresetTriggerOrderAsync endpoint
    * Added clientOrderId parameter to CancelOrderAsync and CancelTriggerOrderAsync endpoints
    * Added planType parameter to GetTriggerOrdersAsync endpoint

* Version 1.2.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Spot and Usd Futures Rest and Socket clients
    * Added SpotApi.Account.GetDepositHistoryAsync endpoint
    * Added SpotApi.Account.GetwithdrawalHistoryAsync endpoint
    * Updated Sourcelink package version
    * Updated FuturesKlineInterval, FuturesStreamKlineInterval and KlineStreamInterval Enum values to match number of seconds
    * Updated TradeStatus property type from string to SymbolStatus? Enum on BitMartSymbol model
    * Fixed UsdFuturesApi.Trading.GetClosedOrdersAsync and GetUserTradesAsync startTime/endTime filter
    * Marked ISpotClient references as deprecated

* Version 1.1.3 - 11 Sep 2024
    * Fixed SpotApi Websocket error response parsing

* Version 1.1.2 - 25 Aug 2024
    * Added websocket connection ratelimiter
    * Updated CryptoExchange.Net to version 7.11.1 to fix some reconnect issues, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.1

* Version 1.1.1 - 09 Aug 2024
    * Fixed SpotApi.GetSymbolName not being implemented

* Version 1.1.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0
    * Updated XML code comments
    * Added SpotApi.Trading.CancelAllOrdersAsync endpoint

* Version 1.0.1 - 02 Aug 2024
    * Removed temporary workaround for UsdFuturesApi.Account.GetBalancesAsync as endpoint is fixed

* Version 1.0.0 - 30 Jul 2024
    * Initial release

