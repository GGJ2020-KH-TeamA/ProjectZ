using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetManager : MonoBehaviour {

    [System.Serializable]
    public struct SpriteData {
        public string name;
        public Sprite[] sprites;
    }

    public SpriteData[] spriteDatas;

    SpriteRenderer _spriteRenderer;

    bool     _isAnimPlaying   = false;
    float    _prevSpriteTime  = 0f;
    float    _nowInterval     = 0f;
    Sprite[] _nowSprites      = new Sprite[0];
    int      _nowSpritesIndex = 0;

    void Start () {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        foreach (SpriteData data in spriteDatas) {

            if (data.name == animName) {

                _isAnimPlaying   = true;
                _nowInterval     = interval;
                _prevSpriteTime  = Time.realtimeSinceStartup;
                _nowSprites      = data.sprites;
                _nowSpritesIndex = 0;
            }
        }

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
