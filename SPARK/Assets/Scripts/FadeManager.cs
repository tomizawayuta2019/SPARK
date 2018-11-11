using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェード処理
/// </summary>
public class FadeManager : SingletonMonoBehaviour<FadeManager> {
    [SerializeField]
    Image fadeImage;
    [SerializeField, Range(0.01f, 5f)]
    public float defaultFadeTime;
    [SerializeField, Range(0.01f, 3f)]
    public float defaultFadeOutStopTime;
    Coroutine fade = null;
    FadingState fadingState;
    /// <summary>
    /// 現在フェード中か
    /// </summary>
    public bool IsFade { get { return fade != null; } }

    /// <summary>
    /// フェード用構造体
    /// </summary>
    public struct FadeState {
        public float fadeTime;//フェードにかかる時間（2.0fなら「暗くなる」、「明るくなる」でそれぞれ1.0fずつ経過する）
        public float fadeOutStopTime;//フェードして完全に暗くなっている時間
        /// <summary>
        /// フェード開始時に行う処理
        /// </summary>
        public System.Action begin;
        public void Begin() { if (begin != null) { begin(); } }

        /// <summary>
        /// 画面が完全に暗くなった時に呼ばれる処理
        /// </summary>
        public System.Action outComp;
        public void OutComp() { if (outComp != null) { outComp(); } }

        /// <summary>
        /// フェード終了時に呼ばれる処理
        /// </summary>
        public System.Action fadeComp;
        public void FadeComp() { if (fadeComp != null) { fadeComp(); } }

        /// <summary>
        /// 各種変数を初期値に設定
        /// </summary>
        /// <returns></returns>
        public FadeState Init() {
            fadeTime = instance.defaultFadeTime;
            fadeOutStopTime = instance.defaultFadeOutStopTime;
            return this;
        }
    }

    /// <summary>
    /// フェード中の情報を保持する構造体
    /// </summary>
    private struct FadingState {
        public FadeState state;
        public int timing;//現在どこまで実行しているか 0 まだ何もしてない 1 暗くなり中 2 暗くなって待機中 3 明るくなり中 4 終了

        //一瞬で最後まで実行
        public void Comp() {
            if (timing <= 0) { state.Begin(); }
            if (timing <= 2) { state.OutComp(); }
            if (timing <= 3) { state.FadeComp(); }

            timing = 4;
        }
    }

    /// <summary>
    /// フェード開始処理 諸々の設定を初期値でやる版
    /// </summary>
    [ContextMenu("FadeStart")]
    public void FadeStart() {
        if (fade != null){
            StopCoroutine(fade);
            fadingState.Comp();
            fade = null;
        }
        fade = StartCoroutine(Fade(fadeImage, new FadeState().Init()));
    }

    /// <summary>
    /// フェードの開始処理 構造体作って渡す
    /// </summary>
    /// <param name="action"></param>
    public void FadeStart(FadeState state) {
        if (fade != null) {
            StopCoroutine(fade);
            fadingState.Comp();
            fade = null;
        }
        fade = StartCoroutine(Fade(fadeImage, state));
    }

    private IEnumerator Fade(Image targetImage,FadeState state) {
        fadingState = new FadingState() { state = state, timing = 0 };

        state.Begin();
        fadingState.timing = 1;
        yield return StartCoroutine(ValueChange(1, state.fadeTime / 2, targetImage));

        state.OutComp();
        fadingState.timing = 2;
        if (state.fadeOutStopTime > 0) { yield return new WaitForSeconds(state.fadeOutStopTime); }

        fadingState.timing = 3;
        yield return StartCoroutine(ValueChange(0, state.fadeTime / 2, targetImage));
        state.FadeComp();

        fadingState.timing = 4;
        fade = null;
    }

    private IEnumerator ValueChange(float targetValue,float targetTime,Image targetImage) {
        float nowTime = 0;
        Color color = targetImage.color;
        float defValue = color.a;
        float valueDelta = (targetValue - defValue) / targetTime;

        while (nowTime < targetTime) {
            yield return null;

            nowTime += Time.deltaTime;
            color = targetImage.color;
            color.a = defValue + (valueDelta * nowTime);
            targetImage.color = color;
        }

        color = targetImage.color;
        color.a = targetValue;
        targetImage.color = color;
    }
}
