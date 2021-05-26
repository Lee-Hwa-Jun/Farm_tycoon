using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Del_plant : MonoBehaviour
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
    public void plant_del()
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
    public void menu()
    {
        link.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
        GameObject.Find("Body").GetComponent<PlayerMove>().information = 0;
    }
}
