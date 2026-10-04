using System;

namespace ExamSystem
{
    // ============================================================
    // سؤال صح / خطأ (True or False)
    // المفهوم: Inheritance - يرث كل شيء من Question.
    // الإجابات ثابتة دائماً: 1. True   2. False
    // ============================================================
    public class TFQuestion : Question
    {
        // base(...) تنادي Constructor الكلاس الأب (Question)
        // نبني مصفوفة الإجابات هنا مباشرة لأنها ثابتة (True / False)
        // ونحدد الإجابة الصحيحة: لو correctIsTrue = true فالصحيحة رقم 1 وإلا رقم 2
        public TFQuestion(string header, string body, int mark, bool correctIsTrue)
            : base(header, body, mark,
                   new Answer[] { new Answer(1, "True"), new Answer(2, "False") },
                   correctIsTrue ? 1 : 2)
        {
        }

        // Override: نعيد كتابة Display ونضيف نوع السؤال ثم نستخدم كود الأب
        public override void Display()
        {
            Console.WriteLine("[True or False Question]");
            base.Display();   // ننادي دالة الأب لعرض الباقي دون تكرار الكود
        }
    }
}
