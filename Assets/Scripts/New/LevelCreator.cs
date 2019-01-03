using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public BoxCollider2D initialPlatform;
    public BoxCollider2D[] platformPrefabs;

    private float minWidth;
    private float maxWidth;
    private Transform player;
    private float jumpWidth;
    private float jumpHeight;
    private float distanceToPlayer;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        //InitJumpDistance();
        InitDistanceToPlayer();
    }

    private void Update()
    {
        CreateLevel();
    }

    public float GetMinPlatformWidth()
    {
        return minWidth;
    }

    public float GetMaxPlatformWidth()
    {
        return maxWidth;
    }

    public void DifficultySwitched(
        float newMinWidth,
        float newMaxWidth
        )
    {
        minWidth = newMinWidth;
        maxWidth = newMaxWidth;
        InitJumpDistance();
    }

    private void InitJumpDistance()
    {
        GameObject player = GameObject.Find("Player");
        Movement playerMovement = player.GetComponent<Movement>();
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        BoxCollider2D boxCollider = player.GetComponent<BoxCollider2D>();

        float a1 = playerMovement.jumpSpeed;
        float d = Physics2D.gravity.y * playerRigidbody.gravityScale * Time.fixedDeltaTime;
        float n = Mathf.Ceil(a1 / -d);
        float initialHeight = playerMovement.jumpSpeed * playerMovement.maxJumpTime;

        jumpHeight = initialHeight + ((2 * a1 + d * (n - 1)) / 2 * n) * Time.fixedDeltaTime - boxCollider.bounds.extents.y;
        jumpWidth = GetJumpStepsCount(initialHeight, playerMovement.jumpSpeed, d) * playerMovement.runSpeed * Time.fixedDeltaTime + boxCollider.bounds.extents.x;

        jumpHeight *= 0.9f;
    }

    private float GetJumpStepsCount(float initialHeight, float initialSpeed, float speedStep)
    {
        int counter = 0;
        while (initialHeight > 0)
        {
            initialHeight += initialSpeed * Time.fixedDeltaTime;
            initialSpeed += speedStep;
            counter++;
        }
        return counter;
    }

    private void InitDistanceToPlayer()
    {
        CameraInfo cameraInfo = GameObject.Find("Main Camera").GetComponent<CameraInfo>();
        distanceToPlayer = cameraInfo.GetSize().x * 1.5f;
    }

    private void CreateLevel()
    {
        Vector2 platformEnd = initialPlatform.bounds.center + initialPlatform.bounds.extents;
        if (platformEnd.x - player.position.x <= distanceToPlayer)
        {
            BoxCollider2D platformPrefab = GetRandomPrefab();
            Vector2 distance = GetRandomDistance();
            float width = Random.Range(minWidth, maxWidth);
            Vector2 platformPosition = platformEnd + distance;
            platformPosition.x += width / 2f;
            platformPosition.y -= platformPrefab.bounds.size.y;

            initialPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            initialPlatform.transform.localScale = new Vector2(
                width / initialPlatform.bounds.size.x,
                initialPlatform.transform.localScale.y
                );
        }
    }

    private Vector2 GetRandomDistance()
    {
        float widthCoefficient = Random.Range(0.1f, 0.9f);
        float heightCoefficient = 1 - widthCoefficient;
        return new Vector2(
            jumpWidth * widthCoefficient,
            jumpHeight * heightCoefficient * (Random.Range(0, 2) == 0 ? 1 : -1) - initialPlatform.size.y / 2f
            );
    }

    private BoxCollider2D GetRandomPrefab()
    {
        return platformPrefabs[Random.Range(0, platformPrefabs.Length)];
    }

}
