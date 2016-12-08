using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyUtil
{
    public class MyUtil
    {

        #region 加密算法，采用MD5算法
        /// <summary>
        /// 采用MD5的加密算法
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EncoedeMethod(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bt = Encoding.Default.GetBytes(text);//将待加密字符转为 字节型数组
            byte[] resualt = md5.ComputeHash(bt);//将字节数组转为加密的字节数组
            string pwds = BitConverter.ToString(resualt).Replace("-", "");//将数字转为string 型去掉内部的无关字符 
            return pwds;
        }
        #endregion
        
        #region 读写INI配置文件，包括API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI配置文件信息
        /// </summary>
        /// <param name="Section">模块，即[]标识的部分</param>
        /// <param name="Key">键</param>
        /// <param name="NoText">默认值</param>
        /// <param name="iniFilePath">配置文件全路径</param>
        /// <returns>返回读取的信息</returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 写入INI配置文件
        /// </summary>
        /// <param name="Section">模块，即[]模块标识部分</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="iniFilePath">配置文件全路径</param>
        /// <returns>返回值为标识写入是否成功</returns>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}
