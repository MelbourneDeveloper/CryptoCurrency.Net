using System.Collections.Generic;
using System.Linq;

namespace CryptoCurrency.Net.Model
{
    public class Transaction
    {
        public List<TransactionPiece> Inputs { get; } = new List<TransactionPiece>();
        public List<TransactionPiece> Outputs { get; } = new List<TransactionPiece>();

        public decimal Value => Outputs.Sum(o => o.Amount);
    }
}
