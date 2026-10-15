using UnityEngine;

public class PlayerModelColor : MonoBehaviour
{
	[SerializeField] private PlayerColor _playerColor;
	[SerializeField] private Renderer[] _renderers;

	private void Start()
	{
		_renderers = GetComponentsInChildren<Renderer>();
	}

	private void Update()
	{
		EColorType currentColor = _playerColor.currentColor;

		switch(currentColor)
		{
			case EColorType.Default:
				foreach (Renderer renderer in _renderers)
				{
					foreach (Material material in renderer.materials)
					{
						material.color = Color.white;
					}
				}
				break;
			case EColorType.Red:
				foreach (Renderer renderer in _renderers)
				{
					foreach (Material material in renderer.materials)
					{
						material.color = Color.red;
					}
				}
				break;
			case EColorType.Blue:
				foreach (Renderer renderer in _renderers)
				{
					foreach (Material material in renderer.materials)
					{
						material.color = Color.blue;
					}
				}
				break;
			case EColorType.Green:
				foreach (Renderer renderer in _renderers)
				{
					foreach (Material material in renderer.materials)
					{
						material.color = Color.green;
					}
				}
				break;
		}
	}

}
