using System.Threading.Tasks;

namespace Hardwarewallets.Net
{
    public interface ITransactionSigner
    {
        Task<T2> SignTransaction<T, T2>(T transaction)
        where T : ITransaction
        where T2 : ISignedTransaction;
    }
}
