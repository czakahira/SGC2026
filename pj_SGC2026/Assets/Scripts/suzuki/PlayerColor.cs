using UnityEngine;

public class PlayerColor : MonoBehaviour
{
	public static PlayerColor instance;

	public EColorType currentColor = EColorType.Default;

	// ★ SkinnedMeshRenderer 1つを参照する
	public Renderer playerRenderer;

	private void Awake()
	{
		instance = this;
	}

	public void SetColor(EColorType newColor)
	{
		currentColor = newColor;

		Color targetColor = Color.white;

		switch (newColor)
		{
			case EColorType.Default:
				targetColor = Color.white;
				break;
			case EColorType.Red:
				targetColor = Color.red;
				break;
			case EColorType.Blue:
				targetColor = Color.blue;
				break;
			case EColorType.Green:
				targetColor = Color.green;
				break;
		}

		// ★ マテリアルが複数あるので全部変更する
		foreach (var mat in playerRenderer.materials)
		{
			// URP Lit シェーダー対応
			if (mat.HasProperty("_BaseColor"))
			{
				mat.SetColor("_BaseColor", targetColor);
			}
			else
			{
				mat.color = targetColor;
			}
		}
	}
}
