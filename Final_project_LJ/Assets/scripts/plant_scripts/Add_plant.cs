using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_plant : MonoBehaviour
{
    public string def;
    public GameObject link;
    public GameObject line, plant;
    public GameObject[] lines = new GameObject[4];
    public int price, tmp;
    public GameObject this_farm, sell_button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void plant_add()
    {
        int idx = GameObject.Find("Body").GetComponent<PlayerMove>().information;
        if (lines[idx].transform.childCount == 0)
        {
            if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= price)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= price;
                Vector3 line_spot_p = lines[idx].transform.position;
                GameObject _obj = Instantiate(plant, line_spot_p, Quaternion.identity) as GameObject;
                _obj.transform.parent = lines[idx].gameObject.transform;
                menu();

                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("구매 되었습니다.");
                this_farm.transform.parent.GetComponent<Make_farm>().plant_list[idx] = tmp;
            }
            else
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돈이 부족합니다.");
                menu();
            }
        }
        else
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("이미 농사중인 곳입니다.");
            menu();
        }
        //lines,plant필요

    }
    public void menu()
    {
        link.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
        GameObject.Find("Body").GetComponent<PlayerMove>().information = 0;
    }
}
