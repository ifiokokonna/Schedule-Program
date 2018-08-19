using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace Schedule_Program.lib
{
    public static class Utility
    {
        /*
         * Core Class for administering the entire system
         */
        private static Random random = new Random();
        public static List<HtmlInputText> Shuffle(List<HtmlInputText> data)
        {
            int size = data.Count;
            int[] range = GetRandomRange(0, size, 0);
            List<HtmlInputText> newData = new List<HtmlInputText>();

            for (int i = 0; i < size; i++)
                newData.Add(data.ElementAt(range[i] - 1));

            return newData;
        }
        public static List<HtmlInputText> Clean(List<HtmlInputText> textboxes)
        {
            List<HtmlInputText> temp = new List<HtmlInputText>();
            foreach (HtmlInputText textbox in textboxes)
                if (!String.IsNullOrEmpty(textbox.Value))
                    temp.Add(textbox);
            return temp;
        }
        public static List<HtmlInputText> Repopulate(List<HtmlInputText> data, int days)
        {
            int[] last = GetRandomRange(0, data.Count, 2, days);

            List<HtmlInputText> temp = new List<HtmlInputText>();
            for (int i = 0; i < days; i++)
            {
                HtmlInputText text = new HtmlInputText();
                text.Value = data.ElementAt(last[i] - 1).Value;
                temp.Add(text);
            }
            return temp;
        }
        public static HashMap<int, List<HtmlInputText>> Split(List<HtmlInputText> data, int segments, int weekdays, bool isFullDay)
        {
            data = isFullDay ? data : Shuffle(Double(data));

            int main = (int)(data.Count / segments);
            int mode = data.Count % segments;
            int total = mode > 0 ? segments + 1 : segments;
            HashMap<int, List<HtmlInputText>> output = new HashMap<int, List<HtmlInputText>>();

            for (int i = 1, j = 0; i <= total; i++)
            {
                List<HtmlInputText> temp = new List<HtmlInputText>();
                for (int k = 1; k <= (isFullDay ? weekdays : weekdays * 2); k++, j++)
                {
                    try
                    {
                        if (j == data.Count)
                            break;
                        temp.Add(data.ElementAt(j));
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                output.Add(i, temp);

                if (j == data.Count)
                    break;
            }
            return output;
        }
        public static List<HtmlInputText> Double(List<HtmlInputText> data)
        {
            List<HtmlInputText> temp = new List<HtmlInputText>();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < data.Count; j++)
                    temp.Add(data.ElementAt(j));

            return temp;
        }
        public static int GetNumberOfDays(DateTime d1, DateTime d2, int weekdays, bool inclsat, bool inclsun)
        {
            int days = (int)(d2 - d1).TotalDays;
            int weeks = (int)days / weekdays;
            
            if (!inclsat) days -= weeks;
            if (!inclsun) days -= weeks;

            return (int)(d2 - d1).TotalDays;
        }
        public static List<DateTime> PopulateDates(DateTime d1, DateTime d2, int weekdays, bool inclsat, bool inclsun)
        {
            List<DateTime> output = new List<DateTime>();
            int dif = GetNumberOfDays(d1, d2, weekdays, inclsat, inclsun);

            DateTime tmp = d1;
            for (int i = 0; i < dif; i++)
            {
                if (!inclsun && tmp.AddDays(i).ToString("dddd").Equals("Sunday"))
                    continue;
                if (!inclsat && tmp.AddDays(i).ToString("dddd").Equals("Saturday"))
                    continue;
                output.Add(tmp.AddDays(i));
            }
            return output;
        }
        public static List<DateTime> PopulateDates(DateTime date, int days, bool inclsat, bool inclsun)
        {
            List<DateTime> output = new List<DateTime>();

            DateTime tmp = date;
            for (int i = 0; i < days; i++)
            {
                if (!inclsun && tmp.AddDays(i).ToString("dddd").Equals("Sunday"))
                    continue;
                if (!inclsat && tmp.AddDays(i).ToString("dddd").Equals("Saturday"))
                    continue;
                output.Add(tmp.AddDays(i));
            }
            return output;
        }
        public static int GetNumberOfWeeks(int days, int weekformat)
        {
            int main = (int)(days / weekformat);
            int mode = days % weekformat;
            return mode > 0 ? main + 1 : main;
        }
        public static int GetRandom(int min, int max)
        {
            return random.Next(min, max);
        }
        public static int[] GetRandomRange(int start, int end, int occurence)
        {
            int count = end - start;
            int[] temp = new int[count];

            int shift = 0;

            while (true)
            {
                int num = GetRandom(start + 1, end + 1);

                if (occurence == 0)
                {
                    if (Search<int>.Contains(temp, num))
                        continue;
                }
                else
                {
                    if (Search<int>.Occurrence(temp, num) >= occurence)
                        continue;
                }

                temp[shift] = num;

                if (shift == (count - 1))
                    break;

                shift++;
            }

            return temp;
        }
        public static int[] GetRandomRange(int start, int end, int occurence, int length)
        {
            int[] temp = new int[length];

            int shift = 0;

            while (true)
            {
                int num = GetRandom(start + 1, end + 1);
                int occur = Search<int>.Occurrence(temp, num);
                //if (occurence > 0)
                //    if (occur >= occurence)
                //        continue;

                temp[shift] = num;

                if (shift == (length - 1))
                    break;

                shift++;
            }

            return temp;
        }
    }

    public static class Search<T>
    {
        public static bool Contains(T[] array, T match)
        {
            foreach (T t in array)
                if (t.Equals(match))
                    return true;

            return false;
        }
        public static int Occurrence(T[] array, T match)
        {
            int count = 0;
            foreach (T t in array)
                if (t.Equals(match))
                    count++;

            return count;
        }
    }
}