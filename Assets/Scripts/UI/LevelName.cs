using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelName : MonoBehaviour
{
    [SerializeField] private Animation _levelNameAnimator;
    [SerializeField] private AnimationClip _levelNameFadeOutAnimation;
    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _levelNameAnimator.clip = _levelNameFadeOutAnimation;
        _levelNameAnimator["LevelTextFadeOut"].speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelText(string levelName)
    {
        _text.fontStyle = FontStyle.Italic;
        _text.text = levelName;
    }

    public void FadeOut()
    {
        gameObject.SetActive(true);
        _levelNameAnimator.Stop();
        _levelNameAnimator.Play();
    }
}
