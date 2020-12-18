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
    public float WalkAcceleration;
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

    public bool IsAiming => isAiming;
    
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

    public Vector3 GetMouseLookDirectionWeapon()
    {
        var weaponScreenPos = camera.WorldToScreenPoint(PlayerTransformRotation.position + Vector3.up * weaponController.transform.position.y);
        var playerScreenPosV3 = new Vector3(weaponScreenPos.x, 0, weaponScreenPos.y);
        var lookVectorV3 = new Vector3(lookVector.x, 0, lookVector.y);
        
        return (lookVectorV3 - playerScreenPosV3);
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
        var lookDir = GetMouseLookDirectionRelative();
        lookDir.y = 0;
        lookDir = lookDir.normalized;
        
        var angle = Vector3.Lerp(PlayerTransformRotation.forward, lookDir, RotationSpeed * deltaTime);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformRotation.position, PlayerTransformRotation.position + lookDir, Color.red);
#endif

        PlayerTransformRotation.LookAt(PlayerTransformRotation.position + angle);
    }

    private void UpdateMovement(float deltaTime)
    {
        var v3_mov = new Vector3(moveVector.x, 0, moveVector.y);
        var speed = WalkSpeed * deltaTime;
        
        translateMov = Vector3.Lerp(translateMov, v3_mov, speed * WalkAcceleration / Vector3.Distance(translateMov, v3_mov));
        PlayerTransformMovement.Translate(translateMov * speed, Space.World);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + v3_mov, Color.green);
        Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + translateMov, Color.blue);
#endif

        relativeMove = PlayerTransformRotation.InverseTransformVector(translateMov);
    }

    public Vector3 GetMouseLookDirection()
    {
        var playerScreenPos = camera.WorldToScreenPoint(PlayerTransformRotation.position + Vector3.up * Height);
        var playerScreenPosV3 = new Vector3(playerScreenPos.x, 0, playerScreenPos.y);
        var lookVectorV3 = new Vector3(lookVector.x, 0, lookVector.y);
        
        return (lookVectorV3 - playerScreenPosV3).normalized;
    }
    
    public Vector3 GetMouseLookDirectionRelative()
    {
        var lookVectorV3 = GetRelativeMousePosition();
        
        return (lookVectorV3 - (PlayerTransformRotation.position + Vector3.up * Height)).normalized;
    }

    public Vector3 GetRelativeMousePosition()
    {
        var ray = camera.ScreenPointToRay(lookVector);
        Vector3 position;
        if (Physics.Raycast(ray, out var hitInfo, 100, Tools.LayerHelper.PointerHitLayer))
        {
            var hitPointToCameraV3 = camera.transform.position - hitInfo.point;
            var hitPointToHeightLength = (Height * hitPointToCameraV3.magnitude / camera.transform.position.y);
            position = hitInfo.point + hitPointToCameraV3.normalized * hitPointToHeightLength;
        }
        else position = PlayerTransformRotation.forward;
        
        return position;
    }
}
