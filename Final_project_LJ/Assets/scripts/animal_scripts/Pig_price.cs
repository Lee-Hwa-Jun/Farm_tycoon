using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_price : MonoBehaviour
{
    public int baby=2000, big=3000;
    private float time = 0;
    // Update is called once per frame
    private void Start()
    {
        GameObject.Find("Body").GetComponent<PlayerMove>().property_int[5] = baby;
        GameObject.Find("Body").GetComponent<PlayerMove>().property_int[6] = big;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            int up_and_down1 = Random.RandomRange(0, 3);
            int up_and_down2 = Random.RandomRange(0, 3);
            if (baby < 500)
                up_and_down1 = 1;
            else if (baby > 3500)
                up_and_down1 = 3;

            if (big < 1000)
                up_and_down2 = 1;
            else if (big > 4500)
                up_and_down2 = 3;

            switch (up_and_down1)
            {
                case 0:
                    baby += 50;
                    break;
                case 1:
                    break;
                case 2:
                    baby -= 50;
                    break;
            }
            switch (up_and_down2)
            {
                case 0:
                    big += 50;
                    break;
                case 1:
                    break;
                case 2:
                    big -= 50;
                    break;
            }
            time = 0;

            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[5] = baby;
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[6] = big;
        }
    }
}
