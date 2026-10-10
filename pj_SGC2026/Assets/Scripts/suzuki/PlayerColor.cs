using UnityEngine;

public class PlayerColor : MonoBehaviour
{
	public static PlayerColor instance;

	public ColorType currentColor = ColorType.Default;
	public Renderer playerRenderer;

	private void Awake()
	{
		instance = this;
	}

	public void SetColor(ColorType newColor)
	{
		currentColor = newColor;

		switch (newColor)
		{
			case ColorType.Default:
				playerRenderer.material.color = Color.white;
				break;
			case ColorType.Red:
				playerRenderer.material.color = Color.red;
				break;
			case ColorType.Blue:
				playerRenderer.material.color = Color.blue;
				break;
			case ColorType.Green:
				playerRenderer.material.color = Color.green;
				break;
		}
	}
}
