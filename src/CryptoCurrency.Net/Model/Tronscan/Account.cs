using System.Collections.Generic;

#pragma warning disable CA2227 // Collection properties should be read only
#pragma warning disable CA1056 // Uri properties should not be strings

namespace CryptoCurrency.Net.Model.Tronscan
{
    public class Trc20tokenBalances
    {
        public string symbol { get; set; }
        public string balance { get; set; }
        public int decimals { get; set; }
        public string name { get; set; }
        public string contract_address { get; set; }
    }

    public class Bandwidth
    {
        public int energyRemaining { get; set; }
        public long totalEnergyLimit { get; set; }
        public int totalEnergyWeight { get; set; }
        public int netUsed { get; set; }
        public int storageLimit { get; set; }
        public double storagePercentage { get; set; }
        public double netPercentage { get; set; }
        public int storageUsed { get; set; }
        public int storageRemaining { get; set; }
        public int freeNetLimit { get; set; }
        public int energyUsed { get; set; }
        public int freeNetRemaining { get; set; }
        public int netLimit { get; set; }
        public int netRemaining { get; set; }
        public int energyLimit { get; set; }
        public int freeNetUsed { get; set; }
        public long totalNetWeight { get; set; }
        public double freeNetPercentage { get; set; }
        public double energyPercentage { get; set; }
        public long totalNetLimit { get; set; }
    }

    public class Frozen
    {
        public int total { get; set; }
        public List<object> balances { get; set; }
    }

    public class FrozenBalanceForEnergy
    {
    }

    public class AccountResource
    {
        public FrozenBalanceForEnergy frozen_balance_for_energy { get; set; }
    }

    public class TokenBalance
    {
        public object balance { get; set; }
        public string name { get; set; }
        public string owner_address { get; set; }
    }

    public class Balance
    {
        public object balance { get; set; }
        public string name { get; set; }
        public string owner_address { get; set; }
    }

    public class ReceivedDelegatedResource
    {
        public long expire_time_for_energy { get; set; }
        public int frozen_balance_for_energy { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }

    public class Delegated
    {
        public List<object> sentDelegatedBandwidth { get; set; }
        public List<object> sentDelegatedResource { get; set; }
        public List<ReceivedDelegatedResource> receivedDelegatedResource { get; set; }
        public List<object> receivedDelegatedBandwidth { get; set; }
    }

    public class Representative
    {
        public int lastWithDrawTime { get; set; }
        public int allowance { get; set; }
        public bool enabled { get; set; }
        public string url { get; set; }
    }

    public class Account
    {
        public List<Trc20tokenBalances> trc20token_balances { get; set; }
        public List<object> allowExchange { get; set; }
        public string address { get; set; }
        public List<object> frozen_supply { get; set; }
        public Bandwidth bandwidth { get; set; }
        public int accountType { get; set; }
        public List<object> exchanges { get; set; }
        public Frozen frozen { get; set; }
        public AccountResource accountResource { get; set; }
        public List<TokenBalance> tokenBalances { get; set; }
        public List<Balance> balances { get; set; }
        public decimal balance { get; set; }
        public string name { get; set; }
        public Delegated delegated { get; set; }
        public Representative representative { get; set; }
    }
}

#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1056 // Uri properties should not be strings
