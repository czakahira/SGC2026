using UnityEngine;

public class ColorGround : MonoBehaviour
{
	public ColorType floorColor;
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
}
