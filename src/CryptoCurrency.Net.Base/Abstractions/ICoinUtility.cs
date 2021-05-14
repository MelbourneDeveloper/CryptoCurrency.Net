namespace CryptoCurrency.Net.Base.Abstractions
{
    public interface ICoinUtility
    {
        uint GetCoinType(string coinShortCut);
        string GetCoinShortcut(uint coinType);
        string GetCoinName(uint coinType);
    }
}
