using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Searching
{
    public class Leaderboard : MonoBehaviour
    {
        private List<PlayerScore> scores = new List<PlayerScore>();
        public GameObject UIScore;
        public Transform UiParent;

        void Awake()
        {
            // Add initial scores in unsorted order
            RecordScore(new PlayerScore("Alice", 100));
            RecordScore(new PlayerScore("Bob", 50));
            RecordScore(new PlayerScore("Charlie", 75));
            RecordScore(new PlayerScore("David", 25));
            RecordScore(new PlayerScore("Eve", 125));
            RecordScore(new PlayerScore("Frank", 150));
            RecordScore(new PlayerScore("Lily", 300));
            RecordScore(new PlayerScore("Grace", 175));
            RecordScore(new PlayerScore("Oscar", 375));
            RecordScore(new PlayerScore("Ivan", 225));
            RecordScore(new PlayerScore("Heidi", 200));
            RecordScore(new PlayerScore("Judy", 250));
            RecordScore(new PlayerScore("Kevin", 275));
            RecordScore(new PlayerScore("Nina", 350));
            RecordScore(new PlayerScore("Mona", 325));
        }

        public void RecordScore(PlayerScore score)
        {
            // [1] Sequential search if the player is already in the list
            int existingIndex = -1;
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].playerName == score.playerName)
                {
                    existingIndex = i;
                    break;
                }
            }

            if (existingIndex != -1)
            {
                scores.RemoveAt(existingIndex);
            }

            // [2] Find the insertion index using binary search (descending order)
            int low = 0;
            int high = scores.Count - 1;
            int insertIndex = scores.Count;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (scores[mid].score < score.score)
                {
                    insertIndex = mid;
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            // [3] Insert the score at the appropriate index
            scores.Insert(insertIndex, score);
        }

        public void PrintScores()
        {
            string allScores = scores.Aggregate("", (acc, score) => acc + score.score.ToString() + ",");
            Debug.Log(allScores);
        }

        public void ShowleaderBoard()
        {
            foreach (var score in scores)
            {
                UIPlayerScore uIScore = Instantiate(UIScore, UiParent).GetComponent<UIPlayerScore>();
                uIScore.SetUpTextScore(score);
            }
        }
    }
}