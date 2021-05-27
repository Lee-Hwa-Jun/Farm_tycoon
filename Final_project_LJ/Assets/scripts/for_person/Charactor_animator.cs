using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor_animator : MonoBehaviour
{
    public Animator animator;
    private GameObject look_spot;
    private int num;
    private float time = 0;
    private float time2 = 0.5f;
    private bool avoid = false;
    private bool idle = false;
    private bool click = false;
    private bool iswave = false;
    public GameObject canvas;
    public TextMesh text;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("idle", idle);
        //animator.SetTrigger("wave");
    }
    void Start()
    {
        look_spot = GameObject.Find("look0").gameObject;
        num = Random.Range(12,20);
        this.transform.LookAt(look_spot.transform);

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //서로 충돌시 피하기
        if (avoid && time2 >= 0)
        {
            //서 있던 도중(앞으로 조금 간다.)
            if (idle == true)
            {
                animator.SetBool("idle", false);
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                this.transform.position += this.transform.forward * Time.deltaTime * 2f;
                time2 -= Time.deltaTime;
            }
            //가던 도중(잠깐 멈춘다.)
            if (idle == false)
            {
                animator.SetBool("idle", true);
                time2 -= Time.deltaTime;
            }

        }
        //클릭 되었을때
        else if (click&& time2 >= 0)
        {
            
            canvas.SetActive(true);
            animator.SetBool("idle", true);
            this.transform.LookAt(GameObject.Find("Body").transform);
            if (iswave == false)
            {
                text_change();
                animator.SetBool("idle", false);
                animator.SetTrigger("wave");
            }
            iswave = true;
            time2 -= Time.deltaTime;
        }
        //원래상태로 돌리기
        else
        {
            canvas.SetActive(false);
            animator.SetBool("idle", idle);
            time2 = 2.0f;
            avoid = false;
            click = false;

            //구경하러가기
            if (time < num)
            {
                this.transform.LookAt(look_spot.transform);
                this.transform.localRotation = Quaternion.Euler(0, -90, 0);
                this.transform.position += this.transform.forward * Time.deltaTime * 2f;

            }
            //구경하기
            if (time > num)
            {
                idle = true;
                animator.SetBool("idle", idle);
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //돌아가기
            if (time > 2 * num)
            {
                idle = false;
                animator.SetBool("idle", idle);
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
                this.transform.position += this.transform.forward * Time.deltaTime * 2f;
            }
        }


    }
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.transform.tag == "wall") //2000원을 주고 퇴장
        {
            GameObject.Find("Body").GetComponent<PlayerMove>().property_int[0] += 2000; 
            Destroy(this.gameObject);
        }
        if(collision.transform.tag == "person")
        {
            avoid = true;
        }
    }
    public void click_me()
    {
        click = true;
    }
    private void text_change()
    {
        string[] texts = { "너무 재밌어요!", "또 왔어요!", "안녕하세요!", "놀러왔어요!", "심심해요", "다음에 또 올게요!" };
        text.text = texts[Random.Range(0,6)];
    }
}
