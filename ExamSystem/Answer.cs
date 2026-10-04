using System;

namespace ExamSystem
{
    // ============================================================
    // كلاس الإجابة (Answer)
    // يمثل إجابة واحدة من إجابات السؤال: رقم الإجابة + نصها.
    // المفاهيم المستخدمة:
    //  1) Encapsulation: الحقول private والوصول لها عبر Properties.
    //  2) Constructor Chaining: الـ Constructor الفارغ ينادي الـ Constructor الآخر بـ this(...)
    //  3) ICloneable: لعمل نسخة مطابقة من الكائن.
    // ============================================================
    public class Answer : ICloneable
    {
        // ---------- الحقول (Fields) - خاصة لا يراها أحد من الخارج ----------
        private int answerId;
        private string answerText;

        // ---------- الخصائص (Properties) - الطريقة الآمنة للوصول للحقول ----------
        public int AnswerId
        {
            get { return answerId; }
            set { answerId = value; }
        }

        public string AnswerText
        {
            get { return answerText; }
            set
            {
                // تحقق بسيط: لا نقبل نصاً فارغاً
                if (string.IsNullOrWhiteSpace(value))
                    answerText = "No text";
                else
                    answerText = value;
            }
        }

        // ---------- Constructors ----------

        // Constructor فارغ: يستدعي الـ Constructor الثاني بقيم افتراضية
        // هذا هو Constructor Chaining (الكلمة this(...) تعني: نادِ constructor آخر في نفس الكلاس)
        public Answer() : this(0, "No text")
        {
        }

        // Constructor الأساسي: يضع القيم في الخصائص
        public Answer(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }

        // ---------- ICloneable ----------
        // ينشئ نسخة جديدة مستقلة من الإجابة (تغيير النسخة لا يؤثر على الأصل)
        public object Clone()
        {
            return new Answer(AnswerId, AnswerText);
        }

        // ---------- Override ToString ----------
        // نعيد كتابة ToString (موجودة أصلاً في كلاس object) لتعرض الإجابة بشكل جميل
        public override string ToString()
        {
            return AnswerId + ". " + AnswerText;
        }
    }
}
