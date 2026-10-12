
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ランキングデータ
/// </summary>
public class RankingData
{
	/// <summary>
	/// 全ステージデータ
	/// </summary>
	public StageResultData[] stageData;

	public RankingData()
	{
		stageData = new StageResultData[0];
	}
}

/// <summary>
/// ステージリザルト
/// </summary>
public class StageResultData
{
	/// <summary>
	/// このステージを遊んだすべてのデータ
	/// </summary>
	public List<ScoreData> scoreData;
}
/// <summary>
/// スコアデータ
/// </summary>
public class ScoreData
{
	/// <summary>
	/// ID
	/// </summary>
	public uint id;
	/// <summary>
	/// スコア
	/// </summary>
	public uint score;
}