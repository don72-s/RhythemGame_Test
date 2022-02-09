using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial interface ISceneChange {
        void ChangeScene(Scene _destScene, bool isSaveBuffer = false, int _interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass _fromScene, Scene nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass nextScene, bool isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void ChangeScene(ScenePictureClass _fromScene, ScenePictureClass nextScene, bool _isSaveBuffer = false, int interval = 35, float _alphaOffset = 0.1f, bool isBGMContinue = false);
        void Blink(PictureBox _pictureBox, Image _newImage, int _blinkTime);
        void Blink(PictureBox _pictureBox, Image _newImage, Image _rollbackImg, int _blinkTime);

        void moveDown();
        void moveUp();

        void SetResultScene(Scene _fromScene, int _clearLevel);

        }

    }
