using System;

namespace ExamSystem
{
    // ============================================================
    // الامتحان النهائي (Final Exam)
    // - يقبل أسئلة MCQ و True/False.
    // - بعد الانتهاء يعرض الأسئلة + إجابات الطالب + الدرجة النهائية.
    // ============================================================
    public class FinalExam : Exam
    {
        public FinalExam(int time, int numberOfQuestions, Subject subject)
            : base(time, numberOfQuestions, subject)
        {
        }

        // تنفيذ الدالة المجردة بطريقة الامتحان النهائي
        public override void ShowExam()
        {
            Console.WriteLine();
            Console.WriteLine("========== Final Exam - " + GetSubjectName() + " ==========");
            Console.WriteLine("Time: " + Time + " minutes");

            // 1) نعرض الأسئلة ونأخذ الإجابات
            AskQuestions();

            // 2) نعرض الأسئلة وإجابات الطالب ونحسب الدرجة
            Console.WriteLine();
            Console.WriteLine("========== Exam Result ==========");

            int grade = 0;        // مجموع درجات الأسئلة الصحيحة
            int totalMarks = 0;   // مجموع درجات كل الأسئلة

            for (int i = 0; i < questionCount; i++)
            {
                Question q = questions[i];
                totalMarks += q.Mark;

                Console.WriteLine("Q" + (i + 1) + ": " + q.Header + " - " + q.Body);
                Console.WriteLine("   Your answer: " + q.GetAnswerText(q.UserAnswerId));

                if (q.IsCorrect())
                {
                    grade += q.Mark;
                    Console.WriteLine("   Result: Correct (+" + q.Mark + ")");
                }
                else
                {
                    Console.WriteLine("   Result: Wrong");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Your Grade: " + grade + " / " + totalMarks);
        }
    }
}
