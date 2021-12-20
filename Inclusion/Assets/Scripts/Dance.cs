using UnityEngine;

public class Dance : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (this.gameObject.name.Contains("Left"))
        {
            anim.SetBool("isShuffling", true);
        }

        else if (this.gameObject.name.Contains("Mid"))
        {
            anim.SetBool("isSilly", true);
        }

        else
        {
            anim.SetBool("isChicken", true);
        }
    }
}
