using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Range(0f,0.20f)]
    public  float parallaxSpeed = 0.02f;
    public RawImage backgroung;
    public RawImage plataform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        backgroung.uvRect = new Rect(backgroung.uvRect.x + finalSpeed,0f,1f,1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed ,0f,1f,1f);
    }
}
