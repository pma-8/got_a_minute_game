using UnityEngine;

public class B_Game : MonoBehaviour
{
    public GameObject roundTimer;
    public GameObject bubbles;
    public GameManager gameManager;

    private B_ObjectSpawner bSpawner;

    private bool end = false;

    private void Start()
    {
        bSpawner = bubbles.GetComponent<B_ObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTimer.activeSelf)
        {
            bubbles.SetActive(true);
            end = true;
        }

        if(!roundTimer.activeSelf && end){
            foreach(GameObject bubble in bSpawner.bubbles)
            {
                if (bubble.activeSelf)
                {
                    gameManager.LostMiniGame();
                }
            }
        }
        
    }
}
