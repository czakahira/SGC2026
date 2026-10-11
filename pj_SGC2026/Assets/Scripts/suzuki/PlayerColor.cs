using UnityEngine;

public class PlayerColor : MonoBehaviour
{
	public static PlayerColor instance;

	public ColorType currentColor = ColorType.Default;

	// ★ SkinnedMeshRenderer 1つを参照する
	public Renderer playerRenderer;

	private void Awake()
	{
		instance = this;
	}

	public void SetColor(ColorType newColor)
	{
		currentColor = newColor;

		Color targetColor = Color.white;

		switch (newColor)
		{
			case ColorType.Default:
				targetColor = Color.white;
				break;
			case ColorType.Red:
				targetColor = Color.red;
				break;
			case ColorType.Blue:
				targetColor = Color.blue;
				break;
			case ColorType.Green:
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
