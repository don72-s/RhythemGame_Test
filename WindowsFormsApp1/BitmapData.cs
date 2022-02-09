using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApp1
{
    [Serializable]
    class ImageData
    {

        List<Image> imageList = new List<Image>();

        public void addImage(Image _i)
        {
            imageList.Add(_i);
        }

        public List<Image> getImgaeList()
        {
            return imageList;
        }

        

    }

    class RWImageData {


        public static void serializeImageData()
        {

            ImageData imageData = new ImageData();

            #region 그림 직렬화 추가 영역

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\lodingImg.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\lodingImg.png"));
            //
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\startBackBuffer.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back00.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\a.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\b.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\c.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\Desktop\동방\동방RC홍\dat\image\stageST_02\N\chM00.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\Desktop\동방\동방RC홍\dat\image\stageST_02\N\chM01.png"));
            //option
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\optionBackBuffer.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\riku.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\chS00.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm4.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm5.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm6.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm7.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm8.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\bgm9.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff4.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff5.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff6.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff7.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff8.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\sndEff9.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\option_cur1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\option_cur2.png"));
            
            //
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back001_Buffer.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\back001.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_A.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_B.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_C.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\cur_D.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_tuto.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_play.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\mainmenu_auto.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));
            //
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_testbackground_Default.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\0,0-1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\0,0-2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1,0.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1-1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-0.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\2-1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\1-2.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_flashlight_Image.png"));
            //
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage2_default.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\idle1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\idle2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\looking_up.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\looking_forward.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\left_mistake.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\right_mistake.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\forward_success.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\forward_fail.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\raindropBubble1.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\raindropBubble2.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));
            //

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage3_BackDefault.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowDefault_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowLeft_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowRight_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\arrowDown_2.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\nomalLeft.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\nomalRight.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count4.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count5.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\count10.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamSafe.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamWarning1_ver2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\steamWarning2_ver2.png"));

            //stage3 subform 이미지
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\belt.gif"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\dump3.png"));

            

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\textbox_800_140.png"));

            //stage4

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1_testbackground_Default.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\stage1testbackground.png"));

            //result scene

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\scoreBack.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\scoreBack.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result3.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_1_3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_2_3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_3_3.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_1.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_2.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\result_4_3.png"));

            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));
            imageData.addImage(Image.FromFile(@"C:\Users\ASUS\source\repos\testWindowForm\WindowsFormsApp1\Resources\none.png"));


            #endregion


            using (Stream stm = File.Open("test.dat", FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stm, imageData);
                stm.Close();
            }
        }


        public static List<Image> deserializImageData()
        {

            ImageData imageData = new ImageData();

            using (Stream readStm = File.Open("test.dat", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter readBf = new BinaryFormatter();
                imageData = (ImageData)readBf.Deserialize(readStm);
                readStm.Close();
            }

            return imageData.getImgaeList();

        }

    }
}
