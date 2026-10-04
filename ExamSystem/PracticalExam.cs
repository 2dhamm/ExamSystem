using System;

namespace ExamSystem
{
    // ============================================================
    // الامتحان العملي (Practical Exam)
    // - يقبل أسئلة MCQ فقط.
    // - بعد انتهاء الطالب يعرض الإجابات الصحيحة (بدون درجة).
    // ============================================================
    public class PracticalExam : Exam
    {
        public PracticalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject)
        {
        }

        // Override لدالة AddQuestion: نقبل MCQ فقط
        public override bool AddQuestion(Question question)
        {
            // الكلمة is تفحص النوع الحقيقي للكائن
            if (!(question is MCQQuestion))
            {
                Console.WriteLine("Practical exam accepts MCQ questions only!");
                return false;
            }

            return base.AddQuestion(question);
        }

        // تنفيذ الدالة المجردة (override) الخاصة بالامتحان العملي
        public override void ShowExam()
        {
            Console.WriteLine();
            Console.WriteLine("========== Practical Exam - " + GetSubjectName() + " ==========");
            Console.WriteLine("Time: " + Time + " minutes");

            // 1) نعرض الأسئلة ونأخذ الإجابات
            AskQuestions();

            // 2) بعد الانتهاء نعرض الإجابات الصحيحة
            Console.WriteLine();
            Console.WriteLine("========== Right Answers ==========");
            for (int i = 0; i < questionCount; i++)
            {
                Console.WriteLine("Q" + (i + 1) + ": " + questions[i].Header);
                Console.WriteLine("   Right answer: " + questions[i].RightAnswer);
            }
        }
    }
}
