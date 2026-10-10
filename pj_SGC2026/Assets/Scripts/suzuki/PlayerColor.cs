using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public ColorType currentColor = ColorType.Default;
    public Renderer playerRenderer;

    public void SetColor(ColorType newColor)
    {
		Debug.Log("色変更: " + newColor);
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
		}
    }
}
