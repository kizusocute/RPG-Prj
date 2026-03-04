using UnityEngine;
using UnityEngine.Rendering.UI;

public class Player_AnimationEvents : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        player.CallAnimTrigger();
    }
}
