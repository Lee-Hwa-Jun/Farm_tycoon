using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hidden_animal : MonoBehaviour
{
    public bool active = false;
    public GameObject menu;
    public Text text;
    public string animal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            text.text = animal;
            menu.SetActive(true);
            active = false;
            GameObject.Find("Body").GetComponent<PlayerMove>().one_time_message("새로운 동물을 만나보세요!");
        }
    }
}
