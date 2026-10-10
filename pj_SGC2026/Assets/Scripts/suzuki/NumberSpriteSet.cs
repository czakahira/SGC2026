using UnityEngine;

[CreateAssetMenu(menuName = "Score/NumberSpriteSet")]
public class NumberSpriteSet : ScriptableObject
{
	public Sprite plusSprite;
	public Sprite[] numberSprites; // 0〜9 の画像
}
