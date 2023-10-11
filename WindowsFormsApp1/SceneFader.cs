using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public interface ISceneChange
    {
        void ChangeScene(Scene _destScene, bool isSaveBuffer = false, int _interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass _fromScene, Scene nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass nextScene, bool isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass _fromScene, ScenePictureClass nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void Blink(PictureBox _pictureBox, Image _newImage, int _blinkTime);
        void Blink(PictureBox _pictureBox, Image _newImage, Image _rollbackImg, int _blinkTime);

        void BlinkTimerCallback();
        void FadingTimerCallback();
        void SubFadingTimerCallback();

        void moveDown();
        void moveUp();

        void initResultScene(ResultScene _resultScene);
        void SetResultScene(Scene _fromScene, int _clearLevel);

    }

    class SceneFader : ISceneChange
    {

        private PictureBox blinkPictureBox;//깜빡거리는 이펙트 임시변수
        private Image rollbackImg;

        private Form1 originalForm;
        private PicBox backgroundPicturebox;
        private ResultScene resultScene;
        Dictionary<Scene, ScenePictureClass> SceneDic;

        float alpha = 1f;
        float alphaOffset;

        public SceneFader(Form1 _form, PicBox _backgroundPicturebox, Dictionary<Scene, ScenePictureClass> _SceneDic) { 
        
            originalForm = _form;
            backgroundPicturebox = _backgroundPicturebox;
            SceneDic = _SceneDic;

        }


        #region ISceneChanger 인터페이스 영역


        public void BlinkTimerCallback()
        {
            Timers.StopTimer(TimerType.BLINK_TIMER);
            blinkPictureBox.Image = rollbackImg;
        }


        public void Blink(PictureBox _pictureBox, Image _blinkImg, int _blinkTime)
        {

            blinkPictureBox = _pictureBox;
            rollbackImg = blinkPictureBox.Image;
            blinkPictureBox.Image = _blinkImg;

            Timers.SetInterval(TimerType.BLINK_TIMER, _blinkTime);
            Timers.StartTimer(TimerType.BLINK_TIMER);
        }

        public void Blink(PictureBox _pictureBox, Image _blinkImg, Image _rollbackImg, int _blinkTime)
        {

            blinkPictureBox = _pictureBox;
            rollbackImg = _rollbackImg;
            blinkPictureBox.Image = _blinkImg;

            Timers.SetInterval(TimerType.BLINK_TIMER, _blinkTime);
            Timers.StartTimer(TimerType.BLINK_TIMER);
        }

        public void moveDown()
        {
            originalForm.SetDesktopLocation(originalForm.xPos, originalForm.yPos + 100);
        }

        public void moveUp()
        {
            originalForm.SetDesktopLocation(originalForm.xPos, originalForm.yPos);
        }

        public void SetResultScene(Scene _fromScene, int _clearLevel)
        {
            resultScene.setResultScene(_fromScene, _clearLevel);
        }

        #endregion

        #region 페이딩

        //장면들 임시저장 변수
        private ScenePictureClass currentSceneTmp;
        private ScenePictureClass afterSceneTmp;

        /// <summary>
        /// 현재 장면 상태를 옮기면서 장면 전환
        /// </summary>
        /// <param name="nextScene">옮겨갈 장면의 상태</param>
        /// <param name="_isSaveBuffer">페이딩시 캡쳐한 이미지를 나중에 페이드 인 시에 사용할지.</param>
        /// <param name="interval">페이딩 틱 주기</param>
        /// <param name="_alphaOffset">페이딩 투명도 증감 정도</param>
        public void ChangeScene(Scene nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false)
        {
            if (nextScene == Scene.EXIT)
            {
                originalForm.CloseForm();
                return;
            }
            isSaveBuffer = _isSaveBuffer;
            currentSceneTmp = SceneDic[originalForm.getCurrentScene()];
            afterSceneTmp = SceneDic[nextScene];


            ChangeScene(interval, _alphaOffset, isBGMContinue);

            originalForm.setCurrentScene(nextScene);
            //_3.Text = currentScene.ToString();
        }

        /// <summary>
        /// 현재 장면 상태를 옮기면서 장면 전환
        /// </summary>
        /// <param name="nextScene">옮겨갈 장면의 상태</param>
        /// <param name="_isSaveBuffer">페이딩시 캡쳐한 이미지를 나중에 페이드 인 시에 사용할지.</param>
        /// <param name="interval">페이딩 틱 주기</param>
        /// <param name="_alphaOffset">페이딩 투명도 증감 정도</param>
        public void ChangeScene(ScenePictureClass _fromScene, Scene nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false)
        {
            if (nextScene == Scene.EXIT)
            {
                originalForm.CloseForm();
                return;
            }
            isSaveBuffer = _isSaveBuffer;
            currentSceneTmp = _fromScene;
            afterSceneTmp = SceneDic[nextScene];


            ChangeScene(interval, _alphaOffset, isBGMContinue);

            originalForm.setCurrentScene(nextScene);
            //_3.Text = currentScene.ToString();
        }

        /// <summary>
        /// 현재 장면 상태를 유지하면서 장면만 전환
        /// </summary>
        /// <param name="nextScene">출력 장면으로 변경할 클래스</param>
        /// <param name="_isSaveBuffer">페이딩시 캡쳐한 이미지를 나중에 페이드 인 시에 사용할지.</param>
        /// <param name="interval">페이딩 틱 주기</param>
        /// <param name="_alphaOffset">페이딩 투명도 증감 정도</param>
        public void ChangeScene(ScenePictureClass _fromScene, ScenePictureClass nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false)
        {
            isSaveBuffer = _isSaveBuffer;
            currentSceneTmp = _fromScene;
            afterSceneTmp = nextScene;

            ChangeScene(interval, _alphaOffset, isBGMContinue);
        }

        /// <summary>
        /// 현재 장면 상태를 유지하면서 장면만 전환
        /// </summary>
        /// <param name="nextScene">출력 장면으로 변경할 클래스</param>
        /// <param name="_isSaveBuffer">페이딩시 캡쳐한 이미지를 나중에 페이드 인 시에 사용할지.</param>
        /// <param name="interval">페이딩 틱 주기</param>
        /// <param name="_alphaOffset">페이딩 투명도 증감 정도</param>
        public void ChangeScene(ScenePictureClass nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false)
        {
            isSaveBuffer = _isSaveBuffer;
            currentSceneTmp = SceneDic[originalForm.getCurrentScene()];
            afterSceneTmp = nextScene;

            ChangeScene(interval, _alphaOffset, isBGMContinue);
        }


        private Image tmpImg;
        private bool isSaveBuffer;

        private void ChangeScene(int interval, float _alphaOffset, bool isBGMContinue)
        {

            Timers.SetInterval(TimerType.FADING_TIMER, interval);

            originalForm.setInputState(InputState.DENY);

            //새로찍은 이미지
            Bitmap bitmap = new Bitmap(backgroundPicturebox.pictureBox.Width, backgroundPicturebox.pictureBox.Height);
            backgroundPicturebox.pictureBox.DrawToBitmap(bitmap, new Rectangle(0, 0, backgroundPicturebox.pictureBox.Width, backgroundPicturebox.pictureBox.Height));
            backgroundPicturebox.pictureBox.Image = bitmap;


            foreach (PicBox _p in currentSceneTmp.getPlayerControlPicboxes())
            {
                _p.pictureBox.Visible = false;
            }

            foreach (PicBox _p in currentSceneTmp.GetStaticPicBoxes())
            {
                _p.pictureBox.Visible = false;
            }

            foreach (PicBox _p in currentSceneTmp.GetIndependentPicBoxes())
            {
                _p.pictureBox.Visible = false;
            }

            tmpImg = bitmap;

            alphaOffset = -_alphaOffset;


            //다음장면의 bgm이 같은 bgm일 경우 bgm을 끊지 않고 계속 재생함
            afterSceneTmp.IsBGMContinue = isBGMContinue ? true : SceneDic[originalForm.getCurrentScene()].StopBGM(afterSceneTmp.getBGM());

            Timers.StartTimer(TimerType.FADING_TIMER);
        }


        public List<Bitmap> l = new List<Bitmap>();
        public void FadingTimerCallback()
        {
            alpha += alphaOffset;

            l.Add(Fading());

            if (alpha <= 0)//페이드 아웃 종료
            {
                alpha = 0;
                alphaOffset = -alphaOffset;

                if (isSaveBuffer)
                {

                    currentSceneTmp.BackgrndBuffer.Dispose();
                    currentSceneTmp.BackgrndBuffer = tmpImg;

                }
                else
                {
                    tmpImg.Dispose();

                }

                tmpImg = afterSceneTmp.BackgrndBuffer;

                currentSceneTmp = afterSceneTmp;
                //destScene = Scene.NONE;
            }
            else if (alpha >= 1)
            {//페이드 인 종료
                Timers.StopTimer(TimerType.FADING_TIMER);
                Timers.StartTimer(TimerType.FADING_SUB_TIMER);
            }
        }

        private Bitmap Fading()
        {
            Image _img = tmpImg;
            Bitmap bit = new Bitmap(_img.Width, _img.Height);
            Graphics g = Graphics.FromImage(bit);
            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = alpha;
            ImageAttributes imgatt = new ImageAttributes();
            imgatt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(_img, new Rectangle(0, 0, bit.Width, bit.Height), 0, 0, _img.Width, _img.Height, GraphicsUnit.Pixel, imgatt);

            g.Dispose();
            backgroundPicturebox.pictureBox.Image = bit;
            return bit;
        }

        public void SubFadingTimerCallback()
        {
            Timers.StopTimer(TimerType.FADING_SUB_TIMER);
            endFading();
        }

        void endFading()
        {
            List<PicBox> tmpPicBoxes = new List<PicBox>();
            tmpPicBoxes = afterSceneTmp.getPlayerControlPicboxes();

            if (tmpPicBoxes.Count > 0)
            {
                for (int i = 0; i < afterSceneTmp.getCurStackSize(); i++)//스택 크기만큼 조작가능 이미지 활성화
                {
                    tmpPicBoxes[i].pictureBox.Visible = true;
                }
            }

            foreach (PicBox _p in afterSceneTmp.GetStaticPicBoxes())
            {
                _p.pictureBox.Visible = true;
            }

            foreach (PicBox _p in afterSceneTmp.GetIndependentPicBoxes())
            {
                _p.pictureBox.Visible = true;
            }

            afterSceneTmp.SceneStart();

            foreach (Bitmap _b in l)
            {
                _b.Dispose();
            }

            l.Clear();


            backgroundPicturebox.pictureBox.Image.Dispose();
            backgroundPicturebox.pictureBox.Image = afterSceneTmp.BackgrndDefault;
            originalForm.setInputState(InputState.FINE);

        }

        public void initResultScene(ResultScene _resultScene)
        {
            resultScene = _resultScene;
        }

        #endregion

    }
}
