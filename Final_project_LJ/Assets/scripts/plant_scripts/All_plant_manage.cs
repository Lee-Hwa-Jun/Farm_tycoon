using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_plant_manage : MonoBehaviour
{
    public GameObject tomato, cabbage;
    public GameObject[] del_button = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_plant(int[] plant)
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().information = i;
            if (plant[i] == 1)
            {
                tomato.GetComponent<All_event>().def = "plant_load";
                tomato.GetComponent<All_event>().init();
            }
            if (plant[i] == 2)
            {
                cabbage.GetComponent<All_event>().def = "plant_load";
                cabbage.GetComponent<All_event>().init();
            }
        }

    }

    public void reset_plant()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().information = i;
            del_button[i].GetComponent<All_event>().def = "plant_del";
            del_button[i].GetComponent<All_event>().init();
        }
    }
}
