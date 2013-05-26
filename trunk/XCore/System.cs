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
    /// XCenter Jsonת�����ߡ�
    /// </summary>
    public class Json {
        /// <summary>
        /// �� json �ַ��������л�Ϊ����
        /// </summary>
        /// <param name="oneJsonString">json �ַ���</param>
        /// <param name="t">Ŀ������</param>
        /// <returns></returns>
        public static Object ToObject(String oneJsonString, Type t)
        {

            Dictionary<String, object> map = System.Serialization.JsonParser.Parse(oneJsonString) as Dictionary<String, object>;
            return System.Serialization.JSON.setValueToObject(t, map);
        }

        /// <summary>
        /// �� json �ַ��������л�Ϊ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json �ַ���</param>
        /// <returns></returns>
        public static T ToObject<T>(String jsonString)
        {
            Object result = ToObject(jsonString, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// ���������л�Ϊjson�ַ���,��֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToString(Object obj)
        {
            return System.Serialization.SimpleJsonString.ConvertObject(obj);
        }
        /// <summary>
        /// ���������л�Ϊjson�ַ���,֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToStringEx(Object obj)
        {
            return System.Serialization.JsonString.Convert(obj);
        }
        /// <summary>
        /// �����󼯺����л�Ϊjson�ַ���,��֧���Ӷ���(������Ϊ����)�����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">���л��Ķ���</param>
        /// <returns></returns>
        public static String ToStringList(IList list)
        {
            return System.Serialization.SimpleJsonString.ConvertList(list);
        }

        /// <summary>
        /// �� json �ַ��������л�Ϊ�����б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json �ַ���</param>
        /// <returns>���ض����б�</returns>
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
        /// ��ȡConfiger���ݣ��Ȳ���KeyValue���ݣ��ٲ���Web.Config��
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
    /// �����������
    /// </summary>
    public class Rand
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
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
        /// ���������ĸ������
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
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
        /// �����������ĸ�����
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }
        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
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
            return DateTime.UtcNow.AddHours(8);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static Int64 GetValidityNum()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static Int64 GetValidityNum(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);// ��UTCʱ��Ϊ׼��ʱ���
        }
        public static String GetDayOfWeek(DateTime now)
        {
            switch (Convert.ToInt32(now.DayOfWeek))
            {
                case 0: return "����";
                case 1: return "��һ";
                case 2: return "�ܶ�";
                case 3: return "����";
                case 4: return "����";
                case 5: return "����";
                case 6: return "����";
            }
            return string.Empty;
        }
    }
}
