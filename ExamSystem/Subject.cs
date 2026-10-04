using System;

namespace ExamSystem
{
    // ============================================================
    // كلاس المادة (Subject)
    // يحتوي: SubjectId, SubjectName, Exam + دالة CreateExam()
    // المفاهيم المستخدمة:
    //  1) Polymorphism: المتغير Exam من نوع الأب لكنه قد يحمل PracticalExam أو FinalExam.
    //  2) Constructor Chaining.
    //  3) ICloneable و IComparable (مقارنة حسب رقم المادة).
    // ============================================================
    public class Subject : ICloneable, IComparable<Subject>
    {
        // ---------- Properties ----------
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam Exam { get; set; }     // امتحان المادة (قد يكون null قبل الإنشاء)

        // ---------- Constructors ----------
        // Constructor Chaining: الفارغ ينادي الثاني بقيم افتراضية
        public Subject() : this(0, "Unknown")
        {
        }

        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            Exam = null;
        }

        // ---------- إنشاء الامتحان ----------
        // نسأل المستخدم عن نوع الامتحان ثم نأخذ الأسئلة منه ونبني الامتحان
        public void CreateExam()
        {
            Console.WriteLine("===== Create Exam for subject: " + SubjectName + " =====");
            Console.WriteLine("Exam type:  1) Practical   2) Final");
            int examType = InputHelper.ReadInt("Your choice: ", 1, 2);
            int time = InputHelper.ReadInt("Exam time (minutes): ", 1, 300);
            int count = InputHelper.ReadInt("Number of questions: ", 1, 50);

            // Polymorphism: نخزن كائناً مشتقاً داخل متغير من نوع الأب Exam
            // this = كائن المادة الحالي (نربط الامتحان بالمادة)
            if (examType == 1)
                Exam = new PracticalExam(time, count, this);
            else
                Exam = new FinalExam(time, count, this);

            // نأخذ الأسئلة من المستخدم
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("----- Enter question " + i + " -----");

                Question question;

                if (examType == 1)
                {
                    // الامتحان العملي: MCQ فقط
                    question = ReadMCQQuestion();
                }
                else
                {
                    // الامتحان النهائي: نترك للمستخدم اختيار النوع
                    Console.WriteLine("Question type:  1) MCQ   2) True/False");
                    int qType = InputHelper.ReadInt("Your choice: ", 1, 2);

                    if (qType == 1)
                        question = ReadMCQQuestion();
                    else
                        question = ReadTFQuestion();
                }

                Exam.AddQuestion(question);
            }

            Console.WriteLine();
            Console.WriteLine("Exam created successfully!");
        }

        // ---------- دالة خاصة (private): قراءة سؤال MCQ ----------
        private Question ReadMCQQuestion()
        {
            string header = InputHelper.ReadText("Header: ");
            string body = InputHelper.ReadText("Body: ");
            int mark = InputHelper.ReadInt("Mark: ", 1, 100);
            int answersCount = InputHelper.ReadInt("Number of answers (2-6): ", 2, 6);

            Answer[] answers = new Answer[answersCount];
            for (int i = 0; i < answersCount; i++)
            {
                string text = InputHelper.ReadText("Answer " + (i + 1) + ": ");
                answers[i] = new Answer(i + 1, text);   // الأرقام تبدأ من 1
            }

            int rightId = InputHelper.ReadInt("Right answer number: ", 1, answersCount);

            return new MCQQuestion(header, body, mark, answers, rightId);
        }

        // ---------- دالة خاصة (private): قراءة سؤال True/False ----------
        private Question ReadTFQuestion()
        {
            string header = InputHelper.ReadText("Header: ");
            string body = InputHelper.ReadText("Body: ");
            int mark = InputHelper.ReadInt("Mark: ", 1, 100);
            int right = InputHelper.ReadInt("Right answer (1 = True, 2 = False): ", 1, 2);

            return new TFQuestion(header, body, mark, right == 1);
        }

        // ---------- ICloneable ----------
        // نسخ سطحي (Shallow Copy): ينسخ القيم لكن الامتحان يبقى نفس المرجع.
        // (للتبسيط، وهو كافٍ لأغراض التمرين)
        public object Clone()
        {
            return MemberwiseClone();
        }

        // ---------- IComparable: المقارنة حسب رقم المادة ----------
        public int CompareTo(Subject other)
        {
            if (other == null)
                return 1;
            return SubjectId.CompareTo(other.SubjectId);
        }

        // ---------- Override ToString ----------
        public override string ToString()
        {
            return "Subject [" + SubjectId + "] " + SubjectName;
        }
    }
}
