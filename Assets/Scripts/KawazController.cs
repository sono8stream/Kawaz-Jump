using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KawazController : MonoBehaviour
{
    float gravity = 0.01f;
    public float vyMax,vy;
    [SerializeField]
    BarsController bars;
    float speedX;
    bool isJumping;
    bool isCharging;
    float jumpCount;
    bool end;
    [SerializeField]
    GameObject endMessage;
    [SerializeField]
    AudioClip jump, result;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector2(0, 10);
        vy = vyMax;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            if (!isJumping)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Animator>().SetBool("Jumping", true);
                    jumpCount = 0;
                    isCharging = true;
                }
                else if (isCharging)
                {
                    if (Input.GetMouseButtonUp(0) || 50 <= jumpCount)
                    {
                        GetComponent<Animator>().SetBool("Jumping", false);
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpCount * 16);
                        isJumping = true;
                        jumpCount = 0;
                        isCharging = false;
                        GetComponent<AudioSource>().PlayOneShot(jump);
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        jumpCount++;
                    }
                }
            }
            float subX = transform.position.x;
            transform.position = new Vector3(MousePosToWorld().x, transform.position.y, 0);
            Debug.Log(subX < transform.position.x);
            if (transform.position.x < subX && transform.eulerAngles.y == 0)
            {
                Debug.Log("!!");
                transform.eulerAngles = Vector3.up * 180;
            }
            else if (subX < transform.position.x && transform.eulerAngles.y != 0)
            {
                Debug.Log("??");
                transform.eulerAngles = Vector3.zero;
            }
            if (transform.position.y <= -9)
            {
                end = true;
                bars.enabled = false;
                endMessage.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(result);
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        float x = transform.position.x;
        float colX = col.transform.position.x;
        if (col.transform.position.y < transform.position.y
            && colX - 1.5f < x && x < colX + 1.5f)
        {
            isJumping = false;
        }
    }

    Vector3 MousePosToWorld()
    {
        Vector3 position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        return Camera.main.ScreenToWorldPoint(position);
    }
}
