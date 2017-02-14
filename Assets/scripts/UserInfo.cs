using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


[Serializable]
public class UserInfo
{
	public string userName;
	public string userPwd;

	private static UserInfo _singleton; //(2)

	private UserInfo()    //(1)
	{
		userName = "";
		userPwd = "";
	}

	public static UserInfo GetSingleton () //(3)
	{
		if (_singleton == null)
			readFromFile ();
		return _singleton;            
	}


	public static void readFromFile()
	{
		BinaryFormatter formater = new BinaryFormatter();
		string path;
		if(Application.platform==RuntimePlatform.IPhonePlayer)
		{
			path = Application.persistentDataPath + "/";
//			path = Application.dataPath.Substring (0, Application.dataPath.Length - 5);
//			path = path.Substring(0, path.LastIndexOf('/'))+"/Documents/";    
		}
		else
		{
			path=Application.dataPath+"/Resource/GameData/";
		}

		path = path + "account.bin";

		FileInfo myFile = new FileInfo (path);
		if (myFile.Exists) {
			Stream stream = new FileStream (path, FileMode.Open, FileAccess.Read, FileShare.None);
			if (stream.Length == 0) {
				_singleton = new UserInfo ();
			} else {
				_singleton = (UserInfo)formater.Deserialize (stream);
			}
			stream.Close ();
		} else {
			_singleton = new UserInfo ();
		}
	}

	public static void write2File()
	{
		BinaryFormatter formater = new BinaryFormatter();
		string path = "";
		if(Application.platform==RuntimePlatform.IPhonePlayer)
		{
			path = Application.persistentDataPath + "/";
//			path= Application.dataPath.Substring (0, Application.dataPath.Length - 5);
//			path = path.Substring(0, path.LastIndexOf('/'))+"/Documents/";    
		}
		else
		{
			//path=Application.dataPath+"/Resource/GameData/";
		}
		path = path + "account.bin";

		Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
		formater.Serialize(stream, UserInfo.GetSingleton());
		stream.Close();
	}


	#region 5.0 生成随机字符串 + static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
	///<summary>
	///生成随机字符串 
	///</summary>
	///<param name="length">目标字符串的长度</param>
	///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
	///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
	///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
	///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
	///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
	///<returns>指定长度的随机字符串</returns>
	public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
	{
		byte[] b = new byte[4];
		new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
		System.Random r = new System.Random(BitConverter.ToInt32(b, 0));
		string s = null, str = custom;
		if (useNum == true) { str += "0123456789"; }
		if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
		if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
		if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
		for (int i = 0; i < length; i++)
		{
			s += str.Substring(r.Next(0, str.Length - 1), 1);
		}
		return s;
	} 
	#endregion
}