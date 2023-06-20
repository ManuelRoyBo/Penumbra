using UnityEngine;

public class FireCrystalConsume : MonoBehaviour, ICrystalConsume
{
    [SerializeField] public float DURATION_OF_CRYSTAL { get; private set; }
    [SerializeField] public Color PLAYER_COLOR_AURA { get; private set; }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float abilityDuration = 4f;
    [SerializeField] private float arrivalThreshold = 0.3f;
    [SerializeField] private LayerMask platformLayer;

    private GameObject player;
    private PlayerController playerController;
    private bool isAbilityActive = false;
    private Vector3 abilityTarget;
    private float abilityTimer;
    private BoxCollider2D playerCollider;

    void Start()
    {
        player = GameManager.Instance.Player;
        playerController = player.GetComponent<PlayerController>();
        playerCollider = player.GetComponent<BoxCollider2D>();
        // Destroy(gameObject, DURATION_OF_CRYSTAL);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !isAbilityActive)
        {
            // Activate the ability
            isAbilityActive = true;
            abilityTarget = GetMouseWorldPosition();
            abilityTimer = 0f;

            // Disable the PlayerController script
            playerController.enabled = false;
        }

        if (isAbilityActive)
        {
            // Update the ability timer
            abilityTimer += Time.deltaTime;

            // Calculate the direction from the player to the ability target
            Vector3 direction = abilityTarget - player.transform.position;
            direction.Normalize(); 

            // Perform boxcast and check for collisions with platforms
            if (BoxCastCollisionCheck(player.transform.position, direction))
            {
                // Ability is blocked by a platform, stop the ability
                isAbilityActive = false;

                // Enable the PlayerController script
                playerController.enabled = true;

                return; // Exit the Update function early
            }

            // Move the player towards the ability target
            player.transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check if the player has arrived at the destination
            if (abilityTimer >= abilityDuration || Vector3.Distance(player.transform.position, abilityTarget) <= arrivalThreshold)
            {
                // Deactivate the ability
                isAbilityActive = false;

                // Enable the PlayerController script
                playerController.enabled = true;
            }
        }
    }

    private float GetComponentValue(float value)
    {
        if (value > 0f)
            return 1f;
        else if (value < 0f)
            return -1f;
        else
            return 0f;
    }

    private bool BoxCastCollisionCheck(Vector3 origin, Vector3 direction)
    {
        Vector3 newDirection = new Vector3(GetComponentValue(direction.x), GetComponentValue(direction.y), GetComponentValue(direction.z));
        float raycastLength = 0.1f;//playerCollider.size.magnitude + 0.2f; // Add a small offset for accuracy
        RaycastHit2D hit = Physics2D.BoxCast(origin, playerCollider.size, 0f, newDirection, raycastLength, platformLayer);
        return hit.collider != null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
