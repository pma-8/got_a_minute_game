using TMPro;
using UnityEngine;

public class LevelCounterAnimation : MonoBehaviour
{
    public Animator transition;
    public GameObject instruction;
    public GameObject timer;
    public LiveCounter liveCounter;
    public int scaleNum = 5;

    private void OnEnable()
    {
        if (gameObject.GetComponent<RectTransform>().localScale.x <= 0)
        {
            //Out Animation
            LeanTween.scale(gameObject, new Vector3(scaleNum, scaleNum, scaleNum), 0.5f).setEaseOutBounce();
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = PlayerInformation.levelNumber.ToString();
            //In Animation
            LeanTween.scale(gameObject, new Vector3(scaleNum + 1, scaleNum + 1, scaleNum + 1), 0.2f).setEaseShake().setOnComplete(UpdateLevelCounter);

        }
    }

    private void UpdateLevelCounter()
    {
        liveCounter.InAnimation();
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInQuint().setOnComplete(StartGame);
    }

    private void StartGame()
    {
        transition.SetTrigger("LoadIn");
        instruction.SetActive(true);
        timer.SetActive(true);
        gameObject.SetActive(false);
    }
}
