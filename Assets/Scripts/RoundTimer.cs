using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{

    public static float totalTime;

    private float startTime;

    public LevelInformation levelInfo;

    public Image timerBar;

    public LevelLoader loader;


    private void Awake()
    {
        startTime = levelInfo.timeInSec;
        totalTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        timerBar.fillAmount = totalTime / startTime;

        if(totalTime <= 0)
        {
            gameObject.SetActive(false);
            loader.LoadRandomLevel();
        }

    }

    private void OnDisable()
    {
        totalTime = startTime;
        timerBar.fillAmount = 1;
    }
}
