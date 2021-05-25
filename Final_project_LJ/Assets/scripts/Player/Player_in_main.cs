using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_in_main : MonoBehaviour
{
    public Transform camera;
    public Image img;
    private Color img_color;
    public GameObject car;

    private bool ishighlight = false;

    public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  
    public Image fadeImage;            // UGUI의 Image컴포넌트 참조 변수.  
    private float start = 1f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;            // Mathf.Lerp 메소드의 시간 값. 

    private AudioSource click;

    void Start()
    {
        click = GetComponent<AudioSource>();
        img_color = img.color;
    }
    void Update()
    {
        PlayFadeIn();
        RaycastHit hit;

        Physics.Raycast(camera.position, camera.forward, out hit, 10);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("car"))
            {
                img.color = new Color(0, 0, 255);
                ishighlight = true;
            }
            else
                ishighlight = false;
        }
        else
        {
            ishighlight = false;
            img.color = img_color;
        }

        if (Input.GetMouseButtonDown(0))
        {
            click.Play();
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("car")&&ishighlight)
                {
                    car.GetComponent<Car_move>().ShowOverheadView();
                }
            }

        }
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


}
