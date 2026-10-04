using System;

namespace ExamSystem
{
    // ============================================================
    // كلاس مساعد (Static Class) لقراءة المدخلات من المستخدم بأمان.
    // الكلاس الـ static لا نستطيع عمل new منه، نستدعي دوالـه مباشرة
    // باسم الكلاس مثل: InputHelper.ReadInt(...)
    // الهدف: تجنب تكرار كود التحقق من المدخلات في كل مكان.
    // ============================================================
    public static class InputHelper
    {
        // تقرأ رقماً صحيحاً من المستخدم ويجب أن يكون بين min و max
        // وتكرر السؤال حتى يدخل المستخدم قيمة صحيحة.
        public static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                // TryParse تحاول تحويل النص لرقم دون أن توقف البرنامج عند الخطأ
                int value;
                if (int.TryParse(input, out value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine("Invalid input! Please enter a number between " + min + " and " + max + ".");
            }
        }

        // تقرأ نصاً غير فارغ من المستخدم.
        public static string ReadText(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Text cannot be empty!");
            }
        }
    }
}
