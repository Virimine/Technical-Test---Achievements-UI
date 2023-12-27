using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour {
	[SerializeField] Animator achievementsTitle;
	[Space]
	[SerializeField] Animator counter;
	[SerializeField] TextMeshProUGUI counterTMP;
	[Space]
	[SerializeField] AchievementsCard card;
	[SerializeField] RectTransform cardsParent;
	[Space]
	[SerializeField] DummyData data;

	public List<AchievementsCard> activeAchievements = new();
	int earnedIndex = 4;
	bool isActive { get; set; }

	/// POLISH: Toggle Group, start with HOME
	/// POLISH: Toggle Group, Cute Selection Anim

	void Start() => PopulatePlayerDataList();

	void Update() => EarnAchievementHack();

	void PopulatePlayerDataList() {
		for (int i = 0; i < earnedIndex; i++) {
			data.playerAchievements.Add(data.gameAchievements[i]);
		}
	}

	// Called in "Items" Button on NavBar. Normally a NavBar script would be used instead of the editor shortcut, but doin it this way to save time
	public void Initialize() {
		if (isActive) { return; }

		/// Play animations (title, cards)

		foreach (var achievement in data.playerAchievements) {
			var newCard = Instantiate(card, cardsParent);
			newCard.Initialize(achievement.title, achievement.description, achievement.icon);
			activeAchievements.Add(newCard);
		}

		UpdateEarnedText();
		//StartCoroutine(UpdateLayoutGroup());
		StartCoroutine(SpawnCoroutine());

		isActive = true;
	}


	IEnumerator SpawnCoroutine() {
		foreach (var achievement in data.playerAchievements) {
			var newCard = Instantiate(card, cardsParent);

			newCard.Initialize(achievement.title, achievement.description, achievement.icon);
			activeAchievements.Add(newCard);

			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator UpdateLayoutGroup() {
		var verticalLayout = cardsParent.GetComponent<VerticalLayoutGroup>();
		verticalLayout.enabled = false;
		yield return new WaitForEndOfFrame();
		verticalLayout.enabled = true;
	}

	void UpdateEarnedText() => counterTMP.text = $"{earnedIndex} / {data.gameAchievements.Count}";

	// Called in "Home" Button on NavBar. Normally a NavBar script would be used instead of the editor shortcut, but doin it this way to save time
	public void ResetUI() {
		/// Play despawn Anim and disable cards

		foreach (var card in activeAchievements) {
			Destroy(card.gameObject);
		}

		activeAchievements.Clear();

		isActive = false;
	}
	void EarnAchievementHack() {
		if (earnedIndex >= data.gameAchievements.Count || isActive) { return; }

		if (Input.GetKeyDown(KeyCode.E)) {
			data.playerAchievements.Add(data.gameAchievements[earnedIndex]);
			Debug.Log("new Achievement Unlocked! Reload tab to view");
			earnedIndex++;
		}

	}
}
