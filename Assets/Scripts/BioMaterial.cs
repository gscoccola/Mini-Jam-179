using UnityEngine;

public class BioMaterial : MonoBehaviour
{
    [SerializeField] float InitialYield;
    [SerializeField] float BurnRate;
    float _yield;
    bool _isBeingBurnt;
    SpriteRenderer _spriteRenderer;
    bool _isBurnt;
    Color InitialColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _yield = InitialYield;
        InitialColor = _spriteRenderer.color;
    }

    void Update()
    {
        if (_isBeingBurnt && _yield>0)
        {
            _yield -= BurnRate * Time.deltaTime;
            float ratio = _yield / InitialYield;
            _spriteRenderer.color = new Color(InitialColor.r * ratio, InitialColor.g * ratio, InitialColor.b * ratio);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _isBeingBurnt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _isBeingBurnt = false;
        }
    }
}
