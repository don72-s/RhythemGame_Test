namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FadingTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.FadingSubTimer = new System.Windows.Forms.Timer(this.components);
            this.stageTicTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.stage1BlinkTimer = new System.Windows.Forms.Timer(this.components);
            this.textTimer = new System.Windows.Forms.Timer(this.components);
            this.stage1TextPictureboxLabel = new System.Windows.Forms.Label();
            this.mainMenuTextboxLabel = new System.Windows.Forms.Label();
            this.stage2TextPictureboxLabel = new System.Windows.Forms.Label();
            this.ScreenShakeTimer = new System.Windows.Forms.Timer(this.components);
            this.stage3TextPictureboxLabel = new System.Windows.Forms.Label();
            this.resultTextLabel = new System.Windows.Forms.Label();
            this.optionSoundEffectPictureBox = new System.Windows.Forms.PictureBox();
            this.optionBgmPictureBox = new System.Windows.Forms.PictureBox();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.resultTextbox = new System.Windows.Forms.PictureBox();
            this.stage2_CharacterPicturebox = new System.Windows.Forms.PictureBox();
            this.stage3TextPicturebox = new System.Windows.Forms.PictureBox();
            this.stage3SmokePictureBox = new System.Windows.Forms.PictureBox();
            this.stage3CommandPictureBox = new System.Windows.Forms.PictureBox();
            this.stage3_arrowPictureBox = new System.Windows.Forms.PictureBox();
            this.stage2TextPicturebox = new System.Windows.Forms.PictureBox();
            this.mainMenuTextbox = new System.Windows.Forms.PictureBox();
            this.stage1TextPicturebox = new System.Windows.Forms.PictureBox();
            this.mainPicture2 = new System.Windows.Forms.PictureBox();
            this.stage2_raindropPic_2 = new System.Windows.Forms.PictureBox();
            this.stage2_raindropPic_1 = new System.Windows.Forms.PictureBox();
            this.stage1_flashEffect = new System.Windows.Forms.PictureBox();
            this.stage1_picturebox = new System.Windows.Forms.PictureBox();
            this.optionPictureBox = new System.Windows.Forms.PictureBox();
            this.mainPicture = new System.Windows.Forms.PictureBox();
            this.startmenuImgBox = new System.Windows.Forms.PictureBox();
            this.startMenuSelectBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.optionCursorPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.optionSoundEffectPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionBgmPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultTextbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_CharacterPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3TextPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3SmokePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3CommandPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3_arrowPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2TextPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuTextbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1TextPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_raindropPic_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_raindropPic_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1_flashEffect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1_picturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startmenuImgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startMenuSelectBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionCursorPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FadingTimer
            // 
            this.FadingTimer.Enabled = true;
            this.FadingTimer.Interval = 35;
            this.FadingTimer.Tick += new System.EventHandler(this.fading_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // FadingSubTimer
            // 
            this.FadingSubTimer.Interval = 1;
            this.FadingSubTimer.Tick += new System.EventHandler(this.subFading_Tick);
            // 
            // stageTicTimer
            // 
            this.stageTicTimer.Interval = 1;
            this.stageTicTimer.Tick += new System.EventHandler(this.stageTicTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(534, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(534, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // stage1BlinkTimer
            // 
            this.stage1BlinkTimer.Interval = 70;
            this.stage1BlinkTimer.Tick += new System.EventHandler(this.stage1BlinkTimer_Tick);
            // 
            // textTimer
            // 
            this.textTimer.Interval = 5;
            this.textTimer.Tick += new System.EventHandler(this.textTimer_Tick);
            // 
            // stage1TextPictureboxLabel
            // 
            this.stage1TextPictureboxLabel.AutoSize = true;
            this.stage1TextPictureboxLabel.BackColor = System.Drawing.Color.Transparent;
            this.stage1TextPictureboxLabel.Font = new System.Drawing.Font("제주감귤 R", 30F);
            this.stage1TextPictureboxLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stage1TextPictureboxLabel.Location = new System.Drawing.Point(0, 0);
            this.stage1TextPictureboxLabel.MinimumSize = new System.Drawing.Size(800, 450);
            this.stage1TextPictureboxLabel.Name = "stage1TextPictureboxLabel";
            this.stage1TextPictureboxLabel.Padding = new System.Windows.Forms.Padding(25, 330, 0, 0);
            this.stage1TextPictureboxLabel.Size = new System.Drawing.Size(800, 450);
            this.stage1TextPictureboxLabel.TabIndex = 20;
            this.stage1TextPictureboxLabel.Text = "aaaaa";
            // 
            // mainMenuTextboxLabel
            // 
            this.mainMenuTextboxLabel.AutoSize = true;
            this.mainMenuTextboxLabel.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuTextboxLabel.Font = new System.Drawing.Font("제주감귤 R", 30F);
            this.mainMenuTextboxLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.mainMenuTextboxLabel.Location = new System.Drawing.Point(0, 0);
            this.mainMenuTextboxLabel.MinimumSize = new System.Drawing.Size(800, 450);
            this.mainMenuTextboxLabel.Name = "mainMenuTextboxLabel";
            this.mainMenuTextboxLabel.Padding = new System.Windows.Forms.Padding(25, 330, 0, 0);
            this.mainMenuTextboxLabel.Size = new System.Drawing.Size(800, 450);
            this.mainMenuTextboxLabel.TabIndex = 22;
            this.mainMenuTextboxLabel.Text = "사진을 찍으려는 것 같은데 뭔가 문제가 있는걸까요?\r\n무슨일인지 한번 살펴보러 가 봅시다!";
            // 
            // stage2TextPictureboxLabel
            // 
            this.stage2TextPictureboxLabel.AutoSize = true;
            this.stage2TextPictureboxLabel.BackColor = System.Drawing.Color.Transparent;
            this.stage2TextPictureboxLabel.Font = new System.Drawing.Font("제주감귤 R", 30F);
            this.stage2TextPictureboxLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stage2TextPictureboxLabel.Location = new System.Drawing.Point(0, 0);
            this.stage2TextPictureboxLabel.MinimumSize = new System.Drawing.Size(800, 450);
            this.stage2TextPictureboxLabel.Name = "stage2TextPictureboxLabel";
            this.stage2TextPictureboxLabel.Padding = new System.Windows.Forms.Padding(25, 330, 0, 0);
            this.stage2TextPictureboxLabel.Size = new System.Drawing.Size(800, 450);
            this.stage2TextPictureboxLabel.TabIndex = 24;
            this.stage2TextPictureboxLabel.Text = "사진을 찍으려는 것 같은데 뭔가 문제가 있는걸까요?\r\n무슨일인지 한번 살펴보러 가 봅시다!";
            // 
            // ScreenShakeTimer
            // 
            this.ScreenShakeTimer.Interval = 60;
            this.ScreenShakeTimer.Tick += new System.EventHandler(this.ScreenShakeTimer_Tick);
            // 
            // stage3TextPictureboxLabel
            // 
            this.stage3TextPictureboxLabel.AutoSize = true;
            this.stage3TextPictureboxLabel.BackColor = System.Drawing.Color.Transparent;
            this.stage3TextPictureboxLabel.Font = new System.Drawing.Font("제주감귤 R", 30F);
            this.stage3TextPictureboxLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.stage3TextPictureboxLabel.Location = new System.Drawing.Point(0, 0);
            this.stage3TextPictureboxLabel.MinimumSize = new System.Drawing.Size(800, 450);
            this.stage3TextPictureboxLabel.Name = "stage3TextPictureboxLabel";
            this.stage3TextPictureboxLabel.Padding = new System.Windows.Forms.Padding(25, 330, 0, 0);
            this.stage3TextPictureboxLabel.Size = new System.Drawing.Size(800, 450);
            this.stage3TextPictureboxLabel.TabIndex = 29;
            this.stage3TextPictureboxLabel.Text = "3번";
            // 
            // resultTextLabel
            // 
            this.resultTextLabel.AutoSize = true;
            this.resultTextLabel.BackColor = System.Drawing.Color.Transparent;
            this.resultTextLabel.Font = new System.Drawing.Font("제주감귤 R", 30F);
            this.resultTextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.resultTextLabel.Location = new System.Drawing.Point(0, 0);
            this.resultTextLabel.MinimumSize = new System.Drawing.Size(800, 450);
            this.resultTextLabel.Name = "resultTextLabel";
            this.resultTextLabel.Padding = new System.Windows.Forms.Padding(80, 90, 0, 0);
            this.resultTextLabel.Size = new System.Drawing.Size(800, 450);
            this.resultTextLabel.TabIndex = 33;
            this.resultTextLabel.Text = "3번";
            // 
            // optionSoundEffectPictureBox
            // 
            this.optionSoundEffectPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.optionSoundEffectPictureBox.Location = new System.Drawing.Point(0, 0);
            this.optionSoundEffectPictureBox.Name = "optionSoundEffectPictureBox";
            this.optionSoundEffectPictureBox.Size = new System.Drawing.Size(800, 450);
            this.optionSoundEffectPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.optionSoundEffectPictureBox.TabIndex = 36;
            this.optionSoundEffectPictureBox.TabStop = false;
            this.optionSoundEffectPictureBox.Visible = false;
            // 
            // optionBgmPictureBox
            // 
            this.optionBgmPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.optionBgmPictureBox.Location = new System.Drawing.Point(0, 0);
            this.optionBgmPictureBox.Name = "optionBgmPictureBox";
            this.optionBgmPictureBox.Size = new System.Drawing.Size(800, 450);
            this.optionBgmPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.optionBgmPictureBox.TabIndex = 35;
            this.optionBgmPictureBox.TabStop = false;
            this.optionBgmPictureBox.Visible = false;
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.resultPictureBox.Location = new System.Drawing.Point(0, 0);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(800, 450);
            this.resultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.resultPictureBox.TabIndex = 34;
            this.resultPictureBox.TabStop = false;
            this.resultPictureBox.Visible = false;
            // 
            // resultTextbox
            // 
            this.resultTextbox.BackColor = System.Drawing.Color.Transparent;
            this.resultTextbox.Image = global::WindowsFormsApp1.Properties.Resources.textbox_800_140;
            this.resultTextbox.Location = new System.Drawing.Point(0, 0);
            this.resultTextbox.Name = "resultTextbox";
            this.resultTextbox.Padding = new System.Windows.Forms.Padding(0, 310, 0, 0);
            this.resultTextbox.Size = new System.Drawing.Size(800, 450);
            this.resultTextbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.resultTextbox.TabIndex = 32;
            this.resultTextbox.TabStop = false;
            // 
            // stage2_CharacterPicturebox
            // 
            this.stage2_CharacterPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.stage2_CharacterPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.idle1;
            this.stage2_CharacterPicturebox.Location = new System.Drawing.Point(0, 0);
            this.stage2_CharacterPicturebox.Name = "stage2_CharacterPicturebox";
            this.stage2_CharacterPicturebox.Size = new System.Drawing.Size(800, 450);
            this.stage2_CharacterPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage2_CharacterPicturebox.TabIndex = 31;
            this.stage2_CharacterPicturebox.TabStop = false;
            this.stage2_CharacterPicturebox.Visible = false;
            // 
            // stage3TextPicturebox
            // 
            this.stage3TextPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.stage3TextPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.textbox_800_140;
            this.stage3TextPicturebox.Location = new System.Drawing.Point(0, 0);
            this.stage3TextPicturebox.Name = "stage3TextPicturebox";
            this.stage3TextPicturebox.Padding = new System.Windows.Forms.Padding(0, 310, 0, 0);
            this.stage3TextPicturebox.Size = new System.Drawing.Size(800, 450);
            this.stage3TextPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage3TextPicturebox.TabIndex = 30;
            this.stage3TextPicturebox.TabStop = false;
            // 
            // stage3SmokePictureBox
            // 
            this.stage3SmokePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.stage3SmokePictureBox.Location = new System.Drawing.Point(0, 0);
            this.stage3SmokePictureBox.Name = "stage3SmokePictureBox";
            this.stage3SmokePictureBox.Size = new System.Drawing.Size(800, 450);
            this.stage3SmokePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage3SmokePictureBox.TabIndex = 28;
            this.stage3SmokePictureBox.TabStop = false;
            this.stage3SmokePictureBox.Visible = false;
            // 
            // stage3CommandPictureBox
            // 
            this.stage3CommandPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.stage3CommandPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("stage3CommandPictureBox.Image")));
            this.stage3CommandPictureBox.Location = new System.Drawing.Point(0, 0);
            this.stage3CommandPictureBox.Name = "stage3CommandPictureBox";
            this.stage3CommandPictureBox.Size = new System.Drawing.Size(800, 450);
            this.stage3CommandPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage3CommandPictureBox.TabIndex = 27;
            this.stage3CommandPictureBox.TabStop = false;
            this.stage3CommandPictureBox.Visible = false;
            // 
            // stage3_arrowPictureBox
            // 
            this.stage3_arrowPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.stage3_arrowPictureBox.Image = global::WindowsFormsApp1.Properties.Resources.arrowDefault_2;
            this.stage3_arrowPictureBox.Location = new System.Drawing.Point(0, 0);
            this.stage3_arrowPictureBox.Name = "stage3_arrowPictureBox";
            this.stage3_arrowPictureBox.Size = new System.Drawing.Size(800, 450);
            this.stage3_arrowPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage3_arrowPictureBox.TabIndex = 26;
            this.stage3_arrowPictureBox.TabStop = false;
            this.stage3_arrowPictureBox.Visible = false;
            // 
            // stage2TextPicturebox
            // 
            this.stage2TextPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.stage2TextPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.textbox_800_140;
            this.stage2TextPicturebox.Location = new System.Drawing.Point(0, 0);
            this.stage2TextPicturebox.Name = "stage2TextPicturebox";
            this.stage2TextPicturebox.Padding = new System.Windows.Forms.Padding(0, 310, 0, 0);
            this.stage2TextPicturebox.Size = new System.Drawing.Size(800, 450);
            this.stage2TextPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage2TextPicturebox.TabIndex = 25;
            this.stage2TextPicturebox.TabStop = false;
            // 
            // mainMenuTextbox
            // 
            this.mainMenuTextbox.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuTextbox.Image = global::WindowsFormsApp1.Properties.Resources.textbox_800_140;
            this.mainMenuTextbox.Location = new System.Drawing.Point(0, 0);
            this.mainMenuTextbox.Name = "mainMenuTextbox";
            this.mainMenuTextbox.Padding = new System.Windows.Forms.Padding(0, 310, 0, 0);
            this.mainMenuTextbox.Size = new System.Drawing.Size(800, 450);
            this.mainMenuTextbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainMenuTextbox.TabIndex = 23;
            this.mainMenuTextbox.TabStop = false;
            // 
            // stage1TextPicturebox
            // 
            this.stage1TextPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.stage1TextPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.textbox_800_140;
            this.stage1TextPicturebox.Location = new System.Drawing.Point(0, 0);
            this.stage1TextPicturebox.Name = "stage1TextPicturebox";
            this.stage1TextPicturebox.Padding = new System.Windows.Forms.Padding(0, 310, 0, 0);
            this.stage1TextPicturebox.Size = new System.Drawing.Size(800, 450);
            this.stage1TextPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage1TextPicturebox.TabIndex = 21;
            this.stage1TextPicturebox.TabStop = false;
            // 
            // mainPicture2
            // 
            this.mainPicture2.BackColor = System.Drawing.Color.Transparent;
            this.mainPicture2.Image = global::WindowsFormsApp1.Properties.Resources.mainmenu_tuto;
            this.mainPicture2.Location = new System.Drawing.Point(0, 0);
            this.mainPicture2.MinimumSize = new System.Drawing.Size(800, 450);
            this.mainPicture2.Name = "mainPicture2";
            this.mainPicture2.Size = new System.Drawing.Size(800, 450);
            this.mainPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPicture2.TabIndex = 19;
            this.mainPicture2.TabStop = false;
            this.mainPicture2.Visible = false;
            // 
            // stage2_raindropPic_2
            // 
            this.stage2_raindropPic_2.BackColor = System.Drawing.Color.Transparent;
            this.stage2_raindropPic_2.Image = global::WindowsFormsApp1.Properties.Resources.raindropBubble2;
            this.stage2_raindropPic_2.Location = new System.Drawing.Point(0, 0);
            this.stage2_raindropPic_2.Name = "stage2_raindropPic_2";
            this.stage2_raindropPic_2.Padding = new System.Windows.Forms.Padding(486, 17, 221, 352);
            this.stage2_raindropPic_2.Size = new System.Drawing.Size(800, 450);
            this.stage2_raindropPic_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage2_raindropPic_2.TabIndex = 18;
            this.stage2_raindropPic_2.TabStop = false;
            this.stage2_raindropPic_2.Visible = false;
            // 
            // stage2_raindropPic_1
            // 
            this.stage2_raindropPic_1.BackColor = System.Drawing.Color.Transparent;
            this.stage2_raindropPic_1.Image = global::WindowsFormsApp1.Properties.Resources.raindropBubble1;
            this.stage2_raindropPic_1.Location = new System.Drawing.Point(0, 0);
            this.stage2_raindropPic_1.Name = "stage2_raindropPic_1";
            this.stage2_raindropPic_1.Padding = new System.Windows.Forms.Padding(239, 17, 468, 352);
            this.stage2_raindropPic_1.Size = new System.Drawing.Size(800, 450);
            this.stage2_raindropPic_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage2_raindropPic_1.TabIndex = 17;
            this.stage2_raindropPic_1.TabStop = false;
            this.stage2_raindropPic_1.Visible = false;
            // 
            // stage1_flashEffect
            // 
            this.stage1_flashEffect.BackColor = System.Drawing.Color.Transparent;
            this.stage1_flashEffect.Image = ((System.Drawing.Image)(resources.GetObject("stage1_flashEffect.Image")));
            this.stage1_flashEffect.Location = new System.Drawing.Point(0, 0);
            this.stage1_flashEffect.Name = "stage1_flashEffect";
            this.stage1_flashEffect.Size = new System.Drawing.Size(800, 450);
            this.stage1_flashEffect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage1_flashEffect.TabIndex = 15;
            this.stage1_flashEffect.TabStop = false;
            this.stage1_flashEffect.Visible = false;
            // 
            // stage1_picturebox
            // 
            this.stage1_picturebox.BackColor = System.Drawing.Color.Transparent;
            this.stage1_picturebox.Image = global::WindowsFormsApp1.Properties.Resources._0_0_1;
            this.stage1_picturebox.Location = new System.Drawing.Point(0, 0);
            this.stage1_picturebox.Name = "stage1_picturebox";
            this.stage1_picturebox.Size = new System.Drawing.Size(800, 450);
            this.stage1_picturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stage1_picturebox.TabIndex = 13;
            this.stage1_picturebox.TabStop = false;
            this.stage1_picturebox.Visible = false;
            // 
            // optionPictureBox
            // 
            this.optionPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.optionPictureBox.Image = global::WindowsFormsApp1.Properties.Resources.chS00;
            this.optionPictureBox.Location = new System.Drawing.Point(0, 0);
            this.optionPictureBox.Name = "optionPictureBox";
            this.optionPictureBox.Padding = new System.Windows.Forms.Padding(118, 30, 432, 170);
            this.optionPictureBox.Size = new System.Drawing.Size(800, 450);
            this.optionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.optionPictureBox.TabIndex = 7;
            this.optionPictureBox.TabStop = false;
            this.optionPictureBox.Visible = false;
            // 
            // mainPicture
            // 
            this.mainPicture.BackColor = System.Drawing.Color.Transparent;
            this.mainPicture.Image = global::WindowsFormsApp1.Properties.Resources.cur_A;
            this.mainPicture.Location = new System.Drawing.Point(0, 0);
            this.mainPicture.Name = "mainPicture";
            this.mainPicture.Size = new System.Drawing.Size(800, 450);
            this.mainPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPicture.TabIndex = 6;
            this.mainPicture.TabStop = false;
            this.mainPicture.Visible = false;
            // 
            // startmenuImgBox
            // 
            this.startmenuImgBox.BackColor = System.Drawing.Color.Transparent;
            this.startmenuImgBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startmenuImgBox.Image = global::WindowsFormsApp1.Properties.Resources.chM00;
            this.startmenuImgBox.Location = new System.Drawing.Point(0, 0);
            this.startmenuImgBox.Name = "startmenuImgBox";
            this.startmenuImgBox.Padding = new System.Windows.Forms.Padding(0, 0, 562, 225);
            this.startmenuImgBox.Size = new System.Drawing.Size(800, 450);
            this.startmenuImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.startmenuImgBox.TabIndex = 0;
            this.startmenuImgBox.TabStop = false;
            this.startmenuImgBox.Visible = false;
            // 
            // startMenuSelectBox
            // 
            this.startMenuSelectBox.BackColor = System.Drawing.Color.Transparent;
            this.startMenuSelectBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.startMenuSelectBox.Image = global::WindowsFormsApp1.Properties.Resources.a;
            this.startMenuSelectBox.Location = new System.Drawing.Point(0, 0);
            this.startMenuSelectBox.Name = "startMenuSelectBox";
            this.startMenuSelectBox.Padding = new System.Windows.Forms.Padding(400, 0, 0, 0);
            this.startMenuSelectBox.Size = new System.Drawing.Size(800, 450);
            this.startMenuSelectBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.startMenuSelectBox.TabIndex = 2;
            this.startMenuSelectBox.TabStop = false;
            this.startMenuSelectBox.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.lodingImg;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // optionCursorPictureBox
            // 
            this.optionCursorPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.optionCursorPictureBox.Location = new System.Drawing.Point(0, 0);
            this.optionCursorPictureBox.Name = "optionCursorPictureBox";
            this.optionCursorPictureBox.Size = new System.Drawing.Size(800, 450);
            this.optionCursorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.optionCursorPictureBox.TabIndex = 37;
            this.optionCursorPictureBox.TabStop = false;
            this.optionCursorPictureBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.optionCursorPictureBox);
            this.Controls.Add(this.optionSoundEffectPictureBox);
            this.Controls.Add(this.optionBgmPictureBox);
            this.Controls.Add(this.resultPictureBox);
            this.Controls.Add(this.resultTextLabel);
            this.Controls.Add(this.resultTextbox);
            this.Controls.Add(this.stage2_CharacterPicturebox);
            this.Controls.Add(this.stage3TextPicturebox);
            this.Controls.Add(this.stage3TextPictureboxLabel);
            this.Controls.Add(this.stage3SmokePictureBox);
            this.Controls.Add(this.stage3CommandPictureBox);
            this.Controls.Add(this.stage3_arrowPictureBox);
            this.Controls.Add(this.stage2TextPicturebox);
            this.Controls.Add(this.stage2TextPictureboxLabel);
            this.Controls.Add(this.mainMenuTextbox);
            this.Controls.Add(this.mainMenuTextboxLabel);
            this.Controls.Add(this.stage1TextPictureboxLabel);
            this.Controls.Add(this.stage1TextPicturebox);
            this.Controls.Add(this.mainPicture2);
            this.Controls.Add(this.stage2_raindropPic_2);
            this.Controls.Add(this.stage2_raindropPic_1);
            this.Controls.Add(this.stage1_flashEffect);
            this.Controls.Add(this.stage1_picturebox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.optionPictureBox);
            this.Controls.Add(this.mainPicture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startmenuImgBox);
            this.Controls.Add(this.startMenuSelectBox);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.optionSoundEffectPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionBgmPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultTextbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_CharacterPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3TextPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3SmokePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3CommandPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage3_arrowPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2TextPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuTextbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1TextPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_raindropPic_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage2_raindropPic_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1_flashEffect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stage1_picturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startmenuImgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startMenuSelectBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionCursorPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer FadingTimer;
        private System.Windows.Forms.PictureBox startmenuImgBox;
        private System.Windows.Forms.PictureBox startMenuSelectBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox mainPicture;
        private System.Windows.Forms.Timer FadingSubTimer;
        private System.Windows.Forms.PictureBox optionPictureBox;
        private System.Windows.Forms.Timer stageTicTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox stage1_picturebox;
        private System.Windows.Forms.PictureBox stage1_flashEffect;
        private System.Windows.Forms.Timer stage1BlinkTimer;
        private System.Windows.Forms.PictureBox stage2_raindropPic_1;
        private System.Windows.Forms.PictureBox stage2_raindropPic_2;
        private System.Windows.Forms.PictureBox mainPicture2;
        private System.Windows.Forms.Timer textTimer;
        private System.Windows.Forms.Label stage1TextPictureboxLabel;
        private System.Windows.Forms.PictureBox stage1TextPicturebox;
        private System.Windows.Forms.Label mainMenuTextboxLabel;
        private System.Windows.Forms.PictureBox mainMenuTextbox;
        private System.Windows.Forms.Label stage2TextPictureboxLabel;
        private System.Windows.Forms.PictureBox stage2TextPicturebox;
        private System.Windows.Forms.PictureBox stage3_arrowPictureBox;
        private System.Windows.Forms.PictureBox stage3CommandPictureBox;
        private System.Windows.Forms.PictureBox stage3SmokePictureBox;
        private System.Windows.Forms.Timer ScreenShakeTimer;
        private System.Windows.Forms.Label stage3TextPictureboxLabel;
        private System.Windows.Forms.PictureBox stage3TextPicturebox;
        private System.Windows.Forms.PictureBox stage2_CharacterPicturebox;
        private System.Windows.Forms.PictureBox resultTextbox;
        private System.Windows.Forms.Label resultTextLabel;
        private System.Windows.Forms.PictureBox resultPictureBox;
        private System.Windows.Forms.PictureBox optionBgmPictureBox;
        private System.Windows.Forms.PictureBox optionSoundEffectPictureBox;
        private System.Windows.Forms.PictureBox optionCursorPictureBox;
    }
}

