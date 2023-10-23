using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiroplayer : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 1;
    public float destroytime = 1.5f; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider2D other)
    {
        Destroy(gameObject);
    }
}
