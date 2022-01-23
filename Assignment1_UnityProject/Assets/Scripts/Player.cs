using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using PGGE;

public class Player : MonoBehaviour
{
  [HideInInspector]
  public FSM mFsm = new FSM();
  public Animator mAnimator;
  public PlayerMovement mPlayerMovement;
  public LayerMask mPlayerMask;
  void Start()
  {
    mFsm.Add(new PlayerState_MOVEMENT(this));
    mFsm.SetCurrentState((int)PlayerStateType.MOVEMENT);
    PlayerConstants.PlayerMask = mPlayerMask;
  }

  void Update()
  {
    mFsm.Update();
  }

  public void Move()
  {
    mPlayerMovement.HandleInputs();
    mPlayerMovement.Move();
  }
}
