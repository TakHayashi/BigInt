using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CS_BigIntArith1;

namespace CS_BigIntArith0
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BigDigitCal());
        }
        static public int RX = 10000;
        static public int Maxv = 500;
        static public int prgr = 0;
        static public bool abrt = false;
        static public int gfx = 0;
    }
}
