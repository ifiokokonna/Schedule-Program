using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Schedule_Program.lib
{
    public enum RuleCheck
    {
        OneHalfDayShift = 0,
        NoTwoNoonShift = 1,
        OneDayInTwoWeeks = 2,
        TwoDaysExemption = 3
    }
    public static class RuleBook
    {
        public static HashMap<int, List<HtmlInputText>> RuleCompile(HashMap<int, List<HtmlInputText>> data, RuleCheck rule, bool isFullDay, List<HtmlInputText> distinctElements)
        {
            switch (rule)
            {
                case RuleCheck.OneHalfDayShift:
                    return RuleZero(data, isFullDay, distinctElements);
                case RuleCheck.NoTwoNoonShift:
                    return RuleOne(data, isFullDay, distinctElements);
                case RuleCheck.OneDayInTwoWeeks:
                    return RuleTwo(data, isFullDay, distinctElements);
                case RuleCheck.TwoDaysExemption:
                    break;
            }
            return data;
        }
        private static HashMap<int, List<HtmlInputText>> RuleZero(HashMap<int, List<HtmlInputText>> data, bool isFullDay, List<HtmlInputText> distinctElements)
        {
            HashMap<int, List<HtmlInputText>> final = new HashMap<int, List<HtmlInputText>>();
            
            if (!isFullDay)
            {
                for (int i = 0; i < distinctElements.Count; i++)
                {
                    foreach (List<HtmlInputText> tmp in data)
                    {
                        for (int j = 0; j < tmp.Count; j++)
                        {
                            if (j % 2 == 0 && tmp.ElementAt(j).Value.Equals(distinctElements.ElementAt(i).Value))
                            {
                                if (tmp.ElementAt(j + 1).Value.Equals(distinctElements.ElementAt(i).Value))
                                {
                                    while (true)
                                    {
                                        int range = Utility.GetRandom(1, distinctElements.Count + 1);
                                        
                                        if (range - 1 == i)
                                            continue;
                                        else
                                        {
                                            if(!tmp.ElementAt(j + 1).Value.Equals(distinctElements.ElementAt(range - 1).Value))
                                                tmp.ElementAt(j).Value = distinctElements.ElementAt(range - 1).Value;
                                            else
                                                continue;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return data;
        }
        private static HashMap<int, List<HtmlInputText>> RuleOne(HashMap<int, List<HtmlInputText>> data, bool isFullDay, List<HtmlInputText> distinctElements)
        {
            HashMap<int, List<HtmlInputText>> final = new HashMap<int, List<HtmlInputText>>();

            if (!isFullDay)
            {
                for (int i = 0; i < distinctElements.Count; i++)
                {
                    foreach (List<HtmlInputText> tmp in data)
                    {
                        for (int j = 0; j < tmp.Count; j++)
                        {

                            if (j % 2 == 1 && tmp.ElementAt(j).Value.Equals(distinctElements.ElementAt(i).Value))
                            {
                                try
                                {
                                    if (tmp.ElementAt(j + 2).Value.Equals(distinctElements.ElementAt(i).Value))
                                    {
                                        while (true)
                                        {
                                            int range = Utility.GetRandom(1, distinctElements.Count + 1);

                                            if (range - 1 == i)
                                                continue;
                                            else
                                            {
                                                if (!tmp.ElementAt(j + 1).Value.Equals(distinctElements.ElementAt(range - 1).Value))
                                                    tmp.ElementAt(j + 2).Value = distinctElements.ElementAt(range - 1).Value;
                                                else
                                                    continue;
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }


                        }
                    }
                }
            }

            return data;
        }
        private static HashMap<int, List<HtmlInputText>> RuleTwo(HashMap<int, List<HtmlInputText>> data, bool isFullDay, List<HtmlInputText> distinctElements)
        {
            HashMap<int, List<HtmlInputText>> final = new HashMap<int, List<HtmlInputText>>();

            distinctElements = Utility.Shuffle(distinctElements);

            bool found = false;

            //Check Explicitly for 2 Weeks
            if (!isFullDay && data.Count == 2)
            {
                for (int i = 0; i < distinctElements.Count; i++)
                {
                    found = false;
                    foreach (List<HtmlInputText> tmp in data)
                    {
                        for (int j = 0; j < tmp.Count; j++)
                        {

                            if (tmp.ElementAt(j).Value.Equals(distinctElements.ElementAt(i).Value))
                            {
                                try
                                {
                                    /**
                                     * This rule does not implement because of a possible conflict with rule no 1 (RuleZero). 
                                     * If 10 employees works at least one whole day shift, there will be little or no days left 
                                     * for rule one to be effective.
                                     * 
                                     * The development of the code below has been discontinued for now
                                     * /

                                    /*if (j % 2 == 0)
                                        tmp.ElementAt(j + 1).Value = distinctElements.ElementAt(i).Value;
                                    else if (j % 2 == 1)
                                        tmp.ElementAt(j - 1).Value = distinctElements.ElementAt(i).Value;
                                    found = true;*/
                                    break;
                                }
                                catch (Exception)
                                {
                                }
                            }

                        }
                        if (found) break;
                    }
                }
            }

            return data;
        }
        private static HashMap<int, List<HtmlInputText>> RuleThree(HashMap<int, List<HtmlInputText>> data)
        {
            /**
             * This Rule was suppose to implement exemption after two days of shift
             * This rule is applied by default in this system but since rules are not prioritized in this system,
             * it is easily overridden by the other rules. If we force implementing this rule now, it will override 
             * every other rule in the system, thereby creating an imbalance in the schedule.
             */

            return data;
        }
        private static List<HtmlInputText> DistinctElements(HashMap<int, List<HtmlInputText>> data)
        {
            List<HtmlInputText> temp = new List<HtmlInputText>();

            foreach (List<HtmlInputText> tmp in data)
            {
                for (int i = 0; i < tmp.Count; i++)
                {
                    if ( !temp.Exists(e => e.Value.Equals(tmp.ElementAt(i).Value, StringComparison.OrdinalIgnoreCase) ))
                        temp.Add(tmp.ElementAt(i));
                }
            }

            return temp;
        }
    }
}