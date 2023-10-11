using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace WindowsFormsApp1
{
    public interface ITextPrinter {

        /// <summary>
        /// bgm설정
        /// </summary>
        /// <param name="_bgm"></param>
        void setBGM(MediaPlayer _bgm);
        /// <summary>
        /// 대사 리스트에 텍스트 추가
        /// </summary>
        /// <param name="_s">추가할 문자열</param>
        void addText(string _s);
        /// <summary>
        /// 임시 변수에 다음 출력할 텍스트 로딩
        /// </summary>
        /// <param name="_counter">리스트에서 읽어올 인덱스</param>
        void setTmpStr(int _counter);
        /// <summary>
        /// 텍스트 출력
        /// </summary>
        void PrintText();
        /// <summary>
        /// 출력할 스트링 디렉토리 인덱스, 즉시출력 설정,
        /// 텍스트가 이미 전부 출력되어 있었으면 true 반환 
        /// 다음으로 넘어가지는 않음
        /// </summary>
        /// <param name="_index">인덱스</param>
        /// <param name="isImmidate">즉시 완성 여부</param>
        /// <returns>텍스트가 이미 전부 출력되어 있었으면 true</returns>
        bool PrintTextLineByIndex(int _index, bool _isImmidate = false, bool _isClear = true);
        /// <summary>
        /// zKeyInput 이벤트가 트리거 되었을 때 호출될 텍스트 출력 메소드
        /// </summary>
        void TextBoxzKeyInput(Action _init);
        /// <summary>
        /// 텍스트박스 투명화 세팅
        /// </summary>
        /// <param name="_isVisible">투명화 변수</param>
        void TextboxVisible(bool _isVisible);
        /// <summary>
        /// 텍스트 비움
        /// </summary>
        void ClearTextBox();
        /// <summary>
        /// 텍스트박스 초기화
        /// </summary>
        void InitTextBox();
        /// <summary>
        /// 명령어단어 설정 및 행동 분기
        /// </summary>
        /// <returns></returns>
        bool IsKeywordText();
        void KeywordPass();
        void KeywordRepeat();
        void keywordRepeatJump();

    }

    class TextPrinter : ITextPrinter
    {
        private int textListCounter = 0;
        private int textIndexer = 0;
        private bool isFin = true;
        private string _tmpStr;

        private Label textPictureboxLabel;
        private PicBox textPicBox;
        private MediaPlayer bgm;
        private ISceneChange SceneChanger;
        private Func<bool> isKeywordText;
        private Action define1;
        private Action define2;

        private int jumpCounter = 1;

        private List<string> textList = new List<string>();

        public TextPrinter(Label _l, PicBox _pb, MediaPlayer _bgm, ISceneChange _isch, Func<bool> _func, Action _def1, Action _def2) {

            textPictureboxLabel = _l;
            textPicBox = _pb;
            bgm = _bgm;
            SceneChanger = _isch;
            isKeywordText = _func;
            define1 = _def1;
            define2 = _def2;
        }

        /// <summary>
        /// bgm설정
        /// </summary>
        /// <param name="_bgm"></param>
        public void setBGM(MediaPlayer _bgm)
        {
            bgm = _bgm;
        }

        /// <summary>
        /// 대사 리스트에 텍스트 추가
        /// </summary>
        /// <param name="_s">추가할 문자열</param>
        public void addText(string _s)
        {
            textList.Add(_s);
        }

        /// <summary>
        /// 임시 변수에 다음 출력할 텍스트 로딩
        /// </summary>
        /// <param name="_counter">리스트에서 읽어올 인덱스</param>
        public void setTmpStr(int _counter)
        {

            if (_counter < textList.Count)
            {
                _tmpStr = textList[_counter];
            }
            else
            {
                throw new Exception("텍스트 출력 인덱서 오류");
            }

        }

        /// <summary>
        /// 텍스트 출력
        /// </summary>
        public void PrintText()
        {

            //다음 출력할 텍스트 설정
            setTmpStr(textListCounter);

            //명령어 텍스트인지 확인
            if (isKeywordText()) return;

            if (isFin)
            {
                textPictureboxLabel.Text = _tmpStr;
                Timers.StopTimer(TimerType.TEXT_TIMER);
            }
            else
            {
                if (_tmpStr.Length > textIndexer)
                {
                    textPictureboxLabel.Text += _tmpStr[textIndexer];
                    textIndexer++;
                }
                else
                {
                    isFin = true;
                    Timers.StopTimer(TimerType.TEXT_TIMER);
                }
            }


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
            setTmpStr(_index);
            if (isKeywordText()) return false;

            if (_isImmidate)
            {
                Timers.StopTimer(TimerType.TEXT_TIMER);
                if (!isFin)
                {
                    if (_isClear)
                    {
                        textPictureboxLabel.Text = _tmpStr;
                        isFin = true;
                        return false;
                    }
                    else
                    {//다음에 붙여서 추가 출력
                        textPictureboxLabel.Text = textPictureboxLabel.Text + _tmpStr;
                        return false;
                    }
                }
            }
            else
            {
                InitTextBox();
                textListCounter = _index;
                isFin = false;
                Timers.StartTimer(TimerType.TEXT_TIMER);
            }
            return isFin;
        }

        /// <summary>
        /// zKeyInput 이벤트가 트리거 되었을 때 호출될 텍스트 출력 메소드
        /// </summary>
        public void TextBoxzKeyInput(Action _init)
        {
            if (!Timers.isTimerEnabled(TimerType.STAGE_TIC_TIMER))
            {
                //텍스트를 끝까지 전부 출력 -> 메인메뉴 귀환
                if (textPictureboxLabel.Text.Equals(textList[textList.Count - 1]))
                {
                    _init();
                    SceneChanger.ChangeScene(Scene.MAIN_MENU);
                    return;
                }

                if (!isFin)
                {
                    isFin = true;
                }
                else
                {
                    ClearTextBox();
                    isFin = false;
                    textListCounter++;
                    textIndexer = 0;
                    Timers.StartTimer(TimerType.TEXT_TIMER);
                }
            }
        }

        /// <summary>
        /// 텍스트박스 투명화 세팅
        /// </summary>
        /// <param name="_isVisible">투명화 변수</param>
        public void TextboxVisible(bool _isVisible)
        {
            if (textPicBox == null) return;
            ClearTextBox();
            textPicBox.pictureBox.Image = _isVisible ? textPicBox.images[1] : textPicBox.images[0];
        }

        /// <summary>
        /// 텍스트 비움
        /// </summary>
        public void ClearTextBox()
        {
            textPictureboxLabel.Text = "";
        }

        /// <summary>
        /// 텍스트박스 초기화
        /// </summary>
        public void InitTextBox()
        {
            ClearTextBox();
            textListCounter = 0;
            textIndexer = 0;
            isFin = false;
        }


        

        /// <summary>
        /// 명령어단어 설정 및 행동 분기
        /// </summary>
        /// <returns></returns>
        public bool IsKeywordText()//asdfasdf
        {

            if (_tmpStr.Equals("Pass"))
            {
                KeywordPass();
            }
            else if (_tmpStr.Equals("Repeat"))
            {
                KeywordRepeat();
            }
            else if (_tmpStr.Equals("Define1"))
            {
                keywordDefine1();
            }
            else if (_tmpStr.Equals("Define2"))
            {
                keywordDefine2();
            }
            else
            {
                return false;
            }

            return true;

        }


        #region 명령어 텍스트별 행동 정의(override 가능)

        public virtual void KeywordPass()
        {
            textListCounter++;
            Timers.StopTimer(TimerType.TEXT_TIMER);
            Timers.StartTimer(TimerType.STAGE_TIC_TIMER);
            bgm.Play();
            TextboxVisible(false);
        }


        public virtual void KeywordRepeat()
        {
            textListCounter -= jumpCounter;
            Timers.StopTimer(TimerType.TEXT_TIMER);
            Timers.StartTimer(TimerType.STAGE_TIC_TIMER);
            bgm.Play();
            TextboxVisible(false);
        }

        public void keywordRepeatJump()
        {
            textListCounter = textListCounter + jumpCounter + 1;
        }

        public virtual void keywordDefine1()
        {
            define1();
            textListCounter++;
        }

        public virtual void keywordDefine2()
        {
            define2();
        }

        #endregion

        
    }
}
