using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CryptoCurrency.Net.Model
{
    public class ModelBase : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Protected Methods
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
