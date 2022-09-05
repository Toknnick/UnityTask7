using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHeroKnight : MonoBehaviour
{
    public int Death { get; private set; }
    public int Hurt { get; private set; }
    public int Grounded { get; private set; }
    public int AnimState { get; private set; }
    public int AirSpeedY { get; private set; }
    public int Jump { get; private set; }
    public int Block { get; private set; }
    public int IdleBlock { get; private set; }


    public int ChangeAttackMod(int numberOfAttack)
    {
        return Animator.StringToHash("Attack" + numberOfAttack);
    }

    private void Start()
    {
        Death = Animator.StringToHash("Death");
        Hurt = Animator.StringToHash("Hurt");
        Grounded = Animator.StringToHash("Grounded");
        AnimState = Animator.StringToHash("AnimState");
        AirSpeedY = Animator.StringToHash("AirSpeedY");
        Jump = Animator.StringToHash("Jump");
        Block = Animator.StringToHash("Block");
        IdleBlock = Animator.StringToHash("IdleBlock");
    }
}
