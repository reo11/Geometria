using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
public class ScoreRanking : MonoBehaviour {
	public DatabaseReference ScoreRankDB;
    private int mapNum = 0;
   	private GameObject rankList;
   	private Score[] scoreList = new Score[Constants.RankingCounts];
    public Text firstPrize, secondPrize, thirdPrize, forthPrize, fifthPrize;


  // Use this for initialization
    void Start () {
        // 参考 http://sleepnel.hatenablog.com/entry/2017/01/26/124500
        // https://firebase.google.com/docs/database/unity/start
        // 公式ドキュメントの方がわかりやすい
        // データベースURLを設定
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://geometria-161bd.firebaseio.com/");
        // DB参照をprivateフィールドにとっておく
        ScoreRankDB = FirebaseDatabase.DefaultInstance.GetReference("ScoreRanking");
        // for test
        // addNewUser("aaa","aaa","aaa");
        // insertScore(0, "aaa", "aaa", 5000);
        initialSet(ScoreRankDB, mapNum);
    }

    


    // 特定のマップのみ取得/更新
    private void initialSet(DatabaseReference DB, int mapNum){
        DB.Child(mapNum.ToString()).OrderByChild("point").LimitToLast(Constants.RankingCounts).ChildAdded += HandleChildAdded;
        DB.Child(mapNum.ToString()).OrderByChild("point").LimitToLast(Constants.RankingCounts).ChildChanged += HandleChildChanged;
        DB.Child(mapNum.ToString()).OrderByChild("point").LimitToLast(Constants.RankingCounts).ChildRemoved += HandleChildRemoved;
        DB.Child(mapNum.ToString()).OrderByChild("point").LimitToLast(Constants.RankingCounts).ChildMoved += HandleChildMoved;
    }

    void HandleChildAdded(object sender, ChildChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("HandleChildAdded");
        getScoreRanking(args.Snapshot.Reference.Parent);
        // Do something with the data in args.Snapshot
    }

    void HandleChildChanged(object sender, ChildChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("HandleChildChanged");
	    getScoreRanking(args.Snapshot.Reference.Parent);
        // Do something with the data in args.Snapshot
    }

    void HandleChildRemoved(object sender, ChildChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("HandleChildRemoved");
        getScoreRanking(args.Snapshot.Reference.Parent);
         // Do something with the data in args.Snapshot
    }

    void HandleChildMoved(object sender, ChildChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("HandleChildMoved");
        getScoreRanking(args.Snapshot.Reference.Parent);
        // Do something with the data in args.Snapshot
    }
    public void getScoreRanking(DatabaseReference DB){
        DB.OrderByChild("point").LimitToLast(Constants.RankingCounts).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
            // Handle the error...
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...
                IEnumerator<DataSnapshot> en = snapshot.Children.GetEnumerator();
                int count=1;
                while(en.MoveNext()){
                    DataSnapshot data = en.Current;
                    string userName = (string)data.Child("userName").GetValue(true);
                    string point = data.Child("point").GetValue(true).ToString();
                    Score score = new Score(userName, point);
                    scoreList[Constants.RankingCounts-count] = score;
                    count++;
                }
                this.firstPrize.text = "1st " + scoreList[0].userName + " : " + scoreList[0].point;
                this.secondPrize.text = "2nd " + scoreList[1].userName + " : " + scoreList[1].point;
                this.thirdPrize.text = "3rd " + scoreList[2].userName + " : " + scoreList[2].point;
                this.forthPrize.text = "4th " + scoreList[3].userName + " : " + scoreList[3].point;
                this.fifthPrize.text = "5th " + scoreList[4].userName + " : " + scoreList[4].point;
            }
        });
    }
    private void insertScore(int mapNum, string userId, string userName, string point){
        Score score = new Score(userName, point);
        string json = JsonUtility.ToJson(score);

        ScoreRankDB.Child(mapNum.ToString()).Child(userId).SetRawJsonValueAsync(json);
    }
}
