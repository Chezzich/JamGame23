using UnityEngine;

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
    private bool isIntro = false;
    private bool canQuit = false;

    private float dialogueCheckTimer = 999f;
    private Vector3 defaultPos;

    private void Start()
    {
        PublicVars.playerController = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        defaultPos = gameObject.transform.position;

        var uiPos = mainCamera.WorldToScreenPoint(gameObject.transform.position);
        PublicVars.uiManager.ShowDialogueAtPos(new Vector3(uiPos.x, uiPos.y + 50), "Intro");
        isIntro = true;
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

        if (dialogueCheckTimer > 2f && !busy)
        {
            dialogueCheckTimer = 0;
            var uiPos = mainCamera.WorldToScreenPoint(gameObject.transform.position);
            PublicVars.uiManager.ShowPlayerDialogueIfNeeded(new Vector3(uiPos.x, uiPos.y + 50));
        }

        dialogueCheckTimer += Time.deltaTime;
    }

    private void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.E)
            {
                if (canQuit)
                {
                    Application.Quit();
                }
                if (PublicVars.questManager.GetCurrentQuest().questData.Name != "EndGame")
                    Click(GetPlayerCellPosition());
            }
        }
    }

    private void Click(Vector3Int playerPosition)
    {
        if (isIntro)
        {
            var uiPos = mainCamera.WorldToScreenPoint(gameObject.transform.position);
            PublicVars.uiManager.ShowDialogueAtPos(new Vector3(uiPos.x, uiPos.y + 50), "Intro");
            return;
        }

        if (PublicVars.activeNpc != null)
        {
            var uiPos = mainCamera.WorldToScreenPoint(PublicVars.activeNpc.gameObject.transform.position);
            PublicVars.uiManager.ShowDialogueAtPos(new Vector3(uiPos.x, uiPos.y + 50), PublicVars.questManager.GetCurrentQuest().GetCurrentDialogueName());
            return;
        }

        if (PublicVars.uiManager.IsActiveDialogue())
        {
            PublicVars.uiManager.ShowDialogueAtPos(Vector3.zero, PublicVars.questManager.GetCurrentQuest().GetCurrentDialogueName());
        }

        if (PublicVars.questManager.GetCurrentQuest().IsStartDialogueShowed() && PublicVars.questManager.GetCurrentQuest().questData.NeedCrops)
        {
            PublicVars.farmingController.CheckCell(playerPosition);
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

    public void SetIsIntro(bool intro)
    {
        isIntro = intro;
    }

    public void SetCanQuit(bool quit)
    {
        canQuit = quit;
    }

    public void ResetPos()
    {
        gameObject.transform.position = defaultPos;
    }
}
