namespace CS_BigIntArith1
{
    partial class BigDigitCal
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BigDigitCal));
            this.txtIn1 = new System.Windows.Forms.TextBox();
            this.txtIn2 = new System.Windows.Forms.TextBox();
            this.btnDivide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExp = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnMult = new System.Windows.Forms.Button();
            this.btnPai = new System.Windows.Forms.Button();
            this.txtAddtional = new System.Windows.Forms.TextBox();
            this.btnSQRT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboDispdigit = new System.Windows.Forms.ComboBox();
            this.btnSub = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.btnRND1 = new System.Windows.Forms.Button();
            this.btnRND2 = new System.Windows.Forms.Button();
            this.txtGenDigit = new System.Windows.Forms.TextBox();
            this.btnPower = new System.Windows.Forms.Button();
            this.btnCrypt = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPowm = new System.Windows.Forms.Button();
            this.txtIn3 = new System.Windows.Forms.TextBox();
            this.btnMark = new System.Windows.Forms.Button();
            this.btnFact = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIn1
            // 
            this.txtIn1.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtIn1.Location = new System.Drawing.Point(47, 7);
            this.txtIn1.Multiline = true;
            this.txtIn1.Name = "txtIn1";
            this.txtIn1.Size = new System.Drawing.Size(1165, 53);
            this.txtIn1.TabIndex = 0;
            // 
            // txtIn2
            // 
            this.txtIn2.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtIn2.Location = new System.Drawing.Point(45, 94);
            this.txtIn2.Multiline = true;
            this.txtIn2.Name = "txtIn2";
            this.txtIn2.Size = new System.Drawing.Size(1165, 53);
            this.txtIn2.TabIndex = 1;
            // 
            // btnDivide
            // 
            this.btnDivide.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDivide.Location = new System.Drawing.Point(1165, 632);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(52, 23);
            this.btnDivide.TabIndex = 2;
            this.btnDivide.Text = "÷";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(1000, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fixed Point Digit";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox3.Location = new System.Drawing.Point(1134, 66);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(76, 23);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtOut
            // 
            this.txtOut.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOut.Location = new System.Drawing.Point(47, 184);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOut.Size = new System.Drawing.Size(1163, 401);
            this.txtOut.TabIndex = 5;
            this.txtOut.TextChanged += new System.EventHandler(this.txtOut_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAdd.Location = new System.Drawing.Point(1063, 631);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(46, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExp
            // 
            this.btnExp.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnExp.Location = new System.Drawing.Point(577, 630);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(79, 24);
            this.btnExp.TabIndex = 7;
            this.btnExp.Text = "Calc \"e\"";
            this.btnExp.UseVisualStyleBackColor = true;
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(49, 657);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1168, 19);
            this.txtMessage.TabIndex = 8;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // btnMult
            // 
            this.btnMult.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMult.Location = new System.Drawing.Point(1115, 632);
            this.btnMult.Name = "btnMult";
            this.btnMult.Size = new System.Drawing.Size(44, 23);
            this.btnMult.TabIndex = 9;
            this.btnMult.Text = "×";
            this.btnMult.UseVisualStyleBackColor = true;
            this.btnMult.Click += new System.EventHandler(this.btnMult_Click);
            // 
            // btnPai
            // 
            this.btnPai.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPai.Location = new System.Drawing.Point(492, 630);
            this.btnPai.Name = "btnPai";
            this.btnPai.Size = new System.Drawing.Size(79, 24);
            this.btnPai.TabIndex = 10;
            this.btnPai.Text = "Calc \"Π\"";
            this.btnPai.UseVisualStyleBackColor = true;
            this.btnPai.Click += new System.EventHandler(this.btnPai_Click);
            // 
            // txtAddtional
            // 
            this.txtAddtional.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAddtional.Location = new System.Drawing.Point(47, 589);
            this.txtAddtional.Name = "txtAddtional";
            this.txtAddtional.Size = new System.Drawing.Size(1163, 22);
            this.txtAddtional.TabIndex = 11;
            this.txtAddtional.TextChanged += new System.EventHandler(this.txtAddtional_TextChanged);
            // 
            // btnSQRT
            // 
            this.btnSQRT.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSQRT.Location = new System.Drawing.Point(662, 631);
            this.btnSQRT.Name = "btnSQRT";
            this.btnSQRT.Size = new System.Drawing.Size(79, 24);
            this.btnSQRT.TabIndex = 12;
            this.btnSQRT.Text = "SQRT";
            this.btnSQRT.UseVisualStyleBackColor = true;
            this.btnSQRT.Click += new System.EventHandler(this.btnSQRT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(782, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Disp digit Number";
            // 
            // comboDispdigit
            // 
            this.comboDispdigit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboDispdigit.FormattingEnabled = true;
            this.comboDispdigit.Location = new System.Drawing.Point(923, 66);
            this.comboDispdigit.Name = "comboDispdigit";
            this.comboDispdigit.Size = new System.Drawing.Size(71, 24);
            this.comboDispdigit.TabIndex = 15;
            this.comboDispdigit.SelectedIndexChanged += new System.EventHandler(this.comboDispdigit_SelectedIndexChanged);
            // 
            // btnSub
            // 
            this.btnSub.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSub.Location = new System.Drawing.Point(1014, 632);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(43, 23);
            this.btnSub.TabIndex = 16;
            this.btnSub.Text = "－";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(49, 634);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Repeat";
            // 
            // txtRepeat
            // 
            this.txtRepeat.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtRepeat.Location = new System.Drawing.Point(112, 631);
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.Size = new System.Drawing.Size(76, 22);
            this.txtRepeat.TabIndex = 18;
            // 
            // btnRND1
            // 
            this.btnRND1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRND1.Location = new System.Drawing.Point(49, 67);
            this.btnRND1.Name = "btnRND1";
            this.btnRND1.Size = new System.Drawing.Size(63, 23);
            this.btnRND1.TabIndex = 19;
            this.btnRND1.Text = "Random";
            this.btnRND1.UseVisualStyleBackColor = true;
            this.btnRND1.Click += new System.EventHandler(this.btnRND1_Click);
            // 
            // btnRND2
            // 
            this.btnRND2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRND2.Location = new System.Drawing.Point(222, 66);
            this.btnRND2.Name = "btnRND2";
            this.btnRND2.Size = new System.Drawing.Size(63, 23);
            this.btnRND2.TabIndex = 20;
            this.btnRND2.Text = "Random";
            this.btnRND2.UseVisualStyleBackColor = true;
            this.btnRND2.Click += new System.EventHandler(this.btnRND2_Click);
            // 
            // txtGenDigit
            // 
            this.txtGenDigit.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGenDigit.Location = new System.Drawing.Point(118, 67);
            this.txtGenDigit.Name = "txtGenDigit";
            this.txtGenDigit.Size = new System.Drawing.Size(98, 22);
            this.txtGenDigit.TabIndex = 21;
            this.txtGenDigit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGenDigit_KeyPress);
            // 
            // btnPower
            // 
            this.btnPower.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPower.Location = new System.Drawing.Point(917, 631);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(79, 24);
            this.btnPower.TabIndex = 22;
            this.btnPower.Text = "POWER";
            this.btnPower.UseVisualStyleBackColor = true;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // btnCrypt
            // 
            this.btnCrypt.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCrypt.Location = new System.Drawing.Point(348, 629);
            this.btnCrypt.Name = "btnCrypt";
            this.btnCrypt.Size = new System.Drawing.Size(60, 25);
            this.btnCrypt.TabIndex = 23;
            this.btnCrypt.Text = "CRYPT";
            this.btnCrypt.UseVisualStyleBackColor = true;
            this.btnCrypt.Click += new System.EventHandler(this.btnCrypt_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnTest.Location = new System.Drawing.Point(279, 629);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(63, 25);
            this.btnTest.TabIndex = 24;
            this.btnTest.Text = "Prime";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.Location = new System.Drawing.Point(488, 64);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 25);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPowm
            // 
            this.btnPowm.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPowm.Location = new System.Drawing.Point(832, 631);
            this.btnPowm.Name = "btnPowm";
            this.btnPowm.Size = new System.Drawing.Size(79, 24);
            this.btnPowm.TabIndex = 26;
            this.btnPowm.Text = "PWR Mod";
            this.btnPowm.UseVisualStyleBackColor = true;
            this.btnPowm.Click += new System.EventHandler(this.btnPowm_Click);
            // 
            // txtIn3
            // 
            this.txtIn3.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtIn3.Location = new System.Drawing.Point(45, 152);
            this.txtIn3.Multiline = true;
            this.txtIn3.Name = "txtIn3";
            this.txtIn3.Size = new System.Drawing.Size(1165, 28);
            this.txtIn3.TabIndex = 27;
            // 
            // btnMark
            // 
            this.btnMark.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMark.Location = new System.Drawing.Point(194, 631);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(67, 23);
            this.btnMark.TabIndex = 28;
            this.btnMark.Text = "PerfMark";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnFact
            // 
            this.btnFact.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFact.Location = new System.Drawing.Point(747, 630);
            this.btnFact.Name = "btnFact";
            this.btnFact.Size = new System.Drawing.Size(79, 24);
            this.btnFact.TabIndex = 29;
            this.btnFact.Text = "FACT";
            this.btnFact.UseVisualStyleBackColor = true;
            this.btnFact.Click += new System.EventHandler(this.btnFact_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 616);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1166, 10);
            this.progressBar1.TabIndex = 30;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(414, 629);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 25);
            this.button1.TabIndex = 31;
            this.button1.Text = "GCD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnGCD_Click);
            // 
            // BigDigitCal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1295, 695);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFact);
            this.Controls.Add(this.btnMark);
            this.Controls.Add(this.txtIn3);
            this.Controls.Add(this.btnPowm);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnCrypt);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.txtGenDigit);
            this.Controls.Add(this.btnRND2);
            this.Controls.Add(this.btnRND1);
            this.Controls.Add(this.txtRepeat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.comboDispdigit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSQRT);
            this.Controls.Add(this.txtAddtional);
            this.Controls.Add(this.btnPai);
            this.Controls.Add(this.btnMult);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnExp);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.txtIn2);
            this.Controls.Add(this.txtIn1);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BigDigitCal";
            this.Text = "多倍長計算 By Takao Hayashi.";
            this.Load += new System.EventHandler(this.BigDigitCal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIn1;
        private System.Windows.Forms.TextBox txtIn2;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnMult;
        private System.Windows.Forms.Button btnPai;
        private System.Windows.Forms.TextBox txtAddtional;
        private System.Windows.Forms.Button btnSQRT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboDispdigit;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.Button btnRND1;
        private System.Windows.Forms.Button btnRND2;
        private System.Windows.Forms.TextBox txtGenDigit;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Button btnCrypt;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPowm;
        private System.Windows.Forms.TextBox txtIn3;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnFact;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
    }
}

