using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    public GameObject arrowUp;
    public GameObject arrowDown;

    private float animTime = 0.5f;

    // Update is called once per frame
    void Start()
    {
        Color arrowUpCol = arrowUp.GetComponent<MeshRenderer>().material.color;
        LeanTween.value(arrowUp, 1f, 0, animTime).setOnUpdate((float val) =>
        {
            arrowUp.GetComponent<MeshRenderer>().material.color =  new Color(arrowUpCol.r, arrowUpCol.g, arrowUpCol.b, val);
        }).setLoopPingPong();

        Color arrowDownCol = arrowDown.GetComponent<MeshRenderer>().material.color;
        LeanTween.value(arrowUp, 1f, 0, animTime).setOnUpdate((float val) =>
        {
            arrowDown.GetComponent<MeshRenderer>().material.color = new Color(arrowDownCol.r, arrowDownCol.g, arrowDownCol.b, val);
        }).setLoopPingPong();
    }
}
