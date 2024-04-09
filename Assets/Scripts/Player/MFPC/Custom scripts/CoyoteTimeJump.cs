using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFPC.PlayerStats;
using MFPC.Utils;


namespace MFPC
{
    public class CoyoteTimeJump : MonoBehaviour
    {
        public Player player;
        public PlayerData playerData;
        //To support external coroutines being called:
        bool canUseCoyoteTime = true;

        private void FixedUpdate()
        {
            if(IsGround())
            {
                canUseCoyoteTime = true;
                playerData.canJump = true;
            }
            else
            {
                canUseCoyoteTime = false;
                StartCoroutine(nameof(CoyoteTime));
            }
        }        

        private bool IsGround()
        {
            Ray ray = new Ray(player.CharacterController.GetUnderPosition(), Vector3.down);
            return Physics.Raycast(ray, out RaycastHit raycastHit, playerData.UnderRayDistance);
        }

        IEnumerator CoyoteTime()
        {
            yield return new WaitForSeconds(playerData.CoyoteTime);
            playerData.canJump = false;
        }
    }
}