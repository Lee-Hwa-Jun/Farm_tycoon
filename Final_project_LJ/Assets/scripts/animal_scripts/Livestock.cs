﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livestock : MonoBehaviour
{
    public List<GameObject> chickens = new List<GameObject>();
    public List<GameObject> cows = new List<GameObject>();
    public List<GameObject> pigs = new List<GameObject>();
    public List<GameObject> lions = new List<GameObject>();
    public List<GameObject> foxs = new List<GameObject>();
    public GameObject chicken,cow,pig,lion,fox;
    public GameObject animal_spot;

    private AudioSource sound;
    public AudioClip chicken_sound, cow_sound, lion_sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void add_chicken()
    {
        if (pigs.Count == 0 && cows.Count == 0)
        {
            if (chickens.Count < 15)
            {
                if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= 5000)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= 5000;
                    Vector3 animal_spot_p = animal_spot.transform.position;
                    GameObject _obj = Instantiate(chicken, animal_spot_p, Quaternion.identity) as GameObject;
                    chickens.Add(_obj);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("닭이 구매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people += 0.01f;
                }
            }
            else
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("우리가 꽉 찼습니다.");
        }
        else
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("다른 동물이 있습니다.");
    }
    public void add_cow()
    {
        if (pigs.Count == 0 && chickens.Count == 0)
        {
            if (cows.Count < 2)
            {
                if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= 20000)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= 20000;
                    Vector3 animal_spot_p = animal_spot.transform.position;
                    GameObject _obj = Instantiate(cow, animal_spot_p, Quaternion.identity) as GameObject;
                    cows.Add(_obj);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("젖소가 구매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people += 0.06f;
                }
            }
            else
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("우리가 꽉 찼습니다.");
        }
        else
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("다른 동물이 있습니다.");
    }
    public void add_pig()
    {
        if (chickens.Count == 0 && cows.Count == 0)
        {
            if (pigs.Count < 4)
            {
                int price = GameObject.Find("Body").GetComponent<Pig_price>().baby;
                if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= price)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= price;
                    Vector3 animal_spot_p = animal_spot.transform.position;
                    GameObject _obj = Instantiate(pig, animal_spot_p, Quaternion.identity) as GameObject;
                    pigs.Add(_obj);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돼지가 구매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people += 0.06f;
                }
            }
            else
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("우리가 꽉 찼습니다.");
        }
        else
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("다른 동물이 있습니다.");
    }

    public void add_lion()
    {
        if (lions.Count < 5)
        {
            if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= 100000)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= 100000;
                Vector3 animal_spot_p = animal_spot.transform.position;
                GameObject _obj = Instantiate(lion, animal_spot_p, Quaternion.identity) as GameObject;
                lions.Add(_obj);
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("사자가 구매되었습니다.");
                GameObject.Find("MAP").GetComponent<person_manage>().people += 0.06f;
            }
        }
        else
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("우리가 꽉 찼습니다.");
    }
    public void add_fox()
    {
        if (foxs.Count < 9)
        {
            if (GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] >= 70000)
            {
                GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] -= 70000;
                Vector3 animal_spot_p = animal_spot.transform.position;
                GameObject _obj = Instantiate(fox, animal_spot_p, Quaternion.identity) as GameObject;
                foxs.Add(_obj);
                GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("여우가 구매되었습니다.");
                GameObject.Find("MAP").GetComponent<person_manage>().people += 3 ;
            }
        }
        else
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("우리가 꽉 찼습니다.");
    }

    public void del_animal(int type)
    {
        switch (type){
            case 1:
                if (chickens.Count >= 1)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += 5000;
                    Destroy(chickens[0]);
                    chickens.RemoveAt(0);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("닭이 판매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people -= 0.01f;
                }
                else
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("닭이 없습니다.");
                break;
            case 2:
                if (cows.Count >= 1)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += 20000;
                    Destroy(cows[0]);
                    cows.RemoveAt(0);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("젖소가 판매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people -= 0.06f;
                }
                else
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("젖소가 없습니다.");
                break;
            case 3:
                int price = GameObject.Find("Body").GetComponent<Pig_price>().big;
                if (pigs.Count >= 1)
                {
                    if (pigs[0].GetComponent<Pig_move>().grow == 0)
                    {
                        GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += price;
                        Destroy(pigs[0]);
                        pigs.RemoveAt(0);
                        GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돼지가 판매되었습니다.");
                        GameObject.Find("MAP").GetComponent<person_manage>().people -= 0.06f;
                    }
                    else
                    {
                        GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돼지가 성장이 안되었습니다. 양배추를 먹여주세요");
                    }

                }
                else
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("돼지가 없습니다.");
                break;
            case 4:
                if (lions.Count >= 1)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += 80000;
                    Destroy(lions[0]);
                    lions.RemoveAt(0);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("사자가 판매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people -= 0.06f;
                }
                else
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("사자가 없습니다.");
                break;
            case 5:
                if (foxs.Count >= 1)
                {
                    GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += 50000;
                    Destroy(foxs[0]);
                    foxs.RemoveAt(0);
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("여우가 판매되었습니다.");
                    GameObject.Find("MAP").GetComponent<person_manage>().people -= 3;
                }
                else
                    GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("여우가 없습니다.");
                break;
        }
    }

    public void animal_sound()
    {
        if (chickens.Count != 0)
        {
            sound.clip = chicken_sound;
            sound.Play();
        }
        else if (cows.Count != 0)
        {
            sound.clip = cow_sound;
            sound.Play();
        }
        else if (lions.Count != 0)
        {
            sound.clip = lion_sound;
            sound.Play();
        }
        else sound.Stop();
    }
}