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
    public PlayerItemController ItemController;
    public PlayerAimController AimController;
    
    [Header("Stats")] // TODO: move to config
    public float WalkSpeed;
    public float WalkAcceleration;
    public float RotationSpeed;
    public float BodyRotationAngle;
    public float Height = 1.65f;
    public float AimSpeed;
    
    private Vector3 relativeMove;

    public Player Player { get; private set; }
    public bool IsAiming => Player.IsAiming;
    public Vector3 WeaponPosition => PlayerTransformRotation.InverseTransformPoint(ItemController.transform.position);

    public void Init(Player player)
    {
        Player = player;
        player.SetConfig(WalkSpeed, WalkAcceleration, RotationSpeed, BodyRotationAngle, Height, AimSpeed);
        ItemController.Init(this);
        AimController.Init(this);
        
        EnableActions();
    }
    
    public Vector3 GetCameraTargetPosition(float maxRadiusSqr)
    {
        if (IsAiming)
        {
            var newPos = Player.GetCameraTargetPosition(PlayerTransformRotation, maxRadiusSqr) + PlayerTransformMovement.position;
            newPos.y = PlayerTransformRotation.position.y;
            return newPos;
        }
        
        return PlayerTransformMovement.position;
    }
    
    public Vector3 GetMouseLookDirection(float floorHeight)
    {
        var lookVectorV3 = Player.GetMousePositionRelativeHeight(Height);
        if (lookVectorV3 == Vector3.zero)
        {
            lookVectorV3 = PlayerTransformRotation.forward;
        }

        lookVectorV3.y = floorHeight;
    
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
        Player.OnAim += AimHandler;
        Player.OnFire += FireHandler;
        Player.OnMelee += MeleeHandler;
        Player.OnCrouch += CrouchHandler;
        ItemController.EnableActions();
        Player.ItemChange(Player.CurrentItem);
    }

    private void DisableActions()
    {
        Player.DisableInput();
        Player.OnAim -= AimHandler;
        Player.OnFire -= FireHandler;
        Player.OnMelee -= MeleeHandler;
        Player.OnCrouch -= CrouchHandler;
        ItemController.DisableActions();
    }
    
    private void AimHandler(bool isAiming)
    {
        Animator.SetBool("IsAiming", isAiming);
    }

    private void FireHandler(bool isFiring)
    {
        //Animator.SetBool("IsAttacking", isFiring);
        //WeaponController.Shoot();
    }

    private void CrouchHandler(bool isCrouching)
    {
        Animator.SetBool("IsCrouching", isCrouching);
    }

    private void MeleeHandler()
    {
        Animator.SetTrigger("IsAttackingMelee");
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
        var lookDir = GetMouseLookDirection(PlayerTransformRotation.position.y);
            
        // If rotation is delayed
        var angle = Vector3.Lerp(PlayerTransformRotation.forward, lookDir, RotationSpeed * deltaTime);

#if UNITY_EDITOR
        Debug.DrawLine(PlayerTransformRotation.position, PlayerTransformRotation.position + lookDir, Color.red);
#endif

        PlayerTransformRotation.LookAt(PlayerTransformRotation.position + angle);
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
