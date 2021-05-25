using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{

    private int move = 0;
    private float cabbage_time = 0;
    private bool iscabbage = false;

    public GameObject cabbage;
    private GameObject tmp_cabbage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cabbage_time += Time.deltaTime;
        if (cabbage_time >= 1.0f && !iscabbage)
        {
            Vector3 vector = this.gameObject.transform.position;
            vector.y = 1.0f;
            tmp_cabbage = Instantiate(cabbage, vector, Quaternion.identity);
            tmp_cabbage.transform.parent = this.gameObject.transform;
            iscabbage = true;
        }
        if (cabbage_time >= 2.0f && iscabbage)
        {
            Destroy(tmp_cabbage);
            cabbage_time = 0;
            iscabbage = false;
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[2] += 1;
        }
        else if (iscabbage)
        {
            Vector3 dir = tmp_cabbage.transform.up;
            tmp_cabbage.transform.position += dir * Time.deltaTime;
        }
    }
}
