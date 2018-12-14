namespace CryptoCurrency.Net.Model.JSONRPC
{
    public enum DefaultBlock
    {
        /// <summary>
        /// for the earliest/genesis block
        /// </summary>
        earliest,

        /// <summary>
        /// for the latest mined block
        /// </summary>
        latest,

        /// <summary>
        /// for the pending state/transactions
        /// </summary>
        pending
    }
}
