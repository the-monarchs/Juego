using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range(0f, 30f)]
    public float velocity = 2f;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // dentro de <> llamamos el componente  a  usar 
        rb2d.velocity = Vector2.left * velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {//entra en colision contra un trigger
        if (other.gameObject.tag == "Destroyer") //el other hace referencia a otra cosa con el cual el colidder puede chocar, y como es un tag.
        {
            Destroy(gameObject);
        }

    }
}
