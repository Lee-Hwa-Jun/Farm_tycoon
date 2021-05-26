using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_event : MonoBehaviour
{
    public string def;
    public GameObject link;
    public GameObject line,plant;
    public GameObject[] lines = new GameObject[4];
    public int information,tmp=0,price;
    public string unit;
    public TextMesh text;
    public GameObject this_farm,sell_button;

    public void init()
    {
        if(def == "menu")
        {
            menu();
            //link필요
        }
        if(def == "plant_del")
        {
            if (line.transform.childCount == 1)
            {
                int idx = GameObject.Find("Body").GetComponent<PlayerMove>().information;
                GameObject tmp = line.transform.GetChild(0).gameObject;
                Destroy(tmp);
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("삭제 되었습니다.");
                this_farm.transform.parent.GetComponent<Make_farm>().plant_list[idx] = 0;
            }
            else
            {
                Debug.Log("error");
            }
            //line필요
        }
        if(def == "plant_add")
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
        if (def == "add_chicken")
        {
            this_farm.GetComponent<Livestock>().add_chicken();
        }
        if (def == "add_cow")
        {
            this_farm.GetComponent<Livestock>().add_cow();
        }
        if (def == "add_pig")
        {
            this_farm.GetComponent<Livestock>().add_pig();
        }
        if (def == "add_fox")
        {
            this_farm.GetComponent<Livestock>().add_fox();
        }
        if (def == "add_lion")
        {
            this_farm.GetComponent<Livestock>().add_lion();
        }
        if (def == "add_dragon")
        {
            this_farm.GetComponent<Livestock>().add_dragon();
        }
        if (def == "del_animal")
        {
            this_farm.GetComponent<Livestock>().del_animal(information);
        }

        if(def == "add_num")
        {
            sell_button.GetComponent<All_event>().tmp += information;
            text.text = sell_button.GetComponent<All_event>().tmp.ToString() + unit;
        }
        if(def == "sub_num")
        {
            if (sell_button.GetComponent<All_event>().tmp - information >= 0)
            {
                sell_button.GetComponent<All_event>().tmp -= information;
                text.text = sell_button.GetComponent<All_event>().tmp.ToString() + unit;
            }
        }
        if (def == "add_all")
        {
            {
                sell_button.GetComponent<All_event>().tmp = GameObject.Find("Body").GetComponent<PlayerMove>().property_int[information];
                text.text = sell_button.GetComponent<All_event>().tmp.ToString() + unit;
            }
        }
        if (def == "sell_something")
        {
            //[money, tomatos, cabbages, aggs, milk, baby_pig, big_pig]
            if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[information] >= tmp)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[information] -= tmp;
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += tmp * price;
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("판매되었습니다.");
                tmp = 0;
                text.text = tmp.ToString() + unit;
            }
            else
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("개수가 부족합니다.");
            }
        }

        if (def == "save")
        {
            Debug.Log("Save");
            GameObject.Find("SaveBtn").GetComponent<Save>().SaveData();
        }
        if (def == "reset")
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int = new int[] { GameObject.Find("Body").GetComponent<PlayerMove>().Start_Money, 0, 0, 0, 0, 0, 0 };

            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("Reset");

            menu();
        }

    }
    public void menu()
    {
        link.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
        GameObject.Find("Body").GetComponent<PlayerMove>().information = information;
    }
}