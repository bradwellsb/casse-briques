using UnityEngine;

public class NewGameListener : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<GameManager>().NewGame();
        }
    }
}
