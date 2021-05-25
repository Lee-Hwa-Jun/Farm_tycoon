using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    private float tomato_time = 0;
    private bool istomato = false;

    public GameObject tomato;
    private GameObject tmp_tomato;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tomato_time += Time.deltaTime;
        if(tomato_time >= 1.0f && !istomato)
        {
            Vector3 vector = this.gameObject.transform.position;
            vector.y = 1.0f;
            tmp_tomato = Instantiate(tomato, vector, Quaternion.identity);
            tmp_tomato.transform.parent = this.gameObject.transform;
            istomato = true;
        }
        else if (tomato_time >= 2.0f && istomato)
        {
            Destroy(tmp_tomato);
            tomato_time = 0;
            istomato = false;
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[1] += 1;
        }
        else if (istomato)
        {
            Vector3 dir = tmp_tomato.transform.up;
            tmp_tomato.transform.position += dir * Time.deltaTime;
        }
    }
}
