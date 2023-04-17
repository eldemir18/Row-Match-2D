using System.Collections;
using UnityEngine;

public class WinAnimationSequenceUI : MonoBehaviour
{
    [SerializeField] private Animator starAnimator;
    [SerializeField] private Animator text1Animator;
    [SerializeField] private Animator text2Animator;
    
    [SerializeField] private ParticleSystem winParticles;

    void OnEnable()
    {
        // Start the animation sequence
        StartCoroutine(PlayAnimationSequence());
    }

    IEnumerator PlayAnimationSequence()
    {
        // Wait until star animator is finished
        starAnimator.SetTrigger("Play"); 
        while (starAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) yield return null;
        
        winParticles.Play();
        text1Animator.SetTrigger("Play");
        text2Animator.SetTrigger("Play");
    }
}
