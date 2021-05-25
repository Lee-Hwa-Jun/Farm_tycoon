using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow_move : MonoBehaviour
{
    private float time = 0;
    private int move = 0;

    private float milk_time =0 ;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //우유생산구현
        milk_time += Time.deltaTime;
        if (milk_time >= 1.0f)
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[4] += 1;
            milk_time = 0;
        }

        //소의 움직임 구현
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
