using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public const string VR_LOBBY_SCENE = "01.Scene1";
    public const string VR_SCENE_TWO = "02.Scene2";
    public const string VR_SCENE_THREE = "03.Scene3";
    public const string VR_SCENE_FOUR = "04.Scene4";

    public const string VR_DOWN_SCENE = "20.DownScene";
    public const string VR_LOAD_SCENE = "21.LoadingScene";



    public const string SERVER_FILE_PATH =
#if UNITY_ANDROID
        "https://s3.ap-northeast-2.amazonaws.com/cdn.habul.co.kr/habul/AssetBundles_US/VRAssetAndroid/";
#endif

#if UNITY_STANDALONE_WIN
        "https://s3.ap-northeast-2.amazonaws.com/cdn.habul.co.kr/habul/AssetBundles_US/VRAsset/";
#endif

    public const string LOCAL_FILE_PATH = "/Bundle";
    public const string LOCAL_FILE_CHECK_PATH = "/CheckBundle";

}
