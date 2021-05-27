using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public int[] isfarm_list;
    public int[,] livestock_list;
    public int[,] plant_list;
    public GameObject[] farm_spot = new GameObject[16];


    public int[] property_int = new int[] { 0, 0, 0, 0, 0, 0, 0 };
    float timer = 0.0f;

    public int step;

    void Start()
    {
        reset_list();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 60.0f) //10분마다 오토저장
        {
            SaveData();
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("AutoSaved");
            Debug.Log("오토저장완료");
            timer = 0;
        }
    }
    public void SaveData()
    {
        //자산
        property_int = GameObject.Find("Body").GetComponent<PlayerMove>().property_int;
        string SaveArr = ""; // 문자열 생성
        string isfarmArr = ""; // 문자열 생성
        string islivestock = ""; // 문자열 생성

        for (int i = 0; i < property_int.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            SaveArr = SaveArr + property_int[i];
            if (i < property_int.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                SaveArr = SaveArr + ",";
            }
        }

        GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("Saved");
        PlayerPrefs.SetString("Data", SaveArr); // PlyerPrefs에 문자열 형태로 저장

        //농장 자체
        for (int i = 0; i < isfarm_list.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            isfarmArr = isfarmArr + isfarm_list[i];
            Debug.Log(isfarm_list[i]+"@@@");
            if (i < isfarm_list.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                isfarmArr = isfarmArr + ",";
            }
        }
        Debug.Log(isfarmArr+"!!");
        PlayerPrefs.SetString("isFarm", isfarmArr); // PlyerPrefs에 문자열 형태로 저장

        //농장 마다의 동물들
        for(int i = 0; i < 16; i++)
        {
            livestock_list[i, 0] = farm_spot[i].GetComponent<Make_farm>().livestock_list[0];
            livestock_list[i, 1] = farm_spot[i].GetComponent<Make_farm>().livestock_list[1];
        }
        for (int i = 0; i < 16; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            islivestock = islivestock + livestock_list[i,0] + "," +livestock_list[i, 1];
            if (i < isfarm_list.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                islivestock = islivestock + ",";
            }
        }
        PlayerPrefs.SetString("islivestock", islivestock); // PlyerPrefs에 문자열 형태로 저장

    }
    public void CallData()
    {
        string[] dataArr = PlayerPrefs.GetString("Data").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
        string[] isfarmArr = PlayerPrefs.GetString("isFarm").Split(',');
        string[] islivestock = PlayerPrefs.GetString("islivestock").Split(',');

        //자산
        if (dataArr.Length != 1)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }
        }
        //가축들
        if (islivestock.Length != 1)
        {
            for (int i = 0; i < 16; i++)
            {
                farm_spot[i].GetComponent<Make_farm>().livestock_list[0] = int.Parse(islivestock[i * 2]);
                farm_spot[i].GetComponent<Make_farm>().livestock_list[1] = int.Parse(islivestock[i * 2 + 1]);
            }
        }
        step = 1;
        //농장 자체
        if (isfarmArr.Length != 1)
        {
            farm_spot[0].GetComponent<Make_farm>().load_data();
            for (int i = 1; i < isfarmArr.Length; i++)//index 0은 기본 농장이기 때문에 1부터 시작
            {
                if (isfarmArr[i] == "1")
                {
                    farm_spot[i].GetComponent<Make_farm>().make_farm();
                }
            }
        }



        Debug.Log("Called");
    }
    public void reset_list()
    {
        isfarm_list = new int[16];
        livestock_list = new int[16, 2];
        plant_list = new int[16, 4];
        for(int i = 1;i< 16; i++)
        {
            isfarm_list[i] = 0;
        }
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                livestock_list[i,j] = 0;
            }
        }
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                plant_list[i, j] = 0;
            }
        }

        for(int i = 0; i < 16; i++)
        {
            farm_spot[i].GetComponent<Make_farm>().reset_all();
        }
    }
}
