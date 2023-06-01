using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        // Ejemplo de num de telefono 0212-2579910
        private const int DefaultLen = 12;
        //private const string Pattern = @"^\d{3}-\d{7}$|^0\d{4}-\d{7}$";
        private const string Pattern = @"^0\d{3}-\d{7}$";

        // bool isMatch = Regex.IsMatch(phoneNum, pattern);

        public PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLen)
            {
                return null;
            }

            //if (value.Length != DefaultLen) return null;

            return new PhoneNumber(value);

        }

        public string Value { get; init; }

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneNumberRegex();

    }
}
