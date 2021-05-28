using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public int[] isfarm_list;
    public int[,] livestock_list;
    public int[,] plant_list;
    public int lions, foxs, dragons;

    public GameObject[] farm_spot = new GameObject[16];


    public int[] property_int = new int[] { 0, 0, 0, 0, 0, 0, 0 };
    float timer = 0.0f;


    void Start()
    {
        reset_list();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 600.0f) //10분마다 오토저장
        {
            SaveData();
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("AutoSaved");
            timer = 0;
        }
    }
    public void SaveData()
    {
        //자산
        property_int = GameObject.Find("Body").GetComponent<PlayerMove>().property_int;
        string SaveArr = ""; // 문자열 생성
        string isfarmArr = ""; // 문자열 생성
        string livestock = ""; // 문자열 생성
        string plants = ""; // 문자열 생성

        for (int i = 0; i < property_int.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            SaveArr = SaveArr + property_int[i];
            if (i < property_int.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                SaveArr = SaveArr + ",";
            }
        }

        PlayerPrefs.SetString("Data", SaveArr); // PlyerPrefs에 문자열 형태로 저장

        //농장 자체
        for (int i = 0; i < isfarm_list.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            isfarmArr = isfarmArr + isfarm_list[i];
            if (i < isfarm_list.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                isfarmArr = isfarmArr + ",";
            }
        }
        PlayerPrefs.SetString("isFarm", isfarmArr); // PlyerPrefs에 문자열 형태로 저장

        //농장 마다의 동물들
        for(int i = 0; i < 16; i++)
        {
            livestock_list[i, 0] = farm_spot[i].GetComponent<Make_farm>().livestock_list[0];
            livestock_list[i, 1] = farm_spot[i].GetComponent<Make_farm>().livestock_list[1];
        }
        for (int i = 0; i < 16; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            livestock = livestock + livestock_list[i,0] + "," +livestock_list[i, 1];
            if (i < isfarm_list.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                livestock = livestock + ",";
            }
        }
        PlayerPrefs.SetString("livestock", livestock); // PlyerPrefs에 문자열 형태로 저장

        //농장 마다의 식물들
        for (int i = 0; i < 16; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                plant_list[i, j] = farm_spot[i].GetComponent<Make_farm>().plant_list[j];
            }

        }
        for (int i = 0; i < 16; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            plants = plants + plant_list[i, 0] + "," + plant_list[i, 1] + "," + plant_list[i, 2] + "," + plant_list[i, 3];

            if (i < isfarm_list.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                plants = plants + ",";
            }
        }
        PlayerPrefs.SetString("plants", plants); // PlyerPrefs에 문자열 형태로 저장

        //히든몬스터

        PlayerPrefs.SetInt("lions", GameObject.Find("hidden1").GetComponent<Livestock>().lions.Count); 
        PlayerPrefs.SetInt("foxs", GameObject.Find("hidden2").GetComponent<Livestock>().foxs.Count);
        PlayerPrefs.SetInt("dragons", GameObject.Find("hidden3").GetComponent<Livestock>().dragons.Count);

        GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("Saved");

    }
    public void CallData()
    {
        string[] dataArr = PlayerPrefs.GetString("Data").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
        string[] isfarmArr = PlayerPrefs.GetString("isFarm").Split(',');
        string[] livestock = PlayerPrefs.GetString("livestock").Split(',');
        string[] plants = PlayerPrefs.GetString("plants").Split(',');
        string[] hiddens = PlayerPrefs.GetString("hiddens").Split(',');

        //자산
        if (dataArr.Length != 1)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }
        }
        //가축들
        if (livestock.Length != 1)
        {
            for (int i = 0; i < 16; i++)
            {
                farm_spot[i].GetComponent<Make_farm>().livestock_list[0] = int.Parse(livestock[i * 2]);
                farm_spot[i].GetComponent<Make_farm>().livestock_list[1] = int.Parse(livestock[i * 2 + 1]);
            }
        }
        //식물들
        if (plants.Length != 1)
        {
            for (int i = 0; i < 16; i++)
            {
                farm_spot[i].GetComponent<Make_farm>().plant_list[0] = int.Parse(plants[i*4]);
                farm_spot[i].GetComponent<Make_farm>().plant_list[1] = int.Parse(plants[(i * 4)+1]);
                farm_spot[i].GetComponent<Make_farm>().plant_list[2] = int.Parse(plants[(i * 4)+2]);
                farm_spot[i].GetComponent<Make_farm>().plant_list[3] = int.Parse(plants[(i * 4)+3]);
            }
        }
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

        //히든 동물
        for (int i = 0; i < PlayerPrefs.GetInt("lions"); i++)
        {
            GameObject.Find("hidden1").GetComponent<Livestock>().add_lion();
            GameObject.Find("hidden1").GetComponent<Hidden_animal>().active=true;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("foxs"); i++)
        {
            GameObject.Find("hidden2").GetComponent<Livestock>().add_fox();
            GameObject.Find("hidden2").GetComponent<Hidden_animal>().active = true;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("dragons"); i++)
        {
            GameObject.Find("hidden3").GetComponent<Livestock>().add_dragon();
            GameObject.Find("hidden3").GetComponent<Hidden_animal>().active = true;
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
        GameObject.Find("hidden1").GetComponent<Livestock>().lions = new List<GameObject>();
        GameObject.Find("hidden2").GetComponent<Livestock>().foxs = new List<GameObject>();
        GameObject.Find("hidden3").GetComponent<Livestock>().dragons = new List<GameObject>();

    }
}
