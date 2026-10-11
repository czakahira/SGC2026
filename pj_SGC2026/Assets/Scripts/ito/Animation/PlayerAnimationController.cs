using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
		{
			_animator.SetBool("IsGrounded", true);
		}

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ground"))
		{
			_animator.SetBool("IsGrounded", true);
		}

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
		{
			_animator.SetBool("IsGrounded", false);
		}

    }

}
