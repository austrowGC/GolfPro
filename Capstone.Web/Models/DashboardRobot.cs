using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DashboardRobot
    {
        private const int thresholdScore = -1;
        private const float thresholdAvg = 0;

        public DashboardRobot(UserProfile profile)
        {
            _profile = profile;
        }

        private UserProfile _profile;

        public List<ScoredMatch> PlayerScores { get { return _profile.Scores; } }
        public List<League> PlayerLeagues { get { return _profile.Leagues; } }

        private float GetAverageScore()
        {
            float holes = 0;
            float score = 0;
            float avg = 0;

            foreach (ScoredMatch sm in _profile.Scores)
            {
                if (sm.Holes == 9)
                {
                    ScoredMatch csm = ConvertTo18Holes(sm);
                    holes += csm.Holes;
                    score += csm.Score;
                }
                else
                {
                    holes += sm.Holes;
                    score += sm.Score;
                }
            }

            if (holes > 0)
            {
                avg = (score / holes) * 18;
            }

            return avg;
        }

        private int GetBestStrokes18()
        {
            int best = -1;

            foreach (ScoredMatch sm in _profile.Scores)
            {
                if (sm.Holes == 18)
                {
                    if ((best == -1) || (sm.Score < best))
                    {
                        best = sm.Score;
                    }
                }
            }

            return best;
        }

        private int GetBestStrokes9()
        {
            int best = -1;

            foreach (ScoredMatch sm in _profile.Scores)
            {
                if (sm.Holes == 9)
                {
                    if ((best == -1) || (sm.Score < best))
                    {
                        best = sm.Score;
                    }
                }
            }

            return best;

        }

        private ScoredMatch ConvertTo18Holes(ScoredMatch score)
        {
            return new ScoredMatch()
            {
                Score = score.Score * 2,
                Holes = score.Holes * 2
            };
        }

        private bool RealScore(int i)
        {
            return (i > thresholdScore);
        }
        private bool RealScore(float i)
        {
            return (i > thresholdScore);
        }

        public string MessageBest18()
        {
            string text = "Best strokes in 18 holes: N/A";
            int score = GetBestStrokes18();
            if (RealScore(score))
            {
                text = $"Best strokes in 18 holes: {score}";
            }

            return text;
        }

        public string MessageBest9()
        {
            string text = "Best strokes in 9 holes: N/A";
            int score = GetBestStrokes9();
            if (RealScore(score))
            {
                text = $"Best strokes in 9 holes: {score}";
            }

            return text;
        }

        public string MessageAverage()
        {
            string text = "No games yet!";
            float avg = GetAverageScore();
            if (RealScore(avg))
            {
                text = $"Average score: {avg}";
            }

            return text;
        }

        public string MessageGreeting()
        {
            return $"Hello, {_profile.FirstName}";
        }

        public string MessageMatchScore(ScoredMatch sm)
        {
            return $"Score: {sm.Score}\tHoles: {sm.Holes}\tPar: {sm.Par}";
        }

        public bool IsOrganizerOf(League league)
        {
            return (_profile.Id == league.OrganizerId);
        }

        //20180431 below is unused
        private DashboardStats AssembleDashboard()
        {
            return new DashboardStats()
            {
                Best18 = this.MessageBest18(),
                Best9 = this.MessageBest9(),
                Average18 = this.MessageAverage(),
                User = _profile
            };
        }

    }
}