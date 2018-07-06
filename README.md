# NeochiTimer #

Windowsでもスリープ状態の予約をしたかったのでC#の練習がてら作ってみました。  
これで安心して動画見ながら寝落ちしてます。


## 機能 ##

*時刻指定* ないし *時間予約* ができます。

現在時刻より以前の時刻を指定した場合、
暗黙的に明日の時刻だと認識します。

明示的な日付の指定はできません。


## ビルド ##

1. ソースダウンロード

    ```bash:git-bash
    git clone https://github.com/akiakishitai/NeochiTimer
    ```

1. *.NET SDK* をインストール

    インストール方法は[こちら](https://www.microsoft.com/net/learn/get-started/windows)を参考。

1. ビルド

    プロジェクトルート下の `bin/` フォルダにインストールします。

    ```bash:git-bash
    dotnet publish src/main -c Release -o $(pwd)/bin
    ```

## 依存ライブラリ（非同梱） ##

* [`McMaster.Extensions.CommandLineUtils`](https://github.com/natemcmaster/CommandLineUtils/)
  / 2.2.4

  コマンドライン引数を解釈してくれるライブラリ。

  Copyright (c) Nate McMaster.  
  Licensed under the Apache License, Version 2.0.


## 使い方 ##

* **時刻指定**

    22時にスリープ予約。

    ```bash:git-bash
    dotnet bin/NeochiTimer.dll 22:00
    ```

    1時25分にスリープ予約。

    ```bash:git-bash
    dotnet bin/NeochiTimer.dll 1:25
    ```

* **時間予約**

    2時間後にスリープ予約。

    ```bash:git-bash
    dotnet bin/NeochiTimer.dll +2h
    ```

    30分後にスリープ予約。

    ```bash:git-bash
    dotnet bin/NeochiTimer.dll +30m
    ```

    1時間30分後にスリープ予約。

    ```bash:git-bash
    dotnet bin/NeochiTimer.dll +90m
    dotnet bin/NeochiTimer.dll +1h30m
    ```

    今すぐスリープ。

    ```bash
    dotnet bin/NeochiTimer.dll now
    ```


## Copyright ##

This software is released under the MIT License, see LICENSE.txt.

Copyright (c) 2018 akiakishitai
