using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// TradFi group type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradFiGroup>))]
    public enum TradFiGroup
    {
        /// <summary>
        /// ["<c>US_MARKET</c>"] US market
        /// </summary>
        [Map("US_MARKET")]
        UsMarket,
        /// <summary>
        /// ["<c>FOREX</c>"] Forex
        /// </summary>
        [Map("FOREX")]
        Forex,
        /// <summary>
        /// ["<c>HK_STOCK</c>"] HK stock
        /// </summary>
        [Map("HK_STOCK")]
        HkStock,
        /// <summary>
        /// ["<c>INDEX_UK</c>"] UK Index
        /// </summary>
        [Map("INDEX_UK")]
        UkIndex,
        /// <summary>
        /// ["<c>INDEX_JP</c>"] JP Index
        /// </summary>
        [Map("INDEX_JP")]
        JpIndex,
        /// <summary>
        /// ["<c>INDEX_HK</c>"] HK Index
        /// </summary>
        [Map("INDEX_HK")]
        HkIndex,
        /// <summary>
        /// ["<c>INDEX_AU</c>"] AU Index
        /// </summary>
        [Map("INDEX_AU")]
        AuIndex,
        /// <summary>
        /// ["<c>INDEX_TW</c>"] TW Index
        /// </summary>
        [Map("INDEX_TW")]
        TwIndex,
        /// <summary>
        /// ["<c>INDEX_DE</c>"] DE Index
        /// </summary>
        [Map("INDEX_DE")]
        DeIndex,
        /// <summary>
        /// ["<c>INDEX_KR</c>"] KR Index
        /// </summary>
        [Map("INDEX_KR")]
        KrIndex,
        /// <summary>
        /// ["<c>METAL_LME</c>"] London metal exchange
        /// </summary>
        [Map("METAL_LME")]
        MetalLme,
        /// <summary>
        /// ["<c>COMMODITY_CME</c>"] Cme group commodities
        /// </summary>
        [Map("COMMODITY_CME")]
        CommodityCme,
        /// <summary>
        /// ["<c>COMMODITY_ICE</c>"] ICE commodities
        /// </summary>
        [Map("COMMODITY_ICE")]
        CommodityIce,
        /// <summary>
        /// ["<c>PRE_LIST</c>"] Pre-listing
        /// </summary>
        [Map("PRE_LIST")]
        PreListing,
    }
}
