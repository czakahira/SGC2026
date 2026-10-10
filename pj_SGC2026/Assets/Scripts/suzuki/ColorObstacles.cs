using UnityEngine;

public class ColorObstacles : MonoBehaviour
{
    public ColorType ObstacleColor;
	private void OnCollisionEnter(Collision collision)
	{
		//プレイヤー確認
		PlayerColor playerColor = collision.gameObject.GetComponent<PlayerColor>();
		if (playerColor == null) return;
		//色一致確認
		if (playerColor.currentColor == ObstacleColor)
		{
		    //すり抜け
			Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
		}
		else
		{
		　　//衝突
			Debug.Log("衝突！");
		}
	}
}
