using System;

namespace ExamSystem
{
    // ============================================================
    // كلاس الامتحان الأساسي (Abstract Base Class)
    // المفاهيم المستخدمة:
    //  1) Abstraction: الدالة ShowExam() abstract (بدون جسم)،
    //     وكل نوع امتحان ملزم بكتابتها بطريقته (Polymorphism).
    //  2) Inheritance: PracticalExam و FinalExam يرثان من هذا الكلاس.
    //  3) IComparable: مقارنة امتحانين حسب الوقت.
    // ============================================================
    public abstract class Exam : IComparable<Exam>
    {
        // ---------- Properties ----------
        public int Time { get; set; }                  // وقت الامتحان بالدقائق
        public int NumberOfQuestions { get; set; }     // عدد الأسئلة
        public Subject Subject { get; set; }           // المادة المرتبطة بالامتحان

        // protected: تراها الكلاسات المشتقة فقط (ليست public)
        protected Question[] questions;   // مصفوفة أسئلة الامتحان
        protected int questionCount;      // عدد الأسئلة المضافة فعلياً حتى الآن

        // ---------- Constructor ----------
        protected Exam(int time, int numberOfQuestions, Subject subject)
        {
            Time = time;
            NumberOfQuestions = numberOfQuestions;
            Subject = subject;
            questions = new Question[numberOfQuestions];
            questionCount = 0;
        }

        // ---------- إضافة سؤال ----------
        // virtual: يمكن للكلاس المشتق تعديلها (PracticalExam تمنع أسئلة True/False)
        // ترجع true لو تمت الإضافة، false لو فشلت
        public virtual bool AddQuestion(Question question)
        {
            if (question == null || questionCount >= questions.Length)
                return false;

            questions[questionCount] = question;
            questionCount++;
            return true;
        }

        // ---------- دالة مجردة (Abstract) ----------
        // لا يوجد لها جسم هنا، كل امتحان يكتب تنفيذها الخاص به
        public abstract void ShowExam();

        // ---------- دالة مشتركة ----------
        // تعرض كل سؤال وتأخذ إجابة الطالب. تستخدمها الامتحانات المشتقة
        // لأن طريقة الإجابة واحدة، والفرق بينهما في طريقة عرض النتيجة فقط.
        protected void AskQuestions()
        {
            for (int i = 0; i < questionCount; i++)
            {
                Console.WriteLine();
                Console.WriteLine("----- Question " + (i + 1) + " of " + questionCount + " -----");

                // Polymorphism: Display() تتصرف حسب النوع الحقيقي للسؤال (MCQ أو TF)
                questions[i].Display();

                // نأخذ إجابة الطالب (من 1 إلى عدد الإجابات)
                questions[i].UserAnswerId =
                    InputHelper.ReadInt("Your answer (number): ", 1, questions[i].AnswerList.Length);
            }
        }

        // ---------- اسم المادة بشكل آمن (لو المادة null) ----------
        protected string GetSubjectName()
        {
            if (Subject != null)
                return Subject.SubjectName;
            return "Unknown";
        }

        // ---------- IComparable ----------
        public int CompareTo(Exam other)
        {
            if (other == null)
                return 1;
            return Time.CompareTo(other.Time);
        }

        // ---------- Override ToString ----------
        public override string ToString()
        {
            return GetType().Name + " | Subject: " + GetSubjectName()
                   + " | Time: " + Time + " min | Questions: " + NumberOfQuestions;
        }
    }
}
