using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;

    Player rwPlayer;

    [SerializeField]
    WeaponHolder holder;

    [SerializeField]
    int diviser = 1;

    float inputLag = 0;

	private void Start()
	{
        rwPlayer = ReInput.players.GetPlayer(0);
        ModifierManager.Instance.OnPickedModifier += AddInputLag;
    }

    private void OnDisable()
    {
        ModifierManager.Instance.OnPickedModifier -= AddInputLag;
    }

    void AddInputLag(int id )
    {
        if ( id == 2)
        {
            inputLag += 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {      
        Vector2 output = new Vector2();
        output.x = rwPlayer.GetAxis("MoveX");
        output.y = rwPlayer.GetAxis("MoveY");
        DOTween.Sequence().AppendInterval(inputLag).AppendCallback(() => controller.Translate(output)).SetAutoKill(true).Play();

        Vector2 mousePos = Input.mousePosition;

        if ( Camera.main != null)
        {
            Vector3 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            DOTween.Sequence().AppendInterval(inputLag).AppendCallback(() => controller.SetupLookAt(finalPos)).SetAutoKill(true).Play();
        }

        if (rwPlayer.GetButtonDown("Fire"))
        {
            DOTween.Sequence().AppendInterval(inputLag).AppendCallback(() => holder.StartFire()).SetAutoKill(true).Play();
        }

        else if (rwPlayer.GetButtonUp("Fire"))
        {
            DOTween.Sequence().AppendInterval(inputLag).AppendCallback(() => holder.EndFire()).SetAutoKill(true).Play();
        }
    }
}
