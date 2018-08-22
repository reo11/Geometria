using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class FireBaseService : MonoBehaviour {
	private DatabaseReference ScoreRankDB;
    private int mapNum = 1;
    private GameObject rankList;

  // Use this for initialization
    void Start () {
        // データベースURLを設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://geometria-161bd.firebaseio.com/");
        // DB参照をprivateフィールドにとっておく
        ScoreRankDB = FirebaseDatabase.DefaultInstance.GetReference("ScoreRanking");
    }

    public string getScoreRanking(){
        Start();
        string nameText = "error";
        ScoreRankDB.Child(mapNum.ToString()).OrderByChild("score").LimitToFirst(10).GetValueAsync().ContinueWith(task => {
            if(task.IsFaulted){ //取得失敗
                //Handle the Error
            }else if(task.IsCompleted){ //取得成功
                DataSnapshot snapshot = task.Result; //結果取得
                IEnumerator<DataSnapshot> en = snapshot.Children.GetEnumerator(); //結果リストをenumeratorで処理
                //int rank = 0;
                //while(en.MoveNext()){ //１件ずつ処理
                DataSnapshot data = en.Current; //データ取る
                string name = (string)data.Child("name").GetValue(true); //名前取る
                // string score = (string)data.Child("score").GetValue(true); //得点を取る
                //順位のuGUIに値を設定
                //GameObject rankItem = rankList.transform.GetChild(rank).gameObject;
                //ScoreRank scoreRank = rankItem.GetComponent<ScoreRank>();
                //scoreRank.setScore(mapNum, rank+1, name, score); //順位1位から
                //rank++;
                nameText = name;
                //}
            }
        });
        return nameText;
    }
    void insertScore(string name, double score){
        //まずmapNoのノードにレコードを登録(Push)して、生成されたキーを取得（これを１件のノード名に使う）
        string key = ScoreRankDB.Child(mapNum.ToString()).Push().Key;
        //登録する１件データをDictionaryで定義(nameとscore)
        Dictionary<string, object> itemMap = new Dictionary<string, object>();
        itemMap.Add("name", name);
        itemMap.Add("score", score);
        //１件データをさらにDictionaryに入れる。キーはノード(bossNo/さっき生成したキー)
        Dictionary<string, object> map = new Dictionary<string, object>();
        map.Add(mapNum.ToString()+ "/" +key , itemMap);
        //データ更新
        ScoreRankDB.UpdateChildrenAsync(map);
    }

}
