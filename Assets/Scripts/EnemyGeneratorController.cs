using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    public GameObject enemyPrefab; //enlazamos el script con nuestro prefab en unity.
    public float generatorTimer = 1.75f; //Creamos un float con el tiempo de generacion
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // creamos un nuevo metodo
    void CreateEnemy()
    { //instanciamos, El Gameobject, la posicion, y la propiedad por defecto que necesita para instanciar.
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
    public void StartGenerator()
    {//Invocamos repitiendo, El metodo, El tiempo 0, el tiempo de generacion
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);

    }
    public void CancelGenerator(bool clean = false)
    { //para cancelar la invocacion de nuestro metodo
        CancelInvoke("CreateEnemy");
        if (clean)
        {// para borrar a los enemigos de la pantalla cuando player muera
            object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject Enemy in allEnemies)
            {
                Destroy(Enemy);
            }
        }
    }
}
