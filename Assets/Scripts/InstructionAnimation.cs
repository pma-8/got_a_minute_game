using UnityEngine;

public class InstructionAnimation : MonoBehaviour
{
    public Animator transition;

    private void Awake()
    {
        //Play animation
        LeanTween.scale(gameObject, new Vector3(5, 5, 5), 0.75f).setEaseOutBounce().setOnComplete(Shrink);
    }

    void Shrink()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.75f).setEaseInQuint().setOnComplete(Invisible);
    }

    void Invisible()
    {
        gameObject.SetActive(false);
    }
}
