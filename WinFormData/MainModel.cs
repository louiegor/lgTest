using System.Configuration;
using System.Threading;

namespace WinFormData
{
    public class MainModel
    {
        public XmlHelper Api { get; set; }
        public FileWatcher fw { get; set; }

        public int RemainingShare(int positionShare, int wantToBuyShare)
        {
            var result = 0;
            if ((positionShare / wantToBuyShare) < 1)
            {
                result = wantToBuyShare - positionShare;
                result.RoundUpToNearest100();
            }
            return result;
        }

        public void ExecuteBuyOrder(string s)
        {
            Api = new XmlHelper();
            var remaining = Global.ShareSize;
            
            while (remaining > 0)
            {
                var openPos = Api.GetOpenPositionForSymbol(s);
                Api.CancelBuyOrdersForSymbol(s);
                remaining = RemainingShare(openPos.Volume, Global.ShareSize);
                var l1 = Api.GetL1(s);
                Api.ExecuteOrder("Buy", s, l1.AskPrice);
                Thread.Sleep(10000);
            }
        }

        public abstract class Site
        {
            public string Uri
            {
                get { return ConfigurationManager.AppSettings["chrome"]; }
            }
        }
    }
}
