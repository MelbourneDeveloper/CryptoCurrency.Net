namespace CryptoCurrency.Net.APIClients.Model.JSONRPC
{
    public class ResultBase
    {
        /// <summary>
        /// A String specifying the version of the JSON-RPC protocol. MUST be exactly "2.0".
        /// </summary>
        public string JSONRPC { get; set; }

        /// <summary>
        /// This member is REQUIRED. It MUST be the same as the value of the id member in the Request Object. If there was an error in detecting the id in the Request object (e.g.Parse error/Invalid Request), it MUST be Null.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This member is REQUIRED on success. This member MUST NOT exist if there was an error invoking the method. The value of this member is determined by the method invoked on the Server.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// This member is REQUIRED on error. This member MUST NOT exist if there was no error triggered during invocation. The value for this member MUST be an Object as defined in section 5.1.
        /// </summary>
        public Error Error { get; set; }
    }
}
