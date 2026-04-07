using UnityEngine;

public class SporelingAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Transform player;


    private bool playerNearby = false;

    public float showDistance = 34; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
    }
}
