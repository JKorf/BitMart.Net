using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMart.Net
{
    internal static class BitMartErrors
    {
        public static ErrorMapping SpotRestErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "API key error", "30002", "30003", "30011", "30012"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP address error", "30010", "40006"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "30019"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Restricted based on region", "40047"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Signature invalid", "30005"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp invalid", "30007", "30008", "40008"),

                new ErrorInfo(ErrorType.Unauthorized, false, "Deposit not allowed", "60020"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Withdrawal not allowed", "60021"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Withdrawal not allowed for 24 hours", "60022"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Institutional verification required", "61007"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Account restricted based on region", "53001", "53008"),
                new ErrorInfo(ErrorType.Unauthorized, false, "KYC verification required", "53002", "53006", "53007"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "53003", "53005"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Symbol not allowed based on region", "53004"),
                new ErrorInfo(ErrorType.Unauthorized, false, "QR code certification required", "53009"),

                new ErrorInfo(ErrorType.SystemError, false, "Internal server error", "60051"),
                new ErrorInfo(ErrorType.SystemError, false, "Internal server exception", "60052"),
                new ErrorInfo(ErrorType.SystemError, true, "Service unavailable", "50022"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum precision exceeded", "60006"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid address", "60011"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal amount invalid", "60013"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal memo invalid", "60014"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Check withdrawal target", "60053"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Asset not supported", "60055"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime and endTime should be less than 90 days", "60066", "60067"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "60000"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime value invalid", "71001"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "EndTime value invalid", "71002"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime or endTime value invalid", "71003"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Kline limit parameter error", "71004", "50004"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Limit/offset parameter error", "50013"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Limit min value is 1", "50015"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Offset min value is 1", "50017"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "50021"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order book size error", "50024"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameters do not match", "50029"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId can be a max length of 32 characters", "50037"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId only allows numbers and letters", "50038"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Period out of range", "50041"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "EndTime must be greater than startTime", "52004"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Quantity parameter required", "50010"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Price parameter required", "50011"),
                new ErrorInfo(ErrorType.MissingParameter, false, "QuoteQuantity parameter required", "50012"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Limit parameter required", "50014"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Offset parameter required", "50016"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameters", "50028"),
                new ErrorInfo(ErrorType.MissingParameter, false, "One of orderId and clientOrderId is required", "50039"),

                new ErrorInfo(ErrorType.UnknownAsset, false, "Asset does not exist", "60002", "51000"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "70002"),
                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "50001"),

                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol unavailable", "50040"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "50042"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "50005"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too small", "60005", "51015"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity should be greater than 0", "61000"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Less than minimum quantity", "50006", "51009"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "More than maximum quantity", "50007"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Less than minimum order value", "50009", "51011"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity should be between 0 and 10", "50033"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Less than minimum price", "50008", "51010"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "More than maximum price", "50025"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Buy order price can't be higher than open price", "50026"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Sell order price can't be lower than open price", "50027"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "60008", "61001", "50020"),

                new ErrorInfo(ErrorType.IncorrectState, false, "Order already canceled", "50030"),
                new ErrorInfo(ErrorType.IncorrectState, false, "Order already completed", "50031"),
                new ErrorInfo(ErrorType.IncorrectState, false, "Order matched or canceled", "50032"),
                new ErrorInfo(ErrorType.IncorrectState, false, "Order failed to cancel", "50036"),
            ]
        );

        public static ErrorMapping SpotSocketErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "API key error", "91001", "91002", "91003", "91004"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Signature error", "91010", "91011", "91021"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp error", "91022", "91023"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "90003"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid channel", "90004"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid subscription", "90009"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many topics in single subscription", "90005"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol invalid", "92001"),

                new ErrorInfo(ErrorType.RateLimitSubscription, false, "Too many subscriptions", "90006"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many subscriptions", "90007"),

                new ErrorInfo(ErrorType.RateLimitConnection, false, "Too many connection attempts", "94001"),
                new ErrorInfo(ErrorType.RateLimitConnection, false, "Too many connection", "94002"),

                new ErrorInfo(ErrorType.DuplicateSubscription, false, "Duplicate subscription", "90008"),

                new ErrorInfo(ErrorType.SystemError, true, "Duplicate subscription", "95000"),
            ]
        );

        public static ErrorMapping FuturesRestErrors { get; } = new ErrorMapping(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "API key error", "30002", "30003", "30011", "30012"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP address error", "30010", "40006"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "30019"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Restricted based on region", "40047"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Signature invalid", "30005"),

                new ErrorInfo(ErrorType.InvalidTimestamp, false, "Timestamp invalid", "30007", "30008", "40008"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "30013", "30017", "40013"),

                new ErrorInfo(ErrorType.SystemError, true, "Service unavailable", "30014"),
                new ErrorInfo(ErrorType.SystemError, true, "Service maintenance", "30016"),
                new ErrorInfo(ErrorType.SystemError, true, "System error", "40012"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "40007"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order leverage too large", "40029"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order leverage too small", "40030"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Difference between current price and trigger price too large", "40031"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Plan order life cycle too long", "40032"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Plan order life cycle too short", "40033"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Kline interval is invalid", "40038"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Timestamp is invalid", "40039"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage is invalid", "40040"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OrderSide is invalid", "40041"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OrderType is invalid", "40042"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order precision invalid", "40043"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order range is invalid", "40044"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order open type is invalid", "40045"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId can be a max length of 32 characters", "40049"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId only allows numbers and letters", "40048"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "40050"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol does not exist", "40034"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "40035", "40037"),

                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is not trading", "40014"),
                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol is not currently trading", "40015"),

                new ErrorInfo(ErrorType.NoPosition, false, "Position does not exist", "40021"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "40027", "42000"),

                new ErrorInfo(ErrorType.RateLimitOrder, false, "Order rate limit reached", "40028"),

                new ErrorInfo(ErrorType.IncorrectState, false, "Order status is invalid", "40036"),

            ]
        );
    }
}
