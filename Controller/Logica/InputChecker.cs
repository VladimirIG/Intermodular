using System;
using System.Text.RegularExpressions;

namespace Controller.Logica
{
    public class InputChecker
    {
        public static bool comprobarFormatoLogin(string dniToCheck, string passwToCheck)
        {
            string dni = dniToCheck.Trim();
            string passw = passwToCheck.Trim();

            if (comprobarFormatoDni(dniToCheck) && (passw.Length > 3))
            {
                return true;
            }
            return false;
        }

        public static bool comprobarFormatoDni(string dniToCheck)
        {
            Regex regexDNI = new Regex("^[0-9]{8,8}[A-Za-z]");
            Regex regexNIE = new Regex("[XYZ][0-9]{7}[A-Z]");

            Match matchDNI = regexDNI.Match(dniToCheck);
            Match matchNIE = regexNIE.Match(dniToCheck);

            if ((matchDNI.Success || matchNIE.Success) && !String.IsNullOrEmpty(dniToCheck))
            {
                return true;
            }
            return false;
        }

        public static bool isOnlyLetters(String textToCheck)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            Match match = regex.Match(textToCheck);

            if ((match.Success) && !(String.IsNullOrEmpty(textToCheck)))
            {
                return true;
            }
            return false;
        }

        public static bool isOnlyNumbers(String textToCheck)
        {
            Regex regex = new Regex("^[0-9]+$");
            Match match = regex.Match(textToCheck);

            if ((match.Success) && !(String.IsNullOrEmpty(textToCheck)))
            {
                return true;
            }
            return false;
        }

        public static bool isFloatNumbers(String textToCheck)
        {
            Regex regex = new Regex("^[0-9]*(?:\\.[0-9]*)?$");
            Match match = regex.Match(textToCheck);

            if ((match.Success) && (!String.IsNullOrEmpty(textToCheck)))
            {
                return true;
            }
            return false;
        }
       
    }
}