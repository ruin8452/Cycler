
namespace CyclerTester
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.MoniEnable = new System.Windows.Forms.CheckBox();
            this.StatusBtn = new System.Windows.Forms.Button();
            this.DataView = new System.Windows.Forms.ListBox();
            this.ProtectBtn = new System.Windows.Forms.Button();
            this.StepRecipyBtn = new System.Windows.Forms.Button();
            this.StepEndBtn = new System.Windows.Forms.Button();
            this.CtrlBtn = new System.Windows.Forms.Button();
            this.ChamberSetBtn = new System.Windows.Forms.Button();
            this.PreCDBtn = new System.Windows.Forms.Button();
            this.CouplingTestBtn = new System.Windows.Forms.Button();
            this.CalPointCheckBtn = new System.Windows.Forms.Button();
            this.CalOutChangeBtn = new System.Windows.Forms.Button();
            this.CalDmmBtn = new System.Windows.Forms.Button();
            this.CalOutStopBtn = new System.Windows.Forms.Button();
            this.CalRelayBtn = new System.Windows.Forms.Button();
            this.SafetyBtn = new System.Windows.Forms.Button();
            this.ConnectCheck = new System.Windows.Forms.CheckBox();
            this.ConnectCheckBtn = new System.Windows.Forms.Button();
            this.PatternBtn = new System.Windows.Forms.Button();
            this.ChamGroupBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP";
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(53, 9);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(100, 21);
            this.IPBox.TabIndex = 2;
            this.IPBox.Text = "192.168.11.11";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(53, 37);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(100, 21);
            this.PortBox.TabIndex = 3;
            this.PortBox.Text = "1000";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(160, 9);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 4;
            this.ConnectBtn.Text = "연결";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // listView
            // 
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(14, 79);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(765, 359);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // MoniEnable
            // 
            this.MoniEnable.AutoSize = true;
            this.MoniEnable.Location = new System.Drawing.Point(397, 11);
            this.MoniEnable.Name = "MoniEnable";
            this.MoniEnable.Size = new System.Drawing.Size(72, 16);
            this.MoniEnable.TabIndex = 6;
            this.MoniEnable.Text = "모니터링";
            this.MoniEnable.UseVisualStyleBackColor = true;
            this.MoniEnable.CheckedChanged += new System.EventHandler(this.MoniEnable_CheckedChanged);
            // 
            // StatusBtn
            // 
            this.StatusBtn.Location = new System.Drawing.Point(14, 77);
            this.StatusBtn.Name = "StatusBtn";
            this.StatusBtn.Size = new System.Drawing.Size(104, 23);
            this.StatusBtn.TabIndex = 7;
            this.StatusBtn.Text = "장비 상태";
            this.StatusBtn.UseVisualStyleBackColor = true;
            this.StatusBtn.Click += new System.EventHandler(this.StatusBtn_Click);
            // 
            // DataView
            // 
            this.DataView.Font = new System.Drawing.Font("D2Coding", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DataView.FormattingEnabled = true;
            this.DataView.HorizontalScrollbar = true;
            this.DataView.ItemHeight = 14;
            this.DataView.Location = new System.Drawing.Point(160, 77);
            this.DataView.Name = "DataView";
            this.DataView.Size = new System.Drawing.Size(885, 508);
            this.DataView.TabIndex = 8;
            // 
            // ProtectBtn
            // 
            this.ProtectBtn.Enabled = false;
            this.ProtectBtn.Location = new System.Drawing.Point(14, 106);
            this.ProtectBtn.Name = "ProtectBtn";
            this.ProtectBtn.Size = new System.Drawing.Size(104, 23);
            this.ProtectBtn.TabIndex = 9;
            this.ProtectBtn.Text = "보호 조건";
            this.ProtectBtn.UseVisualStyleBackColor = true;
            this.ProtectBtn.Click += new System.EventHandler(this.ProtectBtn_Click);
            // 
            // StepRecipyBtn
            // 
            this.StepRecipyBtn.Enabled = false;
            this.StepRecipyBtn.Location = new System.Drawing.Point(14, 135);
            this.StepRecipyBtn.Name = "StepRecipyBtn";
            this.StepRecipyBtn.Size = new System.Drawing.Size(104, 23);
            this.StepRecipyBtn.TabIndex = 10;
            this.StepRecipyBtn.Text = "스텝 공정";
            this.StepRecipyBtn.UseVisualStyleBackColor = true;
            this.StepRecipyBtn.Click += new System.EventHandler(this.StepRecipyBtn_Click);
            // 
            // StepEndBtn
            // 
            this.StepEndBtn.Enabled = false;
            this.StepEndBtn.Location = new System.Drawing.Point(14, 164);
            this.StepEndBtn.Name = "StepEndBtn";
            this.StepEndBtn.Size = new System.Drawing.Size(104, 23);
            this.StepEndBtn.TabIndex = 11;
            this.StepEndBtn.Text = "스텝 종료n보호";
            this.StepEndBtn.UseVisualStyleBackColor = true;
            this.StepEndBtn.Click += new System.EventHandler(this.StepEndBtn_Click);
            // 
            // CtrlBtn
            // 
            this.CtrlBtn.Enabled = false;
            this.CtrlBtn.Location = new System.Drawing.Point(14, 193);
            this.CtrlBtn.Name = "CtrlBtn";
            this.CtrlBtn.Size = new System.Drawing.Size(104, 23);
            this.CtrlBtn.TabIndex = 12;
            this.CtrlBtn.Text = "제어 명령";
            this.CtrlBtn.UseVisualStyleBackColor = true;
            this.CtrlBtn.Click += new System.EventHandler(this.CtrlBtn_Click);
            // 
            // ChamberSetBtn
            // 
            this.ChamberSetBtn.Enabled = false;
            this.ChamberSetBtn.Location = new System.Drawing.Point(14, 222);
            this.ChamberSetBtn.Name = "ChamberSetBtn";
            this.ChamberSetBtn.Size = new System.Drawing.Size(104, 23);
            this.ChamberSetBtn.TabIndex = 13;
            this.ChamberSetBtn.Text = "챔버 설정";
            this.ChamberSetBtn.UseVisualStyleBackColor = true;
            this.ChamberSetBtn.Click += new System.EventHandler(this.ChamberSetBtn_Click);
            // 
            // PreCDBtn
            // 
            this.PreCDBtn.Enabled = false;
            this.PreCDBtn.Location = new System.Drawing.Point(14, 251);
            this.PreCDBtn.Name = "PreCDBtn";
            this.PreCDBtn.Size = new System.Drawing.Size(104, 23);
            this.PreCDBtn.TabIndex = 14;
            this.PreCDBtn.Text = "간이 충방전";
            this.PreCDBtn.UseVisualStyleBackColor = true;
            this.PreCDBtn.Click += new System.EventHandler(this.PreCDBtn_Click);
            // 
            // CouplingTestBtn
            // 
            this.CouplingTestBtn.Location = new System.Drawing.Point(14, 280);
            this.CouplingTestBtn.Name = "CouplingTestBtn";
            this.CouplingTestBtn.Size = new System.Drawing.Size(104, 23);
            this.CouplingTestBtn.TabIndex = 15;
            this.CouplingTestBtn.Text = "체결 테스트";
            this.CouplingTestBtn.UseVisualStyleBackColor = true;
            this.CouplingTestBtn.Click += new System.EventHandler(this.CouplingTestBtn_Click);
            // 
            // CalPointCheckBtn
            // 
            this.CalPointCheckBtn.Enabled = false;
            this.CalPointCheckBtn.Location = new System.Drawing.Point(14, 309);
            this.CalPointCheckBtn.Name = "CalPointCheckBtn";
            this.CalPointCheckBtn.Size = new System.Drawing.Size(104, 23);
            this.CalPointCheckBtn.TabIndex = 16;
            this.CalPointCheckBtn.Text = "캘 포인트 확인";
            this.CalPointCheckBtn.UseVisualStyleBackColor = true;
            this.CalPointCheckBtn.Click += new System.EventHandler(this.CalPointCheckBtn_Click);
            // 
            // CalOutChangeBtn
            // 
            this.CalOutChangeBtn.Location = new System.Drawing.Point(14, 367);
            this.CalOutChangeBtn.Name = "CalOutChangeBtn";
            this.CalOutChangeBtn.Size = new System.Drawing.Size(104, 23);
            this.CalOutChangeBtn.TabIndex = 18;
            this.CalOutChangeBtn.Text = "캘 출력값 변경";
            this.CalOutChangeBtn.UseVisualStyleBackColor = true;
            this.CalOutChangeBtn.Click += new System.EventHandler(this.CalOutChangeBtn_Click);
            // 
            // CalDmmBtn
            // 
            this.CalDmmBtn.Location = new System.Drawing.Point(14, 396);
            this.CalDmmBtn.Name = "CalDmmBtn";
            this.CalDmmBtn.Size = new System.Drawing.Size(104, 23);
            this.CalDmmBtn.TabIndex = 19;
            this.CalDmmBtn.Text = "캘 DMM 적용";
            this.CalDmmBtn.UseVisualStyleBackColor = true;
            this.CalDmmBtn.Click += new System.EventHandler(this.CalDmmBtn_Click);
            // 
            // CalOutStopBtn
            // 
            this.CalOutStopBtn.Location = new System.Drawing.Point(12, 425);
            this.CalOutStopBtn.Name = "CalOutStopBtn";
            this.CalOutStopBtn.Size = new System.Drawing.Size(104, 23);
            this.CalOutStopBtn.TabIndex = 20;
            this.CalOutStopBtn.Text = "캘 출력 정지";
            this.CalOutStopBtn.UseVisualStyleBackColor = true;
            this.CalOutStopBtn.Click += new System.EventHandler(this.CalOutStopBtn_Click);
            // 
            // CalRelayBtn
            // 
            this.CalRelayBtn.Enabled = false;
            this.CalRelayBtn.Location = new System.Drawing.Point(12, 454);
            this.CalRelayBtn.Name = "CalRelayBtn";
            this.CalRelayBtn.Size = new System.Drawing.Size(104, 23);
            this.CalRelayBtn.TabIndex = 21;
            this.CalRelayBtn.Text = "캘 메인 릴레이";
            this.CalRelayBtn.UseVisualStyleBackColor = true;
            this.CalRelayBtn.Click += new System.EventHandler(this.CalRelayBtn_Click);
            // 
            // SafetyBtn
            // 
            this.SafetyBtn.Enabled = false;
            this.SafetyBtn.Location = new System.Drawing.Point(12, 483);
            this.SafetyBtn.Name = "SafetyBtn";
            this.SafetyBtn.Size = new System.Drawing.Size(104, 23);
            this.SafetyBtn.TabIndex = 22;
            this.SafetyBtn.Text = "안전조건";
            this.SafetyBtn.UseVisualStyleBackColor = true;
            this.SafetyBtn.Click += new System.EventHandler(this.SafetyBtn_Click);
            // 
            // ConnectCheck
            // 
            this.ConnectCheck.AutoSize = true;
            this.ConnectCheck.Enabled = false;
            this.ConnectCheck.Location = new System.Drawing.Point(258, 9);
            this.ConnectCheck.Name = "ConnectCheck";
            this.ConnectCheck.Size = new System.Drawing.Size(72, 16);
            this.ConnectCheck.TabIndex = 23;
            this.ConnectCheck.Text = "연결여부";
            this.ConnectCheck.UseVisualStyleBackColor = true;
            // 
            // ConnectCheckBtn
            // 
            this.ConnectCheckBtn.Location = new System.Drawing.Point(160, 38);
            this.ConnectCheckBtn.Name = "ConnectCheckBtn";
            this.ConnectCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectCheckBtn.TabIndex = 24;
            this.ConnectCheckBtn.Text = "연결확인";
            this.ConnectCheckBtn.UseVisualStyleBackColor = true;
            this.ConnectCheckBtn.Click += new System.EventHandler(this.ConnectCheckBtn_Click);
            // 
            // PatternBtn
            // 
            this.PatternBtn.Enabled = false;
            this.PatternBtn.Location = new System.Drawing.Point(12, 512);
            this.PatternBtn.Name = "PatternBtn";
            this.PatternBtn.Size = new System.Drawing.Size(104, 23);
            this.PatternBtn.TabIndex = 25;
            this.PatternBtn.Text = "패턴 전송";
            this.PatternBtn.UseVisualStyleBackColor = true;
            this.PatternBtn.Click += new System.EventHandler(this.PatternBtn_Click);
            // 
            // ChamGroupBtn
            // 
            this.ChamGroupBtn.Enabled = false;
            this.ChamGroupBtn.Location = new System.Drawing.Point(12, 541);
            this.ChamGroupBtn.Name = "ChamGroupBtn";
            this.ChamGroupBtn.Size = new System.Drawing.Size(104, 23);
            this.ChamGroupBtn.TabIndex = 26;
            this.ChamGroupBtn.Text = "챔버 그룹정보";
            this.ChamGroupBtn.UseVisualStyleBackColor = true;
            this.ChamGroupBtn.Click += new System.EventHandler(this.ChamGroupBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 599);
            this.Controls.Add(this.ChamGroupBtn);
            this.Controls.Add(this.PatternBtn);
            this.Controls.Add(this.ConnectCheckBtn);
            this.Controls.Add(this.ConnectCheck);
            this.Controls.Add(this.SafetyBtn);
            this.Controls.Add(this.CalRelayBtn);
            this.Controls.Add(this.CalOutStopBtn);
            this.Controls.Add(this.CalDmmBtn);
            this.Controls.Add(this.CalOutChangeBtn);
            this.Controls.Add(this.CalPointCheckBtn);
            this.Controls.Add(this.CouplingTestBtn);
            this.Controls.Add(this.PreCDBtn);
            this.Controls.Add(this.ChamberSetBtn);
            this.Controls.Add(this.CtrlBtn);
            this.Controls.Add(this.StepEndBtn);
            this.Controls.Add(this.StepRecipyBtn);
            this.Controls.Add(this.ProtectBtn);
            this.Controls.Add(this.DataView);
            this.Controls.Add(this.StatusBtn);
            this.Controls.Add(this.MoniEnable);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.CheckBox MoniEnable;
        private System.Windows.Forms.Button StatusBtn;
        private System.Windows.Forms.ListBox DataView;
        private System.Windows.Forms.Button ProtectBtn;
        private System.Windows.Forms.Button StepRecipyBtn;
        private System.Windows.Forms.Button StepEndBtn;
        private System.Windows.Forms.Button CtrlBtn;
        private System.Windows.Forms.Button ChamberSetBtn;
        private System.Windows.Forms.Button PreCDBtn;
        private System.Windows.Forms.Button CouplingTestBtn;
        private System.Windows.Forms.Button CalPointCheckBtn;
        private System.Windows.Forms.Button CalOutChangeBtn;
        private System.Windows.Forms.Button CalDmmBtn;
        private System.Windows.Forms.Button CalOutStopBtn;
        private System.Windows.Forms.Button CalRelayBtn;
        private System.Windows.Forms.Button SafetyBtn;
        private System.Windows.Forms.CheckBox ConnectCheck;
        private System.Windows.Forms.Button ConnectCheckBtn;
        private System.Windows.Forms.Button PatternBtn;
        private System.Windows.Forms.Button ChamGroupBtn;
    }
}

