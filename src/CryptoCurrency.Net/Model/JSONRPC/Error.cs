namespace CryptoCurrency.Net.Model.JSONRPC
{
#pragma warning disable CA1716 // Identifiers should not match keywords
    public class Error
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        /// <summary>
        /// A Number that indicates the error type that occurred. This MUST be an integer.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// A String providing a short description of the error. The message SHOULD be limited to a concise single sentence.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A Primitive or Structured value that contains additional information about the error. This may be omitted. The value of this member is defined by the Server (e.g.detailed error information, nested errors etc.).
        /// </summary>
        public string Data { get; set; }
    }
}
