using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class LocalData
{
    public static string VERSION = "v02.01";//如果有数据结构的改动，增加大版本号
    private static LocalData _instance;
    private IList<Archive> archs = new List<Archive>();
    private Archive archive = new Archive();
    private static Archive curArc = null;   //当前备份
                                            //默认密钥向量
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    //每领取一次体力就会有一次存档
    //如果对时发现mt不符合时间规则存档回退至合理时间存档
    //每日奖励需要联网 联网正好验证时间
    public class Archive
    {
        public string version;          //版本
        public float mt;				//修改时间
        public PlayerSelf playerData=PlayerSelf.Instance; //玩家数据
        public Archive()
        {

        }
        public Archive(float mt)
        {
            this.mt = mt;
            this.version = LocalData.VERSION;
            this.playerData = PlayerSelf.Instance;
            this.playerData.CreateData();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public LocalData()
    {
        loadArchive();
    }



    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptString">待加密的字符串</param>
    /// <param name="encryptKey">加密密钥,要求为8位</param>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public static string EncryptDES(string encryptString, string encryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
            return encryptString;
        }
    }

    /// <summary>
    /// DES解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串</param>
    /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public static string DecryptDES(string decryptString, string decryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch
        {
            return decryptString;
        }
    }

    private static void deleteArchiveStr()
    {
        if (true)
        {
            string daPath = Application.persistentDataPath + "/archive.db";
            File.Delete(daPath);
        }
        else
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private static void setArchiveStr(string arcStr)
    {
        if (true)
        {
            string daPath = Application.persistentDataPath + "/archive.db";
            StreamWriter sw = new StreamWriter(daPath);
            char[] arr = arcStr.ToCharArray();
            sw.WriteLine(arr, 0, arr.Length);
            sw.Flush();
            sw.Close();
        }
        else
        {
            PlayerPrefs.SetString("archive", arcStr);
        }
    }

    private static string getArchiveStr()
    {
        string ret = "";
        if (true)
        {
            string daPath = Application.persistentDataPath + "/archive.db";
            Debug.Log(daPath);
            StreamReader sr = new StreamReader(daPath);
            ret = sr.ReadToEnd();
            sr.Close();
        }
        else
        {
            ret = PlayerPrefs.GetString("archive");
        }
        return ret;
    }
    //保存存档
    public void saveArchive()
    {
        string arcStr = JsonConvert.SerializeObject(archs);
        //Debug.Log("save2");
        string s = EncryptDES(arcStr, "asdf" + "htvd");

        setArchiveStr(s);
    }

    //添加一个存档并且存档
    public void saveArchiveExt()
    {
        string arcStr = JsonConvert.SerializeObject(archs[archs.Count]);
        Archive arc = JsonConvert.DeserializeObject<Archive>(arcStr);
        arc.mt = Time.time;
        archs.Add(arc);
        curArc = arc;

        saveArchive();
    }
    //格式化并且重新创建文档
    void formatAndCreartArchive()
    {
        //删除档案
        deleteArchiveStr();
        //使用初始档案
        archs = new List<Archive>();
        Archive arc = new Archive(Time.time);
        //TODO:
        archs.Add(arc);
        //archive = arc;
        curArc = arc;
        saveArchive();
    }

    //加载存档
    void loadArchive()
    {
        //	PlayerPrefs.DeleteAll();
        //	bool archiveException = false;
        try
        {
            string arcStr = getArchiveStr();
            if (string.IsNullOrEmpty(arcStr))
            {
                formatAndCreartArchive();
                return;
            }

            arcStr = DecryptDES(arcStr, "asdf" + "htvd");
            archs = JsonConvert.DeserializeObject<IList<Archive>>(arcStr);
            curArc = archs[archs.Count - 1];
            //archive = JsonConvert.DeserializeObject<Archive>(arcStr);
            //curArc = archive;
            if (!curArc.version.Equals(LocalData.VERSION))
            {
                formatAndCreartArchive();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
            formatAndCreartArchive();
        }
    }

    public static LocalData getInstance()
    {
        if (_instance == null)
        {
            _instance = new LocalData();
        }
        return _instance;
    }
}
