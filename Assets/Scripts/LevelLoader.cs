using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public GameObject instruction;

    public LevelInformation levelInfo;

    public GameObject lvlCounterAnim;
    public GameObject liveCounterAnim;

    public AsyncOperation asyncOperation;

    private static LevelLoader _instance;

    public static LevelLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelLoader>();
                if (_instance == null)
                {
                    _instance = new GameObject("Instance of " + typeof(LevelLoader)).AddComponent<LevelLoader>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {

        if (_instance != null)
        {
            Destroy(gameObject);
        }

        //Load the description
        instruction.GetComponent<TextMeshProUGUI>().text = levelInfo.instruction.Replace("\\n","\n");
    }

    public void LoadRandomLevel()
    {
        LoadNextLevel(MiniGameRandomizer());
    }

    private int MiniGameRandomizer()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int random = currentSceneIndex;

        while(random == currentSceneIndex)
        {
            random = Random.Range(0, SceneManager.sceneCountInBuildSettings);
        }

        return random;
    }

    private void LoadNextLevel(int sceneIndex)
    {
        PlayerInformation.levelNumber++;
        instruction.SetActive(false);
        StartCoroutine(LoadAsynchronouslyLevel(sceneIndex));
    }

    private IEnumerator LoadAsynchronouslyLevel(int sceneIndex)
    {
        //Play animation
        transition.SetTrigger("LoadOut");

        //Load scene asynchronously
        asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        //Wait until process is done
        while (!asyncOperation.isDone)
        {
            //Activate animation to load next level
            if (transition.GetCurrentAnimatorStateInfo(0).IsName("ShowInfo"))
            {
                lvlCounterAnim.SetActive(true);
                liveCounterAnim.SetActive(true);
            }

            /**
            //Check if the load has finished and animation is over
            if (asyncOperation.progress >= 0.9f && transition.GetCurrentAnimatorStateInfo(0).IsName("ChangeScene"))
            {
                //Start new scene
                asyncOperation.allowSceneActivation = true;
            }
            */
            // Wait until next frame before continuing
            yield return null;
        }
        
    }
}
