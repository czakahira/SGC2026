
using System;
using System.Linq;
using UnityEngine;

public static class Utility_Enum
{

	/// <summary>
	/// フラグの整合性をチェックする
	/// </summary>
	private static bool CheckType(this Enum _base, Enum _flag)
	{
		return (_base.GetType() == _flag.GetType());
	}
	/// <summary>
	/// フラグの設定
	/// </summary>
	public static void SetFlag(this Enum _base, Enum _flag)
	{
		if (_base.CheckType(_flag) == false) { return; }
		_base = _flag;
	}
	/// <summary>
	/// フラグが一致するか
	/// </summary>
	public static bool IsFlag(this Enum _base, Enum _flag)
	{
		if (_base.CheckType(_flag) == false) { return false; }
		return _base == _flag;
	}

	/// <summary>
	/// 指定したフラグのいずれかが含まれているか
	/// </summary>
	public static bool ContainsFlag(this Enum _base, Enum _flag)
	{
		if (_base.CheckType(_flag) == false) { return false; }

		ulong b = Convert.ToUInt64(_base);
		ulong f = Convert.ToUInt64(_flag);

		return (b & f) != 0;
	}
	/// <summary>
	/// フラグの追加
	/// </summary>
	public static void AddFlag<T>(ref T _base, T _flag) where T : Enum
	{
		if (_base.CheckType(_flag) == false) { return; }

		ulong b = Convert.ToUInt64(_base);
		ulong f = Convert.ToUInt64(_flag);

		b |= f;

		Type t = _base.GetType();
		_base = (T)(Enum.ToObject(t, b));
	}
	/// <summary>
	/// フラグが無い場合、追加
	/// </summary>
	public static bool AddMissinFlag<T>(ref T _base, T _flag) where T : Enum
	{
		if (_base.HasFlag(_flag) == true) { return false; }

		AddFlag(ref _base, _flag);
		return true;
	}
	/// <summary>
	/// フラグの除外
	/// </summary>
	public static void DelFlag<T>(ref T _base, T _flag) where T : Enum
	{
		if (_base.CheckType(_flag) == false) { return; }

		ulong b = Convert.ToUInt64(_base);
		ulong f = Convert.ToUInt64(_flag);

		b &= ~f;

		Type t = _base.GetType();
		_base = (T)Enum.ToObject(t, b);
	}
	/// <summary>
	/// フラグが含まれている場合、除外
	/// </summary>
	public static bool DelPresentFlag<T>(ref T _base, T _flag) where T : Enum
	{
		if (_base.HasFlag(_flag) == false) { return false; }

		DelFlag(ref _base, _flag);
		return true;
	}


}
