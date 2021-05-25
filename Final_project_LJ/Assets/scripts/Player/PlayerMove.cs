using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public Transform camera;
    public float speed;
    public GameObject Texts,one_time_text;
    public GameObject car;
    public string name = "nothing";
    public Image img;
    private Color img_color;
    public int information;

    private bool isMove = false;

    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    private double m_Timer2 = 0;
    private bool ismessage = false;
    private bool ishighlight = false;
    
    public int[] property_int = new int[] { 0, 0, 0, 0, 0 ,0, 0};//자산 리스트 [money,tomatos,cabbages,aggs,milk,baby_pig,big_pig]
    public int Start_Money = 100000000;
    public Text[] property_text = new Text[7];

    public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  
    public Image fadeImage;            // UGUI의 Image컴포넌트 참조 변수.  
    private float start = 1f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;            // Mathf.Lerp 메소드의 시간 값. 

    private AudioSource click;
    private float animal_sound_time = 0;

    void Start()
    {
        click = GetComponent<AudioSource>();
        speed = 10;
        property_int[0] = Start_Money;
        img_color = img.color;
        BringData();
    }
    void Update()
    {
        animal_sound_time += Time.deltaTime;
        PlayFadeIn();
        RaycastHit hit;

        Physics.Raycast(camera.position, camera.forward, out hit, 10);

        //특정물체를 바라볼 때 포인터에 색을 입히는 하이라이트 처리
        if (hit.collider != null)
        {
            if (hit.collider.tag == "button" || hit.collider.tag == "person" || hit.collider.tag == "car")
            {
                img.color = new Color(0, 0, 255);
                ishighlight = true;
            }
            else if (hit.collider.tag == "hidden1" || hit.collider.tag == "hidden2")
            {
                img.color = new Color(0, 255, 0);
                ishighlight = true;
            }
            else
            {
                ishighlight = false;
            }
        }

        else
        {
            img.color = img_color; 
        }
        //자산 업데이트
        for (int i = 0; i < 7; i++)
        {
            property_text[i].text = property_int[i].ToString();
        }

        //원클릭
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            Debug.Log("One Click");
            m_IsOneClick = false;
        }

        //클릭시 tag별 이벤트 콜
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick)
            {
                click.Play();
                m_Timer = Time.time;
                m_IsOneClick = true;
                try
                {
                    if (ishighlight)
                    {
                        click.Play();
                        if (hit.collider.CompareTag("button"))
                        {
                            hit.collider.GetComponent<All_event>().init();
                            information = hit.collider.GetComponent<All_event>().information;
                        }
                        else if (hit.collider.CompareTag("person"))
                        {
                            hit.collider.GetComponent<Charactor_animator>().click_me();
                        }
                        else if (hit.collider.CompareTag("hidden1"))
                        {
                            GameObject.Find("hidden1").GetComponent<Hidden_animal>().active = true;
                        }
                        else if (hit.collider.CompareTag("hidden2"))
                        {
                            GameObject.Find("hidden2").GetComponent<Hidden_animal>().active = true;
                        }
                        if (hit.collider.CompareTag("car"))
                        {
                            car.gameObject.GetComponent<Car_move>().ShowOverheadView();
                        }
                    }
                    else
                        isMove = true;
                }
                catch (NullReferenceException ex)
                {
                    Debug.Log("error");
                    isMove = true;
                }
            }

            //더블클릭
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                click.Play();
                one_time_text.SetActive(false);
                Debug.Log("Double Click");
                m_IsOneClick = false;
                if (Texts.active)
                    Texts.SetActive(false);
                else
                    Texts.SetActive(true);
            }

        }

        //마우스 클릭해제 시 멈춤
        else if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
        }
        
        if (isMove )
        {
            //클릭 시 바라보는 방향으로 이동
            Vector3 dir = camera.forward;
            dir.y = 0;
            this.transform.position += dir * Time.deltaTime * speed;
        }
        else
        {
            //어떤 물체와 계속 닿았을때 이상한 움직임이 발생하여 확실하게 움직임을 멈춰준다.
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        //message 3초 표현
        if(ismessage == true)
            m_Timer2 += Time.deltaTime;
        if(m_Timer2 >= 3.0f)
        {
            ismessage = false;
            one_time_text.SetActive(false);
            m_Timer2 = 0;
        }
    }

    //다른 스크립트에서 발생되는 메세지를 body에 띄워주기 위한 함수
    public void one_time_message(string message)
    {
        one_time_text.SetActive(true);
        Text tmp = one_time_text.GetComponent<Text>();
        tmp.text = message;
        ismessage = true;
    }

    public void BringData()
    {
        GameObject.Find("SaveBtn").GetComponent<Save>().CallData();
    }
    void PlayFadeIn()
    {
        // 경과 시간 계산.  
        // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
        time += Time.deltaTime / animTime;

        // Image 컴포넌트의 색상 값 읽어오기.  
        Color color = fadeImage.color;
        // 알파 값 계산.  
        color.a = Mathf.Lerp(start, end, time);
        // 계산한 알파 값 다시 설정.  
        fadeImage.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("fence"))
        {
            if (animal_sound_time > 5f)
            {
                collision.transform.parent.parent.GetComponent<Livestock>().animal_sound();
                animal_sound_time = 0;
            }
        }
    }
    
}
