using System;

namespace CryptoCurrency.Net.APIClients.BlockchainClients
{
    public interface IAPIClient
    {
        Uri BaseUri { get; }
        decimal SuccessRate { get; }
        TimeSpan AverageCallTimespan { get; }
        int SuccessfulCallCount { get; }
        int CallCount { get; }
    }
}
