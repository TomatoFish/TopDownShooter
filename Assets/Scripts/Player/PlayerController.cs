using System;
using Assets.Scripts;
using Assets.Scripts.Input;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Transform PlayerTransformMovement;
    public Transform PlayerTransformRotation;
    public Rigidbody PlayerRigidbody;
    public PlayerWeaponController WeaponController;

    [Header("Stats")] // TODO: move to config
    public float WalkSpeed;
    public float WalkAcceleration;
    public float RotationSpeed;
    public float BodyRotationAngle;
    public float Height = 1.65f;

    private Vector3 relativeMove;

    public Player Player { get; private set; }

    public bool IsAiming => Player.IsAiming;

    public void Init(Player player)
    {
        Player = player;
        player.SetConfig(WalkSpeed, WalkAcceleration, RotationSpeed, BodyRotationAngle, Height);
        EnableActions();
        //WeaponController = GetComponentInChildren<PlayerWeaponController>();
        WeaponController.Init(this);
    }
    
    public Vector3 GetCameraTargetPosition(float maxRadiusSqr)
    {
        if (IsAiming)
            return Player.GetCameraTargetPosition(PlayerTransformRotation, maxRadiusSqr) + PlayerTransformMovement.position;
        else
            return PlayerTransformMovement.position;
    }
    
    public Vector3 GetMouseLookDirection()
    {
        var lookVectorV3 = Player.GetMousePositionRelativeFloor();
        if (lookVectorV3 == Vector3.zero)
        {
            lookVectorV3 = PlayerTransformRotation.forward;
        }
    
        return (lookVectorV3 - PlayerTransformRotation.position).normalized;
    }
    
    private void Awake()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        UpdateAnimatorTranslate();
    }

    private void OnEnable()
    {
        EnableActions();
    }

    private void OnDisable()
    {
        DisableActions();
    }

    private void EnableActions()
    {
        if (Player == null) return;
        
        Player.EnableInput();
        Player.OnItemChanged += ItemChangedHandler;
        Player.OnAim += AimHandler;
        Player.OnFire += FireHandler;
        Player.OnMelee += MeleeHandler;
        Player.OnCrouch += CrouchHandler;
        Player.ItemChange(0);
    }

    private void DisableActions()
    {
        Player.DisableInput();
        Player.OnItemChanged -= ItemChangedHandler;
        Player.OnAim -= AimHandler;
        Player.OnFire -= FireHandler;
        Player.OnMelee -= MeleeHandler;
        Player.OnCrouch -= CrouchHandler;
    }
    
    private void AimHandler(bool isAiming)
    {
        Animator.SetBool("IsAiming", isAiming);
    }

    private void FireHandler(bool isFiring)
    {
        Animator.SetBool("IsAttacking", isFiring);
    }

    private void CrouchHandler(bool isCrouching)
    {
        Animator.SetBool("IsCrouching", isCrouching);
    }

    private void MeleeHandler()
    {
        Animator.SetTrigger("IsAttackingMelee");
    }

    private void ItemChangedHandler(int prewItem, int curtItem)
    {
        Animator.SetLayerWeight(prewItem + 2, 0);
        Animator.SetLayerWeight(curtItem + 2, 1);
        Animator.SetInteger("Weapon", curtItem);
        WeaponController.SetItem(curtItem);
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
    
    
    public void UpdateLook(float deltaTime)
    {
        var lookDir = GetMouseLookDirection();
            
        // If rotation is delayed
        // var angle = Vector3.Lerp(rotationTransform.forward, lookDir, RotationSpeed * deltaTime);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformRotation.position, PlayerTransformRotation.position + lookDir, Color.red);
#endif

        PlayerTransformRotation.LookAt(PlayerTransformRotation.position + lookDir);
    }

    public void UpdateMovement(float deltaTime)
    {
        var translateMov = Player.GetMovementTranslate(deltaTime);
        var speed = Player.GetSpeed(deltaTime);
        PlayerTransformMovement.Translate(translateMov * speed, Space.World);

#if UNITY_EDITOR
        //Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + v3_mov, Color.green);
        Debug.DrawLine(PlayerTransformMovement.position, PlayerTransformMovement.position + translateMov, Color.blue);
#endif

        relativeMove = PlayerTransformRotation.InverseTransformVector(translateMov);
    }
}
