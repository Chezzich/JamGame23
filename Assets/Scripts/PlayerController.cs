using Cinemachine;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private bool busy = false;

    private void Start()
    {
        PublicVars.playerController = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        movement.x = busy ? 0 : Input.GetAxisRaw("Horizontal");
        movement.y = busy ? 0 : Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.E)
            {
                Click(GetPlayerCellPosition());
            }
        }
    }

    private void Click(Vector3Int playerPosition)
    {
        if (PublicVars.activeNpc != null)
        {
            var uiPos = mainCamera.WorldToScreenPoint(PublicVars.activeNpc.gameObject.transform.position);
            PublicVars.uiManager.ShowDialogueAtPos(new Vector3(uiPos.x, uiPos.y + 50), PublicVars.questManager.GetCurrentQuest().GetCurrentDialogueName());
            return;
        }
    }

    private Vector3Int GetPlayerCellPosition()
    {
        var map = PublicVars.tilemapsHolder.GetTilemapByName(TilemapName.Ground);
        if (map)
        {
            return map.LocalToCell(rb.position);
        }
        return Vector3Int.zero;
    }

    public void SetBusy(bool isBusy)
    {
        busy = isBusy;
    }
}
