using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SquareStartup : MonoBehaviour
{
    private GameController GameControllerObject;

    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject = GameObject.FindObjectOfType<GameController>();
        if (GameControllerObject == null)
        {
            Debug.Log("did not find controller object");
        }
        else
        {
            TextMeshProUGUI textField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            GameControllerObject.RegisterSquareOnStartup(gameObject.GetComponent<Button>(), textField);
        }
    }
}
