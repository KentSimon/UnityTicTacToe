using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquareStartup : MonoBehaviour
{
    private GameController GameControllerObject;

    // Start is called before the first frame update
    private void Start()
    {
        GameControllerObject = FindObjectOfType<GameController>();
        TextMeshProUGUI textField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        GameControllerObject.RegisterSquareOnStartup(gameObject.GetComponent<Button>(), textField);
    }
}
