﻿using DoAnTotNghiep.EditModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoAnTotNghiep.Config
{
    public static class StringExtentions
    {
        public static string GetCaptchaWord(int length)
        {
            Random random = new Random(System.DateTime.Now.Millisecond);

            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            string cw = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return cw;
        }

        public static string GetDateTimeStringFormat(this TimeSpan span)
        {
            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            DateTime zeroTime = new DateTime(1, 1, 1);
            int years = (zeroTime + span).Year - 1;
            int months = (zeroTime + span).Month - 1;
            int days = (zeroTime + span).Day;
            if (years != 0)
            {
                year = string.Format("{0} năm", years);
            }
            if (months != 0)
            {
                month = string.Format("{0} tháng", months);
            }
            if (days != 0)
            {
                day = string.Format("{0} ngày", days);
            }
            return string.Format("{0} {1} {2}", year, month, day);
        }

        public static string GenerateCode(this int count)
        {
            count++;
            return count.ToString("D3");
        }

        public static string GetPathUrl(this string str)
        {
            if (str == null)
            {
                return String.Empty;
            }
            str = str.Replace('\\', '/');
            return str;
        }
        public static string Format0(decimal value)
        {
            return value.ToString("n0");
        }

        public static string Format0(decimal? value)
        {
            return value?.ToString("n0");
        }
        public static string FormatLong(long value)
        {
            return value.ToString("n0");
        }
        public static string FormatNullLong(long? value)
        {
            return value?.ToString("n0");
        }
        public static string FormatNullInt(int? value)
        {
            return value?.ToString("n0");
        }
        public static string Parse(string value)
        {
            return Regex.Replace(value, @"\$\s?|(,*)", "");
        }
        public static bool IsRelationshipException(this Exception ex)
        {
            return ex?.InnerException?.Message?.Contains("The DELETE statement conflicted with the REFERENCE constraint") == true;
        }
        public static string GetNormalizeString(this string str)
        {
            str = str.ToLower();
            str = Regex.Replace(str, @"[áàảạãăắẳằặẵâấầẩậẫ]", "a");
            str = Regex.Replace(str, @"[eéèẻẹẽêếềểệễ]", "e");
            str = Regex.Replace(str, @"[oõóòỏọôốồổộỗơớờởỡợ]", "o");
            str = Regex.Replace(str, @"[uủùúụưứừửựũữ]", "u");
            str = Regex.Replace(str, @"[iíìỉịĩ]", "i");
            str = Regex.Replace(str, @"[yýỳỷỵỹ]", "y");
            str = Regex.Replace(str, @"[đ]", "d");
            str = Regex.Replace(str, @"[^a-zA-Z0-9]", " ");
            str = Regex.Replace(str, " +", " ");
            str = Regex.Replace(str, "^[ ]", "");
            str = Regex.Replace(str, "[ ]$", "");

            return str.Replace(" ", "").ToUpper();
        }
        public static bool IsNumberFormatVN(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9.]*\,?[0-9]+$");
            return regex.IsMatch(pText);
        }
       
        public static DataColumn CopyTo(this DataColumn column)
        {
            DataColumn newColumn = new DataColumn(column.ColumnName, column.DataType, column.Expression, column.ColumnMapping);
            newColumn.AllowDBNull = column.AllowDBNull;
            newColumn.AutoIncrement = column.AutoIncrement;
            newColumn.AutoIncrementSeed = column.AutoIncrementSeed;
            newColumn.AutoIncrementStep = column.AutoIncrementStep;
            newColumn.Caption = column.Caption;
            newColumn.DateTimeMode = column.DateTimeMode;
            newColumn.DefaultValue = column.DefaultValue;
            newColumn.MaxLength = column.MaxLength;
            newColumn.ReadOnly = column.ReadOnly;
            newColumn.Unique = column.Unique;
            newColumn.DataType = column.DataType;
            return newColumn;
        }
        public static int IndexOfEpplus(this List<string> listHeader, string prop)
        {
            int index = listHeader.IndexOf(prop) + 1;
            return index;
        }

        public static int IndexOfEpplus(this Dictionary<int, string> listHeader, string prop)
        {
            return listHeader.First(c => c.Value == prop).Key;
        }

       
        public static string RemoveSpace(this string ma)
        {
            return ma?.Replace(" ", "");
        }
        public static string ConvertToDot(this string ma)
        {
            return ma?.Replace(",", ".");
        }

       
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 2,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static T GetEnumByDescription<TEditModel, T>(this TEditModel editModel, string nameProperty, string value, T defaultValue)
            where T : struct, System.Enum
            where TEditModel : EditBaseModel
        {
            if (editModel.DataSource.ContainsKey(nameProperty))
            {
                var item = editModel.DataSource[nameProperty].FirstOrDefault(c => c.Value.GetDisplay() == value);
                if (item.Key.IsNotNullOrEmpty())
                {
                    return Enum.Parse<T>(item.Key);
                }
            }
            return defaultValue;
        }

        public static string ConvertNumberToWords(int inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng" : "");
        }

        public static string GenerateXML(object o)
        {
            StringWriter sw = new StringWriter();
            string utf8 = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                using (StringWriter writer = new Utf8StringWriter())
                {
                    serializer.Serialize(writer, o);
                    utf8 = writer.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return utf8;
        }

        public static string GeneratePassword(int length)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+~`|}{[]:;?><,./-=";
            const string allChars = lowercaseChars + uppercaseChars + digitChars + specialChars;

            Random random = new Random();
            char[] password = new char[length];

            // Ensure at least one lowercase, uppercase, digit, and special character
            password[0] = lowercaseChars[random.Next(lowercaseChars.Length)];
            password[1] = uppercaseChars[random.Next(uppercaseChars.Length)];
            password[2] = digitChars[random.Next(digitChars.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            // Shuffle the password characters
            for (int i = 0; i < length; i++)
            {
                int r = random.Next(i, length);
                char temp = password[i];
                password[i] = password[r];
                password[r] = temp;
            }

            return new string(password);
        }

        public static string CreateMD5(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
      
        public static string GetNextContractNumber(this string current, int prefixNum = 4)
        {
            if (int.TryParse(current, out int number))
            {
                return (number + 1).ToString("D" + prefixNum);
            }
            return string.Empty;
        }

        public static string GetDisplayOfEnumStringValue(this string ma, Type typeEnum)
        {
            try
            {
                if (ma.IsNullOrEmpty())
                {
                    return string.Empty;
                }
                var enumValue = Enum.Parse(typeEnum, ma);
                return enumValue.GetDescription();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static List<string> GetRelationshipOrders()
        {
            //Thứ tự sắp xếp các mỗi quan hệ
            return new List<string> { "00", "03", "04", "06", "07", "05", "02", "01", "08", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "09", "21", "99" };
        }

        public static List<string> GetRelationshipMoreOrders()
        {
            //Thứ tự sắp xếp các mỗi quan hệ bên vợ hoặc chồng
            return new List<string> { "21", "04", "05", "08", "22", "07", "06", "09", "03", "10", "14", "19", "13", "11", "20", "15", "12", "01", "02", "18", "17", "00", "16", "99" };
        }

        public static string CutSentence(this string sentence, int wordCount = 4)
        {
            if (sentence.IsNullOrEmpty())
            {
                return string.Empty;
            }
            if (wordCount >= sentence.Split(' ').Length)
            {
                return sentence;
            }
            else
            {
                string[] words = sentence.Split(' ');
                string shortenedSentence = string.Join(" ", words, 0, wordCount);
                shortenedSentence += "...";

                return shortenedSentence;
            }
        }

        public static string JoinList(this IEnumerable<string> list, string separator)
        {
            return string.Join(separator, list);
        }

    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
