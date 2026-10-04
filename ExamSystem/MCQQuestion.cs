using System;

namespace ExamSystem
{
    // ============================================================
    // سؤال اختيار من متعدد (MCQ) - اختيار إجابة واحدة
    // المفهوم: Inheritance + Polymorphism (override)
    // ============================================================
    public class MCQQuestion : Question
    {
        // نمرر كل البيانات للأب عبر base(...)
        public MCQQuestion(string header, string body, int mark, Answer[] answerList, int rightAnswerId)
            : base(header, body, mark, answerList, rightAnswerId)
        {
        }

        public override void Display()
        {
            Console.WriteLine("[MCQ Question - choose one answer]");
            base.Display();
        }
    }
}
