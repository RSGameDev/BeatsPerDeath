using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    
    public void Jump()
    {
        _animator.Play("jump", 0, 0f);
        //_animator.Play("jump");
    }
    
    
    
    
}

