using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { Idle, Playing, Ended }; //parado oo jugando, un enum es una lista de opciones
//public enum GameState { Idle, Playing,Ended }; //parado oo jugando, un enum es una lista de opciones
public class GameController : MonoBehaviour
{
    [Range(0f, 0.30f)] // este es un rango para la velocidad 
    public float parallaxspeed = 0.02f;
    public RawImage Background;
    public RawImage Platform;
    public GameState gamestate = GameState.Idle;
    public GameObject uiIdle;
    public GameObject uiScore;
    public GameObject player;
    public GameObject EnemyGenerator; //Enlazo el game Object, Desde nuestro main Canvas para poder utilizarlo aca.
    private AudioSource MusicPlayer;
    public Text pointstext;
    float scaleTime = 6f;
    float scaleInc = 0.25f;
    private int Point = 0;
    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // empezamos el juego
        // re hacer 2 condicionales If. prepguntamos si estamos quieto, y despues si aprieta una tecla
        if (gamestate == GameState.Idle)
        {
            if (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))
            {
                //tecla flecha arriba / o el boton de el mouse posicion 0       {
                gamestate = GameState.Playing; // el stado de el juego cambio a playing.
                uiIdle.SetActive(false);
                uiScore.SetActive(true);
                player.SendMessage("updateState", "Playerrun");
                //llamamos a la el otro script.
                EnemyGenerator.SendMessage("StartGenerator"); //Llamamos y hacemos que empieze el generador
                MusicPlayer.Play();



                InvokeRepeating("GameTimeScale", scaleTime, scaleTime); // lo llamamos repitiendo cada 6 seg
            }
        }
        else if (gamestate == GameState.Playing)
        {
            parallax();
        }
        else if (gamestate == GameState.Ended)
        {
            if (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
    }
    void parallax()
    { //si estamos jugando, empieza el efecto parallax.  el de movimiento
        float finalspeed;
        finalspeed = parallaxspeed * Time.deltaTime;
        //bakcground, en la propiedad uv rect. velocidad en cada eje, x,Y, Altura y ancho
        Background.uvRect = new Rect(Background.uvRect.x + finalspeed, 0f, 1f, 1f);
        // ahora para la plataforma 
        Platform.uvRect = new Rect(Platform.uvRect.x + finalspeed * 3, 0f, 1f, 1f);
        
    }
    /// <summary>
    /// reinicio de juego
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("escena_1");
    }
    public void GameTimeScale() // aumentamos la velocidad de el juego
    {
        Time.timeScale += scaleInc;
        Debug.Log("ritmo incrementado" + Time.timeScale.ToString());
    }
    public void ResetTimeScale() //para reset 
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = 1f;
        Debug.Log("Ritmo Restablecido"+ Time.timeScale.ToString());
    }
    public void IncreasePoints()
    {
        Point++;
        pointstext.text = Point.ToString();
    }
}
