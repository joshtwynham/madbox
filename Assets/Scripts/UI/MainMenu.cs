using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    // Start is called before the first frame update
    void Start()
    {
        _startGameButton = GetComponentInChildren<Button>();
        _startGameButton.onClick.AddListener(HandleStartButtonClicked);
    }

    private void HandleStartButtonClicked()
    {
        gameObject.SetActive(false);


        UIManager.Instance.SetDummyCameraActive(false);

        GameManager.Instance.StartGame();
    }
}
