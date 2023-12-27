using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DummyData", menuName = "Dummy Data")]
public class DummyData : ScriptableObject {
	public List<Achievement> gameAchievements = new();
	[NonSerialized] public List<Achievement> playerAchievements = new();

	[System.Serializable]
	public class Achievement {
		public string title;
		public string description;
		public Sprite icon;
	}

}
