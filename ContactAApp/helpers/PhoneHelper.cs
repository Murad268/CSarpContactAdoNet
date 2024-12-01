using System;

namespace ContactAApp.helpers
{
    internal static class PhoneHelper
    {
        public static bool ValidatePhoneNumber(string phone)
        {
            foreach (char c in phone)
            {
                if (!char.IsDigit(c) && c != '+' && c != '(' && c != ')')
                {
                    return false;
                }
            }

            return phone.Length >= 7;
        }
    }
}
