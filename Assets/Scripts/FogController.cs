using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour {

    [SerializeField]
    float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed;
        if(transform.position.x<-15)
        {
            transform.position = Vector3.right * 27;
        }
    }
}
