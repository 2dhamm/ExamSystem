using System;

namespace ExamSystem
{
    // ============================================================
    // نقطة بداية البرنامج (Main)
    // السيناريو:
    //   1) ننشئ كائن Subject
    //   2) ننادي CreateExam() لإنشاء امتحان (نوع واحد)
    //   3) ننادي ShowExam() لتشغيل الامتحان وعرض النتيجة
    // ============================================================
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********** Examination System **********");
            Console.WriteLine();

            // 1) إنشاء المادة
            Subject subject = new Subject(1, "C# Programming");
            Console.WriteLine(subject);   // ToString() تُستدعى تلقائياً
            Console.WriteLine();

            // 2) إنشاء الامتحان
            subject.CreateExam();

            // 3) تشغيل الامتحان
            // Polymorphism: ShowExam() تعمل حسب النوع الحقيقي للامتحان
            subject.Exam.ShowExam();

            Console.WriteLine();
            Console.WriteLine(subject.Exam);   // عرض معلومات الامتحان
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
