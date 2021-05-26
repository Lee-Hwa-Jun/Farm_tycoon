using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_farm : MonoBehaviour
{
    public GameObject farm,buy,sell,message;
    public TextMesh buy_text,sell_text;
    private GameObject child_farm;
    private int price = 50000;
    public int farm_idx;
    public int[] livestock_list = { 0,0};
    public int[] plant_list = { 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void make_farm()
    {
        child_farm = Instantiate(farm, this.transform.position + new Vector3(-4.8f, 0f, -0.5f), Quaternion.identity);
        child_farm.transform.SetParent(this.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ( GameObject.Find("Body").GetComponent<PlayerMove>().information == 100)
        {
            if (child_farm == null)
            {
                if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= price)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= price;
                    make_farm();
                    GameObject.Find("Body").GetComponent<PlayerMove>().information = 0;
                    message.SetActive(false);
                    buy.SetActive(true);

                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("구매 되었습니다.");
                    GameObject.Find("SaveBtn").GetComponent<Save>().isfarm_list[farm_idx] = 1;

                }
                else
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돈이 부족합니다.");
                    message.SetActive(false);
                    buy.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("다른 곳을 선택해주세요.");
                message.SetActive(false);
                buy.SetActive(true);
            }
        }
        if (GameObject.Find("Body").GetComponent<PlayerMove>().information == -100)
        {
            if (child_farm != null)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += price;
                child_farm = this.transform.GetChild(1).gameObject;
                Destroy(child_farm);
                GameObject.Find("Body").GetComponent<PlayerMove>().information = 0;
                message.SetActive(false);
                sell.SetActive(true);
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("판매 되었습니다.");
            }
            else
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("다른 곳을 선택해주세요.");
                message.SetActive(false);
                buy.SetActive(true);
            }
        }

    }
}
