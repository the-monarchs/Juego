using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public GameObject game; //lo enlazamos a nuestro canvas en el script
    public GameObject EnemyGenerator;
    private Animator Animacion;
    public AudioClip jumpClip;
    public AudioClip dieClip;
    public AudioClip pointClip;
    private AudioSource audioPlayer;

    void Start()
    {
        Animacion = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying; // Creamos un boleano para saber si esta jugando.
        gamePlaying = game.GetComponent<GameController>().gamestate == GameState.Playing;
        if (gamePlaying && Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))
        {
            updateState("PlayerJump");
            audioPlayer.clip = jumpClip;
            audioPlayer.Play();
        }
    }
    public void updateState(string state = null)
    {
        if (state != null)
        {
            Animacion.Play(state);
            
        }
    }
    /// <summary>
    /// Que pasa cuando nos topamos con un enemigo
    /// </summary>
    /// <param name="other">Un enemigo con una etiqueta de el tipo tal</param>
    private void OnTriggerEnter2D(Collider2D other)
    {//entra en colision contra un trigger
        if (other.gameObject.tag == "Enemy") //el other hace referencia a otra cosa  TAG con el cual el colidder puede chocar, y como es un tag.
        {
            //Debug.Log("Me muero perro"); //Mostramos un mensaje al mommento de chocar contra el Tag enemy.
            updateState("Player_Die");
            game.GetComponent<GameController>().gamestate = GameState.Ended; //para poder llamar al estado de juego aca
            //tuve que hacer que la enum de estados se vuelva publica.
            EnemyGenerator.SendMessage("CancelGenerator", true);//el True es el bool de la variable clean.
            game.SendMessage("ResetTimeScale");



            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();
        }else if (other.gameObject.tag == "Point")
        {
            game.SendMessage("IncreasePoints");
            audioPlayer.clip = pointClip;
            audioPlayer.Play();
        }

    }
}
