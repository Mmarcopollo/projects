using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour
{
    private Challenge CurrentChallenge;
    public Color unCompletedColor;
    public Color completedColor;
    public Color failedColor;
    public GameObject gameOverScreen;

    public void ChangeChallenge (Challenge challenge)
    {
        this.transform.GetChild(0).GetComponent<Image>().color = unCompletedColor;
        CurrentChallenge = challenge;
        CurrentChallenge.InitiateChallenge();
        TextMeshProUGUI challengeName = this.transform.GetChild(0).transform.Find("Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI challengeDescription = this.transform.GetChild(0).transform.Find("Description").GetComponent<TextMeshProUGUI>();
        GameObject imageParent = this.transform.GetChild(0).transform.Find("ImageParent").gameObject;
        //Image challengeImageFill = imageParent.transform.Find("Fill").GetComponent<Image>();
        Image challengeImage = imageParent.transform.Find("Image").GetComponent<Image>();
        challengeName.text = challenge.challengeName;
        challengeDescription.text = challenge.description;
        if (challenge.sprite != null)
        {
            //challengeImageFill.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);
            challengeImage.sprite = challenge.sprite;
            //challengeImageFill.sprite = challenge.spriteFill;
            //challengeImageFill.transform.localScale = challenge.fillScale;
            //Vector2 oldImageSizeDelta = challengeImage.gameObject.GetComponent<RectTransform>().sizeDelta;
            //Vector2 newFillSizeDelta = new Vector2(oldImageSizeDelta.x / challenge.spriteWidthRatio, oldImageSizeDelta.y / challenge.spriteHeightRatio);
            //challengeImageFill.gameObject.GetComponent<RectTransform>().sizeDelta = newFillSizeDelta;
            //challengeImageFill.transform.localScale = new Vector3(challenge.spriteWidthRatio, challenge.spriteHeightRatio, 1);
            imageParent.gameObject.SetActive(true);
        }
        else imageParent.gameObject.SetActive(false);
    }
	
	public void Refresh (GameObject obj)
    {
		if(CurrentChallenge!=null && CurrentChallenge.state == ChallengeState.uncompleted)
        {
            CurrentChallenge.RefreshChallenge(obj);
            if(CurrentChallenge.state == ChallengeState.completed)
            {
                this.transform.GetChild(0).GetComponent<Image>().color = completedColor;
                gameOverScreen.GetComponent<GameOverScreen>().AddChallengePrize(CurrentChallenge.getPointPrize());
            }
            else if(CurrentChallenge.state == ChallengeState.failed)
            {
                this.transform.GetChild(0).GetComponent<Image>().color = failedColor;
            }
        }
	}
}
