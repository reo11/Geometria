# AR 2018 編入組(仮)
2018年度AR改でゲーム製作を行う．
### メンバー
- 平尾礼央 (B3) エンジニア
- 伊藤広樹 (B3) エンジニア
- 田熊さん (B4) プロジェクトマネージャ

### 開発環境
- Unity 2017.4.1f1

### セットアップ
コミットした時に表示されるユーザ名、メールアドレスを入力
```
git config --global user.name "ユーザ名"
git config --global user.email メールアドレス
```
GitHubにあるリポジトリをクローン
```
git clone https://github.com/reo11/AR_2018_Hennyugumi.git
```

### issueを消化する時の手順
- 改変する箇所はissueが無ければ自分でissueを立てて対処する
- issueごとにbranchを切る
- issueごとにpull requestを出して問題なさそうならmasterにマージする
- 試作版ができた後はmasterではなくdevelopブランチで作業を行う
- 機能が全て実装できた時にdevelopをmasterにマージする

### branchの命名規則
- issueごとに命名する
- 作業を始める時は最新のmasterからpullし，branchを切る
```
git branch issue/"番号"
```
例えばissue 1に関するコミットは
```
git branch issue/1
```

### 作業する時の手順
- issue 1をこなす場合
```
git pull master
git branch issue/1
git checkout issue/1
```
- issue/1で作業を終えてコミットする時
```
git add .
git commit -m "commit message"
git push origin issue/1
```

### コンフリクトが発生した場合
- masterにマージしたい場合
masterを最新のものにする
```
git checkout master
git pull
git checkout issue/[branch number]
git rebase master
```
コンフリクトを見る
```
git status
```
コンフリクト解消＆保存した後
```
git add .
git rebase --continue
```
コンフリクトがなくなるまでaddとrebaseを繰り返す

- それでも直らなかったら
```
git log
git reset "コミット番号"
```
コミットを戻してから
```
git add .
git commit -m "commit message"
git push origin issue/number -f
```
`-f`オプションは強制上書きなので出来るだけ使わないようにしましょう

rebse test reo

reoreoreo
