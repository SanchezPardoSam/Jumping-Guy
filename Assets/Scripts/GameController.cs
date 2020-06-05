using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState {Idle,Playing,Ended,Ready};
public class GameController : MonoBehaviour
{

    [Range(0f,0.20f)]
    public  float parallaxSpeed = 0.02f;
    public RawImage backgroung;
    public RawImage plataform;
    public GameObject uiIdle;
    public GameObject uiScore;   
    public float scaleTime = 8f;
    public float scaleInc = .125f;
    public Text pointsText;
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    private AudioSource musicPlayer;
    private  int points = 0;
    // Start is called before the first frame update
    void Start(){       
        musicPlayer=GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction =Input.GetKeyDown("up")|| Input.GetMouseButtonDown(0);
        if(gameState == GameState.Idle&&userAction){
            gameState = GameState.Playing;
            uiIdle.SetActive(false);            
            uiScore.SetActive(true);
            player.SendMessage("UpdateState","PlayerRun");
            player.SendMessage("DustPlay");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();
            InvokeRepeating("GameTimeScale",scaleTime,scaleTime);      
        }
        else if(gameState == GameState.Playing){
            Parallax();
        }   
        else if(gameState == GameState.Ready){
            if(userAction){
                RestartGame();
            }
        }  
    }
    void Parallax(){
        float finalSpeed = parallaxSpeed * Time.deltaTime;
            backgroung.uvRect = new Rect(backgroung.uvRect.x + finalSpeed,0f,1f,1f);
            plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed *4,0f,1f,1f);
    } 
    public void RestartGame(){
        ResetTimeScale();
        SceneManager.LoadScene("SampleScene");
    }
    void GameTimeScale(){
        Time.timeScale += scaleInc;

        Debug.Log("Ritmo incrementado: "+ Time.timeScale.ToString());
    }
    public void ResetTimeScale(float newTimeScale = 1f){
        CancelInvoke("GameTimeScale");
        Time.timeScale = newTimeScale;
        Debug.Log("Ritmo Reestablecido: "+ Time.timeScale.ToString());
    }
    public void IncreasePoints(){        
        pointsText.text = (++points).ToString();
    }
}
