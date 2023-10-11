using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;



namespace WindowsFormsApp1
{


    public enum Scene { NONE, LODING, START_MENU, MAIN_MENU, OPTION, STAGE_1, STAGE_2, STAGE_3, STAGE_4, RESULT, EXIT }
    public enum MediaType { COFFEE, COUKIWOYOMENAI, CUR_MOVE, CAMERA, STAGE1_BACK, STAGE1_DOUBLESHOT, MISTAKE, STAGE2_BACK, RAINDROP_1, RAINDROP_2, STAGE3_BACK, STEAM, MACHIENE, STAGE4_BACK}
    public enum StageMode { TUTORIAL, NOMAL, AUTO}
    public enum InputState { FINE, DENY }
    enum ImageChangeDirection { PREVIOUS, NEXT}

    public partial class Form1 : Form
    {
        String filePath = Application.StartupPath;//실행파일 경로
        Scene currentScene;
        InputState currentInputState = InputState.DENY;

        #region 사운드 변수 초기화 + mediaType 추가하기!
        List<Uri> sndUri = new List<Uri>();
        Dictionary<MediaType, MediaPlayer> mediaPlayerDic = new Dictionary<MediaType, MediaPlayer>();

        #endregion

        ISceneChange iSceneChanger;

        #region 장면추가
        ScenePictureClass lodingScene;
        ScenePictureClass startMenu;
        ScenePictureClass optionMenu;
        ScenePictureClass mainMenu;

        Stage1 stage_1;
        Stage2 stage_2;
        Stage3 stage_3;
        Stage4 stage_4;

        ResultScene resultScene;
        #endregion

        Dictionary<Scene, ScenePictureClass> SceneDic = new Dictionary<Scene, ScenePictureClass>();



        //배경 이미지 박스
        PicBox backgroundPicturebox = new PicBox();

        //페이딩 변수
        /*float alpha = 1f;
        float alphaOffset;*/

        //사운드 로딩 현황 변수
        static int curSndDownloaded = 0;


        public int xPos, yPos;//스테이지 3 변수
        private bool isLR = true;
        public Stage3Helper stage3SubForm;

        //디버그용 라벨
        /*
        public static Label _1;
        public static Label _2;
        public static Label _3;
        public static Label _4;
        */

        public Form1()
        {

            InitializeComponent();

            /*
            _1 = label1;
            _2 = label2;
            _3 = label3;
            _4 = label4;
            */

            //x좌표 계산
            xPos = this.DesktopLocation.X;

            iSceneChanger = new SceneFader(this, backgroundPicturebox, SceneDic);

            #region 장면 추가 및 picturebox 부모자식 연결
            lodingScene = new ScenePictureClass(iSceneChanger);
            startMenu = new ScenePictureClass(iSceneChanger);
            optionMenu = new ScenePictureClass(iSceneChanger);
            mainMenu = new ScenePictureClass(iSceneChanger);

            stage_1 = new Stage1(iSceneChanger);
            stage_2 = new Stage2(iSceneChanger);

            stage3SubForm = new Stage3Helper(pictureBox1.Size, this);
            stage_3 = new Stage3(iSceneChanger, stage3SubForm);

            stage_4 = new Stage4(iSceneChanger, stage_1, stage_2, stage_3, stage3SubForm);

            resultScene = new ResultScene(iSceneChanger);

            iSceneChanger.initResultScene(resultScene);

            //부모자식 순서대로 나열 및 선언, textbox가 부모면 그 labelControl을 부모로 지정!
            PicBox startMenuPic = new PicBox(startmenuImgBox, pictureBox1);
            PicBox startMenuSelect = new PicBox(startMenuSelectBox, startmenuImgBox);

            PicBox optionMenuPic = new PicBox(optionPictureBox, pictureBox1);
            PicBox optionBgmPic = new PicBox(optionBgmPictureBox, optionPictureBox);
            PicBox optionSndEffPic = new PicBox(optionSoundEffectPictureBox, optionBgmPictureBox);
            PicBox optionCursorPic = new PicBox(optionCursorPictureBox, optionSoundEffectPictureBox);

            PicBox mainMenuPic = new PicBox(mainPicture, pictureBox1);
            PicBox mainMenuTextBoxPic = new PicBox(mainMenuTextbox, mainPicture);
            PicBox mainMenuPic2 = new PicBox(mainPicture2, mainMenuTextboxLabel);

            PicBox stage1Pic = new PicBox(stage1_picturebox, pictureBox1);
            PicBox stage1TextBoxPic = new PicBox(stage1TextPicturebox, stage1_picturebox);
            PicBox flashEffect = new PicBox(stage1_flashEffect, stage1TextPictureboxLabel);

            PicBox characterPic = new PicBox(stage2_CharacterPicturebox, pictureBox1);
            PicBox rainDrop1PicBox = new PicBox(stage2_raindropPic_1, stage2_CharacterPicturebox);
            PicBox rainDrop2PicBox = new PicBox(stage2_raindropPic_2, stage2_raindropPic_1);
            PicBox stage2TextBoxPic = new PicBox(stage2TextPicturebox, stage2_raindropPic_2);

            PicBox stage3ArrowPicBox = new PicBox(stage3_arrowPictureBox, pictureBox1);
            PicBox stage3CommandPicBox = new PicBox(stage3CommandPictureBox, stage3_arrowPictureBox);
            PicBox stage3SmokePicBox = new PicBox(stage3SmokePictureBox, stage3CommandPictureBox);
            PicBox stage3TextBoxPic = new PicBox(stage3TextPicturebox, stage3SmokePictureBox);

            PicBox resultPicBox = new PicBox(resultPictureBox, pictureBox1);
            PicBox resultTextBoxPic = new PicBox(resultTextbox, resultPictureBox);
            #endregion
            

            //타이머 추가
            Timers.AddTimer(TimerType.STAGE_TIC_TIMER, stageTicTimer);
            Timers.AddTimer(TimerType.FADING_TIMER, FadingTimer);
            Timers.AddTimer(TimerType.FADING_SUB_TIMER, FadingSubTimer);
            Timers.AddTimer(TimerType.BLINK_TIMER, stage1BlinkTimer);
            Timers.AddTimer(TimerType.TEXT_TIMER, textTimer);
            Timers.AddTimer(TimerType.SCREEN_SHAKE_TIMER, ScreenShakeTimer);

            //모든 타이머 정지
            Timers.StopAllTimers();
            
            //base picturebox 연결
            backgroundPicturebox.pictureBox = pictureBox1;

            //이미지 및 사운드파일 링크
            try
            {

                List<Image> imageList = RWImageData.deserializImageData();
                int cnt = 0;

                #region 장면 추가 fin (이미지 로딩 -> 배포 시에는 .dat파일에서 비트맵 역직렬화, 딕셔너리에 클래스 추가)

                //lodingScene.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\lodingImg.png");
                //lodingScene.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\lodingImg.png");
                lodingScene.BackgrndBuffer = imageList[cnt++];
                lodingScene.BackgrndDefault = imageList[cnt++];



                SceneDic.Add(Scene.LODING, lodingScene);

                //-------------------------------------------------------------------------


                //startMenu.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\startBackBuffer.png");
                //startMenu.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back00.png");
                //다른 요소 있는 배경사진
                startMenu.BackgrndBuffer = imageList[cnt++];
                //배경만 있는 배경사진
                startMenu.BackgrndDefault = imageList[cnt++];



                //각 picbox객체의 0번은 조작가능(controllable)한 이미지로 추가********************
                /*startMenuSelect.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\a.png"));
                startMenuSelect.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\b.png"));
                startMenuSelect.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\c.png"));
                */
                startMenuSelect.images.Add(imageList[cnt++]);
                startMenuSelect.images.Add(imageList[cnt++]);
                startMenuSelect.images.Add(imageList[cnt++]);

                startMenuSelect.pictureBox.Image = startMenuSelect.images[0];

                startMenu.AddPlayerControlPicBox(startMenuSelect);
                //클래스 내 이미지 목록에 이미지 박스 추가

                //startMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\Desktop\동방\동방RC홍\dat\image\stageST_02\N\chM00.png"));
                //startMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\Desktop\동방\동방RC홍\dat\image\stageST_02\N\chM01.png"));
                startMenuPic.images.Add(imageList[cnt++]);
                startMenuPic.images.Add(imageList[cnt++]);

                startMenuPic.pictureBox.Image = startMenuPic.images[0];

                startMenu.AddStaticPicBox(startMenuPic);

                SceneDic.Add(Scene.START_MENU, startMenu);

                //-------------------------------------------------------------------------


                //optionMenu.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\optionBackBuffer.png");
                //optionMenu.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\riku.png");
                optionMenu.BackgrndBuffer = imageList[cnt++];
                optionMenu.BackgrndDefault = imageList[cnt++];



                //optionMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\chS00.png"));
                optionMenuPic.images.Add(imageList[cnt++]);

                optionMenuPic.pictureBox.Image = optionMenuPic.images[0];


                optionMenu.AddStaticPicBox(optionMenuPic);
                /*
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm1.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm2.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm3.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm4.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm5.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm6.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm7.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm8.png"));
                optionBgmPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm9.png"));
                */
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);
                optionBgmPic.images.Add(imageList[cnt++]);

                optionBgmPic.pictureBox.Image = optionBgmPic.images[4];
                optionMenu.setCurState(0, 4);//스택의 기본 이미지 상태 설정

                optionMenu.AddPlayerControlPicBox(optionBgmPic);
                /*
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff1.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff2.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff3.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff4.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff5.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff6.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff7.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff8.png"));
                optionSndEffPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff9.png"));
                */
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);
                optionSndEffPic.images.Add(imageList[cnt++]);


                optionSndEffPic.pictureBox.Image = optionSndEffPic.images[4];
                optionMenu.addStack(4);

                optionMenu.AddPlayerControlPicBox(optionSndEffPic);


                //optionCursorPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\option_cur1.png"));
                //optionCursorPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\option_cur2.png"));

                optionCursorPic.images.Add(imageList[cnt++]);
                optionCursorPic.images.Add(imageList[cnt++]);


                optionCursorPic.pictureBox.Image = optionCursorPic.images[0];
                optionMenu.addStack(0);

                optionMenu.AddPlayerControlPicBox(optionCursorPic);


                SceneDic.Add(Scene.OPTION, optionMenu);

                //-------------------------------------------------------------------------

                //mainMenu.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back001_Buffer.png");
                //mainMenu.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back001.png");
                //다른 요소 있는 배경사진
                mainMenu.BackgrndBuffer = imageList[cnt++];
                //배경만 있는 배경사진
                mainMenu.BackgrndDefault = imageList[cnt++];




                //mainMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_A.png"));
                //mainMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_B.png"));
                //mainMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_C.png"));
                //mainMenuPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_D.png"));

                mainMenuPic.images.Add(imageList[cnt++]);
                mainMenuPic.images.Add(imageList[cnt++]);
                mainMenuPic.images.Add(imageList[cnt++]);
                mainMenuPic.images.Add(imageList[cnt++]);

                mainMenuPic.pictureBox.Image = mainMenuPic.images[0];

                mainMenu.AddPlayerControlPicBox(mainMenuPic);


                /*
                mainMenuPic2.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_tuto.png"));
                mainMenuPic2.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_play.png"));
                mainMenuPic2.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_auto.png"));
                */
                mainMenuPic2.images.Add(imageList[cnt++]);
                mainMenuPic2.images.Add(imageList[cnt++]);
                mainMenuPic2.images.Add(imageList[cnt++]);

                mainMenuPic2.pictureBox.Image = mainMenuPic2.images[0];

                mainMenu.AddPlayerControlPicBox(mainMenuPic2);

                //mainMenuTextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //mainMenuTextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));
                mainMenuTextBoxPic.images.Add(imageList[cnt++]);
                mainMenuTextBoxPic.images.Add(imageList[cnt++]);

                mainMenuTextBoxPic.pictureBox.Image = mainMenuTextBoxPic.images[0];

                mainMenu.AddStaticPicBox(mainMenuTextBoxPic);
                //텍스트박스 추가작업
                mainMenu.textboxSetting(mainMenuTextBoxPic, mainMenuTextboxLabel);


                SceneDic.Add(Scene.MAIN_MENU, mainMenu);

                //-------------------------------------------------------------------------

                //stage_1.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_testbackground_Default.png");
                //stage_1.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png");
                stage_1.BackgrndBuffer = imageList[cnt++];
                stage_1.BackgrndDefault = imageList[cnt++];


                /*
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\0,0-1.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\0,0-2.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1,0.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1-1.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-0.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-2.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-1.png"));
                stage1Pic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1-2.png"));
                */
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);
                stage1Pic.images.Add(imageList[cnt++]);

                stage1Pic.pictureBox.Image = stage1Pic.images[0];

                stage_1.AddStaticPicBox(stage1Pic);


                //stage1TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //stage1TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));
                stage1TextBoxPic.images.Add(imageList[cnt++]);
                stage1TextBoxPic.images.Add(imageList[cnt++]);

                stage1TextBoxPic.pictureBox.Image = stage1TextBoxPic.images[0];

                stage_1.AddStaticPicBox(stage1TextBoxPic);
                //텍스트박스 추가작업
                stage_1.textboxSetting(stage1TextBoxPic, stage1TextPictureboxLabel);


                //flashEffect.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //flashEffect.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_flashlight_Image.png"));
                flashEffect.images.Add(imageList[cnt++]);
                flashEffect.images.Add(imageList[cnt++]);

                flashEffect.pictureBox.Image = flashEffect.images[0];

                stage_1.AddindependentPicBox(flashEffect);
                ///


                SceneDic.Add(Scene.STAGE_1, stage_1);

                //-------------------------------------------------------------------------

                stage_2.BackgrndBuffer = imageList[cnt++];
                stage_2.BackgrndDefault = imageList[cnt++];
                //stage_2.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage2_default.png");
                //stage_2.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png");


                //none(투명)이 없음!
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\idle1.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\idle2.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\looking_up.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\looking_forward.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_1.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_2.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_3.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_mistake.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_1.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_2.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_mistake.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\forward_success.png"));
                //characterPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\forward_fail.png"));

                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);
                characterPic.images.Add(imageList[cnt++]);

                characterPic.pictureBox.Image = characterPic.images[0];

                stage_2.AddStaticPicBox(characterPic);

                //rainDrop1PicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //rainDrop1PicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\raindropBubble1.png"));
                rainDrop1PicBox.images.Add(imageList[cnt++]);
                rainDrop1PicBox.images.Add(imageList[cnt++]);

                rainDrop1PicBox.pictureBox.Image = rainDrop1PicBox.images[0];
                stage_2.AddindependentPicBox(rainDrop1PicBox);


                //rainDrop2PicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //rainDrop2PicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\raindropBubble2.png"));

                rainDrop2PicBox.images.Add(imageList[cnt++]);
                rainDrop2PicBox.images.Add(imageList[cnt++]);

                stage_2.AddindependentPicBox(rainDrop2PicBox);

                rainDrop2PicBox.pictureBox.Image = rainDrop2PicBox.images[0];


                //stage2TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //stage2TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));
                stage2TextBoxPic.images.Add(imageList[cnt++]);
                stage2TextBoxPic.images.Add(imageList[cnt++]);

                stage2TextBoxPic.pictureBox.Image = stage2TextBoxPic.images[0];

                stage_2.AddStaticPicBox(stage2TextBoxPic);
                //텍스트박스 추가작업
                stage_2.textboxSetting(stage2TextBoxPic, stage2TextPictureboxLabel);


                SceneDic.Add(Scene.STAGE_2, stage_2);

                //============================================



                //stage_3.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage3_BackDefault.png");
                //stage_3.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png");

                stage_3.BackgrndBuffer = imageList[cnt++];
                stage_3.BackgrndDefault = imageList[cnt++];


                /*
                stage3ArrowPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowDefault_2.png"));
                stage3ArrowPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowLeft_2.png"));
                stage3ArrowPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowRight_2.png"));
                stage3ArrowPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowDown_2.png"));
                */
                stage3ArrowPicBox.images.Add(imageList[cnt++]);
                stage3ArrowPicBox.images.Add(imageList[cnt++]);
                stage3ArrowPicBox.images.Add(imageList[cnt++]);
                stage3ArrowPicBox.images.Add(imageList[cnt++]);

                stage3ArrowPicBox.pictureBox.Image = stage3ArrowPicBox.images[0];

                stage_3.AddStaticPicBox(stage3ArrowPicBox);

                /*
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\nomalLeft.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\nomalRight.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count2.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count3.png"));//4
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count4.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count5.png"));
                stage3CommandPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count10.png"));
                */
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);
                stage3CommandPicBox.images.Add(imageList[cnt++]);

                stage3CommandPicBox.pictureBox.Image = stage3CommandPicBox.images[0];

                stage_3.AddStaticPicBox(stage3CommandPicBox);


                /*
                stage3SmokePicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamSafe.png"));
                stage3SmokePicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamWarning1_ver2.png"));
                stage3SmokePicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamWarning2_ver2.png"));
                */
                stage3SmokePicBox.images.Add(imageList[cnt++]);
                stage3SmokePicBox.images.Add(imageList[cnt++]);
                stage3SmokePicBox.images.Add(imageList[cnt++]);

                stage3SmokePicBox.pictureBox.Image = stage3SmokePicBox.images[0];

                stage_3.AddStaticPicBox(stage3SmokePicBox);


                #region 서브폼 이미지 등록

                stage3SubForm.setImg(imageList[cnt++], imageList[cnt++]);

                #endregion


                //stage3TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //stage3TextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));

                stage3TextBoxPic.images.Add(imageList[cnt++]);
                stage3TextBoxPic.images.Add(imageList[cnt++]);

                stage3TextBoxPic.pictureBox.Image = stage3TextBoxPic.images[0];

                stage_3.AddStaticPicBox(stage3TextBoxPic);
                //텍스트박스 추가작업
                stage_3.textboxSetting(stage3TextBoxPic, stage3TextPictureboxLabel);




                SceneDic.Add(Scene.STAGE_3, stage_3);

                //-------------------------------------------------------------------------------------------------------------------

                //stage_4.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_testbackground_Default.png");
                //stage_4.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png");

                stage_4.BackgrndBuffer = imageList[cnt++];
                stage_4.BackgrndDefault = imageList[cnt++];

                //텍스트박스 추가작업
                stage_4.textboxSetting();

                SceneDic.Add(Scene.STAGE_4, stage_4);

                //-------------------------------------------------------------------------------------------------------------------

                //resultScene.BackgrndBuffer = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\scoreBack.png");
                //resultScene.BackgrndDefault = Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\scoreBack.png");

                resultScene.BackgrndBuffer = imageList[cnt++];
                resultScene.BackgrndDefault = imageList[cnt++];

                //resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));//0
                //resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result1.png"));
                //resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result2.png"));
                //resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result3.png"));//3

                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                /*
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_1.png"));//4
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_2.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_3.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_1.png"));//7
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_2.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_3.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_1.png"));//10
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_2.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_3.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_1.png"));//13
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_2.png"));
                resultPicBox.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_3.png"));
                */
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                resultPicBox.images.Add(imageList[cnt++]);
                

                resultPicBox.pictureBox.Image = resultPicBox.images[0];

                resultScene.AddStaticPicBox(resultPicBox);



                //resultTextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
                //resultTextBoxPic.images.Add(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));

                resultTextBoxPic.images.Add(imageList[cnt++]);
                resultTextBoxPic.images.Add(imageList[cnt++]);

                resultTextBoxPic.pictureBox.Image = resultTextBoxPic.images[0];

                resultScene.AddStaticPicBox(resultTextBoxPic);

                resultScene.textboxSetting(resultTextBoxPic, resultTextLabel);
                
                SceneDic.Add(Scene.RESULT, resultScene);
                #endregion

                #region 사운드 파일 연결 (볼륨 -> 0, 로딩 완료시 이벤트 발생 추가, 미디어 딕셔너리에 플레이어 추가)

                AddSnd(MediaType.CUR_MOVE, new Uri(filePath + @"\Resources\startMenuCurSnd.wav"));
                AddSnd(MediaType.CAMERA, new Uri(filePath + @"\Resources\camera_tmp.wav"));
                AddSnd(MediaType.STAGE1_BACK, new Uri(filePath + @"\Resources\stage1back.wav"));
                AddSnd(MediaType.MISTAKE, new Uri(filePath + @"\Resources\mistake.wav"));
                AddSnd(MediaType.STAGE1_DOUBLESHOT, new Uri(filePath + @"\Resources\stage1_doubleShot.wav"));
                AddSnd(MediaType.STAGE2_BACK, new Uri(filePath + @"\Resources\stage2_back.wav"));
                AddSnd(MediaType.RAINDROP_1, new Uri(filePath + @"\Resources\raindrop1.wav"));
                AddSnd(MediaType.RAINDROP_2, new Uri(filePath + @"\Resources\raindrop2.wav"));
                AddSnd(MediaType.STAGE3_BACK, new Uri(filePath + @"\Resources\stage3_back.wav"));
                AddSnd(MediaType.STEAM, new Uri(filePath + @"\Resources\SteamEffect.wav"));
                AddSnd(MediaType.MACHIENE, new Uri(filePath + @"\Resources\testCrash.wav"));
                AddSnd(MediaType.STAGE4_BACK, new Uri(filePath + @"\Resources\stage4back.wav"));


                //각 씬에 bgm 등록 ( 없으면 생략 )
                stage_1.setBGM(mediaPlayerDic[MediaType.STAGE1_BACK]);
                stage_2.setBGM(mediaPlayerDic[MediaType.STAGE2_BACK]);
                stage_3.setBGM(mediaPlayerDic[MediaType.STAGE3_BACK]);
                stage_4.setBGM(mediaPlayerDic[MediaType.STAGE4_BACK]);
                
                #endregion

            }
            catch (Exception e)
            {
                throw e;
            }



            #region 키입력 행동 정의

            startMenu.AddKeyInputEvent(Keys.Up, () => {
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);//1
                MoveCursor(ImageChangeDirection.PREVIOUS, 1);
            });
            startMenu.AddKeyInputEvent(Keys.Down, () => {
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);//1
                MoveCursor(ImageChangeDirection.NEXT, 1);
            });
            startMenu.AddKeyInputEvent(Keys.Z, () => {
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);//1
                if (startMenu.getCurState() == 0)
                {
                    iSceneChanger.ChangeScene(Scene.MAIN_MENU, true);
                }
                else if (startMenu.getCurState() == 1)
                {
                    iSceneChanger.ChangeScene(Scene.OPTION, true);
                }
                else if (startMenu.getCurState() == 2)
                {
                    iSceneChanger.ChangeScene(Scene.EXIT);
                }
            });
            //이미지 초기 설정 행동 정의
            /*
            startMenu.AddKeyInputEvent(Keys.T, () => {
                RWImageData.serializeImageData();
            });*/

            optionMenu.AddKeyInputEvent(Keys.Left, () =>
            {
                if (optionMenu.getCurState() == 0)
                {
                    if (optionMenu.getCurState(0) != 0)
                    {
                        MoveCursor(ImageChangeDirection.PREVIOUS, 0, 1);
                        SoundPlayer.addBgmVolume(-0.1f);
                    }
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }else if (optionMenu.getCurState() == 1) {
                    if (optionMenu.getCurState(1) != 0)
                    {
                        MoveCursor(ImageChangeDirection.PREVIOUS, 1, 1);
                        SoundPlayer.addFxVolume(-0.1f);
                    }
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }
            });
            optionMenu.AddKeyInputEvent(Keys.Right, () =>
            {
                if (optionMenu.getCurState() == 0)
                {
                    if (optionMenu.getCurState(0) != 8)
                    {
                        MoveCursor(ImageChangeDirection.NEXT, 0, 1);
                        SoundPlayer.addBgmVolume(0.1f);
                    }
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }
                else if (optionMenu.getCurState() == 1) {
                    if (optionMenu.getCurState(1) != 8)
                    {
                        MoveCursor(ImageChangeDirection.NEXT, 1, 1);
                        SoundPlayer.addFxVolume(0.1f);
                    }
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }
            });
            optionMenu.AddKeyInputEvent(Keys.Up, () => {
                if (optionMenu.getCurStackSize() == 3) {
                    MoveCursor(ImageChangeDirection.NEXT, 1);
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }
            });
            optionMenu.AddKeyInputEvent(Keys.Down, () => {
                if (optionMenu.getCurStackSize() == 3)
                {
                    MoveCursor(ImageChangeDirection.PREVIOUS, 1);
                    SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                }
            });
            optionMenu.AddKeyInputEvent(Keys.X, () =>
            {
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);
                iSceneChanger.ChangeScene(Scene.START_MENU, true);
            });

            mainMenu.AddKeyInputEvent(Keys.Left, () => {
                if (mainMenu.getCurStackSize() == 1)
                {
                    SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                    MoveCursor(ImageChangeDirection.PREVIOUS, 1);
                    mainMenu.PrintTextLineByIndex(mainMenu.getCurState(0));
                }
            });
            mainMenu.AddKeyInputEvent(Keys.Right, () => {

                if (mainMenu.getCurStackSize() == 1)
                {
                    SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                    MoveCursor(ImageChangeDirection.NEXT, 1);
                    mainMenu.PrintTextLineByIndex(mainMenu.getCurState(0));
                }
            });
            mainMenu.AddKeyInputEvent(Keys.Up, () => {
                if (mainMenu.getCurStackSize() == 2)
                {
                    SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                    MoveCursor(ImageChangeDirection.PREVIOUS, 1);
                }

            });
            mainMenu.AddKeyInputEvent(Keys.Down, () => {
                if (mainMenu.getCurStackSize() == 2)
                {
                    SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                    MoveCursor(ImageChangeDirection.NEXT, 1);
                }

            });
            mainMenu.AddKeyInputEvent(Keys.X, () => {
                SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                if (mainMenu.getCurStackSize() == 1)
                {
                    mainMenu.PrintTextLineByIndex(mainMenu.getCurState(0), true);
                    iSceneChanger.ChangeScene(Scene.START_MENU, true);
                }
                else
                {
                    mainMenu.removeState();
                }
            });
            mainMenu.AddKeyInputEvent(Keys.Z, () => {

                SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                int _curStateSize = mainMenu.getCurStackSize();
                if (_curStateSize == 1)
                {
                    if (!mainMenu.PrintTextLineByIndex(mainMenu.getCurState(0), true)) return;
                    mainMenu.addCurState();
                }
                else if (_curStateSize == 2)
                {

                    if (mainMenu.getCurState(0) == 0)//stage1
                    {
                        if (mainMenu.getCurState() == 0)
                        {
                            stage_1.setStageMode(StageMode.TUTORIAL);
                        }
                        else if (mainMenu.getCurState() == 1)
                        {
                            stage_1.setStageMode(StageMode.NOMAL);
                        }
                        else if (mainMenu.getCurState() == 2)
                        {
                            stage_1.setStageMode(StageMode.AUTO);
                        }
                        else
                        {
                            return;
                        }

                        iSceneChanger.ChangeScene(Scene.STAGE_1, true);


                    }
                    else if (mainMenu.getCurState(0) == 1)
                    {
                        if (mainMenu.getCurState() == 0)
                        {
                            stage_2.setStageMode(StageMode.TUTORIAL);
                        }
                        else if (mainMenu.getCurState() == 1)
                        {
                            stage_2.setStageMode(StageMode.NOMAL);
                        }
                        else if (mainMenu.getCurState() == 2)
                        {
                            stage_2.setStageMode(StageMode.AUTO);
                        }
                        else
                        {
                            return;
                        }
                        iSceneChanger.ChangeScene(Scene.STAGE_2, true);
                    }
                    else if (mainMenu.getCurState(0) == 2)
                    {
                        stage3SubForm.Show();

                        if (mainMenu.getCurState() == 0)
                        {
                            stage3SubForm.Visible = false;
                            stage_3.setStageMode(StageMode.TUTORIAL);
                        }
                        else if (mainMenu.getCurState() == 1)
                        {
                            stage_3.setStageMode(StageMode.NOMAL);
                        }
                        else if (mainMenu.getCurState() == 2)
                        {
                            stage_3.setStageMode(StageMode.AUTO);
                        }
                        else
                        {
                            return;
                        }


                        xPos = DesktopLocation.X;
                        yPos = DesktopLocation.Y;


                        int borderSize = (Width - pictureBox1.Width) / 2;

                        stage3SubForm.SetDesktopLocation(xPos + borderSize, yPos + Height - borderSize + 100 - (pictureBox1.Width / 20));
                        stage3SubForm.setXpos(xPos + borderSize);

                        Activate();

                        iSceneChanger.ChangeScene(Scene.STAGE_3, true);

                    }
                    else if (mainMenu.getCurState(0) == 3) {
                        if (mainMenu.getCurState() == 1)
                        {
                            stage_4.SetStageMode(StageMode.NOMAL);
                        }
                        else if (mainMenu.getCurState() == 2)
                        {
                            stage_4.SetStageMode(StageMode.AUTO);
                        }
                        else
                        {
                            return;
                        }

                        stage3SubForm.Show();

                        xPos = DesktopLocation.X;
                        yPos = DesktopLocation.Y;


                        int borderSize = (Width - pictureBox1.Width) / 2;
                        stage3SubForm.SetDesktopLocation(xPos + borderSize, yPos + Height - borderSize + 100 - (pictureBox1.Width / 20));
                        stage3SubForm.setXpos(xPos + borderSize);

                        stage3SubForm.Visible = false;
                        iSceneChanger.ChangeScene(Scene.STAGE_4, true);

                    }

                }
            });

            #endregion



            //스테이지들 초기화
            stage_1.init();
            stage_2.init();
            stage_3.init();
            stage_4.init();
            resultScene.init();

            #region 텍스트 등록

            mainMenu.addText("사진을 찍으려는 것 같은데 뭔가 문제가 있는걸까요?\n무슨일인지 한번 살펴보러 가 봅시다!");
            mainMenu.addText("갑작스래 소나기가 찾아왔네요. 마침 적당히 비를\n피할만한 곳이 있군요..어라? 그런데 이건 무슨소리죠?");
            mainMenu.addText("이시이가 잠깐 도와달라고 해서 따라오긴 했는데\n쿵쾅쿵쾅 시끄럽군요..위험한 곳은 아니겠죠?");
            mainMenu.addText("앞으로도 힘이 닿는데까지 여행을 떠나 봅시다!\n(※리믹스 스테이지 / 튜토리얼 없음)");

            stage_1.addText("사진을 찍으려는것 처럼 보이는데 뭔가\n카메라 타이머가 고장난 듯 하군요.");
            stage_1.addText("이래서는 둘이 같이 사진에 나오긴 힘들겠네요.\n...좀 도와줘 볼까요?");
            stage_1.addText("두 사람이 일정한 박자로 포즈를 취하면...\n그 다음 박자에 셔터를 누르면 됩니다!");
            stage_1.addText("먼저 어떤 느낌인지 한번 보는게 좋겠죠?\n카메라 플래시가 터지는 타이밍을 잘 보세요?");
            stage_1.addText("Pass");
            stage_1.addText("하나 둘 셋에 찰칵! 감이 좀 오시나요?");
            stage_1.addText("이번에는 실제로 해 봅시다. 지금이다!라고 생각되면\n[스페이스바]를 눌러 사진을 찍어보세요!");
            stage_1.addText("만약 타이밍이 맞았다면\n플래시가 터질거에요!");
            stage_1.addText("Pass");
            stage_1.addText("다시한번 해 봅시다!\n3번째 타이밍에 찰칵!");
            stage_1.addText("Repeat");
            stage_1.addText("잘했어요! ...어라? 이게 무슨 소리죠?");
            stage_1.addText("카메라에서 높은 경고음이 나기 시작했어요!");
            stage_1.addText("경고음이 두번 나면, 같은 리듬으로\n동시에 포즈를 두번 취할 거에요!");
            stage_1.addText("말로 하는것보다 한번 직접 보는게 낫겠죠?\n타이밍을 익히면서 봐 봅시다.");
            stage_1.addText("Pass");
            stage_1.addText("어때요? 두번 울린뒤에, 두번 찍는다!\n이렇게 4박자를 세어보는거에요!");
            stage_1.addText("실제로 해 볼 준비 되셨나요?");
            stage_1.addText("Pass");
            stage_1.addText("숫자를 4까지 마음속으로 세어보고\n3,4번째에 찍어보세요!");
            stage_1.addText("Repeat");
            stage_1.addText("좋아요! 이번에는 두가지를 섞어보죠\n아, 하는김에 속도도 좀 올려볼까요?");
            stage_1.addText("처음에는 한번 찍은 후에 두번,\n다음에는 두번 찍은다음 한번 찍을거에요!");
            stage_1.addText("우선 어떤 느낌인지 먼저 봅시다.\n집중하고! 잘 보세요~?");
            stage_1.addText("Pass");
            stage_1.addText("잘 보셨나요? 다시 확인시켜드리면\n한번 두번, 두번 한번, 총 여섯번이에요!");
            stage_1.addText("조금 어려울 수도 있지만 자신감 있게\n가 봅시다!");
            stage_1.addText("Pass");
            stage_1.addText("힘내세요! 조금만 더 박자에 귀 기울여보죠!");
            stage_1.addText("Repeat");
            stage_1.addText("완벽해요! 이렇게만 하시면 됩니다.");
            stage_1.addText("혹시 타이밍을 못 잡으시거나 어려우신 분들은\n자동 플레이를 이용해보세요!");
            stage_1.addText("그럼 이제 추억을 마구 남기러 가 볼까요~?");

            stage_2.addText("갑작스래 내리기 시작한 소나기가 곤란했지만\n다행히 비를 피할 곳을 찾았네요!");
            stage_2.addText("....어라? 무슨 소리가 나는것 같지 않나요...?");
            stage_2.addText("Pass");
            stage_2.addText("아무래도 빗방울들이 우리와 놀고싶어하는 것 같네요.");
            stage_2.addText("Pass");
            stage_2.addText("...아니면 그 반대인걸까요? 참 못말리겠군요\n(이미지를 구하면 삽입)");
            stage_2.addText("기왕 이렇게 된거, 떨어지는 빗방울들을\n모두 받아 내봅시다!");
            stage_2.addText("우선 신호음이 들릴 때까지\n빗방울들이 떨어지는 리듬을 잘 기억했다가...");
            stage_2.addText("Pass");
            stage_2.addText("같은 박자로 받아내는 거에요!\n");
            stage_2.addText("Pass");
            stage_2.addText("하늘색 물방울은 [왼쪽 방향키] \n파란색 물방울은 [오른쪽 방향키]를 누르면 된답니다!");
            stage_2.addText("만약 실수로 받지 못하면....벌칙!!\n...같은건 없지만 좀 아쉽겠죠?");
            stage_2.addText("한번 실제로 받아내 봅시다!");
            stage_2.addText("Pass");
            stage_2.addText("빗방울들이 떨어지는 패턴을 잘 들어보세요!\n신호음은 포홤되지 않는답니다!");
            stage_2.addText("Repeat");
            stage_2.addText("...의외로 좋은 소리가 나는데요?\n저도 왠지 들뜨기 시작했어요!");
            stage_2.addText("빗방울들도 신이 난 걸까요?\n조금 더 짧은 간격으로 떨어지네요!");
            stage_2.addText("아니면 그저 비가 거세진걸 수도 있구요...\n다시한번 잘 관찰 해 봅시다!");
            stage_2.addText("Pass");
            stage_2.addText("꽤 좋은 리듬으로 떨어지네요!\n사실은 빗방울들도 연주할 줄 아는걸까요?");
            stage_2.addText("아무튼 이번엔 직접 받아볼 준비 되셨나요?");
            stage_2.addText("Pass");
            stage_2.addText("조금 간격이 짧아졌을 뿐!\n달라진건 없어요! 파이팅!");
            stage_2.addText("Repeat");
            stage_2.addText("좋아요! 잘 하시는데요? 계속 하고 싶지만\n비도 조금 약해진것 같고..");
            stage_2.addText("조금 쉬었다가 본 게임으로 들어가 볼까요?");
            //             |"                                              wn                                                 "|
            stage_3.addText("도와준 덕분에 비행기가 거의 다 완성되어가는데\n용접해 붙일 철판이 부족해.");
            stage_3.addText("이 기계를 사용하면 철판을 구할 수 있지만\n문제가 있어서 혼자서는 사용하기 힘들거든.");
            stage_3.addText("우선 전기를 공급할건데, 발전기가 고장인지\n전기가 양쪽으로 번갈아가며 들어오는데");
            stage_3.addText("전류의 흐름은 내가 알려줄테니까 \n전류가 흐르는 방향으로 전선을 연결해줄래?");
            stage_3.addText("전선을 연결할때 왼쪽은[왼쪽 방향키],\n오른쪽은[오른쪽 방향키]로 연결하면 돼.");
            stage_3.addText("우선[보통으로] 신호와 함께 방향을 알려줄거야\n그럼 한박자 뒤에 그 방향으로 연결하고");
            stage_3.addText("다음 신호가 있을때까지 계속 한 박자 간격으로\n번갈아가면서 연결하면 되는거지.");
            stage_3.addText("우선 어떤 느낌인지 한번 보자구");
            stage_3.addText("Pass");
            stage_3.addText("좋아, 느낌이 왔니? 그럼 한번 해보자.");
            stage_3.addText("Pass");
            stage_3.addText("일정한 박자로 좌우로 연결하면 돼\n천천히 다시 해보자");
            stage_3.addText("Repeat");
            stage_3.addText("좋아 다음으로 넘어가 볼까?");
            stage_3.addText("이번에는 전류 흐름 변화 주기가 짧아지는 상황이야\n다시 얘기하자면");
            stage_3.addText("좌우로 더 빨리 번갈아가면서 연결해야 한다는\n이야기가 되는거지");
            stage_3.addText("주의할 점은, 이 경우에는 알아채는게 한계기 때문에\n어느 방향인지는 알려줄 수가 없어.");
            stage_3.addText("다만 좌우 번갈아가는 형식은 변하지 않으니까\n신호가 오면 한 박자 뒤에 속도만 2배가 되는거지");
            stage_3.addText("또 다른점이 한가지 있는데, 빨라지는 현상은 \n특정 횟수만큼만 진행되거든?");
            stage_3.addText("내가 몇번인지 외치는 횟수를 잘 봐뒀다가\n그 횟수만큼 반복하면 돼!");
            stage_3.addText("간단히 정리하자면");
            stage_3.addText("주기가 빨라지기 한 박자 전에\n내가 횟수와 같이 알려준다.");
            stage_3.addText("만일 다 끝났는데 다음 신호가 없으면\n그대로 다음 신호를 기다린다.");
            stage_3.addText("너무 설명이 길었지? 직접 한번 보면\n그렇게 어렵지는 않을거야");
            stage_3.addText("Pass");
            stage_3.addText("알겠어? 한박자 뒤에 숫자만큼 2배속!\n정리하자면 이런거지");
            stage_3.addText("실전으로 넘어가 보자");
            stage_3.addText("Pass");
            stage_3.addText("신호가 오면 한 박자 뒤에 적용하는거야!\n반박자로 진행중이라고 반 박자 뒤가 아니라");
            stage_3.addText("Repeat");
            stage_3.addText("좋아 이제 마지막이야, 조금만 더 집중!");
            stage_3.addText("기계에 충분한 전기가 모이면\n증기가 빠지는 소리가 날거야.");
            stage_3.addText("그것도 내가 신호를 보낼때랑 똑같이\n대응하면 돼");
            stage_3.addText("한박자 뒤에 [아래 화살표]를 눌러서\n기계를 작동시키면 철판이 만들어지는거지.");
            stage_3.addText("오래된 기계를 쓰는거라 좀 충격이 오겠지만\n괜찮으니까 그렇게 당황하지는 말고.");
            stage_3.addText("참 그리고 하나 더, [아래 방향키]를 누른 다음엔\n반드시 [왼쪽 방향키] 부터 시작이야 알겠지?");
            stage_3.addText("마찬가지로 한번 보고 시작하자.");
            stage_3.addText("Pass");
            stage_3.addText("생각보다 많이 흔들리네..\n이러다 잘못하면 무너지겠는걸?");
            stage_3.addText("(쿠르르르.....)");
            stage_3.addText("....얼른 연습할까?");
            stage_3.addText("Pass");
            stage_3.addText("한박자 뒤에 [아래 화살표]!\n흔들려도 당황하지 말고!");
            stage_3.addText("Repeat");
            stage_3.addText("좋아 이정도면 기계전담 조수로 일해도\n부족함 없는 실력인걸?");
            stage_3.addText("농담이니까 그런 눈으로 보지 말아줄래? 애초에\n나는 빨리 비행기를 만들어서 이곳을 떠나고 싶어.");
            stage_3.addText("(꼬르륵~~~~)");
            stage_3.addText(".............\n.............");
            stage_3.addText("배고픈건 어쩔수 없지, 저기 저 가방 있지?\n감자를 몇개 가져왔으니까");
            stage_3.addText("먹으면서 좀 쉬고 있다가 다시 본격적으로\n시작하자.");
            //

            resultScene.addText("카나자와의 감상\n\n");
            resultScene.addText("순간적인 포즈 선정도 좋고.\n");
            resultScene.addText("빠른 신호에도 정확한 포즈를 취했고\n");
            resultScene.addText("흔들린 사진도 거의 없네.. 대단한데?");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("카나자와의 감상\n\n");
            resultScene.addText("순간 반응속도가 괜찮은데?\n");
            resultScene.addText("가끔씩 당황하는것만 주의하면\n");
            resultScene.addText("좋은 사진이 나올 것 같아!");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("카나자와의 감상\n\n");
            resultScene.addText("하..하하.. 많이 긴장한 것 같네\n");
            resultScene.addText("사진들 대부분이 흔들렸어\n");
            resultScene.addText("조금만 더 긴장 풀고 찍어보자!");
            resultScene.addText("Define1");
            resultScene.addText("Define2");
            //
            resultScene.addText("빗방울들의 속삭임\n\n");
            resultScene.addText("받아주는 타이밍이 아주 정확했어!\n");
            resultScene.addText("빠르게 뛰어드는데도 잘 받아내 주더라구!\n");
            resultScene.addText("우리도 오랜만에 신나게 놀아본 것 같아!");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("빗방울들의 속삭임\n\n");
            resultScene.addText("가끔씩 놓쳐서 아쉬워..\n");
            resultScene.addText("그래도 즐겁지 않았어?\n");
            resultScene.addText("응! 다음에도 꼭 같이 놀아줘~?");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("빗방울들의 속삭임\n\n");
            resultScene.addText("어...음... 미안해..?\n");
            resultScene.addText("그래도 즐거웠어!\n");
            resultScene.addText("다음에는 좀 더 재미있게 놀아줘!");
            resultScene.addText("Define1");
            resultScene.addText("Define2");
            //
            resultScene.addText("이시이의 평가\n\n");
            resultScene.addText("전류 유도를 훌륭하게 해냈고\n");
            resultScene.addText("갑작스런 신호에도 침착하게 대처했네\n");
            resultScene.addText("오히려 전기가 남아버렸는걸..?");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("이시이의 평가\n\n");
            resultScene.addText("대체로 신호에 잘 따라주었어.\n");
            resultScene.addText("드문드문 당황하는게 보이긴 했지만\n");
            resultScene.addText("이정도면 충분해 수고했어.");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("이시이의 평가\n\n");
            resultScene.addText("음.. 너무 정신이 없었나?\n");
            resultScene.addText("그래도 뭐든지 계속하면 익숙해 질 테니\n");
            resultScene.addText("조금만 더 힘내 보자고");
            resultScene.addText("Define1");
            resultScene.addText("Define2");
            //
            resultScene.addText("구양이의 한마디\n\n");
            resultScene.addText("누이이이!(즐거웠어!)\n");
            resultScene.addText("누이!(신났어!)\n");
            resultScene.addText("누이 누이이~?(또 놀자~?)");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("구양이의 한마디\n\n");
            resultScene.addText("누이 누이이이..(조금 아쉬워어..)\n");
            resultScene.addText("누이이이..(그래도...)\n");
            resultScene.addText("누이!(재밌어!)");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("구양이의 한마디\n\n");
            resultScene.addText("누이이!(아쉬워!)\n");
            resultScene.addText("누이이...(그래도...)\n");
            resultScene.addText("누이 누이이!(다음에 또 놀자!)");
            resultScene.addText("Define1");
            resultScene.addText("Define2");

            resultScene.addText("디버그용 텍스트");
            #endregion

        }

        public Scene getCurrentScene() {
            return currentScene;
        }

        public void setCurrentScene(Scene _scene) {
            currentScene = _scene;
        }


        public InputState getInputState() {
            return currentInputState;
        }

        public void setInputState(InputState _inputState) { 
            currentInputState = _inputState;
        }


        private void AddSnd(MediaType _type, Uri _filePathUri) {
            MediaPlayer tmpPlayer = new MediaPlayer();
            tmpPlayer.MediaOpened += new System.EventHandler(SndLodeFinished);
            tmpPlayer.Volume = 0.0f;
            mediaPlayerDic.Add(_type, tmpPlayer);
            sndUri.Add(_filePathUri);
        }

        //폼 로드시 발생
        private void Form1_Load(object sender, EventArgs e)
        {
            currentScene = Scene.LODING;

            int cnt = 0;

            //사운드 링크 시작
            if (mediaPlayerDic.Count == sndUri.Count) {
                foreach (MediaPlayer _m in mediaPlayerDic.Values) {
                    _m.Open(sndUri[cnt++]);
                }

            }
            else {
                //todo : throw 사운드 링크 에러
            }


        }
        
        //키입력시 발생
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentInputState == InputState.DENY) return;

            SceneDic[currentScene].KeyInput(e.KeyCode);

        }


        #region 씬 내부 스택에 등록된 이미지 박스의 이미지 교체 함수

        /// <summary>
        /// 현재 씬의 picbox들중 스택의 크기(가장 마지막 스택 요소)에 해당하는 번호의 picbox이미지를 수정
        /// </summary>
        /// <param name="_d">이미지 변경 방향 매개변수</param>
        /// <param name="_moveCount">넘어갈 이미지 크기</param>
        private void MoveCursor(ImageChangeDirection _d, int _moveCount)
        {
            ScenePictureClass _c = SceneDic[currentScene];
            int curStateSize = _c.getCurStackSize();//현재 씬의 스택 사이즈(현재 포커스되어있는 사이즈)
            List<PicBox> _pb = _c.getPlayerControlPicboxes();//현재 씬의 picbox 목록


            if (curStateSize > 0 && curStateSize <= _pb.Count)//조작가능한 picbox가 존재하고 && 전체 picbox의 크기보다 인덱스가 작거나 같다
            {
                PicBox _p = _pb[curStateSize - 1];

                int imgListSize = _p.images.Count;

                if (_d == ImageChangeDirection.PREVIOUS)
                {
                    _c.setCurState((_c.getCurState() + (imgListSize - _moveCount)) % imgListSize);
                }
                else if (_d == ImageChangeDirection.NEXT)
                {
                    _c.setCurState((_c.getCurState() + _moveCount) % imgListSize);

                }
                _p.pictureBox.Image = _p.images[_c.getCurState()];


            }
        }

        /// <summary>
        /// 현재 씬의 picbox들중 원하는 스텍 index 번호의 picbox이미지를 수정
        /// </summary>
        /// <param name="_d">이미지 변경 방향 매개변수</param>
        /// <param name="_changeStackIndex">바꿀 pictureBox 스택 index ( 0부터 )</param>
        /// <param name="_moveCount">넘어갈 이미지 크기</param>
        private void MoveCursor(ImageChangeDirection _d, int _changeStackIndex, int _moveCount)
        {
            ScenePictureClass _c = SceneDic[currentScene];
            int curStateSize = _c.getCurStackSize();//현재 씬의 스택 사이즈(현재 포커스되어있는 사이즈)
            List<PicBox> _pb = _c.getPlayerControlPicboxes();//현재 씬의 picbox 목록


            if (curStateSize > 0 && curStateSize <= _pb.Count)//조작가능한 picbox가 존재하고 && 전체 picbox의 크기보다 인덱스가 작거나 같다
            {
                PicBox _p = _pb[_changeStackIndex];

                int imgListSize = _p.images.Count;

                if (_d == ImageChangeDirection.PREVIOUS)
                {
                    _c.setCurState(_changeStackIndex, (_c.getCurState(_changeStackIndex) + (imgListSize - _moveCount)) % imgListSize);
                }
                else if (_d == ImageChangeDirection.NEXT)
                {
                    _c.setCurState(_changeStackIndex, (_c.getCurState(_changeStackIndex) + _moveCount) % imgListSize);

                }
                _p.pictureBox.Image = _p.images[_c.getCurState(_changeStackIndex)];


            }
        }

        #endregion

        public void CloseForm()
        {
            this.Close();
        }

        //사운드 링크가 완료될 때마다 호출되는 함수
        private void SndLodeFinished(object sender, EventArgs e)
        {
            curSndDownloaded++;
            if (curSndDownloaded >= mediaPlayerDic.Count)
            {
                SoundPlayer.setMediaDic(mediaPlayerDic);
                iSceneChanger.ChangeScene(Scene.START_MENU);
            }


        }




        #region 타이머 및 테스트 영역

        private void stage1BlinkTimer_Tick(object sender, EventArgs e)
        {
            iSceneChanger.BlinkTimerCallback();
        }

        private void stageTicTimer_Tick(object sender, EventArgs e)
        {
            SceneDic[currentScene].StageNodeSwitcher();
        }
        
        private void ScreenShakeTimer_Tick(object sender, EventArgs e)
        {
            if (isLR)
            {
                isLR = !isLR;
                SetDesktopLocation(xPos + 5, yPos);
            }
            else {
                isLR = !isLR;
                SetDesktopLocation(xPos - 5, yPos);
            }
        }

        private void textTimer_Tick(object sender, EventArgs e)
        {
            SceneDic[currentScene].TextPrinter();
        }


        #endregion

        private void fading_Tick(object sender, EventArgs e)
        {
            iSceneChanger.FadingTimerCallback();
        }
        private void subFading_Tick(object sender, EventArgs e)
        {
            iSceneChanger.SubFadingTimerCallback();
        }
        
    }


}
