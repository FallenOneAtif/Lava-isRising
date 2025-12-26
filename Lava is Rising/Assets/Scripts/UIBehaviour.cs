using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    public static UIBehaviour instance {  get; set; }
    public GameObject EndScr;
    public GameObject Player;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    public void EndScreen()
    {
        EndScr.SetActive(true);
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
