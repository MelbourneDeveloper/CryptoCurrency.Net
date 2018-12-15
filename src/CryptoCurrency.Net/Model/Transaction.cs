using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Net.Model
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public List<TransactionPiece> Inputs { get; } = new List<TransactionPiece>();
        public List<TransactionPiece> Outputs { get; } = new List<TransactionPiece>();

        public decimal Value => Outputs.Sum(o => o.Value);

        public Transaction()
        {
        }


        public Transaction(string transactionHash)
        {
            TransactionId = transactionHash;
        }
    }
}
