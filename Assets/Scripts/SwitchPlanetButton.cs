using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchPlanetButton : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private PlanetInfo _planetInfo;

	public void OnPointerClick(PointerEventData eventData)
	{
		MessageBroker.Default.Publish(new SwitchPlanetEvent{Gravity = _planetInfo.Gravity,
															BgColor = _planetInfo.BackgroundColor});
	}
}
