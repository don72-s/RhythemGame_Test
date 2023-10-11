using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

namespace WindowsFormsApp1
{

    public partial class PicBox
    {
        public PicBox() { }

        public PicBox(PictureBox _pictureBox, Control _parentPictureBox) {
            pictureBox = _pictureBox;
            pictureBox.Parent = _parentPictureBox;
        }

        public PictureBox pictureBox;
        public List<Image> images = new List<Image>();
    }

    public partial class ScenePictureClass
    {

        protected ISceneChange SceneChanger;


        protected List<PicBox> playerControlPicBox = new List<PicBox>();
        protected List<PicBox> staticPicBoxes = new List<PicBox>();
        protected List<PicBox> independentPicBoxes = new List<PicBox>();

        protected Dictionary<Keys, Action> keyInputDic = new Dictionary<Keys, Action>();

        protected MediaPlayer bgm = null;
        protected bool isBGMContinue = false;

        private Image backgrndBuffer;
        private Image backgrndDefault;

        private List<int> curStateStack = new List<int>();

/*        protected PicBox textPicBox = null;
        protected Label textPictureboxLabel = null;*/
        protected ITextPrinter ITextPrinter = null;

        public ScenePictureClass(ISceneChange _IScene)
        {
            SceneChanger = _IScene;

            //기본적으로 크기 1 이상의 스택을 반드시 가짊을 의미
            curStateStack.Add(0);
        }

        
        public void AddPlayerControlPicBox(PicBox _p)
        {
            playerControlPicBox.Add(_p);
        }
        public void AddStaticPicBox(PicBox _p)
        {
            staticPicBoxes.Add(_p);
        }
        public void AddindependentPicBox(PicBox _p)
        {
            independentPicBoxes.Add(_p);
        }

        public void setBGM(MediaPlayer _bgm) { 
            bgm = _bgm;
            if (ITextPrinter != null) {
                ITextPrinter.setBGM(bgm);
            }
        }

        public MediaPlayer getBGM() {
            return bgm;
        }


        public Image BackgrndBuffer
        {
            get { return backgrndBuffer; }
            set { backgrndBuffer = value; }
        }
        public Image BackgrndDefault
        {
            get { return backgrndDefault; }
            set { backgrndDefault = value; }
        }

        public bool IsBGMContinue
        {
            set { isBGMContinue = value; }
        }

        public List<PicBox> getPlayerControlPicboxes()
        {
            return playerControlPicBox;
        }
        public List<PicBox> GetStaticPicBoxes()
        {
            return staticPicBoxes;
        }
        public List<PicBox> GetIndependentPicBoxes()
        {
            return independentPicBoxes;
        }


        #region BGM관련 메소드

        /// <summary>
        /// BGM볼륨 설정
        /// </summary>
        /// <param name="_v"></param>
        public void SetBGMVolum(float _v)
        {
            if (bgm == null) return;
            bgm.Volume = (_v >= 1 ? 1 : (_v <= 0 ? 0 : _v));
        }

        /// <summary>
        /// 현재 bgm과 _m이 같다면 true, 같은 bgm이 아니라면 음악 정지
        /// </summary>
        /// <param name="_m">목적 씬의 BGM</param>
        /// <returns></returns>
        public bool StopBGM(MediaPlayer _m)
        {
            if (bgm == null)
            {
                return false;
            }
            else if (!bgm.Equals(_m))
            {
                bgm.Stop();
                return false;
            }
            return true;
        }

        #endregion
        
        #region pictureBox 스택 관련 메소드

        /// <summary>
        /// _stackValue값으로 스택 추가
        /// </summary>
        /// <param name="_stackValue">추가할 스택 변수</param>
        public void addStack(int _stackValue) {
            curStateStack.Add(_stackValue);
        }

        /// <summary>
        /// 0으로 스택 추가
        /// </summary>
        public void addCurState()
        {
            if (curStateStack.Count < playerControlPicBox.Count)
            {
                curStateStack.Add(0);

                PicBox _p = playerControlPicBox[curStateStack.Count - 1];

                _p.pictureBox.Image = _p.images[0];//디폴트 이미지
                _p.pictureBox.Visible = true;
            }
        }

        /// <summary>
        /// 스택의 가장 끝단 요소 반환(가장 최근 input)
        /// </summary>
        /// <returns></returns>
        public int getCurState()
        {
            return curStateStack[curStateStack.Count - 1];
        }

        /// <summary>
        /// 원하는 스택의 인덱스 번호 (0부터)
        /// </summary>
        /// <param name="_i">Index</param>
        /// <returns></returns>
        public int getCurState(int _i)
        {
            if (_i + 1 > curStateStack.Count)
            {
                throw new Exception("controlable 스택 인덱스 지정 오류");
                //return -99;
            }
            else
            {
                return curStateStack[_i];
            }
        }

        /// <summary>
        /// 스택의 끝단 요소 설정
        /// </summary>
        /// <param name="v">수정할 값</param>
        public void setCurState(int v)
        {
            curStateStack[curStateStack.Count - 1] = v;
        }

        /// <summary>
        /// 스택의 요소 설정
        /// </summary>
        /// <param name="index">인덱스( 0부터 )</param>
        /// <param name="v">수정할 값</param>
        public void setCurState(int index, int v)
        {
            if(index < curStateStack.Count) curStateStack[index] = v;
        }

        /// <summary>
        /// 스택 끝단 제거 및 끝단 pictureBox 안보이게 수정(visible = false;), 스택의 크기는 1보다 작아질 수 없음.
        /// </summary>
        public void removeState()
        {
            if (curStateStack.Count > 1)
            {
                curStateStack.RemoveAt(curStateStack.Count - 1);
                playerControlPicBox[curStateStack.Count].pictureBox.Visible = false;
            }
        }

        /// <summary>
        /// 스택 크기 반환
        /// </summary>
        /// <returns></returns>
        public int getCurStackSize()
        {
            return curStateStack.Count;
        }

        #endregion
        
        #region 키 입력 처리 영역

        public void AddKeyInputEvent(Keys _inputKey, Action _action) {
            keyInputDic.Add(_inputKey, _action);
        }

        public void KeyInput(Keys _inputKey) {
            if (keyInputDic.ContainsKey(_inputKey))  keyInputDic[_inputKey]();
        }

        #endregion

        #region 오버라이드용 메소드

        public virtual void SceneStart()
        {
            if (bgm != null && !isBGMContinue)
            {
                bgm.Stop();
                bgm.Play();
            }
        }

        virtual public void StageNodeSwitcher()
        {

        }

        virtual public void init() {

        }

        #endregion

        #region 텍스트 관련 영역


        /// <summary>
        /// 더미 텍스트박스 세팅
        /// </summary>
        public void textboxSetting() {

            ITextPrinter = new TextPrinter(null, null, bgm, SceneChanger, IsKeywordText, keywordDefine1, keywordDefine2);

        }

        /// <summary>
        /// 텍스트 박스 초기 세팅
        /// </summary>
        /// <param name="_p">label의 부모가 될 PicBox 변수</param>
        /// <param name="_l">연결시킬 TextLabel 변수</param>
        public void textboxSetting(PicBox _p, Label _l)
        {
            //textPicBox = _p;
            _p.pictureBox.Image = _p.images[1];
            _l.Parent = _p.pictureBox;
            //textPictureboxLabel = _l;

            ITextPrinter = new TextPrinter(_l, _p, bgm, SceneChanger, IsKeywordText, keywordDefine1, keywordDefine2);

        }


        /// <summary>
        /// 대사 리스트에 텍스트 추가
        /// </summary>
        /// <param name="_s">추가할 문자열</param>
        public void addText(string _s)
        {
            ITextPrinter.addText(_s);
        }

        /// <summary>
        /// 텍스트 출력
        /// </summary>
        public void TextPrinter()
        {

            ITextPrinter.PrintText();

        }


        /// <summary>
        /// 출력할 스트링 디렉토리 인덱스, 즉시출력 설정,
        /// 텍스트가 이미 전부 출력되어 있었으면 true 반환
        /// 다음으로 넘어가지는 않음
        /// </summary>
        /// <param name="_index">인덱스</param>
        /// <param name="isImmidate">즉시 완성 여부</param>
        /// <returns>텍스트가 이미 전부 출력되어 있었으면 true</returns>
        public bool PrintTextLineByIndex(int _index, bool _isImmidate = false, bool _isClear = true)
        {

            return ITextPrinter.PrintTextLineByIndex(_index, _isImmidate, _isClear);
        }
        
        /// <summary>
        /// zKeyInput 이벤트가 트리거 되었을 때 호출될 텍스트 출력 메소드
        /// </summary>
        protected void TextBoxzKeyInput() {

            ITextPrinter.TextBoxzKeyInput(init);
        }

        /// <summary>
        /// 텍스트박스 투명화 세팅
        /// </summary>
        /// <param name="_isVisible">투명화 변수</param>
        protected void TextboxVisible(bool _isVisible) {

            ITextPrinter.TextboxVisible(_isVisible);

        }

        /// <summary>
        /// 텍스트박스 초기화
        /// </summary>
        protected void InitTextBox()
        {
            ITextPrinter.InitTextBox();

        }

        /// <summary>
        /// 명령어단어 설정 및 행동 분기
        /// </summary>
        /// <returns></returns>
        protected bool IsKeywordText() {

            return ITextPrinter.IsKeywordText();

        }


        #region 명령어 텍스트별 행동 정의(override 가능)

        protected virtual void keywordDefine1() {

        }

        protected virtual void keywordDefine2()
        {
        }

        #endregion

        #endregion

    }

    class Stages : ScenePictureClass {

        protected StageMode stageMode;
        protected double cntex = 0;
        protected int cnt = 0;
        protected double bpm;
        protected int score = 0;
        private float defaultBGMVolume;
        private int defaultBGMSyncOffset;

        public Stages(ISceneChange _i, double _bpm, float _bgmVolume, int _tutorialBgmSyncOffset) : base(_i) {
            bpm = _bpm;
            defaultBGMVolume = _bgmVolume;
            defaultBGMSyncOffset = _tutorialBgmSyncOffset;
        }

        /// <summary>
        /// 씬 입장시 호출할 메소드
        /// </summary>
        public override void SceneStart()
        {
            if (stageMode == StageMode.TUTORIAL)
            {
                stageMode = StageMode.AUTO;//키 입력 방지

                TextboxVisible(true);

                SetBGMVolum(0.0f);
                bgm.Position = new TimeSpan(0, 0, 0, 0, defaultBGMSyncOffset);
                Timers.StartTimer(TimerType.TEXT_TIMER);
                return;
            }
            else
            {

                TextboxVisible(false);
                Timers.StartTimer(TimerType.STAGE_TIC_TIMER);
                SetBGMVolum(SoundPlayer.getBGMVolume());
                base.SceneStart();
            }


        }

        /// <summary>
        /// 튜토리얼 구간별 모든 노트 성공 확인
        /// </summary>
        /// <param name="_currectScore">목표 성공 점수</param>
        /// <param name="_combackCount">실패시 복귀할 배열의 크기 카운트</param>
        /// <returns></returns>
        protected bool TutorialSectionCheck(int _currectScore, int _combackCount)
        {

            bool isCurrect;

            if (score == _currectScore)
            {
                ITextPrinter.keywordRepeatJump();
                //keywordRepeatJump();
                isCurrect = true;
            }
            else
            {
                cnt -= _combackCount;
                cntex = cntex - (bpm * _combackCount);
                bgm.Position = new TimeSpan(0, 0, 0, 0, (int)(bgm.Position.TotalMilliseconds - bpm * _combackCount));
                isCurrect = false;
            }

            score = 0;
            TextboxVisible(true);
            bgm.Pause();
            return isCurrect;

        }

        /// <summary>
        /// 점수 추가( 0점 아래로 내려가지 않음 ) 
        /// </summary>
        /// <param name="value">추가할 점수</param>
        /// <returns></returns>
        protected int addScore(int value)
        {
            score = score + value >= 0 ? score + value : 0;

            return score;
        }

        /// <summary>
        /// 현재 점수 반환
        /// </summary>
        /// <returns></returns>
        public int getScore() {
            return score;
        }

    }

    partial class Stage1 : Stages
    {

        private PictureBox blinkEffectPicturebox;
        private Image cameraFlashBlinkImg;

        // private const double bpm = 58.59375;//32
        //117.1875 : 16
        //234.375  : 8
        //468.75(원래 bpm) : 4박자

        private bool isCurrect = false;

        private PicBox _p;

        public Stage1(ISceneChange _IScene) : base(_IScene, 58.59375, 0.5f, 0) {//0.1f
            AddKeyInputEvent(Keys.Z, zKeyInput);
            AddKeyInputEvent(Keys.X, xKeyInput);
            AddKeyInputEvent(Keys.Space, spaceKeyInput);
        }

        private ulong[] stage1_note_selector;

        /// <summary>
        /// 스테이지 모드 설정
        /// </summary>
        /// <param name="_s">모드 타입</param>
        public void setStageMode(StageMode _s)
        {

            if (cnt != 0) {
                cnt = 0;
                cntex = 0;
            }

            stageMode = _s;

            switch (stageMode)
            {
                case StageMode.NOMAL:
                    stage1_note_selector = test_stage1_note_1024_hard;
                    break;

                case StageMode.AUTO:
                    stage1_note_selector = test_stage1_note_1024_hard_auto;
                    break;

                case StageMode.TUTORIAL:
                    stage1_note_selector = test_stage1_tutorial;
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 스테이지 초기화
        /// </summary>
        override public void init()
        {
            _p = staticPicBoxes[0];
            _p.pictureBox.Image = _p.images[0];
            cnt = 0;
            cntex = 0;
            score = 0;           

            Timers.StopTimer(TimerType.STAGE_TIC_TIMER);
            Timers.StopTimer(TimerType.TEXT_TIMER);


            //todo:??
            /*textIndexer = 0;
            textListCounter = 0;
            isFin = false;*/

            TextboxVisible(false);
            InitTextBox();

            blinkEffectPicturebox = independentPicBoxes[0].pictureBox;
            blinkEffectPicturebox.Image = independentPicBoxes[0].images[0];
            cameraFlashBlinkImg = independentPicBoxes[0].images[1];
        }
        
        #region 키 입력 이벤트

        public void zKeyInput()
        {
            TextBoxzKeyInput();
        }

        public void xKeyInput() {
            SceneChanger.ChangeScene(Scene.MAIN_MENU);


            init();
        }

        public void spaceKeyInput() {

            if (stageMode == StageMode.AUTO || !Timers.isTimerEnabled(TimerType.STAGE_TIC_TIMER)) return;

            if (isCurrect)
            {
                isCurrect = false;
                SoundPlayer.PlayFX(MediaType.CAMERA);//0.2
                SceneChanger.Blink(blinkEffectPicturebox, cameraFlashBlinkImg, 70);
                addScore(1);

            }
            else
            {
                SoundPlayer.PlayFX(MediaType.MISTAKE);//0.8
                addScore(-1);
            }

        }

        #endregion

        /// <summary>
        /// 노트배열 분류 및 처리
        /// </summary>
        public override void StageNodeSwitcher()
        {
            if (bgm.Position.TotalMilliseconds >= cntex + 131)
            {
                testNodeSwitcher(cnt, stage1_note_selector);
                cnt++;
                cntex += bpm;
            }

        }

    }

    partial class Stage2 : Stages
    {

        public Stage2(ISceneChange _IScene) : base(_IScene, 64.6551724137931, 0.5f, 3324 + 3) {//1f
            AddKeyInputEvent(Keys.Z, zKeyInput);
            AddKeyInputEvent(Keys.X, xKeyInput);
            AddKeyInputEvent(Keys.Left, leftKeyInput);
            AddKeyInputEvent(Keys.Right, rightKeyInput);
        }


        private PictureBox r1, r2;
        private Image r1BlinkImg, r2BlinkImg;

        private PictureBox characterPicturebox = null;
        private List<Image> characterImgList = null;

        private int sectionPoint = 0;
        private int sectionCmpPoint = 0;
        private int sectArrayCnt = 0;
        
        //private const double bpm = 64.6551724137931;//32
        //129.3103448275862;//: 16
        //258.6206896551724;  //: 8
        //        517.2413793103448;//(원래 bpm) : 4박자
        //560.541316579896165;


        private bool isCurrectLeft = false;
        private bool isCurrectRight = false;

        private ulong[] stage2_noteArray;
        private int[] stage2_sectionPointArray;

        /// <summary>
        /// 구간점수 계산
        /// </summary>
        /// <param name="_i">추가할 점수</param>
        private void addSectionScore(int _i)
        {
            sectionPoint += _i;
        }

        /// <summary>
        /// 스테이지 모드 설정
        /// </summary>
        /// <param name="_s">모드 종류</param>
        public void setStageMode(StageMode _s)
        {

            if (cnt != 0)
            {
                cnt = 0;
                cntex = 0;
            }

            stageMode = _s;

            switch (stageMode)
            {
                case StageMode.TUTORIAL:
                    stage2_noteArray = test_stage2_tutorial;
                    stage2_sectionPointArray = stage2_tutorial_sectionPointArray;
                    break;

                case StageMode.NOMAL:
                    stage2_noteArray = test_stage2_note_2016;
                    stage2_sectionPointArray = stage2_nomalMode_sectionPointArray;
                    stageMode = StageMode.AUTO;//초반 입력 무시 상태 대용
                    break;

                case StageMode.AUTO:
                    //셀렉터                  바꿀 대상 배열
                    stage2_noteArray = test_stage2_note_2016_auto;
                    stage2_sectionPointArray = stage2_nomalMode_sectionPointArray;
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 스테이지 초기화
        /// </summary>
        override public void init()
        {
            cnt = 0;
            cntex = 0;
            score = 0;
            sectionPoint = 0;
            sectionCmpPoint = 0;
            sectArrayCnt = 0;
            isCurrectLeft = false;
            isCurrectRight = false;

            characterPicturebox = staticPicBoxes[0].pictureBox;
            characterPicturebox.Image = staticPicBoxes[0].images[0];
            characterImgList = staticPicBoxes[0].images;

            r1 = independentPicBoxes[0].pictureBox;
            r2 = independentPicBoxes[1].pictureBox;
            r1.Image = independentPicBoxes[0].images[0];
            r2.Image = independentPicBoxes[1].images[0];
            r1BlinkImg = independentPicBoxes[0].images[1];
            r2BlinkImg = independentPicBoxes[1].images[1];

            TextboxVisible(false);
            InitTextBox();

            Timers.StopTimer(TimerType.STAGE_TIC_TIMER);
            Timers.StopTimer(TimerType.TEXT_TIMER);
        }

        #region 키 입력 이벤트

        public void zKeyInput() {
            TextBoxzKeyInput();
        }

        public void xKeyInput() {

            SceneChanger.ChangeScene(Scene.MAIN_MENU);

            init();
        }

        public void leftKeyInput() {
            if (stageMode == StageMode.AUTO) return;
            if (isCurrectLeft)
            {
                isCurrectLeft = false;
                SoundPlayer.PlayFX(MediaType.RAINDROP_1);//0.8
                if (characterPicturebox.Image.Equals(characterImgList[4]))
                {
                    characterPicturebox.Image = characterImgList[5];
                }
                else if (characterPicturebox.Image.Equals(characterImgList[5]))
                {
                    characterPicturebox.Image = characterImgList[6];
                }
                else {
                    characterPicturebox.Image = characterImgList[4];
                }
                addSectionScore(1);
            }
            else
            {
                SoundPlayer.PlayFX(MediaType.MISTAKE);//0.8
                characterPicturebox.Image = characterImgList[7];
                addSectionScore(-1);
            }
        }

        public void rightKeyInput() {
            if (stageMode == StageMode.AUTO) return;
            if (isCurrectRight)
            {
                isCurrectRight = false;
                SoundPlayer.PlayFX(MediaType.RAINDROP_2);//1
                if (characterPicturebox.Image.Equals(characterImgList[8]))
                {
                    characterPicturebox.Image = characterImgList[9];
                }
                else {
                    characterPicturebox.Image = characterImgList[8];
                }
                addSectionScore(1);
            }
            else
            {
                SoundPlayer.PlayFX(MediaType.MISTAKE);//0.8
                characterPicturebox.Image = characterImgList[10];
                addSectionScore(-1);
            }
        }

        #endregion

        /// <summary>
        /// 노트배열 분류 및 처리
        /// </summary>
        public override void StageNodeSwitcher()
        {                                                 //딜레이 오프셋 3796 + 25 - 517 + 20
            if (bgm.Position.TotalMilliseconds >= cntex + 3324 + 3 + 3)//ttat
            {

                testNodeSwitcher(cnt, stage2_noteArray, stage2_sectionPointArray);
                cnt++;
                cntex += bpm;
            }

        }

    }

    partial class Stage3 : Stages
    {

        private bool curLeft, curRight, curDown = false;
        private Stage3Helper subForm;

        //private const double bpm = //487.8048780487805;
        //243.90243902439025;
        // 121.951219512195125;

        private ulong[] note_selector;

        //                                                                                 대충 bpm * 4 - 45 + 25
        public Stage3(ISceneChange _i, Stage3Helper _subForm) : base(_i, 121.951219512195125, 0.5f, 487 - 45 + 25)//0.1f
        {
            AddKeyInputEvent(Keys.X, xKeyInput);
            AddKeyInputEvent(Keys.Z, zKeyInput);
            AddKeyInputEvent(Keys.Left, leftKeyInput);
            AddKeyInputEvent(Keys.Right, rightKeyInput);
            AddKeyInputEvent(Keys.Down, downKeyInput);

            subForm = _subForm;
        }

        private PictureBox arrowPictureBox = null;
        private List<Image> arrowImages = null;

        private PictureBox commandPictureBox = null;
        private List<Image> commandImages = null;

        private PictureBox smokePictureBox = null;
        private List<Image> smokeImages = null;

        /// <summary>
        /// 스테이지 초기화
        /// </summary>
        public override void init()
        {
            arrowPictureBox = staticPicBoxes[0].pictureBox;
            arrowPictureBox.Image = staticPicBoxes[0].images[0];
            arrowImages = staticPicBoxes[0].images;

            commandPictureBox = staticPicBoxes[1].pictureBox;
            commandPictureBox.Image = staticPicBoxes[1].images[0];
            commandImages = staticPicBoxes[1].images;

            smokePictureBox = staticPicBoxes[2].pictureBox;
            smokePictureBox.Image = staticPicBoxes[2].images[0];
            smokeImages = staticPicBoxes[2].images;

            score = 0;

            cnt = 0;
            cntex = 0;

            curLeft = false;
            curRight = false;
            curDown = false;

            Timers.StopTimer(TimerType.STAGE_TIC_TIMER);//all stop으로 대체 ㄱㄴ?
            Timers.StopTimer(TimerType.BLINK_TIMER);
            Timers.StopTimer(TimerType.TEXT_TIMER);
            Timers.StopTimer(TimerType.SCREEN_SHAKE_TIMER);

            TextboxVisible(false);
            InitTextBox();

            SceneChanger.moveUp();

            //
            subForm.resetPos();
            Timers.StopTimer(TimerType.STAGE3_HELPER_TIMER);
        }

        #region 키 입력 이벤트

        public void xKeyInput()
        {

            SceneChanger.ChangeScene(Scene.MAIN_MENU);


            init();
            subForm.Visible = false;
        }
        public void zKeyInput()
        {
            TextBoxzKeyInput();
        }
        public void leftKeyInput()
        {
            if (stageMode == StageMode.AUTO) return;
            if (curLeft)
            {
                curLeft = false;
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);//1
                arrowPictureBox.Image = arrowImages[1];
                score++;
            }
            else {
                score--;
                SoundPlayer.PlayFX(MediaType.MISTAKE);
                if (score < 0) score = 0;
            }
        }
        public void rightKeyInput()
        {
            if (stageMode == StageMode.AUTO) return;
            if (curRight)
            {
                curRight = false;
                SoundPlayer.PlayFX(MediaType.CUR_MOVE);//1
                arrowPictureBox.Image = arrowImages[2];
                score++;
            }
            else {
                score--;
                SoundPlayer.PlayFX(MediaType.MISTAKE);
                if (score < 0) score = 0;
            }

        }
        public void downKeyInput()
        {
            if (stageMode == StageMode.AUTO) return;
            if (curDown)
            {
                curDown = false;
                Timers.StopTimer(TimerType.SCREEN_SHAKE_TIMER);
                Timers.StartTimer(TimerType.STAGE3_HELPER_TIMER);
                SoundPlayer.PlayFX(MediaType.MACHIENE);//0.3
                SceneChanger.moveDown();
                arrowPictureBox.Image = arrowImages[3];
                score++;
            }
            else {
                score--;
                SoundPlayer.PlayFX(MediaType.MISTAKE);
                if (score < 0) score = 0;
            }

        }

        #endregion

        /// <summary>
        /// 스테이지 모드 설정
        /// </summary>
        /// <param name="_s">모드 종류</param>
        public void setStageMode(StageMode _s) {

            if (cnt != 0)
            {
                cnt = 0;
                cntex = 0;
            }

            stageMode = _s;

            switch (stageMode) {
                case StageMode.TUTORIAL:
                    note_selector = test_note_tutorial;
                    break;

                case StageMode.NOMAL:
                    note_selector = test_note_100_hard;
                    break;

                case StageMode.AUTO:
                    note_selector = test_note_100_auto;
                    break;
            }

        }

        /// <summary>
        /// 노트배열 분류 및 처리
        /// </summary>
        public override void StageNodeSwitcher()
        {
            if (bgm.Position.TotalMilliseconds >= cntex + bpm * 4 - 45 + 25 - 12)//ttat
            {
                testNodeSwitcher(cnt, note_selector);
                cnt++;
                cntex += bpm;
            }
        }

    }

    partial class Stage4 : Stages {

        private Stage1 stage_1;
        private Stage2 stage_2;
        private Stage3 stage_3;
        private Stage3Helper stage3Subform;
        private Scene currentFocusScene;

        private ulong[] noteArray = null;

        public Stage4(ISceneChange _i, Stage1 _stage1, Stage2 _stage2, Stage3 _stage3, Stage3Helper _stage3Subform) : base(_i, 69.444444444444444444444444444445, 0.5f, 0) {//0.08f

            stage_1 = _stage1;
            stage_2 = _stage2;
            stage_3 = _stage3;
            stage3Subform = _stage3Subform;

            AddKeyInputEvent(Keys.X, xKeyInput);
            AddKeyInputEvent(Keys.Left, leftKeyInput);
            AddKeyInputEvent(Keys.Right, rightKeyInput);
            AddKeyInputEvent(Keys.Down, downKeyInput);
            AddKeyInputEvent(Keys.Space, spaceKeyInput);
        }

        /// <summary>
        /// 스테이지 모드 설정
        /// </summary>
        /// <param name="_mode">모드 종류</param>
        public void SetStageMode(StageMode _mode) { 
            
            currentFocusScene = Scene.STAGE_1;

            if (cnt != 0)
            {
                cnt = 0;
                cntex = 0;
            }

            if (_mode == StageMode.AUTO)
            {
                noteArray = test_Auto;
            }
            else if (_mode == StageMode.NOMAL) {
                noteArray = test_Nomal;
            }

            stageMode = _mode;
            stage_1.setStageMode(_mode);
            stage_2.setStageMode(_mode);
            stage_3.setStageMode(_mode);
            
        }

        /// <summary>
        /// 스테이지 초기화
        /// </summary>
        public override void init()
        {
            currentFocusScene = Scene.NONE;
            cnt = 0;
            cntex = 0;
            score = 0;
        }

        /// <summary>
        /// [리믹스 스테이지] 스타트 장면용 스테이지 초기화
        /// </summary>
        public override void SceneStart()
        {
            foreach (PicBox _p in stage_1.GetStaticPicBoxes())
            {
                _p.pictureBox.Visible = true;
            }
            foreach (PicBox _p in stage_1.GetIndependentPicBoxes())
            {
                _p.pictureBox.Visible = true;
            }
            base.SceneStart();
        }

        #region 키 입력 이벤트
        private void xKeyInput()
        {
            if (currentFocusScene == Scene.STAGE_1)
            {
                SceneChanger.ChangeScene(stage_1, Scene.MAIN_MENU);
            }
            else if (currentFocusScene == Scene.STAGE_2)
            {
                SceneChanger.ChangeScene(stage_2, Scene.MAIN_MENU);
            }
            else if (currentFocusScene == Scene.STAGE_3)
            {
                stage3Subform.Visible = false;
                SceneChanger.ChangeScene(stage_3, Scene.MAIN_MENU);
            }
            else
            {
                
                SceneChanger.ChangeScene(Scene.MAIN_MENU);
            }
            //ttat
            stage_1.init();
            stage_2.init();
            stage_3.init();
            init();
        }

        private void leftKeyInput()
        {
            if (currentFocusScene == Scene.STAGE_2)
            {
                stage_2.leftKeyInput();
            }
            else if (currentFocusScene == Scene.STAGE_3)
            {
                stage_3.leftKeyInput();
            }
        }

        private void rightKeyInput()
        {
            if (currentFocusScene == Scene.STAGE_2)
            {
                stage_2.rightKeyInput();
            }
            else if (currentFocusScene == Scene.STAGE_3)
            {
                stage_3.rightKeyInput();
            }
        }

        private void downKeyInput()
        {
            if (currentFocusScene == Scene.STAGE_3)
            {
                stage_3.downKeyInput();
            }
        }

        private void spaceKeyInput()
        {
            if (currentFocusScene == Scene.STAGE_1)
            {
                stage_1.spaceKeyInput();
            }
        }

        #endregion

        /// <summary>
        /// 노트배열 분류 및 처리
        /// </summary>
        public override void StageNodeSwitcher()
        {
            if (bgm.Position.TotalMilliseconds >= cntex + 150 - 5 - 5 - 10)//ttat
            {

                testNodeSwitcher(cnt);

                cnt++;
                cntex += bpm;
            }
        }


    }

    public class ResultScene : ScenePictureClass{

        private enum ResultState { TEXT_PRINT, IMAGE_PRINT}
        private ResultState currentResultState;

        private PicBox resultImagePicBox;
        private Image clearLevelImage;
        private Image backgrndImgBuffer;

        private int clearImgIndex;

        public ResultScene(ISceneChange _i) : base(_i) {
            keyInputDic.Add(Keys.Z, zKeyInput);
        }

        /// <summary>
        /// 장면 초기화
        /// </summary>
        public override void init()
        {
            resultImagePicBox = staticPicBoxes[0];
            TextboxVisible(false);
            InitTextBox();
        }

        /// <summary>
        /// 장면 시작
        /// </summary>
        public override void SceneStart()
        {
            TextboxVisible(true);
        }

        private int textCountOffset = 0;

        /// <summary>
        /// zKey 입력 이벤트 메소드
        /// </summary>
        private void zKeyInput() {
            if (currentResultState == ResultState.IMAGE_PRINT) {
                SceneChanger.ChangeScene(Scene.MAIN_MENU);
                BackgrndBuffer = BackgrndDefault = backgrndImgBuffer;
                init();
            } 
            PrintTextLineByIndex(textCountOffset, true, false);
            textCountOffset++;
        }

        #region 텍스트 명령어 오버라이드 재정의 영역

        protected override void keywordDefine1()
        {
            if (clearLevelImage != null)
            {
                resultImagePicBox.pictureBox.Image = clearLevelImage;
                base.keywordDefine1();
            }
            else {
                throw new Exception("이미지 미지정 오류");
            }
        }

        protected override void keywordDefine2() {
            backgrndImgBuffer = BackgrndBuffer;
            BackgrndBuffer = BackgrndDefault = resultImagePicBox.images[clearImgIndex];
            currentResultState = ResultState.IMAGE_PRINT;
            SceneChanger.ChangeScene(this);
            resultImagePicBox.pictureBox.Image = resultImagePicBox.images[0];
            base.keywordDefine2();

        }

        #endregion


        /// <summary>
        /// 장면 입장 전 호출, 결산 등급과 스테이지 종류에 따라 세팅(전처리)
        /// </summary>
        /// <param name="_fromScene">접근해온 스테이지 종류</param>
        /// <param name="_clearLevel">클리어 수준(성공, 보통, 실패)</param>
        public void setResultScene(Scene _fromScene, int _clearLevel) {

            currentResultState = ResultState.TEXT_PRINT;
            //텍스트 수정
            switch (_fromScene) {
                case Scene.STAGE_1:
                    textCountOffset = _clearLevel * 6;
                    clearImgIndex = 4 + _clearLevel;
                    break;
                case Scene.STAGE_2:
                    textCountOffset = _clearLevel * 6 + 18;
                    clearImgIndex = 4 + 3 + _clearLevel;
                    break;
                case Scene.STAGE_3:
                    textCountOffset = _clearLevel * 6 + 36;
                    clearImgIndex = 4 + 3 + 3 + _clearLevel;

                    break;
                case Scene.STAGE_4:
                    textCountOffset = _clearLevel * 6 + 54;
                    clearImgIndex = 4 + 3 + 3 + 3 + _clearLevel;
                    break;

                default:
                    break;
            }

            //클리어 등급 이미지 수정
            if (_clearLevel == 0)
            {
                clearLevelImage = resultImagePicBox.images[1];
            }
            else if (_clearLevel == 1)
            {
                clearLevelImage = resultImagePicBox.images[2];
            }
            else if (_clearLevel == 2)
            {
                clearLevelImage = resultImagePicBox.images[3];
            }
            else {
                clearLevelImage = resultImagePicBox.images[0];
            }


        }
        
    }
}
