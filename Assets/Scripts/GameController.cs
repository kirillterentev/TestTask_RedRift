﻿using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D _ball;
	[SerializeField]
	private float _forcePercentGravity;
	[SerializeField]
	private GameObject _switchPlanetMenu;
	[SerializeField]
	private Text _collisionCountText;
	[SerializeField]
	private GameDataManager _gameDataManager;

	private GameData _gameData;
	private float _leanForce;
	private bool _isTouched;

	private void Start()
	{
		_gameData = _gameDataManager.GetData();
		_collisionCountText.text = _gameData.CollisionCount.ToString();
		SwitchPlanet(_gameData.Gravity, _gameData.Color);
		CalculateForce();

		MessageBroker.Default.Receive<ClickOnScreenEvent>().Subscribe(x => SetTouchStatus(x.IsTouched));
		MessageBroker.Default.Receive<SwitchPlanetEvent>().Subscribe(x => SwitchPlanet(x.Gravity, x.BgColor));
		MessageBroker.Default.Receive<PlatformTouchEvent>().Subscribe(x => CollisionPlatform());
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OpenMenu();
		}
#elif UNITY_ANDROID
		if (Input.GetButtonDown("Cancel"))
		{
			OpenMenu();
		}
#endif

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
		_ball.AddForce(direction * _leanForce * _ball.gravityScale, ForceMode2D.Force);
	}

	private void SetTouchStatus(bool isTouched)
	{
		_isTouched = isTouched;
	}

	private void CalculateForce()
	{
		_leanForce = Mathf.Abs(Physics2D.gravity.y * _forcePercentGravity);
	}

	private void SwitchPlanet(Vector2 gravity, Color bgColor)
	{
		_gameData.Gravity = gravity;
		_gameData.Color = bgColor;
		_gameDataManager.SaveData(_gameData);

		Physics2D.gravity = gravity;
		Camera.main.backgroundColor = bgColor;
		CalculateForce();
		CloseMenu();
	}

	private void OpenMenu()
	{
		_switchPlanetMenu.SetActive(true);
		Time.timeScale = 0;
	}

	private void CloseMenu()
	{
		_switchPlanetMenu.SetActive(false);
		Time.timeScale = 1;
	}

	private void CollisionPlatform()
	{
		++_gameData.CollisionCount;
		_collisionCountText.text = _gameData.CollisionCount.ToString();
		_gameDataManager.SaveData(_gameData);
	}
}
