using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetManager : MonoBehaviour {

    [System.Serializable]
    public struct SpriteData
    {
        public string name;
        public Sprite[] sprites;
    }

    SpriteRenderer               _spriteRenderer;
    public Dictionary<string, Sprite[]> _animSprites = new Dictionary<string, Sprite[]>();
    public SpriteData[] spriteDatas;

    bool     _isAnimPlaying   = false;
    float    _prevSpriteTime  = 0f;
    float    _nowInterval     = 0f;
    Sprite[] _nowSprites      = new Sprite[0];
    int      _nowSpritesIndex = 0;

    void Start () {

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // _animSprites["default"] = (Load Sprites)
    }

    void Update () {

        if (_isAnimPlaying) {

            if (Time.realtimeSinceStartup - _prevSpriteTime > _nowInterval) {

                _nowSpritesIndex = (_nowSpritesIndex + 1) % _nowSprites.Length;
                _prevSpriteTime  = Time.realtimeSinceStartup;

                _spriteRenderer.sprite = _nowSprites[_nowSpritesIndex];
            }
        }

    }

    public void PlayAnim (string animName, float interval) {

        _isAnimPlaying   = true;
        _nowInterval     = interval;
        _prevSpriteTime  = Time.realtimeSinceStartup;
        _nowSprites      = _animSprites[animName];
        _nowSpritesIndex = 0;

        // set the 1st sprite
        _spriteRenderer.sprite = _nowSprites[0];
    }

    public void SetAnimInterval (float interval) {
        _nowInterval = interval;
    }

    public void StopAnim () {
        _isAnimPlaying = false;
    }
}
