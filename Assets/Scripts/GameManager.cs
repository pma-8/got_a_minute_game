using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager _instance;

    public  bool lost = false;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    _instance = new GameObject("Instance of " + typeof(GameManager)).AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    //Call it for loosing a mini game
    public void LostMiniGame()
    {
        lost = true;
    }

    public void WinMiniGame()
    {
        lost = false;
    }

    //Clean up singleton
    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LevelLoader.Instance.LoadRandomLevel();
        }
    }
}
