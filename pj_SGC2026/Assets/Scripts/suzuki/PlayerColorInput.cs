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
		input.Player.ChengeRed.performed += ctx => playerColor.SetColor(ColorType.Red);
		input.Player.ChengeBlue.performed += ctx => playerColor.SetColor(ColorType.Blue);
		input.Player.ChengeGreen.performed += ctx => playerColor.SetColor(ColorType.Green);
		//色戻す
		input.Player.ChengeRed.canceled += ctx => playerColor.SetColor(ColorType.Default);
		input.Player.ChengeBlue.canceled += ctx => playerColor.SetColor(ColorType.Default);
		input.Player.ChengeGreen.canceled += ctx => playerColor.SetColor(ColorType.Default);
	}
	void OnDisable()
	{
		input.Player.Disable();
	}
}
