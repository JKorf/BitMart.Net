using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    public enum StpMode
    {
        /// <summary>
        /// ["<c>1</c>"] Cancel maker order
        /// </summary>
        [Map("1")]
        CancelMaker,
        /// <summary>
        /// ["<c>2</c>"] Cancel taker order
        /// </summary>
        [Map("2")]
        CancelTaker,
        /// <summary>
        /// ["<c>3</c>"] Cancel both orders
        /// </summary>
        [Map("3")]
        CancelBoth
    }
}
