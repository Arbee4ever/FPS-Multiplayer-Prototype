using Photon.Pun;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviourPun {

    #region Private Fields

    [SerializeField]
    private float directionDampTime = 0.25f;

    private Animator animator;

    #endregion

    #region MonoBehaviour Callbacks

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        if (!animator) {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    // Update is called once per frame
    void Update() {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }
        
        if (!animator) {
            return;
        }

        // deal with Jumping
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // only allow jumping if we are running.
        if (stateInfo.IsName("Base Layer.Run")) {
            // When using trigger parameter
            if (Input.GetButtonDown("Fire2")) {
                animator.SetTrigger("Jump");
            }
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (vertical < 0) {
            vertical = 0;
        }
        animator.SetFloat("Speed", horizontal * horizontal + vertical * vertical);
        animator.SetFloat("Direction", horizontal, directionDampTime, Time.deltaTime);
    }

    #endregion
}