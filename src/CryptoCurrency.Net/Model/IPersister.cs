using System;

namespace CryptoCurrency.Net.Model
{
    public interface IPersister
    {
        string LoadFile(string fileName);
        T LoadFile<T>(string fileName) where T : new();
        void SaveFile(string fileName, byte[] bytes, bool isEncrypted);
        void SaveFile(string fileName, object model, bool isEncrypted);
        void SaveFile(string fileName, string text, bool isEncrypted);
        byte[] LoadBytesFromFile(string fileName, bool isEncrypted);
        DateTime? GetModifiedDate(string fileName, bool isEncrypted);
    }
}
