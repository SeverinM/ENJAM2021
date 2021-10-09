using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;

    [SerializeField]
    Player rwPlayer;

    [SerializeField]
    WeaponHolder holder;

	private void Start()
	{
        rwPlayer = ReInput.players.GetPlayer(0);
    }

	// Update is called once per frame
	void Update()
    {      
        Vector2 output = new Vector2();
        output.x = rwPlayer.GetAxis("MoveX");
        output.y = rwPlayer.GetAxis("MoveY");
        controller.Translate(output);

        Vector2 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        controller.SetupLookAt(worldMousePos);

        if (rwPlayer.GetButtonDown("Fire"))
            holder.StartFire();

        else if (rwPlayer.GetButtonUp("Fire"))
            holder.EndFire();
    }
}
