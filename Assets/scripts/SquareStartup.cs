using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SquareStartup : MonoBehaviour
{
    private GameController _gameControllerObject;

    // Start is called before the first frame update
    private void Start()
    {
        _gameControllerObject = FindObjectOfType<GameController>();
        TextMeshProUGUI textField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _gameControllerObject.RegisterSquareOnStartup(gameObject.GetComponent<Button>(), textField);
    }
}
