using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsCard : MonoBehaviour {
	[SerializeField] Animator animator;
	[SerializeField] TextMeshProUGUI titleTMP;
	[SerializeField] TextMeshProUGUI descriptionTMP;
	[SerializeField] TextMeshProUGUI earnedTMP;
	[SerializeField] Image icon;

	int maxTitleChars = 22;

	public void Initialize(string title, string description, Sprite spr) {

		titleTMP.text = title;
		descriptionTMP.text = description;
		icon.sprite = spr;

		TrunicateText(title, maxTitleChars);
		UpdateEarnedText();
	}
	public void PlayDespawn() => animator.Play("Despawn");

	void TrunicateText(string title, int maxVisibleChars) {

		if (title.Length > maxVisibleChars) {
			titleTMP.text = title.Substring(0, 22) + "...";
		}
	}

	// for visual diversity xP Normally it would get the data from the saved player data
	void UpdateEarnedText() => earnedTMP.text = $"Earned on { Random.Range(1, 32)} / { Random.Range(1, 12)} / 2023.";


	void OnClicked() {
		// play some cute animation
	}

}
