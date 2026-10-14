
using UnityEngine;

public class SystemSingletonBehaviour<T> : SingletonBehaviour<T> where T : MonoBehaviour
{

	protected override void OnInitialize()
	{
		base.OnInitialize();

		GameManager.instance.onUpdate += OnUpdate;
		GameManager.instance.onFixedUpdate += OnFixedUpdate;
		GameManager.instance.onLateUpdate += OnLateUpdate;
	}
	protected override void OnDispose()
	{
		base.OnDispose();

		GameManager.instance.onUpdate -= OnUpdate;
		GameManager.instance.onFixedUpdate -= OnFixedUpdate;
		GameManager.instance.onLateUpdate -= OnLateUpdate;
	}

}