using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance;
    public int life = 3;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ReduceLife()
    {
        life--;
        Debug.Log("Life:" +  life);
        if(life <= 0)
        {
            GameOver();
        }
    }
	public void ResetLife(int value =3)
	{
        life = value;
	}
    private void GameOver()
    {
        Debug.Log("ゲームオーバー");
    }
}
