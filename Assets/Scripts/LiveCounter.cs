using TMPro;
using UnityEngine;

public class LiveCounter : MonoBehaviour
{
    public Animator transition;
    public GameObject instruction;
    public GameObject timer;
    public int scaleNum = 5;

    public void InAnimation()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f).setEaseInQuint().setOnComplete(Disable);
    }

    private void OnEnable()
    {
        UpdateLiveCounter();
        if (gameObject.GetComponent<RectTransform>().localScale.x <= 0)
        {
            //Out Animation
            OutAnimation();
        }
    }

    private void OutAnimation()
    {
        if (GameManager.Instance.lost)
        {
            LeanTween.scale(gameObject, new Vector3(scaleNum, scaleNum, scaleNum), 0.5f).setEaseOutBounce().setOnComplete(RemoveLife);
        }
        else
        {
            LeanTween.scale(gameObject, new Vector3(scaleNum, scaleNum, scaleNum), 0.5f).setEaseOutBounce().setOnComplete(ReadyForNextGame);
        }
    }

    private void RemoveLife()
    {
        LeanTween.scale(gameObject, new Vector3(scaleNum + 1f , scaleNum + 1f, scaleNum + 1f), 0.2f).setEaseShake().setOnComplete(ReadyForNextGame);
        PlayerInformation.life--;
        UpdateLiveCounter();
    }

    private void UpdateLiveCounter()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Lives: " + PlayerInformation.life.ToString();
    }

    private void ReadyForNextGame()
    {
        transition.SetTrigger("LoadNextLevel");
        LevelLoader.Instance.asyncOperation.allowSceneActivation = true;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
