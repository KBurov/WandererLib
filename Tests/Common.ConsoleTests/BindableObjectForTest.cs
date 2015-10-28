using System.Reflection;

namespace Wanderer.Library.Common.ConsoleTests
{
    internal class BindableObjectForTest : BindableObject
    {
        #region Variables
        private string _useString;
        private string _useStringVerified;
        private string _useExpression;
        private string _useExpressionVerified;
        private string _useMethodBase;
        private string _useMethodBaseVerified;
        #endregion

        public string UseString
        {
            get { return _useString; }
            set
            {
                _useString = value;

                RaisePropertyChangedNotVerified("UseString");
            }
        }

        public string UseStringVerified
        {
            get { return _useStringVerified; }
            set
            {
                _useStringVerified = value;

                RaisePropertyChanged("UseStringVerified");
            }
        }

        public string UseExpression
        {
            private get { return _useExpression; }
            set
            {
                _useExpression = value;

                RaisePropertyChangedNotVerified(() => UseExpression);
            }
        }

        public string UseExpressionVerified
        {
            private get { return _useExpressionVerified; }
            set
            {
                _useExpressionVerified = value;

                RaisePropertyChanged(() => UseExpressionVerified);
            }
        }

        public string UseMethodBase
        {
            get { return _useMethodBase; }
            set
            {
                _useMethodBase = value;

                RaisePropertyChangedNotVerified(MethodBase.GetCurrentMethod().Name.Remove(0, 4));
            }
        }

        public string UseMethodBaseVerified
        {
            get { return _useMethodBaseVerified; }
            set
            {
                _useMethodBaseVerified = value;

                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Remove(0, 4));
            }
        }
    }
}
