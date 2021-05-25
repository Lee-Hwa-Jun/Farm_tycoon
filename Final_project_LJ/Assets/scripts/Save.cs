using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public int[] farm_list = new int[15];
    public int[] property_int = new int[] { 0, 0, 0, 0, 0, 0, 0 };
    float timer = 0.0f;
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
        property_int = GameObject.Find("Body").GetComponent<PlayerMove>().property_int;
        string SaveArr = ""; // 문자열 생성

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
    }
    public void CallData()
    {
        string[] dataArr = PlayerPrefs.GetString("Data").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장

        if (dataArr.Length != 1)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }
        }


        Debug.Log("Called");
    }
    
}
