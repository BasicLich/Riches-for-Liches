using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions  m_PlayerInputActions;
    private PlayerMovement      m_PlayerMovement;
    private PlayerAttack        m_PlayerAttack;

    public PauseMenu pauseMenu;

    private void OnEnable()
    {
        m_PlayerInputActions.Enable();
    }

    private void OnDisable()
    {
        m_PlayerInputActions.Disable();
    }

    private void Awake()
    {
        m_PlayerInputActions    = new PlayerInputActions();
        m_PlayerMovement        = GetComponent<PlayerMovement>();
        m_PlayerAttack          = GetComponent<PlayerAttack>();

        m_PlayerInputActions.Gameplay.Move.performed    += _ctx => m_PlayerMovement.movementDirection = _ctx.ReadValue<Vector2>();
        m_PlayerInputActions.Gameplay.Move.canceled     += _ctx => m_PlayerMovement.movementDirection = Vector2.zero;
        m_PlayerInputActions.Gameplay.Aim.performed     += _ctx => m_PlayerAttack.aimDirection = _ctx.ReadValue<Vector2>();
        m_PlayerInputActions.Gameplay.Aim.canceled      += _ctx => m_PlayerAttack.aimDirection = Vector2.zero;
        m_PlayerInputActions.Gameplay.Fire.performed    += _ctx => m_PlayerAttack.Fireball();
        m_PlayerInputActions.Gameplay.Pause.performed   += _ctx => pauseMenu.TogglePause();
    }
}
