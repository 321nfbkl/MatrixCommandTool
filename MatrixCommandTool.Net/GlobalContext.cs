using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net
{
    public class GlobalContext
    {
        private static object lockObj = new object();
        private static GlobalContext mCurrent;

        public static GlobalContext Current
        {
            get
            {
                if (mCurrent == null)
                {
                    lock (lockObj)
                    {
                        if (mCurrent == null)
                        {
                            mCurrent = new GlobalContext();
                        }
                    }
                }

                return mCurrent;
            }
        }
    }
}
