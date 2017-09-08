using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkingHUD : MonoBehaviour
{

    public NetworkManager _Manager;

    public Button ButtonServer;
    public Button ButtonClient;
    public Button ButtonHost;

    void Awake()
    {
        _Manager = GetComponent<NetworkManager>();

        ButtonServer = transform.Find("Button.Server").GetComponent<Button>();
        ButtonClient = transform.Find("Button.Client").GetComponent<Button>();
        ButtonHost = transform.Find("Button.Host").GetComponent<Button>();
    }

    void Start()
    {
        ButtonServer.onClick.AddListener(StartServer);
        ButtonClient.onClick.AddListener(StartClient);
        ButtonHost.onClick.AddListener(StartHost);
    }

    void Update()
    {

    }

    public void StartServer()
    {
        _Manager.StartServer();
    }

    public void StartClient()
    {
        _Manager.StartClient();

        SceneManager.LoadScene("antworld", LoadSceneMode.Single);
    }

    public void StartHost()
    {
        _Manager.StartHost();

        SceneManager.LoadScene("antworld", LoadSceneMode.Single);
    }
}
