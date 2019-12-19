# ARPerformanceSample

ARKit3 (AR Foundation) を使ってARパフォーマンスを行うためのサンプルプロジェクトです。
[arfoundation-samples](https://github.com/Unity-Technologies/arfoundation-samples)を拡張する形で構成しています。

## 実行環境
以下のソフトウェア、バージョンで動作確認ができています。
(2019/12現在)

- Mac OS (10.14.5)
- Xcode (11.3)
- Unity (2019.2.16f1)
- iOS対応端末 (A12/A12X Bionic 以上のチップを搭載した端末)
[ARKit 3 - Augmented Reality - Apple Developer](https://developer.apple.com/augmented-reality/arkit/)

  - iPhone XR
  - iPhone XS/XS Max
  - iPhone 11/11 Pro/11 Pro Max
  - iPad Air (第3世代)
  - iPad mini (第5世代)

## 動作手順
こちらのレポジトリをcloneもしくはzipでダウンロードして展開し、UnityEditorで開いてください。すでに各種設定済みなので、cloneしてビルドするだけで実機で動作するはずです。XcodeプロジェクトとしてビルドするためにiOS Build Supportが必要です。Add Modulesからインストールしましょう。動作させるシーンのみチェックを入れた状態でXcodeプロジェクトとしてビルドします。動作シーン(と用途)は下記の通りです。

- MotionCaptureSample (モーションキャプチャを使用して演出を追加)
- OcculusionSample (people occulusionを使用して人物抽出)

![BuildSettings](images/BuildSettings.png)

## プロジェクト内容の説明

以下のパスにarfoundation-samplesのmasterブランチの内容をそのまま入れてあります。  

- Assets/Externals/arfoundation-samples-master
(ARKit3のタグやブランチがなかったので、[commit hash](https://github.com/Unity-Technologies/arfoundation-samples/commit/eb6cd4204503a9f671540cd9a76a67de9d4e0ccb)で指定した内容がインポートされています)

Unity Package Managerを使用して以下のパッケージを導入しています。

- AR Foundation       (3.1.0)
- ARSubsystems        (3.1.0)
- ARKit Face Tracking (3.1.0) (使用していませんがarfoundation-samples的に必要)
- ARKit XR Plugin     (3.1.0)
- XR Legacy Input Helpers (2.0.6)

## シーンの説明
### MotionCaptureSample
モーションキャプチャーを使ってエフェクトを発生させます。両手首から虹色のエフェクトが出ます。
画面タップでモーションキャプチャ用ロボットのON/OFFができます。

![MotionCaptureAppearance](images/MotionCaptureAppearance.png)
![MotionCaptureAppearance](images/MotionCaptureAppearanceWithRobot.png)

- シーンのPath
  - Assets/Scenes/MotionCaptureSample.unity
(Assets/Externals/arfoundation-samples-master/Assets/Scenes/HumanSegmentation/HumanBodyTracking3D.unity
を拡張する形の構成になっています)

- Hierarchy
![MotionCaptureSample](images/MotionCaptureSample.png)

- ControlledRobotオブジェクト(hierarchy上)
![ControlledRobot](images/ControlledRobot.png)
モーションキャプチャ用のロボットです。こちらのロボットの関節などにエフェクトをアタッチしていくことになると思います。(詳しくは`MotionEffectSample.cs`を参考に)
ここで、HumanBodyTracking3Dとの差分として、ControlledRobotがヒエラルキー上に置いてあります。これは、HumanBodyTrackerが端末でしか動作しないため、`HumanBodyTracker.cs` を変更し、
UnityEditor上でも動作確認ができるようにするちょっとしたハックです。

- MotionEffectSample.cs
ターゲットのGameObjectにエフェクトをアタッチするスクリプトです。
`SerializeField`で関節などのオブジェクトとエフェクトをアタッチしています。
画面タップでBodyMeshのSkinnedMeshRendererをトグルし、ロボットの見た目のON/OFFをしています。

### OcculusionSample
PeopleOcculusionとshaderを使って人物領域を赤く塗りつぶすサンプルです。
画像左上にPeopleOcculusionの情報が表示されています。
![OcculusionAppearance](images/OcculusionAppearance.png)

一部こちらのサンプルのコードを参考にしています。
[ARFoundationとARKit3で光学迷彩的エフェクト - Qiita](https://qiita.com/kitasenjudesign/items/2cbe031f40877067b58d)

- シーンのPath
  - Assets/Scenes/OcculusionSample.unity
(Assets/Externals/arfoundation-samples-master/Assets/Scenes/HumanSegmentation/HumanSegmentationImages.unity
を拡張する形の構成になっています。

- Hierarchy
![OcculusionSample](images/OcculusionSample.png)

- OcculusionSample.cs
SerializeFielで指定したShaderの`_StencilTex"`に検出したhumanStencilTexture(人物領域のテクスチャ)を設定し、OnRenderImageでshaderの計算結果を画面に描画しています。OnRenderImageの処理から分かるように、Cameraオブジェクトにアタッチしないと動作しません。
また、こちらもUnityEditor上でのデバッグ用にdebugTextを指定できるようになっています。サンプルとして手を検出したhumanStencilTextureの画像が設定されています。

- OcculusionSample.shader
humanStencilTextureには黒い背景に検出された人物領域が赤色に塗りつぶされたテクスチャが格納されています。こちらのshaderでは、設定された _StencilTex(humanStencilTexture)をもとにカメラ映像の人物領域を赤く塗りつぶしています。こちらは上記の通りkitasenjudesignさんの解説を参考にしています。
