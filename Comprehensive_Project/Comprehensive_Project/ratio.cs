using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comprehensive_Project
{
    static class Ratio
    {
        static public double sex;
        static public double dirtyWord;
        static public double racism;
        static public String tag;
        static public String link;
        static private String videoName;

        static Ratio()
        {
            sex = 0;
            dirtyWord = 0;
            racism = 0;
            tag = "";
        }
        static public void SetvideoName(String name)
        {
            String temp = name.Replace("mono+", ""); ;
            temp = temp.Replace(".webm","");
            temp = temp.Replace(".flac", "");
            temp = temp.Replace(".mp4", "");
            Ratio.videoName = temp;

        }
        static public String GetvideoName()
        {
            return Ratio.videoName;
        }



    }
}
