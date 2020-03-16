using UnityEngine;

public class GameDataManager : MonoBehaviour
{
	private const string SaveDataKey = "SavedData";

	public GameData GetData()
	{
		GameData gameData;

		if (!PlayerPrefs.HasKey(SaveDataKey))
		{
			gameData = new GameData();
			gameData.Color = Camera.main.backgroundColor;
			gameData.Gravity = Physics2D.gravity;
			gameData.CollisionCount = 0;
		}
		else
		{
			string dataString = PlayerPrefs.GetString(SaveDataKey);
			gameData = JsonUtility.FromJson<GameData>(dataString);
		}

		return gameData;
	}

	public void SaveData(GameData gameData)
	{
		string dataString = JsonUtility.ToJson(gameData);
		PlayerPrefs.SetString(SaveDataKey, dataString);
	}
}
