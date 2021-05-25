using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_move : MonoBehaviour { 
    private float time = 0;
    private int move = 0;
    private bool one_time = false;

    private float money_time = 0;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (one_time == false)
            {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 200, 0));
            one_time = true;
            }
        //여우의 움직임 구현
        if (move == 0)
        {
            float random_ro = Random.Range(0, 360f);
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
