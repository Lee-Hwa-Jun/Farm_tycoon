using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Car_move : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    private Vector3 parking;
    private bool ready = false;
    private float speed = 5.0f;

    public GameObject canvas;
    public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  
    public Image fadeImage;            // UGUI의 Image컴포넌트 참조 변수.  
    private float start = 0f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 1f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;
    public float fade_timing;

    private AudioSource drive;
    private float isdrive_start = 2f;

    void Start()
    {
        parking =this.transform.position;
        drive = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (ready)
        {
            isdrive_start -= Time.deltaTime;
        }

        //자동차 움직임 구현
        if (ready && isdrive_start < 0)
        {

            Vector3 dir = this.transform.forward;
            this.transform.position += dir * Time.deltaTime * speed;
            fade_timing -= Time.deltaTime;

            if (fade_timing < 0)
            {
                canvas.SetActive(true);
                //Fade
                time += Time.deltaTime / animTime;
                // Image 컴포넌트의 색상 값 읽어오기.  
                Color color = fadeImage.color;
                // 알파 값 계산.  
                color.a = Mathf.Lerp(start, end, time);
                // 계산한 알파 값 다시 설정.  
                fadeImage.color = color;
            }
        }

        //마우스 클릭시 body카메라로 전환 및 car의 위치 제자리
        if (Input.GetMouseButtonDown(0))
        {
            drive.Stop();
            canvas.SetActive(false);
            this.transform.position = parking;
            ShowFirstPersonView();
            time = 0;
            isdrive_start = 2f;
            ready = false;
        }
        // 경과 시간 계산.  
        // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  

    }

    //car의 3인칭 카메라로 넘어가기 
    public void ShowOverheadView()
    {
        drive.Play();
        ready = true;
        firstPersonCamera.enabled = false;
        firstPersonCamera.gameObject.SetActive(false);
        overheadCamera.enabled = true;
    }

    //body카메라로 넘어가기
    public void ShowFirstPersonView()
    {
        firstPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }


    //다른씬으로 넘어가기 위한 충돌처리
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("wall"))
        {
            SceneManager.LoadScene("Main");
        }
        else if (collision.transform.CompareTag("go_farm"))
        {
            SceneManager.LoadScene("ingame");
        }
    }


}
