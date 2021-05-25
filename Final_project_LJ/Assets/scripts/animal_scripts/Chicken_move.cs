using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_move : MonoBehaviour
{
    private float time = 0;
    private int move = 0;
    private float agg_time = 0;
    private bool isagg = false;

    public GameObject agg;
    private GameObject tmp_agg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //닭의 달걀 생산 구현
        agg_time += Time.deltaTime;
        if (agg_time >= 1.0f && !isagg)
        {
            Vector3 vector = this.gameObject.transform.position;
            vector.y = 0.5f;
            tmp_agg = Instantiate(agg, vector, Quaternion.identity);
            tmp_agg.transform.parent = this.gameObject.transform;
            isagg = true;
        }
        if(agg_time >=2.0f && isagg)
        {
            Destroy(tmp_agg);
            agg_time = 0;
            isagg = false;
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[3] += 1;
        }
        else if (isagg)
        {
            Vector3 dir = tmp_agg.transform.up;
            tmp_agg.transform.position += dir * Time.deltaTime;
        }

        //닭의 움직임 구현
        if (move == 0)
        {
            float random_ro = Random.Range(-60f, 60f);
            this.transform.rotation = Quaternion.Euler(0, random_ro, 0);
            move += 1;
        }
        else if (move == 1)
        {
            this.transform.position += this.transform.forward * Time.deltaTime * 0.3f;
        }
        time += Time.deltaTime;
        if (time > 1)
        {
            time = 0;
            move += 1;
            if (move == 5)
                move = 0;
        }
    }
}
