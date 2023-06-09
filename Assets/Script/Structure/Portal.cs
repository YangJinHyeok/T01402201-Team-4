using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Q) && GameEffects.portalCooltime >= 3.0f)
        {
            gameController.GetComponent<GameEffects>().teleport(transform.gameObject);
            
        }
    }
}
