using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using CS_BigIntArith1;
using CS_BigIntArith0;

//2016/9/11 git location移動
//このソース以降GITで版数管理を行う
//2
namespace CS_BigIntArith1
{
    //delegate BI Arith(BI a);

    public static class Constants
    {

        public const ulong Prec = 10000000000000;
    }

    //public int  Nthread=2;

    class BI
    {
        public static int length = 500;
        public bool err = false;
        public string errs = "No Error        ";
        public int RX = 10000;
        public int dRX = 4;
        public int sign = 0;
        public int size;
        public int fxp;
        public long intdig;
        public long pointdig;
        public int[] v; //array Value
        //public BI Rm=new BI();
        public BI()  //Constructor
        {
            RX = 10000;  //10000進
            size = 1;
            fxp = 0;
            v = new int[length];
            for (int i = 0; i < length; i++)
                v[i] = 0;
        }
        public BI(int max)  //Constructor with max size
        {
            RX = 10000;  //10000進
            size = 1;
            fxp = 0;
            v = new int[max];
            for (int i = 0; i < max; i++)
                v[i] = 0;
        }
        public BI(int size, int decimalplace)
        {
            this.fxp = decimalplace;
            this.size = size;
            length = size * 2;
            // must be size >= fxp
        }
        public struct ippair
        {
            public int intl, pointl;
            public ippair(int i, int f)
            {
                intl = i;
                pointl = f;
            }
        }
        public long BIdigit()
        {
            int n = 0;
            if (size > 0) n = size - 1;
            int k = v[size - 1];
            int i = 10, dig = 0;
            while (k > 0)
            {
                k = k / i;
                i *= 10;
                dig++;
            }
            return (long)dig + n;
        }
        public static void BIgetdigit2(BI x)
        {
            int i;
            //ippair ip = new ippair(0, 0);
            int p = x.size - 1;
            int v = x.v[p];
            for (i = 3; i >= 0; i--)
            {
                int n = (int)Math.Pow(10, i);
                if (v / n == 0) continue;
                else { v -= (v / n) * n; break; }
            }
            x.intdig = ++i;
            if (x.size - x.fxp - 1 > 0) x.intdig += 4 * (x.size - x.fxp - 1);

            if (x.fxp > 0)
            {
                int f = x.v[0];
                for (i = 1; i < 4; i++)
                {
                    int n = (int)Math.Pow(10, i);
                    if (f % n == 0) continue;
                    else break;
                }

                if (x.fxp > 1) x.pointdig = 4 * (x.fxp - 1);
                x.pointdig += 5 - i;
            }

        }
        public bool BIError(string s)
        {
            if (err)
            {
                s = errs;
                errs = "No Error";
                err = false;
                return true;
            }
            else return false;
        }
        public void SetValByInt(int val)
        {
            if (val <= 9999)
            {
                size = 1;
                v[0] = val;
            }
            else if (val <= 99999999)
            {
                size = 2;
                v[0] = val % RX;
                v[1] = val / RX;
            }
            if (Program.gfx > 0)
            {
                int n = Program.gfx;
                int j = size;
                size += n;
                fxp += n;
                for (int i = j - 1; i >= 0; i--)
                    v[i + n] = v[i];
                for (int i = 0; i < n; i++)
                    v[i] = 0;
            }
        }
        //格納先のfxp>0 場合それに合わせて格納する必要あり
        public void SetValByStr(string src) //小数部桁数を統一させる処理必要
        {
            string pad = "", s;
            int I4 = 0;
            size = 0;
            src = src.Replace(" ", "");
            string[] delimiter = { "." };
            string[] parts;
            src = src.Replace("\r\n", "");  //結果を入力した場合の改行文字を削除
            parts = src.Split(delimiter, StringSplitOptions.None);
            int np = parts.Length; //np=2 if PointFound
            BI temp2 = new BI();

            if (np == 2)
            {//Fixed point part
                s = parts[1];
                int NS = s.Length;
                pointdig = NS;
                I4 = NS / 4;
                int R4 = NS % 4;
                if (R4 > 0)
                {
                    pad = new String('0', 4 - R4);
                    I4++;
                }
                s = s + pad;
                for (int j = 0; j <= (I4 - 1) * 4; j += 4)
                {
                    temp2.v[I4 - 1 - (j / 4)] = int.Parse(s.Substring(j, 4));
                }
                temp2.fxp = I4;
                temp2.size = I4;
                //if (temp2.size==(temp2.fxp) && (temp2.v[temp2.fxp]==0))
                //    temp2.size++;
            }

            //integer part
            s = parts[0];
            int N = s.Length;
            int I = N / 4;
            int R = N % 4;
            BI temp = new BI();
            //Added to extend big-int length if size larger than original size
            if (I > temp.v.Length)
            {
                Array.Resize(ref temp.v, I + 1);
                Array.Resize(ref v, I + 1);
            }
            intdig = N;
            temp.size = 0;
            if (R > 0)
            {
                temp.v[I] = int.Parse(s.Substring(0, R));
                temp.size = 1;
            }
            if ((R == 1) && s == "0")
                temp.size = 0;
            for (int j = 0; j < I; j++)
                temp.v[j] = int.Parse(s.Substring(N - (j + 1) * 4, 4));
            temp.size = temp.size + I;

            for (int k = 0; k < temp.size; k++)
                this.v[k] = temp.v[k];
            this.size = temp.size;
            this.fxp = I4;
            if (fxp != 0)
            {
                size = temp.size + temp2.size;
                if (size > temp2.size && size > v.Length) Array.Resize(ref v, size);
                for (int i = temp.size - 1; i >= 0; i--)
                    v[temp2.fxp + i] = temp.v[i];
                for (int i = temp2.fxp - 1; i >= 0; i--)
                    v[i] = temp2.v[i];

                fxp = temp2.fxp;
            }
            if (Program.gfx > fxp)
            {
                int j = Program.gfx - fxp;
                if (size + j > v.Length)
                    Array.Resize(ref v, (size + j));
                for (int i = size - 1; i >= 0; i--)
                    v[i + j] = v[i];
                for (int i = j - 1; i >= 0; i--)
                    v[i] = 0;
                size += Program.gfx;
                fxp += Program.gfx;
            }

        }
        //       全サイズ js
        //       小数サイズ jf
        //       js==0 
        //         jf != 0 
        //          "0"出力
        //       else
        //         Top=true
        //         I=js-1 to if I--
        //           If top ####形式で出力、top=false
        //           Else 0000形式で出力

        //       jf != 0
        //         "."出力
        //         I=jf-1 to 0 I--
        //           0000形式で出力

        public string BIToStr(int n)
        {
            string s = "";

            int j = size - fxp;
            if (BIisZero(this))
            {
                return "0";
            }
            string ctrlb = string.Concat(Enumerable.Repeat("#", n - 1)) + "0";
            string ctrln = string.Concat(Enumerable.Repeat("#", n));
            string ctrls0 = " " + string.Concat(Enumerable.Repeat("0", n));
            string ctrl0s = string.Concat(Enumerable.Repeat("0", n)) + " ";

            if (j == 1)
                s = v[size - 1].ToString(ctrlb);
            else if ((size != fxp) && (size != 0))
                s = v[size - 1].ToString(ctrln);

            for (int i = size - 2; i >= fxp; i--)
                s = s + v[i].ToString(ctrls0);
            if (fxp > 0)
            {
                if ((size == fxp) || (size == 0)) s = "0";
                //int k=s.Length;
                //if ((k != 0) && (s.Substring(k - 1, 1) == " "))
                //    s = s.Substring(0,k-1);
                s = s + ".";
                for (int i = fxp - 1; i >= 0; i--)
                    s = s + v[i].ToString(ctrl0s);
            }
            return s;
        }

        public int BIgettop()
        {
            int top = 0;
            int v = this.v[size - 1];
            for (int i = 3; i >= 0; i--)
            {
                int n = (int)Math.Pow(10, i);
                if (v / n == 0) continue;
                else { top = v / n; break; }
            }
            return top;
        }

        //static class qsqrt
        //{
        //    public int[] tbl;
        //    public qsqrt()
        //    {
        //        tbl = new int[100];
        //        for (int i = 0; i < 100; i++)
        //            tbl[i] = ((int)Math.Sqrt(i) * 10);
        //    }
        //    public static int getqsqrt(int ix)
        //    {
        //        if (ix >= 0 && ix < 100)
        //        {
        //            return tbl[ix];
        //        }
        //        else return 0;
        //    }
        //}


        public static bool BIisZero(BI a)
        {
            if ((a.size == 0) && (a.fxp == 1) && (a.v[0] == 0))
                return true;
            else if (((a.size == 1) || (a.size == 0)) && (a.v[0] == 0))
                return true;
            else if (a.size > 0)
            {
                for (int i = 0; i < a.size; i++)
                {
                    if (a.v[i] != 0) return false;
                }
                return true;
            }
            else return false;

        }

        //整数部サイズの小さな上位側を０クリア.加算・減算ループに判断を入れないための準備
        public void BUpperFillZ(BI a, BI b)
        {
            if ((a.size - a.fxp) > (b.size - b.fxp))
            {
                //N = size-fxp;
                for (int i = b.size - b.fxp; i < a.size - a.fxp; i++)
                    b.v[b.fxp + i] = 0;
            }
            else
            {
                //N = x.size;
                for (int i = a.size - a.fxp; i < b.size - b.fxp; i++)
                    a.v[a.fxp + i] = 0;
            }
        }
        //小数点位置を揃える　これは使わず
        //引数をBShiftLに渡しており直接書き換えていないためref 指定必要
        //public void BAllignFxp(ref BI a, ref BI b)
        //{
        //    if (a.fxp > b.fxp)
        //    {
        //        b = BShiftL(b, a.fxp - b.fxp);
        //        b.fxp = a.fxp;
        //    }
        //    else
        //    {
        //        a = BShiftL(a, b.fxp - a.fxp);
        //        a.fxp = b.fxp;
        //    }
        //}
        //小数点位置を揃える
        //BShiftL使わず直接桁ずらし.結果返すため必要
        public int BAllignFxpr(BI a, BI b)
        {
            int n, j;
            // j = A.size;
            // w.size = A.size + N;
            // for (i = j - 1; i >= 0; i--)
            //     w.v[i + N] = A.v[i];
            // for (i = 0; i < N; i++)
            //     w.v[i] = 0;
            if (a.fxp > b.fxp)
            {
                //b = BShiftL(b, a.fxp - b.fxp);
                j = b.size;
                n = a.fxp - b.fxp;
                b.size += n;
                if (b.size > b.v.Length) Array.Resize(ref b.v, b.size);
                for (int i = j - 1; i >= 0; i--)
                    b.v[i + n] = b.v[i];
                for (int i = 0; i < n; i++)
                    b.v[i] = 0;
                b.fxp = a.fxp;
            }
            else
            {
                //a = BShiftL(a, b.fxp - a.fxp);
                j = a.size;
                n = b.fxp - a.fxp;
                a.size += n;
                for (int i = j - 1; i >= 0; i--)
                    a.v[i + n] = a.v[i];   // 1.0÷0.000000001 でv.length 2で i+n=2 で境界オーバ例外->SetValByStrのRisize処理修正
                for (int i = 0; i < n; i++)
                    a.v[i] = 0;
                a.fxp = b.fxp;
            }
            return n;
        }

        //
        //見直すべき問題
        //　　小数計算で小数以下の桁数を指定の値で一定にする
        //　　今は引数の小数桁数の大きな方に合わせている．

        //小数の場合小数以下桁数の大きな側で小さな側が正規化され結果も同様
        public BI BIAdd(BI x) // Big Add
        {
            int cry = 0;
            BI w = new BI(this.v.Length);
            if (Program.gfx == 0)
            {
                BI wx = new BI(this.v.Length);
                if (!((this.fxp == 0) && (x.fxp == 0))) BAllignFxpr(this, x); // fxpはそろっている前提を回避するため
                BUpperFillZ(this, x); 　 //サイズの小さな上位側を０クリア        
                //BAllignFxpr(this, x);　//小数点位置はインスタンス作成時揃える
                if (this.size > x.size) w.size = this.size;
                else w.size = x.size;
                w.fxp = this.fxp;
            }
            else
            {
                w.size = size;
                w.fxp = fxp;
            }
            //加算実行
            for (int i = 0; i < w.size; i++)
            {
                int D = this.v[i] + x.v[i] + cry;
                w.v[i] = D % RX;
                cry = D / RX;
            }
            //最上位桁上げ処理
            if (cry != 0)
            {
                w.v[w.size] = cry;
                w.size += 1;
            }
            //TrimBI(w);
            //ippair ij = BIgetdigit(w);
            //w.intdig = ij.intl;
            //w.pointdig=ij.pointl;
            BIgetdigit2(w);
            return w;
        }

        //小数の場合小数以下桁数の大きな側で小さな側が正規化され結果も同様
        public BI BISub(BI B)
        {
            BI w = new BI(this.v.Length), wb = new BI(this.v.Length), ws = new BI(this.v.Length);
            int N, CRY = 0;
            int d;
            if (B.size == 0) return this;
            else if ((B.size == 1) && (B.v[0] == 0)) return this;
            if (!((this.fxp == 0) && (B.fxp == 0))) BAllignFxpr(this, B); // fxpはそろっている前提を回避するため
            w.size = 0;
            if (size >= B.size)
            {
                N = size;
                wb = this;
                ws = B;
            }
            else
            {
                N = B.size;
                wb = B;
                ws = this;
            }
            if (B.v.Length < size) Array.Resize(ref B.v, size);
            for (int i = 0; i < N; i++)
            {
                d = v[i] - B.v[i] - CRY;
                if (d < 0)
                {
                    w.v[i] = RX + d;
                    CRY = 1;
                }
                else
                {
                    w.v[i] = d;
                    CRY = 0;
                }
                w.size++;
            }
            TrimBI(w);
            w.fxp = fxp;
            if (w.size < w.fxp) w.size = w.fxp; //0.0～0Xのケースで誤Trimを回復
            //ippair ij = BIgetdigit(w);
            //w.intdig = ij.intl;
            //w.pointdig = ij.pointl;
            BIgetdigit2(w);
            return w;
        }

        public BI BIAddInt(int n) // Big Increment
        {
            //整数でn<10000のみ有効
            BI w = new BI(this.v.Length);
            if (this.fxp != 0 || n >= RX)
            {
                errs = "BIAddInt (int n>=10000)";
                w.size = 0;
                BIgetdigit2(w);
                return w;
            }

            int cry = 0;
            int D = v[0] + n;
            w.v[0] = D % RX;
            cry = D / RX;
            for (int i = 1; i < this.size; i++)
            {
                if (cry > 0)
                {
                    D = v[i + 1] + cry;
                    w.v[i] = D % RX;
                    cry = D / RX;
                }
            }
            w.size = this.size;
            //最上位桁上げ処理
            if (cry != 0)
            {
                w.v[w.size] = cry;
                w.size += 1;
            }
            //ippair ij = BIgetdigit(w);
            //w.intdig = ij.intl;
            //w.pointdig = ij.pointl;
            BIgetdigit2(w);
            return w;
        }

        public bool BIComp(BI B)
        {
            BI w = new BI(), wb = new BI(), ws = new BI();
            int N, CRY = 0;
            int d;
            if (B.size == 0) return true;
            else if ((B.size == 1) && (B.v[0] == 0)) return true;
            if (!((this.fxp == 0) && (B.fxp == 0))) BAllignFxpr(this, B); // fxpはそろっている前提を回避するため
            w.size = 0;
            if (size >= B.size)
            {
                N = size;
                wb = this;
                ws = B;
            }
            else
            {
                N = B.size;
                wb = B;
                ws = this;
            }
            for (int i = 0; i < N; i++)
            {
                d = v[i] - B.v[i] - CRY;
                if (d < 0)
                {
                    w.v[i] = RX + d;
                    CRY = 1;
                }
                else
                {
                    w.v[i] = d;
                    CRY = 0;
                }
                w.size++;
            }
            w.fxp = fxp;
            TrimBI(w);

            if (CRY == 1) return false;
            else return true;

        }

        public BI BIMulInt(int x)
        {
            //unsafe
            {
                int ia, w;

                ia = size;
                int sz = ia + 1;
                BI Mul = new BI(sz);
                if (x >= RX)
                {
                    errs = "BIMulInt (int n>=10000)";
                    Mul.size = 0;
                    Mul.v[0] = 0;
                    BIgetdigit2(Mul);
                    return Mul;
                }
                Mul.size = sz;
                if (sz > Program.Maxv) Program.Maxv = sz;
                ParallelOptions options = new ParallelOptions(); //これを使う場合 ) 必要
                options.MaxDegreeOfParallelism = 4;

                for (int i = 0; i < ia; i++)
                //fixed (int* pa = v, pm = Mul.v)
                {

                    w = x * v[i];

                    w += Mul.v[i];

                    Mul.v[i] = w % RX;

                    Mul.v[i + 1] += w / RX; // Carry
                };

                //Mul.size = ia + ib;


                if (Mul.v[Mul.size - 1] == 0) Mul.size--;//最上位桁無ならsizeを1減らす
                Mul.fxp = this.fxp;
                //ippair ij = BIgetdigit(Mul);
                //Mul.intdig = ij.intl;
                //Mul.pointdig = ij.pointl;
                BIgetdigit2(Mul);
                return Mul;  //3.1622776654443271853371955513 を二乗したしたときにsizeがマイナスになる
            }
        }
        //Karatsuba Algorithm
        //(1 + RN/2)u0v0 + RN/2(u1 − u0)(v0 − v1) + (RN/2 + RN )u1v1
        public BI BIMulKt(BI x)
        {
            BI p = new BI();

            return p;
        }
        //Big Multiply revised
        //小数の場合小数以下桁数の大きな側で小さな側が正規化され結果も同様
        public BI BIMul2(BI x)
        {
            //unsafe
            {
                int ia, ib;
                int w, w1;

                if (!((this.fxp == 0) && (x.fxp == 0))) BAllignFxpr(this, x); // fxpはそろっている前提を回避するため

                ia = size;
                ib = x.size;
                int sz = ia + ib;
                BI Mul = new BI(sz);
                //Mul.size = 0;
                Mul.size = sz;
                if (sz > Program.Maxv) Program.Maxv = sz;
                ParallelOptions options = new ParallelOptions(); //これを使う場合 ) 必要
                options.MaxDegreeOfParallelism = 4;
                for (int i = 0; i < ib; i++)
                {
                    w1 = x.v[i];
                    //Parallel.For(0, ia, j =>
                    for (int j = 0; j < ia; j++)
                    //fixed (int* pa = v, pm = Mul.v)
                    {
                        //w = w1 * v[j];
                        w = w1 * v[j] + Mul.v[j + i];
                        //w += Mul.v[j + i];
                        //*(pm + j + i) = w % RX;
                        Mul.v[j + i] = w % RX;
                        //*(pm + j + i + 1) += w / RX; // Carry
                        Mul.v[j + i + 1] += w / RX; // Carry
                    };//)
                }

                //Mul.size = ia + ib;

                if (!((this.fxp == 0) && (x.fxp == 0)))
                {
                    if ((this.fxp != 0) && (x.fxp != 0))
                    {
                        for (int i = this.fxp; i < Mul.size; i++)
                        {
                            Mul.v[i - this.fxp] = Mul.v[i];
                            Mul.v[i] = 0;
                        }
                        Mul.fxp = this.fxp;
                        Mul.size -= this.fxp;
                        if (Mul.size > Mul.fxp && Mul.v[Mul.size - 1] == 0) Mul.size--;
                    }
                    else if (this.fxp != 0)
                        Mul.fxp = this.fxp;
                    else if (x.fxp != 0)
                        Mul.fxp = x.fxp;
                }
                else
                    if (Mul.v[Mul.size - 1] == 0) Mul.size--;//最上位桁無ならsizeを1減らす
                //ippair ij = BIgetdigit(Mul);
                //Mul.intdig = ij.intl;
                //Mul.pointdig = ij.pointl;
                BIgetdigit2(Mul);
                return Mul;  //3.1622776654443271853371955513 を二乗したしたときにsizeがマイナスになる
            }
        }

        //public BI BIMulN2(int i, int N)
        //{}

        public BI BIDivN(BI B)
        {
            BI rmd = new BI();
            return this.BIDiv(B, rmd);
        }
        // 1 / 123456789 でメーズ(小数桁=20/10000進)　乗算で最下位が負になっている
        public BI BIDiv(BI B, BI rmd)
        {
            //被除数＜除数のケース
            int xp, N, DX, Res, lcnt, m;
            int Kari;
            BI Pa = new BI(this.v.Length), zero = new BI(this.v.Length);
            BI RM = new BI(this.v.Length), Q = new BI(this.v.Length);
            BI w = new BI(this.v.Length), w1 = new BI(this.v.Length);
            zero.SetValByInt(0);
            // a/b でbが0.00000001などの時数値のある桁をbの有効な長さとする必要あり 

            if (B.size == 1 && B.v[0] == 0)
            {
                Q.size = 0;
                Q.v[0] = 0;
            }
            BI divd = new BI(this.v.Length);
            Array.Copy(this.v, divd.v, this.size);
            int sizesave = this.size;
            int fxpsave = this.fxp;

            if (!((this.fxp == 0) && (B.fxp == 0)))
            {
                m = BAllignFxpr(this, B);  //for Fixed point
                if (this.fxp == B.fxp) m = fxp;
            }
            else m = 0;
            xp = this.fxp;
            if (xp > 0)
            {
                BShiftLr(this, xp);
                //divd.fxp = xp;
            }

            if (size >= B.size)
                N = size;
            DX = size - B.size;
            int QS;
            if (DX >= 0)
            {
                if (CompBI(this, B) < 0)
                {
                    Q.SetValByInt(0);
                    CpyBI(rmd, this, size - 1, size);
                    BIgetdigit2(Q);
                    BIgetdigit2(rmd);
                    return Q;
                }
                else
                {
                    CpyBI(Pa, this, size - 1, B.size);
                    Q.size = DX + 1;
                    QS = Q.size;
                }
            }
            else
            {
                Q.SetValByInt(0);
                CpyBI(rmd, this, size - 1, size);
                Array.Copy(divd.v, this.v, sizesave);
                this.size = sizesave;
                this.fxp = fxpsave;

                BIgetdigit2(Q);
                BIgetdigit2(rmd);
                return Q;
            }

            while (DX >= 0)
            {
                Res = CompBI(Pa, B);
                if (Res >= 0)
                {
                    Kari = EstimateDivdnt(Pa, B);
                    w.size = 1;
                    w.fxp = 0;
                    lcnt = 0;
                    do
                    {
                        w.SetValByInt(Kari);
                        w1 = B.BIMul2(w); // 除数 X 仮の商
                        Res = CompBI(Pa, w1);
                        Kari--; //仮が負になってこのループから抜けないバグあり
                        lcnt++;
                    }
                    while (Res < 0); //部分被除数 > 除数 X 仮の商　となるまで仮の商をマイナス１
                    //if (lcnt > 5)
                    //{ System.Diagnostics.Debug.WriteLine(".."); }

                    Q.v[DX] = Kari + 1; //商を格納
                    RM = Pa.BISub(w1);   //部分剰余
                    if (DX > 0)
                    {
                        CpyBI(w, this, DX - 1, 1); // A から1桁取得 
                        ConBI(Pa, RM, w);      //新たな部分被除数}
                        //TrimBI(Pa);
                    }
                    DX--;
                }
                else
                {
                    CpyBI(RM, Pa, Pa.size - 1, Pa.size);
                    if (DX == 0)
                    {
                        Q.SetValByInt(0);
                        //CpyBI(RM, Pa, Pa.size - 1, Pa.size);
                    }
                    else
                    {
                        CpyBI(w, this, DX - 1, 1); // A から1桁取得;
                        ConBI(Pa, RM, w); //新たな部分被除数
                        Q.v[DX] = 0;
                    }
                    DX--;
                }
            }

            TrimBI(RM);
            //剰余の桁合わせ
            int k = RM.size;
            if (k > rmd.v.Length) Array.Resize(ref rmd.v, k);
            Array.Copy(RM.v, 0, rmd.v, 0, RM.size);
            //CpyBI(rmd, RM, 0, RM.size - 1);

            if (!((this.fxp == 0) && (B.fxp == 0)))
            {
                Array.Clear(rmd.v, 0, rmd.v.Length);
                for (int j = m; j < k; j++)
                    rmd.v[j - m] = RM.v[j];
            }
            rmd.size = k;
            rmd.fxp = xp;
            //CpyBI(this.Rm, rmd, 0, k);
            //rmd=0 and v[0]=0 なら１桁右シフト(size--, fxp--)
            //TrimBI(Q);

            //Q.fxp = divd.fxp;
            Q.fxp = B.fxp;   //for Fixed point   ***1.0000 / 77777 1285.0000 となる　Q= 0 1285 size=1 fxp=2
            //if (Q.v[QS - 1] == 0) QS--;  //これを活かすとBISqrtでのBShiftR2で異常にならないがBIAtanでのBShiftR2でおかしくなる．活かさずBSqrtで対応
            Q.size = QS;
            Array.Copy(divd.v, this.v, sizesave);
            this.size = sizesave;
            this.fxp = fxpsave;
            BIgetdigit2(Q);
            BIgetdigit2(rmd);
            return Q;
        }

        public BI BIDivInt(int b, ref int r)
        {
            BI zero = new BI();
            BI q = new BI(this.v.Length);

            if (b >= RX)
            {
                errs = "BIDivInt (int n>=10000)";
                q.SetValByInt(0);
                q.size = 0;
                return q;
            }
            zero.SetValByInt(0);


            int t = 0;
            for (int i = this.size - 1; i >= 0; i--)
            {
                t = t * RX + v[i];
                q.v[i] = t / b;
                t %= b;
            }
            r = t;
            q.size = this.size;
            q.fxp = this.fxp;
            BIgetdigit2(q);
            return q;
        }
        //Big Multiply
        public BI BIMul(BI x)
        {
            int ia, ib;
            int w, w2;

            if (!((this.fxp == 0) && (x.fxp == 0))) BAllignFxpr(this, x);// fxpはそろっている前提を回避するため

            BI Mul = new BI();
            Mul.size = 0;
            BI cr = new BI();
            cr.size = 0;
            ia = size;
            ib = x.size;
            int[,] wk = new int[ib, ia + ib];
            for (int i = 0; i < ib; i++)
            {
                w2 = x.v[i]; //乗数を下位から順に取り出す
                for (int j = 0; j < ia; j++)
                {
                    w = wk[i, j + i] + v[j] * w2;
                    wk[i, j + i] = w % RX;
                    wk[i, j + i + 1] = w / RX;
                }
            }
            w = 0;
            int dgt = 0;
            for (int i = 0; i < ia + ib; i++)
            {
                //System.Diagnostics.Debug.WriteLine(lw);
                for (int j = 0; j < ib; j++)
                    w += wk[j, i];
                Mul.v[i] = w % RX;
                //Mul.v[i] = w1;
                w = w / RX;
            }
            Mul.size = ia + ib;

            if (!((this.fxp == 0) && (x.fxp == 0)))
            {
                if ((this.fxp != 0) && (x.fxp != 0))
                {
                    for (int i = this.fxp; i < Mul.size; i++)
                        Mul.v[i - this.fxp] = Mul.v[i];
                    Mul.fxp = this.fxp;
                    Mul.size = dgt - this.fxp;
                    if (Mul.size > Mul.fxp && Mul.v[Mul.size - 1] == 0) Mul.size--;
                }
                else if (this.fxp != 0)
                    Mul.fxp = this.fxp;
                else if (x.fxp != 0)
                    Mul.fxp = x.fxp;
            }
            else
                if (Mul.v[Mul.size - 1] == 0) Mul.size--;//最上位桁無ならsizeを1減らす
            BIgetdigit2(Mul);
            return Mul;
        }

        public BI BIgcd(BI y)
        {
            BI r = new BI(), x1 = new BI(), y1 = new BI();
            bool f = this.BIComp(y);
            if (f) { x1 = this; y1 = y; }
            else { x1 = y; y1 = this; }

            r.SetValByInt(1);
            while (!BIisZero(r))
            {
                x1.BIDiv(y1, r);
                x1 = new BI();
                CpyBI(x1, y1, y1.size - 1, y1.size);
                y1 = new BI();
                CpyBI(y1, r, r.size - 1, r.size);
            }
            BIgetdigit2(x1);
            return x1;

        }

        // Extended GCD 
        //x>0 , y>0 に対し ax + by = c となる a, b, c=Gcd(a,b) を求める
        public void exGCD(BI x, BI y, BI a, BI b, BI c)
        {
            BI r0 = new BI(); CpyBI(r0, x, x.size - 1, x.size);
            BI r1 = new BI(); CpyBI(r1, y, y.size - 1, y.size);
            BI a0 = new BI(); a0.SetValByInt(1);
            BI a1 = new BI(); a1.SetValByInt(0);
            BI b0 = new BI(); b0.SetValByInt(0);
            BI b1 = new BI(); b1.SetValByInt(1);
            BI b2 = new BI();
            BI q1 = new BI(); BI r2 = new BI();
            BI a2 = new BI();
            BI t = new BI(); BI z = new BI();
            while (r1.BIComp(z))
            {
                q1 = r0.BIDiv(r1, t);
                r0.BIDiv(r1, r2);
                a2 = a0.BISub(q1.BIMul2(a1));
                b2 = b0.BISub(q1.BIMul2(b1)); //b2 = b0 - q1*b1
                r0 = r1; r1 = r2;
                a0 = a1; a1 = a2;
                b0 = b1; b1 = b2;
            }
            c = r0;
            a = a0;
            b = b0;
        }

        public BI BIPower2(int x)
        {
            BI y = new BI(), acc = new BI(), w = new BI();
            //y.SetValByInt(1);
            //CpyBI(y, this, this.size - 1, this.size);
            acc.SetValByInt(1);
            int mask = 1, cnt = 0;
            for (cnt = 0; cnt < 16; cnt++)
            {
                if ((x & mask) != 0)
                {
                    y.SetValByInt(1);
                    //無駄あり
                    for (int i = 1; i <= mask; i++)
                        y = y.BIMul2(this);
                    acc = acc.BIMul2(y);
                }
                //y = this.BIMul2(y);
                mask = mask << 1;
            }
            //for (int i = 0; i < x; i++)
            //    y = this.BIMul2(y);
            //return y;
            BIgetdigit2(acc);
            return acc;
        }
        public BI BIPow2(BI n)
        {
            BI bm = new BI(), r = new BI(), two = new BI(), w = new BI();
            if (n.size == 1 && n.v[0] == 0)
            {
                w.SetValByInt(1);
                BIgetdigit2(w);
                return w;
            }
            two.SetValByInt(2);
            w = this.BIPow2(n.BIDiv(two, r));
            w = w.BIMul2(w);
            System.Diagnostics.Debug.WriteLine(w.size);
            if ((n.v[0] & 1) == 1)
            {
                w = w.BIMul2(this);
            }
            BIgetdigit2(w);
            return w;
        }
        public BI BIPowm1(int n, int m)
        {
            BI bm = new BI(), r = new BI(), ba = new BI(), w = new BI();
            bm.SetValByInt(m);
            if (n == 0)
            {
                w.SetValByInt(1);
                BIgetdigit2(w);
                return w;
            }
            w = this.BIPowm1((int)(n / 2), m);
            w.BIMul2(w).BIDiv(bm, r);
            w = r;
            if ((n % 2) == 1)
            {
                w.BIMul2(this).BIDiv(bm, r);
                w = r;
            }
            BIgetdigit2(w);
            return w;
        }
        //powm(123, 4567890123 ,456789) =108531
        public BI BIPowm(BI n, BI m)
        {
            BI pw = new BI(), a = new BI();
            BI two = new BI(), r = new BI();
            two.SetValByInt(2);
            pw.SetValByInt(1);
            //a=this;
            CpyBI(a, this, size - 1, size);
            while (!BIisZero(n))
            {
                if ((n.v[0] & 1) == 1)
                {
                    a.BIMul2(pw).BIDiv(m, r);
                    //pw=r;
                    CpyBI(pw, r, r.size - 1, r.size);
                }
                a.BIMul2(a).BIDiv(m, r);
                //a=r;
                CpyBI(a, r, r.size - 1, r.size);
                n = n.BIDiv(two, r);
            }
            BIgetdigit2(pw);
            return pw;
        }

        //      01 Function PowerMod(a,n,m)
        //02    pw = 1
        //03    While n >= 1
        //04       If (n mod 2) = 1 then
        //05          pw = a * pw mod m
        //06       End If
        //07       a = a^2 mod m
        //08       n = n \ 2
        //09    Wend
        //10    PowerMod = pw
        //11 End Function

        //public static long Encode(long x, long y, long n)
        //{
        //	string bit = Convert.ToString(y, 2);
        //	long sum = 1;
        //	for (int i=0;i<bit.Length;i++)
        //	{
        //		sum = sum * sum % n;
        //		if(bit[i] == '1') sum = sum * x % n;
        //	}
        //	return sum;
        //}
        //long powmod( int a, int k, int m){
        //  if ( k == 0 )
        //    return 1;
        //
        //  long t = powmod( a, k / 2, m);
        //  t = (t * t) % m;
        //  if (k % 2 == 1)
        //    t = (t * a) % m;
        //  return (int) t;
        //}

        //Optimized

        public BI BIPower(BI n)
        {
            BI y = new BI(), t = new BI();
            BI one = new BI(), two = new BI(), r = new BI();

            one.SetValByInt(1);
            two.SetValByInt(2);
            t = this;
            if ((n.v[0] & 1) == 0)
                y.SetValByInt(1);
            else
                y = this;
            while (n.BIComp(one))
            {
                n = n.BIDiv(two, r);
                t = t.BIMul2(t);
                if ((n.v[0] & 1) == 1)
                    y = y.BIMul2(t);
            }
            BIgetdigit2(y);
            return y;
        }

        public static BI BIFact(int n)
        {
            BI fact = new BI(), w = new BI(), one = new BI();
            if (n >= 10000)
            { }
            one.SetValByInt(1);
            fact.SetValByInt(1);
            for (int i = 2; i <= n; i++)
            {
                w.SetValByInt(i);
                fact = fact.BIMul2(w);
            }
            BIgetdigit2(fact);
            return fact;
        }

        public int BI2Int()
        {
            int n = this.size;
            if (n == 4 || (n == 4))
                throw new FormatException();
            else
            {
                int r = this.v[0];
                r += this.v[1] * RX;
                //r+=this.v[2] *( RX ^ 2);
                return r;
            }
        }
        //isprime(x)
        // if x == 2
        //   return true

        // if x < 2 または x が偶数
        //   return false

        // i = 3
        // while i <= x の平方根
        //   if x が i で割り切れる
        //     return false
        //   i = i + 2

        //return true

        //Only for integer
        public static void CpyBI(BI A, BI B, int S, int L)
        {
            int i = B.size - 1;
            //int k, j;
            if (B.v.Length > A.v.Length)
            {
                Array.Resize(ref  A.v, B.v.Length);

                //Console.WriteLine("at CpyBI.." + B.v.Length);
            }
            A.size = L;
            Array.Copy(B.v, S - L + 1, A.v, 0, L);
            A.fxp = B.fxp;
            BIgetdigit2(A);
            //int j = L - 1;
            //for (int k = S; k >= S - L + 1; k--)
            //{
            //    A.v[j] = B.v[k];
            //    j--;
            //}
        }

        private void TrimBI(BI A)
        {
            int i;
            i = A.v.Length - 1;
            if (i < 0) return;
            while (A.v[i] == 0 && i > 0)
                i--;
            A.size = i + 1;
            BIgetdigit2(A);
        }

        private BI BShiftR(BI A, int N)
        {
            BI w = new BI();
            CpyBI(w, A, A.size - 1, A.size);
            BIgetdigit2(w);
            return w;
        }
        private void BShiftR2(ref BI A, int N)
        {
            for (int i = A.size - 1; i > A.size - 1 - N; i--)
            {
                A.v[i - N] = A.v[i];
                A.v[i] = 0;
            }
            A.size = A.size - N;
            BIgetdigit2(A);
            //CpyBI(w, A, A.size - 1, A.size - N);

        }
        public BI BShiftL(BI a, int N)
        {
            int sz = a.size + N;

            BI w = new BI();
            w.size = sz;
            //***Modified 2016.4.18
            if (sz > w.v.Length)
            {
                Array.Resize(ref w.v, sz);
                //w.size = BI.length;
            }
            //***

            int i, j;
            j = a.size;

            for (i = j - 1; i >= 0; i--)
                w.v[i + N] = a.v[i];
            for (i = 0; i < N; i++)
                w.v[i] = 0;
            BIgetdigit2(w);
            return w;
        }

        public void BShiftLr(BI a, int N)
        {
            BI w = new BI();
            int i, j;
            j = a.size;
            a.size += N;
            //a.fxp += N;  //2016.4.28 Added
            if (a.size > a.v.Length) Array.Resize(ref a.v, a.size);
            //Console.WriteLine("at BShiftLr.." + a.size);
            for (i = j - 1; i >= 0; i--)
                a.v[i + N] = a.v[i];
            for (i = 0; i < N; i++)
                a.v[i] = 0;
            BIgetdigit2(a);
        }

        private void ConBI(BI dest, BI A, BI B)
        {
            //BI w = new BI();  第一パラメに結果を返す関数の場合、返した先の参照が変わり不具合が起きる
            int i, j, k;
            if (A.size == 1 && A.v[0] == 0)
            {
                for (i = 0; i < B.size; i++)
                    dest.v[i] = B.v[i];
                dest.size = B.size;
                dest.v[0] = B.v[0];
                return;
            }

            for (i = 0; i < B.size; i++)
                dest.v[i] = B.v[i];

            k = i;
            j = 0;
            for (i = k; i < k + A.size; i++)
            {
                dest.v[i] = A.v[j];
                j++;
            }

            dest.size = A.size + B.size;
            BIgetdigit2(dest);
        }

        private int EstimateDivdnt(BI PA, BI PB)
        {
            long X = 0, Y = 0; //10000進3桁まで扱う
            int DX;
            DX = PA.size - PB.size;
            if (PA.size >= 2)
            {
                X = (PA.v[PA.size - 1]) * RX + PA.v[PA.size - 2];
                if (PB.size >= 2)
                {
                    if (DX > 0)　//10000進3桁まで扱う
                    {
                        X = X * (long)RX + (long)PA.v[PA.size - 3];
                        Y = (long)PB.v[PB.size - 1] * (long)RX + PB.v[PB.size - 2];
                    }
                    else
                        Y = PB.v[PB.size - 1] * RX + PB.v[PB.size - 2];
                }
                else
                {
                    Y = PB.v[PB.size - 1];
                }
            }
            else
            {
                X = PA.v[PA.size - 1];
                Y = PB.v[PB.size - 1];
            }
            int Kari = (int)(X / Y); //1.0÷0.000000001でY=0　０除算発生
            return Kari;
        }

        //Compair 2 bigint
        private int CompBI(BI A, BI B)
        {
            if (A.size > B.size) return 1;
            else if (A.size < B.size) return -1;
            else
            {
                for (int i = A.size - 1; i >= 0; i--)
                {
                    if (A.v[i] > B.v[i])
                        return 1;
                    else if (A.v[i] < B.v[i])
                        return -1;
                }
                return 0;
            }
        }
        public void SwapBI(ref BI a, ref BI b)
        {
            BI w = new BI(a.v.Length);
            int i, l, k;
            k = a.size - 1;
            for (i = 0; i <= k; i++)
                w.v[i] = a.v[i];

            l = b.size - 1;
            for (i = 0; i <= l; i++)
                a.v[i] = b.v[i];

            a.size = l + 1;
            for (i = 0; i <= k; i++)
                b.v[i] = w.v[i];

            b.size = k + 1;
            BIgetdigit2(a);
            BIgetdigit2(b);
        }
        public Boolean Big2Long(BI a, ref long l)
        {
            int i;
            l = 99999999;
            if (a.size == a.fxp)
            {
                bool ok = true;
                for (i = 1; i < a.size; i++)
                {
                    if (a.v[i] != 0) { ok = false; break; }
                }
                if (ok)
                {
                    l = a.v[0];
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //       Public Function Big2Long(ByVal A As BInt, ByRef L As Long) As Boolean
        //    L = 0
        //    If A.Size > 1 Or A.Size < 1 Then
        //        Return False
        //    Else
        //        Dim K = A.Size - 1
        //        For i = 0 To K
        //            L = L + A.Data(i) * RX ^ i
        //        Next

        //        Return True
        //    End If
        //End Function
        /*
                private BI BIsetfraction(int i, int digit)
                {
                    BI one =new BI(), bii=new BI(),w=new BI(),R=new BI();
                    one.SetValByInt(1);
                    one.BShiftL(one,digit);
                    bii.SetValByInt(i);
                    w= one.BIDiv(bii,R);
                    return w;

                }

        pi2(iter)
           int             iter;
        {
              int             i;
              very_long_float a, b, t, x, y;

              a = 1;
              b = 1 / sqrt(2);
              t = 1;
              x = 4;
              for (i = 0; i < iter; i++) {
                  y = a;
                  a = (a + b) / 2;
                  b = sqrt(y * b);
                  t -= x * sqr(y - a);
                  x *= 2;
            }
              return (sqr(a + b) / t);
        }
         */
        //高速収束π計算 ガウス・ルジャンドル法
        public BI BIPiLegendre(int terms, int digit)
        {
            int pr, n;
            pr = terms;
            n = 0;
            BI a = new BI(), b = new BI(), t = new BI(), x = new BI();
            BI y = new BI(), r = new BI(), two = new BI(), w = new BI();
            two.SetValByInt(2);

            a.SetValByInt(1);
            BShiftLr(a, digit);

            BI ini = new BI();
            ini.v[0] = 5000;
            ini.v[1] = 1;
            ini.size = 2;

            w = BISqrt(two, ini, digit, ref n);
            //BShiftR2(ref w, digit);
            w.fxp = 0;

            b = a.BShiftL(a, digit).BIDiv(w, r);
            //BShiftR2(ref a, digit);
            t.SetValByInt(1);
            BShiftLr(t, digit);
            //t.fxp = pr - 1;
            x.SetValByInt(4);
            BShiftLr(x, digit);
            //x.fxp = pr - 1;

            for (int i = 0; i < pr; i++)
            {
                CpyBI(y, a, a.size - 1, a.size);

                a = a.BShiftL(a, digit).BIAdd(b.BShiftL(b, digit)).BIDiv(two, r);

                w = y.BIMul2(b);
                BShiftR2(ref w, digit);
                ini.v[1] = 1;
                //ini.size = 1;
                //w.fxp = 19;
                b = BISqrt(w, ini, digit, ref n);
                b.size = 19;
                b.fxp = 0;
                if (CompBI(y, a) >= 0)
                    w = y.BISub(a);
                else
                    w = a.BISub(y);
                t = t.BISub(x.BIMul2(w.BIMul2(w)));
                x = x.BIMulInt(2);

            }
            w = a.BIAdd(b);
            return w.BIMul2(w).BIDiv(t, r);
        }
        //Pai Calculation: Term=100 Digit=100 Correct=73 : Elapsed=405mSec
        //Pai Calculation: Term=300 Digit=300 Correct=214 : Elapsed=5,013mSec
        //Pai Calculation: Term=300 Digit=300 Correct=214 : Elapsed=4,009mSec 桁定義をlong->intに変更
        //Pai Calculation: Term=300 Digit=300 Correct=214 : Elapsed=3,053mSec 乗算ループ変更
        //Pai Calculation: Term=700 Digit=700 Correct=498 : Elapsed=44,506mSec
        //Pai Calculation: Term=700 Digit=700 Correct=498 : Elapsed=43,340mSec BIMul 結果のsize計算改善
        //Pai Calculation: Term=700 Digit=700 Correct=498 : Elapsed=32,891mSec 桁定義をlong->intに変更
        //Pai Calculation: Term=800 Digit=800 Correct=569 : Elapsed=45,167mSec
        //Pai Calculation: Term=800 Digit=800 Correct=569 : Elapsed=35,433mSec　乗算ループ変更.
        //Pai Calculation: Term=800 Digit=800 Correct=569 : Elapsed=35,040mSec  乗算ループ変更(by pointer)37Sec->35Sec
        //Pai Calculation: Term=800 Digit=800 Correct=569 : Elapsed=33,396mSec  最適化
        //Pai Calculation: Term=400 Digit=400 Correct=569 : Elapsed= 5,756mSec  配列初期Length500として自動拡張化．MulIntを使用
        public BI BIPai(int terms, int digit)
        {
            BI p = new BI(digit);
            BI BI16 = new BI(digit), BI4 = new BI(digit);
            BI16.SetValByInt(16);
            BI4.SetValByInt(4);
            p = BI16.BIMul2(BIatan(5, terms, digit)).BISub(BI4.BIMul2(BIatan(239, terms, digit)));
            p.fxp = p.size - 1;
            BIgetdigit2(p);
            return p;

        }
        //atan(x)=(y )(1+ (2/3)*y^2+ (2*4)/(3*5)*(y^2)+(2*4*6)/(3*5*7)*(y^3)...
        //y=x/(1+x^2)
        private BI BIatan(int xi, int terms, int digit)
        {

            BI a = new BI(digit), one = new BI(digit), BIOne = new BI(digit);
            BI u = new BI(digit), d = new BI(digit), two = new BI(digit), s = new BI(), R = new BI(digit), t = new BI(digit);
            BI x = new BI(digit), x2 = new BI(digit), bxi = new BI(digit), y1 = new BI(digit), y = new BI(digit), ys = new BI(digit);

            one.SetValByInt(1);
            BIOne = BIOne.BShiftL(one, digit);
            bxi.SetValByInt(xi);
            x = BIOne.BIDiv(bxi, R);  // make 1/x
            x2 = x.BIMul2(x);       // x^2
            //if (x2.v[x2.size - 1] == 0) x2.size--;
            BShiftR2(ref x2, digit);
            //x2 = x2.BShiftR(x2, digit);

            y1 = x.BShiftL(x, digit).BIDiv(x2.BIAdd(BIOne), R);  // y1=x/(1+x^2) OK
            //y1 = y1.BShiftL(y1, digit);
            y = y1.BIMul2(x);                // y=x^2/(1+x^2) OK
            //if (y.v[y.size - 1] == 0) y.size--;
            BShiftR2(ref y, digit);
            //y = y.BShiftR(y,digit);
            s.SetValByInt(1);
            s = s.BShiftL(s, digit);
            two.SetValByInt(2);
            u.SetValByInt(1);
            d.SetValByInt(1);
            ys.SetValByInt(1);
            ys = ys.BShiftL(ys, digit);
            for (int i = 2; i <= terms; i += 2)
            {
                u = u.BIMulInt(i);        // u=u*i
                d = d.BIMulInt(i + 1);        // d=d*(i+1)
                ys = ys.BIMul2(y);
                //if (ys.v[ys.size-1]==0) ys.size--;
                BShiftR2(ref ys, digit);
                //ys = ys.BShiftR(ys,digit);  //y=y*y
                t = u.BShiftL(u, digit).BIDiv(d, R).BIMul2(ys);
                //if (t.v[t.size - 1] == 0) t.size--;
                BShiftR2(ref t, digit);
                //t = t.BShiftR(t, digit);
                s = s.BIAdd(t);         //// s=s+(u/d)*y

                Program.prgr = (int)(100 * i / terms);
                System.Windows.Forms.Application.DoEvents();

                //Console.WriteLine( 100*i/terms+ "% ");
            }
            Program.prgr = 0;
            s = s.BIMul2(y1);
            s = s.BShiftR(s, digit);
            BIgetdigit2(s);
            return s;
        }

        //      double xn;
        //      int i,n;
        //      n=10;
        //      xn=0;
        //      do { /* 平方根に最も近い大きい方の整数を探す */
        //          xn+=1;
        //      } while (xn*xn<c);
        //      for (i=0;i<n;i++) {
        //         xn=(xn+c/xn)/2; /* 交点のx 座標を求める */
        //      }
        //      return xn; /* 求めた近似値を返す */
        public BI BISqrt2(BI a, BI ini, int pr, ref int n)
        {

            int cm;
            long l = 0;
            int pr2 = 2 * pr;
            BI wa = new BI(pr2), w1 = new BI(pr2), w2 = new BI(pr2), w3 = new BI(pr2), r = new BI(pr2);
            BI wn = new BI(pr2), r1 = new BI(pr2), e = new BI(pr2);
            if (pr == 0)
            {
                w1.err = true;
                w1.errs = "point must be >0";
                w1.size = 0;
                BIgetdigit2(w1);
                return w1;
            }
            Array.Copy(a.v, wa.v, a.size);
            wa.size = a.size;
            wa.fxp = a.fxp;
            //BShiftLr(wa, pr - a.fxp); // a を固定小数点化
            //wa.fxp=pr;
            //BI h = new BI(pr2);
            //h.v[0] = 2;
            //h.size = 1;
            BI w = new BI(pr2);
            if (BIisZero(a)) return w;
            Array.Copy(ini.v, w.v, ini.size);
            //w.SetValByInt(5);　　//初期値
            w.size = ini.size;
            w.fxp = ini.fxp;
            BShiftLr(w, pr - a.fxp);
            w.fxp = pr;
            //w.v[pr] = 5;
            w.size = pr + 1;
            int i = 0;
            int rm = 0;
            do
            {
                w1 = w.BIMul2(w); // w^2
                //BShiftR2(ref w1, pr); //
                w2 = w1.BIAdd(wa);//w1+wa
                //w3 = w2.BIDiv(h, r);
                w3 = w2.BIDivInt(2, ref rm);
                //BShiftLr(w3, pr);
                wn = w3.BIDiv(w, r1);
                cm = CompBI(wn, w);
                if (cm > 0)
                    //SwapBI(ref w, ref wn);
                    e = wn.BISub(w);
                else
                    e = w.BISub(wn);
                if (!(Big2Long(e, ref l)))
                {
                    l = 9999999999;
                    //if (cm < 0) SwapBI(ref w, ref wn);
                    w = wn;
                    i++;
                }
                else
                {
                    //if (cm < 0) SwapBI(ref w, ref wn);
                    w = wn;
                }
            }
            while (i < 20 && l > 9999);

            if (i > 20)
            {
                w.size = 0;
                n = 20;
            }
            else
            {
                w.fxp = pr;
                //CpyBI(fxp, w, pr - 1, pr);
                //CpyBI(w, w, w.size - 1, w.size - pr);
                n = i;
            }
            BIgetdigit2(w);
            return w;
        }

        // Newton-Raphson法による平方根
        //  prec : RX進での計算桁数
        //  n : 収束までの反復回数
        // (1+x)^n　のテーラー展開（かなり遅いと思われる）での計算と比較したい
        public BI BISqrt(BI a, BI ini, int pr, ref int n)
        {
            int cm;
            long l = 0;
            int pr2 = 2 * pr;
            BI wa = new BI(pr2), w1 = new BI(pr2), w2 = new BI(pr2), w3 = new BI(pr2), r = new BI(pr2);
            BI wn = new BI(pr2), r1 = new BI(pr2), e = new BI(pr2);
            if (pr == 0)
            {
                w1.err = true;
                w1.errs = "point must be >0";
                w1.size = 0;
                BIgetdigit2(w1);
                return w1;
            }
            //w=( a + w*w ) / 2 / w or (a/w + w) / 2
            Array.Copy(a.v, wa.v, a.size);
            wa.size = a.size;
            BShiftLr(wa, pr - a.fxp); // a を固定小数点化
            BI w = new BI(pr2);
            if (BIisZero(a)) return w;
            //Array.Copy(ini.v, w.v, ini.size);
            //w.size=ini.size;

            //Initial value setting
            int i;
            BIgetdigit2(a);
            int k = (int)a.intdig;
            int k1 = k / 2;
            BI w10 = new BI();
            w10.SetValByInt(10);
            int top = a.BIgettop();
            if (k % 2 == 0)
            {  //Not implemented yet  
                int m = (int)(Math.Sqrt(top * 10 + 5) * 10) * 1000;
                w.v[1] = m / 10000;
                w.v[0] = m % 10000;
                w.size = 2;
                w.fxp = 1;
                BI k1w = new BI();
                k1w.SetValByInt(--k1);
                w10 = w10.BIPower(k1w);
                w = w.BIMul2(w10);
            }
            else
            {
                if (k >= 3)
                {
                    int m = (int)(Math.Sqrt(top) * 10) * 1000;
                    w.v[1] = m / 10000;
                    w.v[0] = m % 10000;
                    w.size = 2;
                    w.fxp = 1;
                    BI k1w = new BI();
                    k1w.SetValByInt(k1);
                    w10 = w10.BIPower(k1w);
                    w = w.BIMul2(w10);
                }
                else
                {
                    //int m = qsqrt.getqsqrt(top) * 1000;
                    int m = (int)(Math.Sqrt(top) * 10) * 1000;
                    w.v[1] = m / 10000;
                    w.v[0] = m % 10000;
                    w.size = 2;
                    w.fxp = 1;
                }

            }
            //end of initial valu setting
            BShiftLr(w, pr - w.fxp);
            w.fxp = 0;
            //1-9 3
            //
            //w.v[0]=3000;　　//初期値 1.3
            //w.v[1] = 1;
            //w.size = 2;
            //w.fxp = ini.fxp;
            i = 0;
            int rm = 0;
            do
            {
                w1 = w.BIMul2(w); // w^2
                BShiftR2(ref w1, pr); //
                w2 = w1.BIAdd(wa); // w^2+wa
                //w3 = w2.BIDiv(h, r);
                w3 = w2.BIDivInt(2, ref rm); //(w^2+wa) / 2
                if (w3.v[w3.size - 1] == 0) w3.size--;
                BShiftLr(w3, pr);
                wn = w3.BIDiv(w, r1);
                if (wn.v[wn.size - 1] == 0) wn.size--;
                cm = CompBI(wn, w);
                if (cm > 0)
                    //SwapBI(ref w, ref wn);
                    e = wn.BISub(w);
                else
                    e = w.BISub(wn);
                Console.WriteLine(e.size);
                if (e.size > 1 || e.size < 1)
                {
                    w = wn;
                    i++;
                    l = 99999;
                }
                else if (e.v[0] < 9999)  //e.size=1
                {
                    l = 9999;
                    w = wn;
                }

            }
            while (i < 35 && l > 9999);

            if (i >= 35)
            {
                //w.size=0;
                w.fxp = pr;
                n = i;
            }
            else
            {
                w.fxp = pr;
                //CpyBI(fxp, w, pr - 1, pr);
                //CpyBI(w, w, w.size - 1, w.size - pr);
                n = i;
            }
            BIgetdigit2(w);
            return w;
        }

        public BI BIRand(int digit)
        {
            int m = digit / dRX;
            int r = digit % dRX;
            Random rnd = new Random();
            this.size = m + 1;
            if (r == 0)
                m--;

            for (int i = 0; i < m; i++)
            {
                this.v[i] = rnd.Next(RX);
            }
            if (m != 0)
            {
                int n = 10 ^ m;
                this.v[m + 1] = rnd.Next(RX);
            }
            BIgetdigit2(this);
            return this;


        }
        static public BI BIRand2(BI x, BI max)
        {
            BI a = new BI(), b = new BI(), r = new BI();
            a.SetValByInt(6511);
            b.SetValByInt(1025);
            a.BIMul2(x).BIAdd(b).BIDiv(max, r);
            BIgetdigit2(r);
            return r;
        }

        public static Boolean BIisPrime(BI p)
        {
            BI d = new BI(), one = new BI(), two = new BI(), r = new BI();
            one.SetValByInt(1);
            two.SetValByInt(2);
            if (p.size == 1 && p.v[0] == 2) return true;
            if ((p.size == 1 && p.v[0] < 2) || (p.v[0] & 1) == 0) return false;

            d = p.BISub(one).BIDiv(two, r);
            while ((d.v[0] & 1) == 0) d = d.BIDiv(two, r);
            BI x = new BI(), y = new BI(), a = new BI(), b = new BI(), t = new BI(), pm = new BI();
            x.SetValByStr("65162");
            a.SetValByInt(2511);
            b.SetValByInt(1003);
            pm = p.BISub(one);
            for (int i = 1; i < 50; i++)
            {
                //合同法乱数 (a*x+b)/p
                a.BIMul2(x).BIAdd(b).BIDiv(pm, r);
                //x = r;
                CpyBI(x, r, r.size - 1, r.size);
                //Console.WriteLine( x.v[0]);
                //t = d;
                CpyBI(t, d, d.size - 1, d.size);

                //x.BIPower(t).BIDiv(p, r);
                y = x.BIPowm(t, p);

                while (!BIisZero(t.BISub(pm)) && !BIisZero(y.BISub(one)) && !BIisZero(y.BISub(pm)))
                {
                    //y.BIPower(two).BIDiv(p, r);
                    y = y.BIPowm(two, p);
                    t = t.BIMul2(two);
                }

                if (!BIisZero(y.BISub(pm)) && (t.v[0] & 1) == 0) return false;
            }
            return true;

        }
        //def is_prime3(q,k=100): 
        //  q = abs(q) 
        //  #計算するまでもなく判定できるものははじく 
        //  if q == 2: return True 
        //  if q < 2 or q&1 == 0: return False 
        //  #n-1=2^s*dとし（但しaは整数、dは奇数)、dを求める 
        //  d = (q-1)>>1 
        //  while d&1 == 0: d >>= 1 
        //  #判定をk回繰り返す 
        //  for i in range(k): 
        //    a = random.randint(1,q-1) 
        //    t = d 
        //    y = pow(a,t,q) 
        //    #[0,s-1]の範囲すべてをチェック 
        //    while t != q-1 and y != 1 and y != q-1: 
        //      y = pow(y,2,q) 
        //      t <<= 1 
        //    if y != q-1 and t&1 == 0: 
        //      return False 
        //  return True
    }
}



