using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormData
{
    public class MainModel
    {
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

        public abstract class Site
        {
            public string Uri
            {
                get { return ConfigurationManager.AppSettings["chrome"]; }
            }
        }
    }
}
