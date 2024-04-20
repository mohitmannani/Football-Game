using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;


public class playerNetwork : NetworkBehaviour
{
   
    private readonly NetworkVariable<Vector3> __netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private readonly NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsOwner)
        {
            // Handle input from the client
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Calculate movement direction based on input
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

            // Update position on the server
            _rigidbody.MovePosition(_rigidbody.position + movement * Time.deltaTime);

            // Update rotation on the server
            _rigidbody.MoveRotation(Quaternion.Euler(0f, moveHorizontal * 90f, 0f));
        }
        else
        {
            // Update position and rotation on the client based on server values
            transform.position = __netPos.Value;
            transform.rotation = _netRot.Value;
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            // Enable input on the client
            enabled = true;
        }
        else
        {
            // Disable input on non-owning clients
            enabled = false;
        }
    }
}

