using Assets.Scripts;
using Assets.Scripts.Input;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Transform PlayerTransformMovement;
    public Transform PlayerTransformRotation;
    public Rigidbody PlayerRigidbody;

    [Header("Stats")] // TODO: move to config
    public float WalkSpeed;
    public float RotationSpeed;
    public float BodyRotationAngle;
    public float Height = 1.65f;

    private Vector2 moveVector;
    private Vector2 lookVector;
    private Vector3 translateMov;
    private Vector3 relativeMove;

    private Camera camera;
    private bool isAiming;
    private bool isFiring;
    private bool isMoving;
    private bool isCrouching;
    private int currentWeapon;
    private Vector2 v2_zero;
    private PlayerWeaponController weaponController;

    public Vector3 GetPlayerMoveVector => relativeMove;
    public InputController Input => GameEnterPoint.Instance?.InputController;

    private void Awake()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();

        Input.OnMove += MoveHandler;
        Input.OnLook += LookHandler;
        Input.OnAim += AimHandler;
        Input.OnFire += FireHandler;
        Input.OnCrouch += CrouchHandler;
        Input.OnMelee += MeleeHandler;
        Input.OnWeaponChange += WeaponChangeHandler;
        camera = Camera.main;
        v2_zero = Vector2.zero;
        weaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    private void Update()
    {
        UpdateAnimatorTranslate();
    }

    private void LateUpdate()
    {
        if (isAiming)
        {
            var lookDir = GetMouseLookDirection();
            var angle = Vector3.Lerp(PlayerTransformRotation.forward, lookDir, RotationSpeed * Time.deltaTime);

            weaponController.SetLook(GetMouseLookForward());
        }
    }

    private void OnEnable()
    {
        Input?.InputActions.Enable();
        SetWeapon(0);
    }

    private void OnDisable()
    {
        Input?.InputActions.Disable();
    }

    private void OnDestroy()
    {
        if (Input != null)
        {
            Input.OnMove -= MoveHandler;
            Input.OnLook -= LookHandler;
            Input.OnAim -= AimHandler;
            Input.OnFire -= FireHandler;
        }
    }

    private void SetWeapon(int weaponIndex)
    {
        Animator.SetInteger("Weapon", weaponIndex);
    }

    private void MoveHandler(Vector2 moveVector)
    {
        this.moveVector = moveVector;
        isMoving = moveVector != v2_zero;
    }

    private void LookHandler(Vector2 lookVector)
    {
        this.lookVector = lookVector;
    }

    private void AimHandler(bool isAiming)
    {
        this.isAiming = isAiming;
        Animator.SetBool("IsAiming", isAiming);
    }

    private void FireHandler(bool isFiring)
    {
        this.isFiring = isFiring;
        Animator.SetBool("IsAttacking", isFiring);
    }

    private void CrouchHandler(bool isCrouching)
    {
        this.isCrouching = isCrouching;
        Animator.SetBool("IsCrouching", isCrouching);
    }

    private void MeleeHandler()
    {
        Animator.SetTrigger("IsAttackingMelee");
    }

    private void WeaponChangeHandler(int currentWeapon)
    {
        Animator.SetLayerWeight(this.currentWeapon + 2, 0);
        this.currentWeapon = currentWeapon;
        Animator.SetLayerWeight(this.currentWeapon + 2, 1);
        Animator.SetInteger("Weapon", currentWeapon);
    }

    private void UpdateAnimatorTranslate()
    {
        Animator.SetFloat("MoveX", relativeMove.x);
        Animator.SetFloat("MoveY", relativeMove.z);
    }

    private void FixedUpdate()
    {
        UpdateLook(Time.deltaTime);
        UpdateMovement(Time.deltaTime);
    }

    private void UpdateLook(float deltaTime)
    {
        var lookDir = GetMouseLookDirection();
        var angle = Vector3.Lerp(PlayerTransformRotation.forward, lookDir, RotationSpeed * deltaTime);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformRotation.position, PlayerTransformRotation.position + lookDir, Color.red);
#endif

        PlayerTransformRotation.LookAt(PlayerTransformRotation.position + angle);
    }

    private void UpdateMovement(float deltaTime)
    {
        var v3_mov = new Vector3(moveVector.x, 0, moveVector.y);

        translateMov = Vector3.Lerp(translateMov, v3_mov, (WalkSpeed * deltaTime) / Vector3.Distance(translateMov, v3_mov));
        PlayerTransformMovement.Translate(translateMov * WalkSpeed * deltaTime, Space.World);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + v3_mov, Color.green);
        Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + translateMov, Color.blue);
#endif

        relativeMove = PlayerTransformRotation.InverseTransformVector(translateMov);
    }

    public Vector3 GetMouseLookDirection()
    {
        Vector2 playerScreenPos = camera.WorldToScreenPoint(PlayerTransformRotation.position + Vector3.up * Height);
        Vector2 playerLook = (lookVector - playerScreenPos).normalized;

        return new Vector3(playerLook.x, 0, playerLook.y);
    }

    public Vector3 GetMouseLookForward()
    {
        Vector2 playerScreenPos = camera.WorldToScreenPoint(PlayerTransformRotation.position + Vector3.up * Height);
        float lookLength = (lookVector - playerScreenPos).magnitude;

        return PlayerTransformRotation.forward * lookLength + Vector3.up * Height;
    }
}
