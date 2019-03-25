using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{

    public Food food;

    public GameObject tailPrefab;

    List<Transform> tail = new List<Transform>();

    bool ate = false;

    Vector2 dir = Vector2.right;

    int scoreCount = 0;
    public Text scoreText;

    public Image gameOverScreen;
    public Text gameOverText;
    

    // Use this for initialization
    void Start()
    {
        gameOverScreen.enabled = false;
        gameOverText.enabled = false;
        

        food = FindObjectOfType(typeof(Food)) as Food;

        scoreText.text = scoreCount.ToString();

        InvokeRepeating("Move", 0.3f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow) && dir != Vector2.left)
        {
            dir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && dir != Vector2.right)
        {
            dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && dir != Vector2.down)
        {
            dir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && dir != Vector2.up)
        {
            dir = Vector2.down;
        }
    }

    void Move()
    {
        Vector2 v = transform.position;

        transform.Translate(dir);


        if (ate)
        {
            GameObject g = Instantiate(tailPrefab, v, Quaternion.identity); // Instancia uma "cauda"

            tail.Insert(0, g.transform); // Insere a instância no index 0 do transform relacionado a list tail

            Debug.Log("Tail count is: " + tail.Count); // Conferir o tamanho da tail list

            ate = false;
        }
        else if (tail.Count > 0)
        {

            tail.Last().position = v;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.StartsWith("foodPrefab"))
        {
            ate = true;

            scoreCount++;
            scoreText.text = scoreCount.ToString();

            Destroy(col.gameObject);

            food.Spawn();
        }
        else
        {
            Time.timeScale = 0;

            IsGameOver();
            
        }

        
        if (col.gameObject.tag == "tail")
        {
            Debug.Log("Colidiu com o tail");
        }       
    }

    void IsGameOver()
    {
        gameOverScreen.enabled = true;
        gameOverText.enabled = true;
        
    }

    void Reboot()
    {
        SceneManager.LoadScene("Gameplay");
    }

    
}
