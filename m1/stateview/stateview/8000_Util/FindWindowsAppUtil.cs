using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FindWindowsAppUtil
{
	/// <summary>
	/// 指定されたファイルに関連付けられたコマンドを取得する
	/// ref https://dobon.net/vb/dotnet/system/findassociatedexe.html
	/// </summary>
	/// <param name="filename"></param>
	/// <param name="extra"></param>
	/// <returns></returns>
	public static string FindAssociatedCommand(string fileName, string extra)
		{
		//拡張子を取得
		string extName = System.IO.Path.GetExtension(fileName);
		if (extName==null || extName.Length == 0 || extName[0] != '.')
		{
			return string.Empty;
		}

		//HKEY_CLASSES_ROOT\(extName)\shell があれば、
		//HKEY_CLASSES_ROOT\(extName)\shell\(extra)\command の標準値を返す
		if (ExistClassesRootKey(extName + @"\shell"))
		{
			return GetShellCommandFromClassesRoot(extName, extra);
		}

		//HKEY_CLASSES_ROOT\(extName) の標準値を取得する
		string fileType = GetDefaultValueFromClassesRoot(extName);
		if (fileType.Length == 0)
		{
			return string.Empty;
		}

		//HKEY_CLASSES_ROOT\(fileType)\shell\(extra)\command の標準値を返す
		return GetShellCommandFromClassesRoot(fileType, extra);
	}
	public static string FindAssociatedCommand(string fileName)
	{
		return FindAssociatedCommand(fileName, "open");
	}

	private static bool ExistClassesRootKey(string keyName)
	{
		Microsoft.Win32.RegistryKey regKey =
			Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(keyName);
		if (regKey == null)
		{
			return false;
		}
		regKey.Close();
		return true;
	}

	private static string GetDefaultValueFromClassesRoot(string keyName)
	{
		Microsoft.Win32.RegistryKey regKey =
			Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(keyName);
		if (regKey == null)
		{
			return string.Empty;
		}
		string val = (string)regKey.GetValue(string.Empty, string.Empty);
		regKey.Close();

		return val;
	}

	private static string GetShellCommandFromClassesRoot(
		string fileType, string extra)
	{
		if (extra.Length == 0)
		{
			//アクションが指定されていない時は、既定のアクションを取得する
			extra = GetDefaultValueFromClassesRoot(fileType + @"shell")
				.Split(',')[0];
			if (extra.Length == 0)
			{
				extra = "open";
			}
		}
		return GetDefaultValueFromClassesRoot(
			string.Format(@"{0}\shell\{1}\command", fileType, extra));
	}
}

