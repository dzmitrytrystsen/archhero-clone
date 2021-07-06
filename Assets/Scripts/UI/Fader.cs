using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float FadeDuration { get { return _fadeDuration; } }

    [SerializeField] private Image _faderImage;

    private Color initialColor;
    private Color _targetColor;
    private float _fadeDuration = 0.5f;

    private void Start()
    {
        initialColor = _faderImage.color;
        _targetColor = initialColor;
        _targetColor.a = 1f;
        _faderImage.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        _faderImage.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color currentColor = Color.Lerp(initialColor, _targetColor, elapsedTime / _fadeDuration);
            _faderImage.color = currentColor;
            yield return null;
        }

        _faderImage.gameObject.SetActive(false);
        yield return null;
    }
}
