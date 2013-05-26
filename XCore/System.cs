using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using System.ORM;
using System.Data;
using System.ORM.Operation;
using System.Web;
using System.ORM.Caching;
namespace System {

    /// <summary>
    /// XCenter Json转换工具。
    /// </summary>
    public class Json {
        /// <summary>
        /// 将 json 字符串反序列化为对象
        /// </summary>
        /// <param name="oneJsonString">json 字符串</param>
        /// <param name="t">目标类型</param>
        /// <returns></returns>
        public static Object ToObject(String oneJsonString, Type t)
        {

            Dictionary<String, object> map = System.Serialization.JsonParser.Parse(oneJsonString) as Dictionary<String, object>;
            return System.Serialization.JSON.setValueToObject(t, map);
        }

        /// <summary>
        /// 将 json 字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json 字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(String jsonString)
        {
            Object result = ToObject(jsonString, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// 将对象序列化为json字符串,不支持子对象(即属性为对象)的序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">序列化的对象</param>
        /// <returns></returns>
        public static String ToString(Object obj)
        {
            return System.Serialization.SimpleJsonString.ConvertObject(obj);
        }
        /// <summary>
        /// 将对象序列化为json字符串,支持子对象(即属性为对象)的序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">序列化的对象</param>
        /// <returns></returns>
        public static String ToStringEx(Object obj)
        {
            return System.Serialization.JsonString.Convert(obj);
        }
        /// <summary>
        /// 将对象集合序列化为json字符串,不支持子对象(即属性为对象)的序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">序列化的对象</param>
        /// <returns></returns>
        public static String ToStringList(IList list)
        {
            return System.Serialization.SimpleJsonString.ConvertList(list);
        }

        /// <summary>
        /// 将 json 字符串反序列化为对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json 字符串</param>
        /// <returns>返回对象列表</returns>
        public static List<T> ToList<T>(String jsonString)
        {

            List<T> list = new List<T>();
            if (System.strUtil.IsNullOrEmpty(jsonString)) return list;
            List<object> lists = System.Serialization.JsonParser.Parse(jsonString) as List<object>;
            if (lists != null)
            {
                foreach (Dictionary<String, object> map in lists)
                {
                    Object result = System.Serialization.JSON.setValueToObject(typeof(T), map);
                    list.Add((T)result);
                }
            }
            return list;
        }
    }

    public class Tool
    {
        /// <summary>
        /// 获取Configer内容（先查找KeyValue内容，再查找Web.Config）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfiger(string key)
        {
            string str = KvTableUtil.GetString(key);
            if (string.IsNullOrEmpty(key))
            {
                str = System.Configuration.ConfigurationManager.AppSettings[key];
            }
            return str;
        }
    }

    /// <summary>
    /// 生成随机内容
    /// </summary>
    public class Rand
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
                result += random.Next(10).ToString();
            return result;
        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        /// <returns></returns>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        /// <returns></returns>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }
        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTools.GetNow().Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
    }

    public class DateTools
    {
        public static DateTime GetNow()
        {
            return DateTime.UtcNow.AddHours(8);// 以UTC时间为准的时间戳
        }
        public static Int64 GetValidityNum()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// 以UTC时间为准的时间戳
        }
        public static Int64 GetValidityNum(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// 以UTC时间为准的时间戳
        }
        public static String GetDayOfWeek(DateTime now)
        {
            switch (Convert.ToInt32(now.DayOfWeek))
            {
                case 0: return "周日";
                case 1: return "周一";
                case 2: return "周二";
                case 3: return "周三";
                case 4: return "周四";
                case 5: return "周五";
                case 6: return "周六";
            }
            return string.Empty;
        }
    }
}
