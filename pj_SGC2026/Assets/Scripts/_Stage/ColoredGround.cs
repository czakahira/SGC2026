
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// 色：床
	/// </summary>
	public class ColoredGround : ColoredObject
	{
#if false
	public EColorType floorColor;
	private Collider floorCol;

	private void Awake()
	{
		floorCol = GetComponent<Collider>();
	}

	private void Update()
	{
		var player = PlayerColor.instance;
		if (player == null) return;

		Collider playerCol = player.GetComponent<Collider>();
		if (playerCol == null) return;

		bool sameColor = (player.currentColor == floorColor);

		// ★長押し中だけすり抜ける
		Physics.IgnoreCollision(playerCol, floorCol, sameColor);
	}
#endif

	}
}