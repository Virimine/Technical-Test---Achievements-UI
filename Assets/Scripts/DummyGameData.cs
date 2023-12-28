using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DummyData", menuName = "Dummy Data")]
public class DummyGameData : ScriptableObject {
	public List<Achievement> gameAchievements = new();
}

public class DummyPlayerData  {
	public List<Achievement> playerAchievements = new();
}

[System.Serializable]
public class Achievement {
	public string title;
	public string description;
	public Sprite icon;
}
