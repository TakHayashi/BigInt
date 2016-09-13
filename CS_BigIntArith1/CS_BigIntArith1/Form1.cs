using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using CS_BigIntArith0;

namespace CS_BigIntArith1
{
    public partial class BigDigitCal : Form
    {
        delegate BI ArithOpe(BI a);
        delegate void ArithIn(string s);

        public int dispdigit, precision, repeat, GenDigit;
        public string Ver;
        private static BI seed;
        public BigDigitCal()
        {
            InitializeComponent();
            precision = 0;
            Program.Maxv = 500;
            textBox3.Text = precision.ToString(); // 10進4ケタ単位
            repeat = 1;
            txtRepeat.Text = repeat.ToString();
            dispdigit = 4;
            GenDigit = 1000;
            txtGenDigit.Text = GenDigit.ToString();

            comboDispdigit.Items.Add(4);    //区切り桁数設定
            comboDispdigit.Items.Add(5);
            comboDispdigit.Items.Add(10);
            comboDispdigit.SelectedIndex = 0; //区切り桁数を４に設定

            timer1.Enabled = true;
            timer1.Interval = 500;
            timer1.Stop();

            //System.Reflection.AssemblyDescriptionAttribute asmdc =
            //  (System.Reflection.AssemblyDescriptionAttribute)
            //  Attribute.GetCustomAttribute(
            //  System.Reflection.Assembly.GetExecutingAssembly(),
            //  typeof(System.Reflection.AssemblyDescriptionAttribute));

            // 実行しているアセンブリ(.exeのアセンブリ情報)取得
            var assm = Assembly.GetExecutingAssembly();
            // アセンブリのファイルパス取得
            var path = (new Uri(assm.CodeBase)).LocalPath;
            // アセンブリのFileVersionInfo取得
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            // ファイル名とバージョンを取得して表示する
            //Console.WriteLine("{0} {1}", versionInfo.FileName, versionInfo.FileVersion);
            Ver = versionInfo.FileVersion;
            this.Text += " Ver:" + Ver;

            Console.WriteLine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }

        //除算
        private void btnDivide_Click(object sender, EventArgs e)
        {
            //BI A = new BI();
            //ArithOpe a = A.BIDivN;
            //ArithIn ain = A.SetValByStr;
            //ArithCommon(a, ain);
            int fpoint = int.Parse(textBox3.Text);  //小数点以下精度指定読み出し
            BI w = new BI();
            w.size = 1;
            repeat = int.Parse(txtRepeat.Text);
            Stopwatch sw = new Stopwatch();
            BI A = new BI();
            BI B = new BI(), R = new BI();
            string s;
            //int rn=0;

            A.SetValByStr(txtIn1.Text);
            B.SetValByStr(txtIn2.Text);
            //int Bn = int.Parse(txtIn2.Text); 
            //下記ある場合非整数の場合textBox3に設定必要

            //A = A.BShiftL(A, fpoint - A.fxp); //小数点以下桁数
            //A.fxp = fpoint;            

            //int digit = int.Parse(textBox3.Text);
            this.Cursor = Cursors.WaitCursor;
            //int r = 0;
            sw.Start();
            for (int i = 1; i <= repeat - 1; i++)
                w = A.BIDiv(B, R);
            //w = A.BIDivInt(Bn, ref rn);
            //w = A.BIDivN(B);
            w = A.BIDiv(B, R);
            s = w.BIToStr(4);
            //s = A.BIDivInt(Bn, ref rn).BIToStr(4);
            //s = A.BIDivN(B).BIToStr(4);
            sw.Stop();
            long T1 = sw.ElapsedMilliseconds;
            string t = "Elapsed=" + T1.ToString("#,##0mSec");
            t = t + " MaxSize=" + Program.Maxv.ToString("###,###") + " xint32" + " digit=" + w.intdig;
            txtMessage.Text = t;
            this.Cursor = Cursors.Default;
            txtOut.Text = s;
            formating();

            txtAddtional.Text = R.BIToStr(dispdigit);
            //txtAddtional.Text = r.ToString();
            //textBox3.Text =( T.size).ToString();
            //txtIn2.Text = T.BIToStr();

        }
        //加算
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BI A = new BI(); //規定値より桁数大きなときコンストラクタで指定必要
            ArithOpe a = A.BIAdd;
            ArithIn ain = A.SetValByStr;
            ArithCommon(a, ain);
        }
        //減算
        private void btnSub_Click(object sender, EventArgs e)
        {
            BI A = new BI();
            ArithOpe a = A.BISub;
            ArithIn ain = A.SetValByStr;
            ArithCommon(a, ain);
        }
        //乗算
        private void btnMult_Click(object sender, EventArgs e)
        {
            BI A = new BI();
            ArithOpe a = A.BIMul2;
            ArithIn ain = A.SetValByStr;
            ArithCommon(a, ain);
        }
        //累乗 
        private void btnPower_Click(object sender, EventArgs e)
        {
            BI A = new BI();
            //string methodName = ((Func<BI>)this.A.BIPower).Method.Name;

            ArithOpe a = A.BIPower;
            ArithIn ain = A.SetValByStr;
            ArithCommon(a, ain);

        }
        private void btnGCD_Click(object sender, EventArgs e)
        {
            BI A = new BI();
            ArithOpe a = A.BIgcd;
            ArithIn ain = A.SetValByStr;
            ArithCommon(a, ain);
        }
        private void ArithCommon(ArithOpe a, ArithIn ain)
        {
            repeat = int.Parse(txtRepeat.Text);

            BI w = new BI();
            Stopwatch sw = new Stopwatch();
            string s;
            ain(txtIn1.Text);
            BI B = new BI();
            B.SetValByStr(txtIn2.Text);
            int Bdigit = B.size;
            this.Cursor = Cursors.WaitCursor;
            sw.Start();
            for (int i = 1; i <= repeat; i++)
                w = a(B);
            sw.Stop();
            s = w.BIToStr(4);
            long T1 = sw.ElapsedMilliseconds;
            string t = "Elapsed=" + T1.ToString("#,##0mSec");
            t = t + " MaxSize=" + Program.Maxv.ToString("###,###") + " xint32.";
            this.Cursor = Cursors.Default;
            t += "  Result size=" + w.size.ToString() + " digit=" + w.intdig;
            txtMessage.Text = t;
            txtOut.Text = s;
            formating();
            txtAddtional.Text = "";
        }

        //Calculate "e"
        private void btnExp_Click(object sender, EventArgs e)
        {

            int MaxF = int.Parse(textBox3.Text);
            int digit = MaxF;
            BI E = new BI(MaxF), w = new BI(MaxF), one = new BI(MaxF), Fact = new BI(MaxF);
            BI big = new BI(MaxF), R = new BI(MaxF), tmp = new BI(MaxF);
            Stopwatch sw = new Stopwatch();
            System.Diagnostics.Debug.WriteLine("..");
            E.SetValByInt(2);
            E = E.BShiftL(E, digit);
            this.Cursor = Cursors.WaitCursor;
            w.SetValByInt(1);
            tmp = w.BShiftL(w, digit);
            BI.CpyBI(one, tmp, tmp.size - 1, tmp.size);
            Fact.SetValByInt(1);
            big.SetValByInt(0);
            sw.Start();

            for (int i = 2; i <= MaxF; i++)
            {
                big.v[0] = i;
                Fact = Fact.BIMul2(big);
                w = one.BIDiv(Fact, R);
                E = E.BIAdd(w);

            }
            sw.Stop();
            this.Cursor = Cursors.Default;
            long T1 = sw.ElapsedMilliseconds;
            string t = "Elapsed=" + T1.ToString("#,##0mSec");
            t += " MaxSize=" + Program.Maxv.ToString("###,###") + " digit=" + E.intdig;
            txtMessage.Text = t;
            E.fxp = digit;
            string s = E.BIToStr(4);

            txtOut.Text = s;
            formating();
            txtAddtional.Text = "";

            //            BI.Size = 1
            //ReDim BI.Data(1)
            //BI.Data(0) = 2
            //BI = BSHIFTL(BI, Digit)
            //Me.Cursor = Cursors.WaitCursor

            //sw.Start()
            //W.Size = 1
            //ReDim W.Data(1)
            //W.Data(0) = 1
            //one = BSHIFTL(W, Digit)
            //Dim FACT As BInt
            //ReDim FACT.Data(1)
            //FACT.Size = 1
            //FACT.Data(0) = 1
            //Dim bigI As BInt
            //bigI.Size = 1
            //ReDim bigI.Data(1)
            //For i = 2 To MaxF
            //    bigI.Data(0) = i
            //    FACT = BMul(FACT, bigI)
            //    W = BDIV(one, FACT, R)
            //    BI = BADD(BI, W)
            //Next
            //sw.Stop()
            //Me.Cursor = Cursors.Default
        }

        //Pai Calculation term=200 digit=200で140桁
        //Term=300 214桁
        private void btnPai_Click(object sender, EventArgs e)
        {
            BI p = new BI();
            Stopwatch sw = new Stopwatch();

            int digit = int.Parse(textBox3.Text);
            if (digit == 0)
            {
                txtMessage.Text = "Fixed point digit must >0";
                return;
            }

            int term = digit * 2;

            this.Cursor = Cursors.WaitCursor;
            sw.Start();
            timer1.Start();
            p = p.BIPai(term, digit);
            //p = p.BIPiLegendre(term, digit);
            //string s = p.BIPai(200, 200).BIToStr(4); // Calculation

            sw.Stop();
            timer1.Stop();
            progressBar1.Value = 0;

            this.Cursor = Cursors.Default;
            //p.fxp = digit;
            string s = p.BIToStr(4); // Calculation
            long T1 = sw.ElapsedMilliseconds;

            txtOut.Text = s;
            formating();
            //string fn = @System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "PAI_1M_Digit.txt";
            string refer = Properties.Resources.PAI_1M_Digit;

            //string fn = @"C:\Users\TAK\Documents\Visual Studio 2013\Projects\CS_BigIntArith1\CS_BigIntArith1\PAI_1M_Digit.txt";
            int ND = matching2(refer, s);
            txtAddtional.Text = "小数点以下 " + (ND - 1).ToString() + "桁まで一致"; //整数部含む一致桁数
            string t = "Pai Calculation:" + " Term=" + term + " Digit=" + digit + " Correct=" + ND.ToString();
            t = t + " : Elapsed=" + T1.ToString("#,##0mSec");
            t = t + " MaxSize=" + Program.Maxv.ToString("###,###");
            txtMessage.Text = t;

            //m_reader.Close();
        }
        private void btnCrypt_Click(object sender, EventArgs e)
        {
            PubKeyCrypto("");
        }
        private void PubKeyCrypto(string s)
        {
            BI pa = new BI(), qa = new BI(), pb = new BI(), qb = new BI();
            BI bla = new BI(), da = new BI();
            BI pq = new BI(), pqm = new BI(), ka = new BI();
            BI alb = new BI();
            //int  kb, lb;
            BI one = new BI(), t = new BI(), r = new BI(), biN = new BI();
            Stopwatch sw = new Stopwatch();
            precision = 0;
            textBox3.Text = precision.ToString(); // 10進4ケタ単位

            one.SetValByStr("1");
            //pa.SetValByStr("7254714113916947");
            //qa.SetValByStr("3254714113214549");
            if (txtIn1.Text != "") pa.SetValByStr(txtIn1.Text);
            else pa.SetValByInt(19);
            if (txtIn2.Text != "") qa.SetValByStr(txtIn2.Text);
            else qa.SetValByInt(11);

            this.Cursor = Cursors.WaitCursor;
            sw.Start();

            while (!BI.BIisPrime(pa))
                pa = pa.BIAdd(one);
            txtIn1.Text = pa.BIToStr(4);
            while (!BI.BIisPrime(qa))
                qa = qa.BIAdd(one);
            txtIn2.Text = qa.BIToStr(4);

            s = "";
            s = "p=" + pa.BIToStr(4);
            s += "\r\n";
            s += "q=" + qa.BIToStr(4);
            s += "\r\n";
            pq = pa.BIMul2(qa); //public key
            s += "p*q=" + pq.BIToStr(4);
            s += "\r\n";
            ka.SetValByInt(100);
            int ika = ka.BI2Int(); //公開
            pq.BIDiv(ka, r);
            pqm = pa.BISub(one).BIMul2(qa.BISub(one));
            s += "(p-1)*(q-1)=" + pqm.BIToStr(4);
            da.SetValByInt(101);
            int m = 1000;
            int i = 2;

            do
            {
                do   //kaを求める
                {
                    //biN.SetValByInt(i);
                    ka = ka.BIAdd(one);
                    pqm.BIDiv(ka, r);
                }
                while (BI.BIisZero(r));

                s += "\r\n" + "ka=" + ka.BIToStr(4);
                s += "\r\n";
                do   //daを求める
                {
                    biN.SetValByInt(i);
                    da = (one.BIAdd(pqm.BIMul2(biN))).BIDiv(ka, r);
                    i++;
                }
                while (!BI.BIisZero(r));

                m--;
            }
            while (BI.BIisZero(da) && m > 0);
            if (m <= 0)
            {
                txtMessage.Text = "Loop Over to find ka/da pair";
                return;
            }

            s += "da=" + da.BIToStr(4);
            s += "\r\n";
            string msg = "Are you alive?　お元気ですか？";
            s += "\r\n" + "\r\n" + "原文文字列: ";
            s += msg;

            int cn = msg.Length;

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = sjisEnc.GetBytes(msg);
            cn = bytes.Length;

            s += "\r\n" + "\r\n" + "原文Byte列: ";
            s += "\r\n";
            //s+=BitConverter.ToString(bytes);
            for (i = 0; i < cn; i++)
                s += bytes[i].ToString() + ",";

            s += "\r\n" + "\r\n" + "暗号化長整数列: " + "\r\n";
            BI bd = new BI();
            BI[] enc = new BI[cn];
            for (i = 0; i < cn; i++)
            {
                bd.SetValByInt((int)bytes[i]);
                //bd.BIPower(ka).BIDiv(pq,r);
                r = bd.BIPowm(ka, pq);
                //y = r.v[0]+r.v[1]*10000;
                enc[i] = r;
                s += r.BIToStr(4) + ",";
            }

            s += "\r\n" + "\r\n" + "復号後Byte列: " + "\r\n";
            //復号
            byte[] dec = new byte[cn];
            for (i = 0; i < cn; i++)
            {
                r = enc[i].BIPowm(da, pq);
                dec[i] = (byte)r.v[0];
                s += dec[i].ToString() + ",";
            }

            s += "\r\n";
            s += "\r\n" + "復号後文字列: ";

            //Byte列->文字列変換
            sjisEnc = Encoding.GetEncoding("Shift_JIS");
            s += sjisEnc.GetString(dec);

            s += "\r\n";

            sw.Stop();
            this.Cursor = Cursors.Default;


            txtOut.Text = s;
            long T1 = sw.ElapsedMilliseconds;
            s = "Elapsed=" + T1.ToString("#,##0mSec");

            txtMessage.Text = s + " MaxSize=" + Program.Maxv.ToString("###,###");
            Program.Maxv = 500;
        }
        private void txtAddtional_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSQRT_Click(object sender, EventArgs e)
        {
            BI a = new BI(), fxp = new BI(), ret = new BI();
            string s;
            Stopwatch sw = new Stopwatch();
            int n = 0;
            int prec = int.Parse(textBox3.Text);
            a.SetValByStr(txtIn1.Text);

            if (prec == 0)
            {
                txtMessage.Text = "prescision must be >0";
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            BI ini = new BI();

            ini.v[0] = 4000; // as 50.4
            ini.v[1] = 50;
            ini.size = 2;
            ini.fxp = 1;
            sw.Start();
            ret = a.BISqrt(a, ini, prec, ref n);
            sw.Stop();

            s = ret.BIToStr(4);
            this.Cursor = Cursors.Default;

            //s = s + "." + fxp.BIToStr(4);
            txtOut.Text = s;
            formating();
            txtAddtional.Text = "";
            if (txtIn1.Text == "2")
            {
                //string fn = @"C:\Users\TAK\Documents\Visual Studio 2013\Projects\CS_BigIntArith1\CS_BigIntArith1\Root_of_2.txt";
                //プロジェクトのプロパティのリソースにファイルを既存の追加するとResourceに埋め込まれ、下記でアクセスできる
                string refer = Properties.Resources.Root_of_2;
                //string fn = @"C:\Users\TAK\Documents\Visual Studio 2013\Projects\CS_BigIntArith1\CS_BigIntArith1\PAI_1M_Digit.txt";
                int ND = matching2(refer, s);
                //int ND = matching(fn, s);
                txtAddtional.Text = ND.ToString() + "桁まで一致"; //整数部含む一致桁数
            }
            txtAddtional.Text += "  Iteration=" + n.ToString();

            long T1 = sw.ElapsedMilliseconds;
            s = "Elapsed=" + T1.ToString("#,##0mSec");
            s = s + " MaxSize=" + Program.Maxv.ToString("###,###");
            txtMessage.Text = s;

        }

        private int matching2(string refer, string s)
        {

            //string sc = m_reader.ReadToEnd();
            int M = refer.Length;
            //string ssc = "";
            System.Text.StringBuilder ssb = new System.Text.StringBuilder();
            for (int i = 0; i < M; i++)
                if (char.IsNumber(refer[i]))
                    ssb.Append(refer[i]);
            string ssc = ssb.ToString();
            int N = s.Length;
            string ss = "";
            for (int i = 0; i < s.Length; i++)
                if (char.IsNumber(s[i]))
                    ss = ss + s[i];

            int L = ss.Length;
            int ND = 0;
            for (int i = 0; i < L; i++)
            {
                if ((ss[i] != ssc[i]) || (i >= ssc.Length - 1))
                {
                    ND = i;
                    break;
                }
            }
            return ND;
        }

        private int matching(string fn, string s)
        {
            System.IO.StreamReader m_reader = null;
            m_reader = new System.IO.StreamReader(fn, System.Text.Encoding.Default);
            char[] c = null;
            c = new Char[3000];
            int rn = m_reader.Read(c, 0, 3000);
            //string sc = m_reader.ReadToEnd();
            string sc = new string(c);
            int M = sc.Length;
            string ssc = "";
            for (int i = 0; i < sc.Length; i++)
                if (char.IsNumber(sc[i]))
                    ssc = ssc + sc[i];

            int N = s.Length;
            string ss = "";
            for (int i = 0; i < s.Length; i++)
                if (char.IsNumber(s[i]))
                    ss = ss + s[i];

            int L = ss.Length;
            int ND = 0;
            for (int i = 0; i < L; i++)
            {
                if ((ss[i] != ssc[i]) || (i >= ssc.Length - 1))
                {
                    ND = i;
                    break;
                }
            }
            m_reader.Close();
            return ND;
        }
        private void formating()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            this.Cursor = Cursors.WaitCursor;
            string w = txtOut.Text, onechar, sout;
            int n = w.Length, nchar;
            for (int i = 0; i < n; i++)  // extract numeric and "."
            {
                onechar = w.Substring(i, 1);
                if (char.IsNumber(onechar, 0) || (onechar == ".")) sb.Append(onechar);
            }
            string buf = sb.ToString();
            n = buf.Length;
            int ipoint = buf.IndexOf(".");
            if (ipoint >= 0)
                nchar = ipoint;  //Start index of Decimal place 
            else
                nchar = n;

            sout = "";
            int rn = nchar % dispdigit;
            int ln = nchar / dispdigit;
            int nn;
            if (dispdigit == 4)
                nn = ln % 25;
            else if (dispdigit == 5)
                nn = ln % 20;
            else
                nn = ln % 10;
            int cr = 0;
            bool pt = false;
            if (rn != 0)
            {
                sout = new String(' ', dispdigit - rn);
                sout = sout + buf.Substring(0, rn);
                if (ipoint >= 0 && buf.Substring(rn, 1) == ".")
                {
                    sout += ".\r\n";
                    pt = true;
                    cr = 0;
                }
                else
                {
                    sout += " ";
                    cr = dispdigit + (100 - (nn + 1) * dispdigit);
                }
            }

            int cnt = 0;
            sb.Clear();
            for (int i = rn; i < n; i++)
            {
                onechar = buf.Substring(i, 1);
                if (onechar != ".")
                {
                    //sout = sout + onechar;
                    sb.Append(onechar);
                    cnt++;
                }
                else
                {
                    if (!pt) { sb.Append(onechar + "\r\n"); cr = 0; cnt = 0; };
                    //sb.Append("\r\n");     
                }
                string nx;
                if (i + 1 < n) nx = buf.Substring(i + 1, 1);
                else nx = "";
                if (cnt == dispdigit)
                {
                    if ((ipoint >= 0 && nx != ".") || ipoint < 0)
                    {
                        cnt = 0;

                        //sout += " ";
                        sb.Append(" ");
                        cr += dispdigit;
                        if (cr >= 100)
                        {
                            sb.Append("\r\n"); cr = 0;
                        }
                    }
                }
            }
            sout += sb.ToString();

            txtOut.Text = sout;
            this.Cursor = Cursors.Default;
        }

        private void comboDispdigit_SelectedIndexChanged(object sender, EventArgs e)
        {
            dispdigit = (int)comboDispdigit.SelectedItem;
            //テキストボックスの内容を再フォーマット
            formating();
        }

        private void BigDigitCal_Load(object sender, EventArgs e)
        {

        }

        private void txtGenDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GenDigit = int.Parse(txtGenDigit.Text);
            }
        }

        private void btnRND2_Click(object sender, EventArgs e)
        {
            GenDigit = int.Parse(txtGenDigit.Text);
            txtIn2.Text = GenRand();
        }

        private void btnRND1_Click(object sender, EventArgs e)
        {
            GenDigit = int.Parse(txtGenDigit.Text);
            txtIn1.Text = GenRand();
        }

        private string GenRand()
        {
            int seed = Environment.TickCount;
            Random rnd = new Random(seed++);
            //Random rnd = new Random();
            int n = 0;
            string s = "";
            for (int i = 1; i <= GenDigit; i++)
            {
                n = rnd.Next(1, 10);
                s += n.ToString();
            }
            return s;
        }
        private void BIRandx()
        {
            BI max = new BI(), x = new BI();
            string s = "";
            txtOut.Text = s;
            max.SetValByStr("20000");
            x.SetValByStr("282982855");
            for (int i = 1; i <= 10; i++)
            {
                x = BI.BIRand2(x, max);
                s += x.BIToStr(4);
                s += "\r\n";
            }
            txtOut.Text = s;
        }
        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
        //Prime
        //Bug 桁数設定を4以外にしたとき（
        private void btnTest_Click(object sender, EventArgs e)
        {
            BI p = new BI(), BIcnt = new BI(), x = new BI();
            int cnt;
            Stopwatch sw = new Stopwatch();

            txtOut.Text = "";
            string s = "";
            p.SetValByStr(txtIn1.Text);
            if (txtIn2.Text != "")
                cnt = int.Parse(txtIn2.Text);
            else
                cnt = 1;
            this.Cursor = Cursors.WaitCursor;
            sw.Start();
            int pc = 0;
            s = "";
            for (int i = 1; i <= cnt; i++)
            {
                if (BI.BIisPrime(p))
                {
                    s += p.BIToStr(dispdigit);
                    s += " is Prime" + " Digit=" + p.intdig + "\r\n";
                    pc++;
                }
                BIcnt.SetValByInt(i);
                p = p.BIAdd(BIcnt);
                //else s =p.BIAdd(BIcnt).BIToStr(dispdigit)+ " Not prime";

            }
            if (pc == 0) s = "Prime not found.";
            txtOut.Text = s;
            sw.Stop();
            this.Cursor = Cursors.Default;



            long T1 = sw.ElapsedMilliseconds;
            s = "Elapsed=" + T1.ToString("#,##0mSec");
            txtMessage.Text = s + " MaxSize=" + Program.Maxv.ToString("###,###");
            Program.Maxv = 500;
            //BIRandx();
            //BI ba = new BI();
            //ba.SetValByInt(111);
            //txtOut.Text = ba.BIPowm( 5, 7).BIToStr(dispdigit);
        }
        //
        private void btnPowm_Click(object sender, EventArgs e)
        {
            BI x = new BI(), n = new BI(), m = new BI(), ans = new BI();
            Stopwatch sw = new Stopwatch();

            txtOut.Text = "";
            string s = "";
            x.SetValByStr(txtIn1.Text);
            n.SetValByStr(txtIn2.Text);
            m.SetValByStr(txtIn3.Text);
            this.Cursor = Cursors.WaitCursor;
            sw.Start();

            ans = x.BIPowm(n, m);

            sw.Stop();
            this.Cursor = Cursors.Default;

            txtOut.Text = ans.BIToStr(dispdigit);

            long T1 = sw.ElapsedMilliseconds;
            s = "Elapsed=" + T1.ToString("#,##0mSec");
            txtMessage.Text = s + " MaxSize=" + Program.Maxv.ToString("###,###") + "digit=" + ans.intdig;
            Program.Maxv = 500;
            //BIRandx();
            //BI ba = new BI();
            //ba.SetValByInt(111);
            //txtOut.Text = ba.BIPowm( 5, 7).BIToStr(dispdigit);
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIn1.Text = "";
            txtIn2.Text = "";
            txtOut.Text = "";
            txtMessage.Text = "";
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            string s;
            long T1;
            int cnt;
            txtIn1.Text = "";
            txtIn2.Text = "";
            txtOut.Text = "";
            txtMessage.Text = "";
            Stopwatch sw = new Stopwatch();
            BI x = new BI();
            BI a = new BI(), b = new BI(), c = new BI();
            BI p = new BI();
            int sav = GenDigit;
            GenDigit = 1000;

            DateTime dtNow = DateTime.Now;
            dtNow.ToString();

            txtOut.Text += dtNow.ToString() + "\r\n" + "Test Data Digit=" + GenDigit.ToString("##,###") + "\r\n";
            txtOut.Text += "Version=" + Ver + "\r\n";
            txtOut.Refresh();

            a.SetValByStr(GenRand());
            b.SetValByStr(GenRand());


            x = a.BIAdd(b);
            x = x.BISub(a);
            x = x.BISub(b);
            if (!BI.BIisZero(x))
                txtMessage.Text = "Add/Sub Error.";

            cnt = 10000;
            this.Cursor = Cursors.WaitCursor;
            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                a.BIAdd(b);
            }
            sw.Stop();

            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=Add : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            this.Cursor = Cursors.WaitCursor;
            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                a.BIAddInt(1);
            }
            sw.Stop();

            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=AddInt : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                x.BISub(a);
            }
            sw.Stop();
            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=Sub : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            x = a.BIMul2(b);
            x = x.BIDivN(a);
            x = x.BISub(b);
            if (!BI.BIisZero(x))
                txtMessage.Text = "Mul/Div Error.";

            //b.SetValByInt(1000);
            cnt = 10000;
            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                a.BIMul2(b);
            }
            sw.Stop();
            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=Mul : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            cnt = 10000;
            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                a.BIMulInt(1000);
            }
            sw.Stop();
            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=MulInt : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            cnt = 200;
            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                a.BIDivN(b);
            }
            sw.Stop();
            T1 = sw.ElapsedMilliseconds;
            txtOut.Text += "Operation=Div : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            sw.Reset();
            sw.Start();
            for (int i = 1; i <= cnt; i++)
            {
                x = BI.BIFact(50);
            }
            sw.Stop();
            s = x.BIToStr(4);
            txtIn3.Text = s;
            txtOut.Text += "Operation=FACTORIAL : Rept=" + cnt.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();

            sw.Reset();
            sw.Start();
            int digit = 138;
            p = p.BIPai(digit, digit);
            sw.Stop();
            p.fxp = digit;
            s = p.BIToStr(4);
            string fn = @"C:\Users\TAK\Documents\Visual Studio 2013\Projects\CS_BigIntArith1\CS_BigIntArith1\PAI_1M_Digit.txt";
            int ND = matching(fn, s);
            txtOut.Text += "Operation=Pai : Precision=" + ND.ToString("##,###") + " Elapsed=" + T1.ToString("#,##0mSec") + "\r\n";
            txtOut.Refresh();


            this.Cursor = Cursors.Default;

            GenDigit = sav; //Recover GenDigit
        }

        private void btnFact_Click(object sender, EventArgs e)
        {

            BI w = new BI();

            /*TextでMethodを指定するコードのテスト
            BI r=new BI();
            r.size=1;  /これが無いとInvokeでrに値が返らない
            Type tt = w.GetType();
            object temporary = System.Activator.CreateInstance(tt);
            MethodInfo mi = tt.GetMethod("BIFact");
            int j = 10;
            r=(BI) mi.Invoke(temporary, new object[] { j });
            */
            repeat = int.Parse(txtRepeat.Text);
            Stopwatch sw = new Stopwatch();
            int a;
            string s;

            a = int.Parse(txtIn1.Text);

            this.Cursor = Cursors.WaitCursor;
            //int r = 0;
            sw.Start();
            for (int i = 1; i <= repeat; i++)
                w = BI.BIFact(a);

            s = w.BIToStr(4);
            sw.Stop();
            long T1 = sw.ElapsedMilliseconds;
            string t = "Elapsed=" + T1.ToString("#,##0mSec") + "digit=" + w.intdig;
            txtMessage.Text = t;
            this.Cursor = Cursors.Default;
            txtOut.Text = s;
            formating();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtMessage.Text +="  " + Program.Maxv.ToString("###,###");
            //txtMessage.Update();
            progressBar1.Value = Program.prgr;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;

            //パラメータを取得する
            int maxLoops = (int)e.Argument;

            //時間のかかる処理を開始する
            for (int i = 1; i <= maxLoops; i++)
            {
                //1秒間待機する（時間のかかる処理があるものとする）
                System.Threading.Thread.Sleep(1000);

                //ProgressChangedイベントハンドラを呼び出し、
                //コントロールの表示を変更する
                bgWorker.ReportProgress(i);
            }

            //ProgressChangedで取得できる結果を設定する
            //結果が必要なければ省略できる
            e.Result = maxLoops;
        }

        private void txtOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
