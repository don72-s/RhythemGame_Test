using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Stage3Helper : Form
    {

        Image dump;

        Point zero = new Point(0, 0);
        Form mainForm;
        private int addPointOffset;

        public Stage3Helper(Size _formSize, Form _mainForm)
        {
            InitializeComponent();

            Timers.AddTimer(TimerType.STAGE3_HELPER_TIMER, Stage3HelperShakeTimer);
            Timers.StopTimer(TimerType.STAGE3_HELPER_TIMER);

            _formSize.Height = _formSize.Width / 10;
            MinimumSize = _formSize;
            MaximumSize = _formSize;

            beltPicturebox.MinimumSize = _formSize;
            beltPicturebox.MaximumSize = _formSize;


            _formSize.Height = _formSize.Height / 2;
            _formSize.Width = _formSize.Height / 4 * 5;

            dumpPicturebox.MinimumSize = _formSize;
            dumpPicturebox.MaximumSize = _formSize;

            addPointOffset = Width / 3;
            zero.X = Width;

            mainForm = _mainForm;
        }

        public void passFocus() {
            mainForm.Activate();
        }

        public void setImg(Image _belt, Image _dump) {
            beltPicturebox.Image = _belt;
            dumpPicturebox.Image = _dump;
            dump = _dump;
        }


        public void resetPos() {
            dumpPicturebox.Location = zero;
        }

        public void updateDump() {
            if (dumpPicturebox.Location.X < -addPointOffset) return;
            dumpPicturebox.Location = new Point(dumpPicturebox.Location.X - addPointOffset, dumpPicturebox.Location.Y);
        }
        

        private bool sh = true;
        private int xPos;
        private int shakeCount = 0;
        
        public void setXpos(int _x) {
            xPos = _x;
        }
        private void Stage3HelperShakeTimer_Tick(object sender, EventArgs e)
        {
            if (shakeCount > 7) {
                Timers.StopTimer(TimerType.STAGE3_HELPER_TIMER);
                shakeCount = 0;
                SetDesktopLocation(xPos, DesktopLocation.Y);
                return;
            }

            if (sh)
            {
                sh = !sh;
                SetDesktopLocation(xPos - 5, DesktopLocation.Y);
            }
            else {
                sh = !sh;
                SetDesktopLocation(xPos + 5, DesktopLocation.Y);
            }

            shakeCount++;
        }
    }
}
