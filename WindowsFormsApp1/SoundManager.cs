using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace WindowsFormsApp1
{
    static partial class SoundPlayer {

        private static Dictionary<MediaType, MediaPlayer> mediaPlayerDic;

        private static float bgmVolume = 0.5f;
        private static float fxVolume = 0.5f;

        public static void addBgmVolume(float _value) { 
            bgmVolume += _value;
        }

        public static float getBGMVolume() {
            return bgmVolume;
        }

        public static void addFxVolume(float _value) {
            fxVolume += _value;
        }

        public static void setMediaDic(Dictionary<MediaType,MediaPlayer> _d) {
            mediaPlayerDic = _d;
        }

        public static void PlayBGM(MediaType _type)
        {
            MediaPlayer _tmp = mediaPlayerDic[_type];
            _tmp.Stop();
            _tmp.Volume = bgmVolume;
            _tmp.Play();
        }

        public static void PlayFX(MediaType _type)
        {
            MediaPlayer _tmp = mediaPlayerDic[_type];
            _tmp.Stop();
            _tmp.Volume = fxVolume;
            _tmp.Play();
        }

        public static void PlaySnd(MediaType _type, float _volum, TimeSpan _timeSpanOffset)
        {
            MediaPlayer _tmp = mediaPlayerDic[_type];
            _tmp.Stop();
            _tmp.Position = _timeSpanOffset;
            _tmp.Volume = _volum;
            _tmp.Play();
        }

    }

}
