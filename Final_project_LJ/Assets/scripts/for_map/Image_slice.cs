using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_slice : MonoBehaviour
{
    public RawImage rawImage;
    public Texture[] textures = new Texture[9];
    private int[] pause = { 3, 2, 2, 2, 2, 2, 2, 2,10 };
    private float time;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if(count > 8)
        {
            count = 0;
        }
        rawImage.texture = textures[count];
        if (time >= pause[count])
        {
            
            count++;
            time = 0;
        }
    }
}
