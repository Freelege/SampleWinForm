using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace ToolsLibrary
{
    public class MyEncryption
    {
         //默认密钥向量
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56,  
                                        0x78, 0x90, 0xAB, 0xCD, 0xEF }; 

        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static byte[] AESEncrypt(string plainText , string strKey )
		{
			//分组加密算法
			SymmetricAlgorithm des = Rijndael.Create () ;
            des.BlockSize = 128;
			byte[] inputByteArray =Encoding.UTF8.GetBytes (plainText ) ;//得到需要加密的字节数组
            //设置密钥及密钥向量
			des.Key =Encoding.UTF8.GetBytes (strKey );
			des.IV = _key1;
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			byte[] cipherBytes = ms .ToArray () ;//得到加密后的字节数组
			cs.Close();
			ms.Close();
			return cipherBytes;
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static byte[] AESDecrypt(byte[] cipherText , string strKey )
        {
        	SymmetricAlgorithm des = Rijndael.Create () ;
            des.BlockSize = 128;
        	des.Key =Encoding.UTF8.GetBytes (strKey );
			des.IV = _key1 ;
        	byte[] decryptBytes = new byte[cipherText.Length] ;
        	MemoryStream ms = new MemoryStream(cipherText ) ;
        	CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor (), CryptoStreamMode.Read );
        	cs.Read(decryptBytes , 0, decryptBytes.Length);
        	cs.Close();
        	ms.Close();
        	return decryptBytes;
	    }
    }

    public class IniFile
    {
        public string path;             //INI文件名  

        //声明写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        //声明读INI文件的API函数 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //类的构造函数，传递INI文件的路径和文件名
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        //写INI文件
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        //读取INI文件 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }
    }

    public class GlobalVar
    {
        public static IniFile theIniFile;
        public static string user;
        public static string iniFileName;
    }
}


