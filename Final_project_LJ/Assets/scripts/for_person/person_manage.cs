using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class person_manage : MonoBehaviour
{
    public GameObject person;
    public Transform person_spot;
    private float time = 0.1f;
    public float people;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(person, person_spot.position, Quaternion.identity);
        people = 1;
        //Instantiate(person, person_spot.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= (60/people))
        { 
            Instantiate(person, person_spot.position, Quaternion.identity);
            time = 0.1f;
        }
        Debug.Log("people : " + people.ToString());
        Debug.Log("result : "+(60 / people).ToString());
    }
}
