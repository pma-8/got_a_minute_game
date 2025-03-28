using UnityEngine;

public class RandomizedColor : MonoBehaviour
{
    public static Color[] colors = { Color.blue, 
        Color.cyan, Color.green, Color.magenta, 
        Color.red, Color.white, Color.yellow };

    // Start is called before the first frame update
    void Start()
    {
        //Randomize picked colors
        Shuffle(colors);
    }

    //Shuffle an array
    void Shuffle(Color[] pArray)
    {
        int n = pArray.Length;
        while (n > 1)
        {
            n--;
            int m = Random.Range(0, n);
            Color value = pArray[m];
            pArray[m] = pArray[n];
            pArray[n] = value;
        }
    }
}
