using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BarsController : MonoBehaviour
{

    public float speed;
    public GameObject barOrigin;
    int interval;
    int count;
    List<GameObject> bars;
    int barsCount;
    Color barColor;
    Color[] colors = new Color[4] { Color.red, Color.green, Color.blue, Color.yellow };
    [SerializeField]
    Text scoreText;
    int score;

    void Awake()
    {
        barColor = Color.white;
        bars = new List<GameObject>();
        bars.Add(Instantiate(barOrigin));
        bars[0].transform.position = new Vector2(0, 9);
        bars[bars.Count - 1].GetComponent<SpriteRenderer>().color = barColor;
        bars[bars.Count - 1].transform.SetParent(transform);
        barsCount = 1;
        score = 1;
        scoreText.text = "Bars: " + score.ToString();
        scoreText.color = barColor;
    }

    // Use this for initialization
    void Start()
    {
        count = 0;
        interval = (int)(4 / speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (interval <= count)
        {
            count = 0;
            bars.Add(Instantiate(barOrigin));
            int posX = 0;
            do
            {
                posX = Random.Range(-1, 2) * 3;
            }
            while (1 < bars.Count
            && posX == (int)bars[bars.Count - 2].transform.position.x);

            bars[bars.Count - 1].transform.position
                = new Vector2(posX, 9);
            bars[bars.Count - 1].GetComponent<SpriteRenderer>().color = barColor;
            bars[bars.Count - 1].transform.SetParent(transform);
            barsCount++;
            if (barsCount % 10 == 0)
            {
                speed += 0.01f;
                barColor = colors[Random.Range(0, colors.Length)];
                scoreText.color = barColor;
            }
            score++;
            scoreText.text = "Bars: " + score.ToString();
        }
        else
        {
            count++;
        }
        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].transform.position += Vector3.down * speed;
            if(bars[i].transform.position.y<-9)
            {
                GameObject sub = bars[i];
                bars.RemoveAt(i);
                i--;
                Destroy(sub);
            }
        }
    }
}
