using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WallHandler : MonoBehaviour
{
	private void OnCollisionEnter2D()
	{
		MessageBroker.Default.Publish(new PlatformTouchEvent());
	}
}
