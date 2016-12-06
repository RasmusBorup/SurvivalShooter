using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	public GameObject playerPrefab;
	public GameObject thisPlayer;

	HostData[] hostList;
	bool refreshedServers;
    bool playerSpawned;

	const string typeName = "Reaper's Survival Shooter";
	const string gameName = "Reaper's Shooter";
	// Use this for initialization
	void Start () 
    {
		refreshedServers = false;
        playerSpawned = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

	void OnServerInitialized()
	{
		Debug.Log ("Server Initialized");
	}

	void OnLevelWasLoaded()
	{
        if(Network.isServer && !playerSpawned)
		{
			SpawnPlayer ();
		}
        if (Network.isClient && !playerSpawned)
        {
            SpawnPlayer();
            playerSpawned = true;
        }
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived) 
        {
			hostList = MasterServer.PollHostList ();
			Debug.Log("Hostlist Received Containing " + hostList.Length + "Hosts");
		}
	}

	void OnConnectedToServer()
	{
        Debug.Log("Connected To Server Succesfully");
        if (!playerSpawned)
        {
            SpawnPlayer();
            playerSpawned = true;
        }
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Disconnecting player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) 
    {
        Application.LoadLevel("Main Menu");
        if (Network.isServer)
        {
            Debug.Log("Local server connection disconnected");
        } 
        else if (info == NetworkDisconnection.LostConnection)
        {
            Debug.Log("Lost connection to the server");
        } 
        else
        {
            Debug.Log("Successfully diconnected from the server");
        }
    }

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer && refreshedServers)
		{
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(20, 80 + (30 * i), 160, 30), hostList[i].gameName))
					{
						Connect(hostList[i]);
					}
				}
			}
		}
	}

	public void HostServer()
	{
		Application.LoadLevel ("Level 01");
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(3, 25000, useNat);
		MasterServer.RegisterHost (typeName, gameName);
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList (typeName);
		refreshedServers = true;
	}

	public void Connect(HostData hostData)
	{
		Network.Connect (hostData);
		Application.LoadLevel ("Level 01");
	}

	void SpawnPlayer()
	{
		thisPlayer = (GameObject)Network.Instantiate (playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation, 0);
		GameObject.Find ("Main Camera").GetComponent<CameraFollow> ().target = thisPlayer.transform;
	}
}
