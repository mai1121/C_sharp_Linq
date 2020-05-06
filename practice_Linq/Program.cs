using System;
using System.Collections.Generic;
using System.Linq;

namespace practice_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = new DateTime(1992, 12, 31);
            //クエリ構文
            var cms = from cm in AppTables.ClassMates
                     where cm.BirthDay < dt
                     select cm;
                     //select cm.LastName;オブジェクトの特定のプロパティを取得したい場合
            foreach (var cm in cms)
            {
                Console.WriteLine(cm);

            }

            //メソッド構文
            var tss1 = AppTables.TestScores
                       .Where(ts => ts.Average < 70).Select(ts => ts.Subject);
            foreach(var ts in tss1)
            {
                Console.WriteLine(ts);
            }

            var tss2 = AppTables.TestScores
                       .Where(ts => ts.Subject.Contains("数学")).Select(ts => ts.MaxScore);
            foreach(var ts in tss2)
            {
                Console.WriteLine($"数学の最高点は{ts}点です");
            }

            var tss3 = AppTables.TestScores
                       .OrderByDescending(ts=>ts.Average)//平均点高い順に並び替え
                       .Where(ts => new string[] { "化学", "英語" }.Contains(ts.Subject))
                       .Select(ts => ts);
            foreach(var ts in tss3)
            {
                Console.WriteLine(ts);
            }

            var cms2 = AppTables.ClassMates.GroupBy(c => c.BirthPlace);
            foreach(var c in cms2)
            {
                Console.WriteLine($"{c.Key}県出身者");
                foreach (var i in c)
                {
                    Console.WriteLine(i.LastName);
                }
            }
        }

    }
    //静的クラス（インスタンス化しなくてもメンバー呼び出し可能)の定義
    public static class AppTables
    {
        //IEnumerable<ClassMate>型のClassMatesプロパティ
        public static IEnumerable<ClassMate> ClassMates { get; private set; }
        public static IEnumerable<TestScore> TestScores { get; private set; }

        //静的コンストラクター定義。ClassMates、TestScoresプロパティの上書きを行う
        static AppTables()
        {
            ClassMates = new List<ClassMate> {
            new ClassMate
            {
                MemberNum=1,
                LastName="石田",
                FirstName="愛子",
                BirthDay=new DateTime(1992,11,23),
                BirthPlace = "神奈川"
            },
            new ClassMate
            {
                MemberNum=2,
                LastName="大塚",
                FirstName="愛",
                BirthDay=new DateTime(1992,04,18),
                BirthPlace = "神奈川"
            },
            new ClassMate
            {
                MemberNum=3,
                LastName="加藤",
                FirstName="貴子",
                BirthDay=new DateTime(1992,10,15),
                BirthPlace = "埼玉"
            },
            new ClassMate
            {
                MemberNum=4,
                LastName="佐藤",
                FirstName="健",
                BirthDay=new DateTime(1993,01,01),
                BirthPlace = "埼玉"
            }
            };

            TestScores = new List<TestScore>
            {
                new TestScore
                {
                    Subject="数学",
                    Average=56,
                    MaxScore=98,
                    MinScore=32
                },
                new TestScore
                {
                    Subject="英語",
                    Average=65,
                    MaxScore=100,
                    MinScore=12
                },
                new TestScore
                {
                    Subject="国語",
                    Average=73,
                    MaxScore=92,
                    MinScore=23
                },
                new TestScore
                {
                    Subject="化学",
                    Average=66,
                    MaxScore=87,
                    MinScore=45
                }
            };
        }
    }

    public class ClassMate
    {
        public int MemberNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDay { get; set; }
        public string BirthPlace { get; set; }

        //ToStringメソッドのオーバーロード
        public override string ToString()
        {
            return $"出席番号　{MemberNum}番の{LastName}{FirstName}さんは{BirthDay}に{BirthPlace}で生まれました";
        }
    }

    public class TestScore
    {
        public string Subject { get; set; }
        public int Average { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }

        public override string ToString()
        {
            return $"{Subject}の平均点は{Average}点で、最高点は{MaxScore}点でした";
        }
    }
}
