using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 5f;       // Speed at which the head bobs
   
   [Range(0f, 1f)]
    public float bobAmount = 0.1f;    // Amount by which the head bobs
    public Transform playerCamera;    // Reference to the player's camera

    private float defaultYPos = 0;
    private float timer = 0;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
        defaultYPos = playerCamera.localPosition.y;
    }

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobSpeed * Time.deltaTime;

            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobAmount;
            float totalAxes = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            translateChange *= totalAxes;
            Vector3 localPosition = playerCamera.localPosition;
            localPosition.y = defaultYPos + translateChange;
            playerCamera.localPosition = localPosition;
        }
        else
        {
            Vector3 localPosition = playerCamera.localPosition;
            localPosition.y = defaultYPos;
            playerCamera.localPosition = localPosition;
        }
    }
}
