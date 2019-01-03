using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public float maxFallTime;

    private float fallCounter = 0f;
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (movement.IsFalling())
        {
            fallCounter += Time.deltaTime;
            if (fallCounter >= maxFallTime)
                Die();
        }
        else
        {
            fallCounter = 0f;
        }
    }

    private void Die()
    {
        //Debug.Log("Dead. Load new scene");
        SceneManager.LoadScene("LooseScene", LoadSceneMode.Single);
    }

}
