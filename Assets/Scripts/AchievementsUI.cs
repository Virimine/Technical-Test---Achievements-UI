using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using DG.Tweening;

public class AchievementsUI : MonoBehaviour {
	[SerializeField] GameObject tutorial;
	[SerializeField] GameObject content;
	[SerializeField] Animator title;
	[SerializeField] Animator counter;
	[SerializeField] TextMeshProUGUI counterTMP;
	[Space]
	[SerializeField] AchievementsCard card;
	[SerializeField] RectTransform cardsParent;
	[SerializeField] CanvasGroup scrollBar;
	[Space]
	[SerializeField] DummyGameData gameData;
	[SerializeField] int earnedIndex = 4;

	List<AchievementsCard> activeAchievements = new();
	DummyPlayerData playerData;

	bool isOpen => content.activeInHierarchy;

	/// POLISH: Toggle Group > start with HOME, cute selection Anim, 
	/// POLISH: Animations > OnClick flare, New Achievement hype

	void Start() => PopulatePlayerDataList();

	void Update() => EarnAchievementHack();

	void PopulatePlayerDataList() {
		playerData = new DummyPlayerData();

		for (int i = 0; i < earnedIndex; i++) {
			playerData.playerAchievements.Add(gameData.gameAchievements[i]);
		}
	}

	// Called in "Items" NavBar Button Event
	public void Open() {
		if (isOpen) { return; }

		content.SetActive(true);

		tutorial.SetActive(false); // used instead of UI toggle panels function

		UpdateEarnedText();
		StartCoroutine(SpawnCoroutine());
	}

	// Called in NavBar Buttons Event
	public void Close() => StartCoroutine(DespawnCoroutine());

	void UpdateEarnedText() => counterTMP.text = $"{earnedIndex} / {gameData.gameAchievements.Count}";

	IEnumerator SpawnCoroutine() {

		title.Play("Spawn");
		counter.Play("Spawn");

		yield return new WaitForSeconds(0.3f);

		foreach (var achievement in playerData.playerAchievements) {

			var newCard = Instantiate(card, cardsParent);
			newCard.Initialize(achievement.title, achievement.description, achievement.icon);
			activeAchievements.Add(newCard);
			yield return new WaitForSeconds(0.15f);


		}
		//scrollBar.DOFade(1, 0.5f);
		StartCoroutine(ScrollbarFade(1, 0.5f));
	}

	IEnumerator DespawnCoroutine() {

		title.Play("Despawn");
		counter.Play("Despawn");

		//scrollBar.DOFade(0, 0.5f);
		StartCoroutine(ScrollbarFade(0, 0.5f));

		foreach (var card in activeAchievements) {
			card.PlayDespawn();
		}

		yield return new WaitForSeconds(0.4f);
		content.SetActive(false);

		foreach (var card in activeAchievements) {
			Destroy(card.gameObject);
		}

		activeAchievements.Clear();
		tutorial.SetActive(true); // used instead of toggle UI panels function
	}

	void EarnAchievementHack() {
		if (earnedIndex >= gameData.gameAchievements.Count || isOpen) { return; }

		if (Input.GetKeyDown(KeyCode.E)) {
			playerData.playerAchievements.Add(gameData.gameAchievements[earnedIndex]);
			Debug.Log("new Achievement Unlocked! Reload tab to view");
			earnedIndex++;
		}
	}

	IEnumerator ScrollbarFade(float endValue, float duration) {

		float t = 0;
		float startValue = scrollBar.alpha;

		while (t < duration) {
			scrollBar.alpha = Mathf.Lerp(startValue, endValue, t / duration);
			t += Time.deltaTime;
			yield return null;
		}
		scrollBar.alpha = endValue;
	}
}
