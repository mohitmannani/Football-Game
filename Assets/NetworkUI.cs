using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;



public class NetworkUI : NetworkBehaviour
{

    [SerializeField] private TextMeshProUGUI playersCount;
    private NetworkVariable<int> playersNUM = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        playersCount.text = "Players : " + playersNUM.Value.ToString();
        if (!IsServer) return;
        playersNUM.Value = NetworkManager.Singleton.ConnectedClients.Count;
       
    }
}
