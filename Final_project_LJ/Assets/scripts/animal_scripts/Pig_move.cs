using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_move : MonoBehaviour
{
    private float time = 0;
    private int move = 0;
    private float grown_time = 0;
    public int grow= 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //돼지의 성장 구현, 양배추 3씩 소비
        grown_time += Time.deltaTime;
        if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[2] > 3)
        {
            if (grown_time > 10.0f && grow != 0)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[2]-=3;
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                grown_time = 0;
                grow--;
            }
        }
        //돼지의 움직임 구현
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
