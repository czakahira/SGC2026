using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerColorInput : MonoBehaviour
{
	public static PlayerColor instance;

	private InputSystem_Actions input;
	private PlayerColor playerColor;

	void Awake()
	{
		input = new InputSystem_Actions();
		playerColor = GetComponent<PlayerColor>();
	}
	void OnEnable()
	{
		input.Player.Enable();
		//色変更
		input.Player.ChengeRed.performed += ctx => playerColor.SetColor(EColorType.Red);
		input.Player.ChengeBlue.performed += ctx => playerColor.SetColor(EColorType.Blue);
		input.Player.ChengeGreen.performed += ctx => playerColor.SetColor(EColorType.Green);
		//色戻す
		input.Player.ChengeRed.canceled += ctx => playerColor.SetColor(EColorType.Default);
		input.Player.ChengeBlue.canceled += ctx => playerColor.SetColor(EColorType.Default);
		input.Player.ChengeGreen.canceled += ctx => playerColor.SetColor(EColorType.Default);
	}
	void OnDisable()
	{
		input.Player.Disable();
	}
	private void OnDestroy()
	{
		input.Dispose();
		input = null;
	}
}
