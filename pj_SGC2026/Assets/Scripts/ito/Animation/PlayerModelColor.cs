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
        ColorType currentColor = _playerColor.currentColor;

        switch(currentColor)
        {
            case ColorType.Default:
				foreach (Renderer renderer in _renderers)
                {
                    renderer.material.color = Color.white;
                }
				break;
			case ColorType.Red:
				foreach (Renderer renderer in _renderers)
                {
                    renderer.material.color = Color.red;
                }
				break;
			case ColorType.Blue:
				foreach (Renderer renderer in _renderers)
                {
                    renderer.material.color = Color.blue;
                }
				break;
			case ColorType.Green:
				foreach (Renderer renderer in _renderers)
                {
                    renderer.material.color = Color.green;
                }
				break;
        }
    }

}
