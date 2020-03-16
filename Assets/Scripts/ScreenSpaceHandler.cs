using System.Numerics;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

public class ScreenSpaceHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		MessageBroker.Default.Publish(new ClickOnScreenEvent { IsTouched = true});
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		MessageBroker.Default.Publish(new ClickOnScreenEvent { IsTouched = false });
	}
}
