# MIDIMusicPlayer_DEMO  
  
## 概要  
 UnityでMIDIファイル再生するDEMOプロジェクトです。  
  
## 内容  
 [C# Synth Project](https://archive.codeplex.com/?p=csharpsynthproject)というMIDIライブラリをUnity用に改修した[n-yoda/Unity-midi](https://github.com/n-yoda/unity-midi)を使った  
MIDIファイル再生DEMOです。  
  
- 画面説明  
![UnityMIDI_DEMO](https://user-images.githubusercontent.com/17695962/85404198-91dc9e80-b599-11ea-9bcb-77d7e4fc0405.png)  
  
- 実行動画  
[![UnityMIDI_DEMO_thumbnail](https://user-images.githubusercontent.com/17695962/83125754-e44dab00-a112-11ea-8109-a8b2e7dfdf6d.png)](https://twitter.com/RerykA99/status/1265940146014150656)  
  
## 後々やるかも？(2020/05/28)  
~~MIDIファイルの管理としてエディタでは「Assets/StreamingAssets/MIDI」配下に、Androidでは  
「～/Android/data/アプリ名/files/MIDI」配下に置いて使っています。しかし、今はファイルを  
自動で配置してくれる処理はありません。後で追加しないとなぁ・・・。~~  
Android版にてStreamingAssetsからの曲データを取得して、Application.persistentDataPathのディレクトリに曲データを移し替える処理を追加しました。以降、Androidでの手動でファイル配置しないで使うことができます。  
  
## 参照  
- MIDI音源ファイル  
[フリーMIDI／MP3のフリーダウンロード ｜ MIFUNO STUDIO](http://www.mifunostudio.com/freemidimp3/)  
  
- MIDIライブラリ  
[C# Synth Project - CodePlex Archive](https://archive.codeplex.com/?p=csharpsynthproject)  
  
- MIDIライブラリをUnity用に改修する  
[UnityでMIDIを再生する - n-yoda's](http://ny.hateblo.jp/entry/2016/01/21/230640)  
  
- 上記github  
[n-yoda/unity-midi: Play MIDI(SMF) on Unity,using C# Synth Project.](https://github.com/n-yoda/unity-midi)  
  
- サンプリングレートの種別についての参考  
["サンプリング周波数 - Wikipedia](https://assetstore.unity.com/packages/3d/characters/unity-chan-model-18705)  
  
- チャネル数の設定についての参考  
[MIDIファイルの基礎知識](http://hp.vector.co.jp/authors/VA029289/midi1.html)  

## 環境
- Unity 2019.3.13f1
- Visual Studio 2017
- C#
