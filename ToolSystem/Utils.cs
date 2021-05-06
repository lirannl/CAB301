using System;

namespace Assignment {
    static class Utils {
        // Given a string, pad it on both sides equally
        public static string padString(string input, char padding, int n)
        {
            string pad = "";
            for (int i = 0; i < n; i++)
            {
                pad += padding;
            }
            return string.Format("{0}{1}{0}", pad, input);
        }
        public delegate void LibAction(ref LibrarySystem library);
    }
}