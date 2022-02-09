using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    enum TimerType { STAGE_TIC_TIMER, FADING_TIMER, FADING_SUB_TIMER, BLINK_TIMER, TEXT_TIMER, SCREEN_SHAKE_TIMER, SCROLL_TIMER, STAGE3_HELPER_TIMER}

    static partial class Timers {

        private static Dictionary<TimerType, Timer> timerDic = new Dictionary<TimerType, Timer>();

        public static void AddTimer(TimerType _type, Timer _timer) {
            timerDic.Add(_type, _timer);
        }

        public static void StartTimer(TimerType _type) {
            timerDic[_type].Start();
        }

        public static bool StopTimer(TimerType _type) {
            bool _isEnabled = timerDic[_type].Enabled;
            timerDic[_type].Stop();
            return _isEnabled;
        }
        

        public static void StopAllTimers() {
            foreach (Timer _t in timerDic.Values) {
                _t.Stop();
            }
        }

        public static void SetInterval(TimerType _type ,int _interval) {
            timerDic[_type].Interval = _interval;
        }

        public static bool isTimerEnabled(TimerType _type) {
            return timerDic[_type].Enabled;
        }

    }
}
