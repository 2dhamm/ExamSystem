using System;

namespace ExamSystem
{
    // ============================================================
    // كلاس السؤال الأساسي (Base Class)
    // المفاهيم المستخدمة:
    //  1) Abstraction: الكلاس abstract أي لا يمكن عمل new Question()،
    //     هو فقط قالب عام يرث منه TFQuestion و MCQQuestion.
    //  2) Encapsulation: الدرجة Mark محمية بتحقق داخل الـ Property.
    //  3) Polymorphism: الدالة Display() virtual ليعيد كل نوع سؤال كتابتها.
    //  4) ICloneable + IComparable: النسخ والمقارنة (حسب الدرجة).
    // ============================================================
    public abstract class Question : ICloneable, IComparable<Question>
    {
        // ---------- Fields ----------
        private int mark;

        // ---------- Properties ----------
        public string Header { get; set; }   // عنوان السؤال
        public string Body { get; set; }     // نص السؤال

        public int Mark                      // درجة السؤال
        {
            get { return mark; }
            set
            {
                // الدرجة يجب أن تكون موجبة، وإلا نجعلها 1
                if (value <= 0)
                    mark = 1;
                else
                    mark = value;
            }
        }

        public Answer[] AnswerList { get; set; }   // مصفوفة الإجابات المتاحة
        public Answer RightAnswer { get; set; }    // الإجابة الصحيحة
        public int UserAnswerId { get; set; }      // رقم الإجابة التي اختارها الطالب

        // ---------- Constructor ----------
        // protected: يستطيع استدعاءه فقط الكلاسات المشتقة (عبر base(...))
        protected Question(string header, string body, int mark, Answer[] answerList, int rightAnswerId)
        {
            Header = header;
            Body = body;
            Mark = mark;
            AnswerList = answerList;
            RightAnswer = FindAnswer(rightAnswerId);
            UserAnswerId = 0; // 0 = لم يجب الطالب بعد
        }

        // ---------- دوال مساعدة ----------

        // تبحث عن إجابة في المصفوفة برقمها وتعيدها (أو null لو لم توجد)
        public Answer FindAnswer(int answerId)
        {
            foreach (Answer a in AnswerList)
            {
                if (a.AnswerId == answerId)
                    return a;
            }
            return null;
        }

        // تعيد نص الإجابة من رقمها
        public string GetAnswerText(int answerId)
        {
            Answer a = FindAnswer(answerId);
            if (a == null)
                return "No answer";
            return a.AnswerText;
        }

        // هل إجابة الطالب صحيحة؟
        public bool IsCorrect()
        {
            return RightAnswer != null && UserAnswerId == RightAnswer.AnswerId;
        }

        // ---------- Polymorphism ----------
        // virtual: تسمح للكلاسات المشتقة بإعادة كتابتها (override)
        // هذه النسخة تعرض الأجزاء المشتركة بين كل الأسئلة.
        public virtual void Display()
        {
            Console.WriteLine(Header + " (Mark: " + Mark + ")");
            Console.WriteLine(Body);
            foreach (Answer a in AnswerList)
            {
                Console.WriteLine("   " + a);   // يستدعي Answer.ToString() تلقائياً
            }
        }

        // ---------- ICloneable ----------
        // نسخ عميق (Deep Copy): ننسخ مصفوفة الإجابات نفسها وليس فقط المرجع لها.
        public object Clone()
        {
            // MemberwiseClone تنسخ كل الحقول وتنشئ كائناً من نفس النوع الحقيقي
            // (يعني لو الكائن MCQQuestion تعطينا MCQQuestion)
            Question copy = (Question)MemberwiseClone();

            // ننسخ المصفوفة عنصراً عنصراً
            copy.AnswerList = new Answer[AnswerList.Length];
            for (int i = 0; i < AnswerList.Length; i++)
            {
                copy.AnswerList[i] = (Answer)AnswerList[i].Clone();
            }

            // نجعل الإجابة الصحيحة تشير للنسخة الجديدة
            if (RightAnswer != null)
                copy.RightAnswer = copy.FindAnswer(RightAnswer.AnswerId);

            return copy;
        }

        // ---------- IComparable ----------
        // مقارنة سؤالين حسب الدرجة (تفيد مثلاً في ترتيب الأسئلة)
        // ترجع: سالب لو أصغر، 0 لو متساوي، موجب لو أكبر
        public int CompareTo(Question other)
        {
            if (other == null)
                return 1;
            return Mark.CompareTo(other.Mark);
        }

        // ---------- Override ToString ----------
        public override string ToString()
        {
            return Header + " - " + Body + " [Mark: " + Mark + "]";
        }
    }
}
