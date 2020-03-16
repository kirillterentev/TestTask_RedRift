using UniRx;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D _ball;
	[SerializeField]
	private float _forcePercentGravity;

	private float _leanForce;
	private bool _isTouched;

	private void Start()
	{
		CalculateForce();
		MessageBroker.Default.Receive<ClickOnScreenEvent>().Subscribe(x => SetTouchStatus(x.IsTouched));
	}

	private void Update()
	{
		if (!_isTouched)
		{
			return;
		}

		Vector2 touchPosition;

#if UNITY_EDITOR
		touchPosition = Input.mousePosition;
#elif UNITY_ANDROID
		touchPosition = Input.touches[0].position;
#endif

		Vector2 direction = (touchPosition - _ball.position).normalized;
		_ball.AddForce(direction * _leanForce, ForceMode2D.Impulse);
	}

	private void SetTouchStatus(bool isTouched)
	{
		_isTouched = isTouched;
	}

	private void CalculateForce()
	{
		_leanForce = Mathf.Abs(Physics2D.gravity.y * _forcePercentGravity);
	}
}
