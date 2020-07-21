using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NrealPlayerSetup : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject FPSCamera;

    [SerializeField]
    TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {

        if (photonView.IsMine)
        {
            transform.GetComponent<NrealMovementController>().enabled = true;
            Camera[] cams = this.GetComponentsInChildren<Camera>();
            foreach (Camera c in cams)
            {
                c.enabled = true;
            }
        }
        else
        {
            transform.GetComponent<NrealMovementController>().enabled = false;
            Camera[] cams = this.GetComponentsInChildren<Camera>();
            foreach (Camera c in cams)
            {
                c.enabled = false;
            }

        }
        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;

        }
    }
}
