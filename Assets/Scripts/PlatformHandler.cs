using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformHandler : MonoBehaviour, IPointerClickHandler
{
	private Image _image;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		SetRandomColor();
	}

	private void OnCollisionEnter2D()
	{
		SetRandomColor();
		MessageBroker.Default.Publish(new PlatformTouchEvent());
	}

	private void SetRandomColor()
	{
		_image.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
	}
}
