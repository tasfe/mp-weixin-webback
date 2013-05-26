//------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\Core\AblerParam.cs
//	运 行 库：2.0.50727.1882
//	代码功能：用来描述请求的参数集合
//	最后修改：2012年3月25日 12:30:27
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
namespace System.Web
{
    /// <summary>
    /// 用来描述请求的参数集合
    /// </summary>
    public class KeyValue : IComparable
    {
        private bool onlyshowvale = false;
        private string key;
        public object value;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Key
        {
            get { return key; }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value
        {
            get
            {
                if (value is Array)
                    return ConvertArrayToString(value as Array);
                else
                    return value.ToString();
            }
            set 
            {
                this.value = value;
            }
        }
        /// <summary>
        /// 参数值
        /// </summary>
        public void SetValue(object val)
        {
            this.value = val;
        }
        protected KeyValue(string key, object value)
        {
            this.key = key;
            this.value = value;
            this.onlyshowvale = false;
        }
        protected KeyValue(string key, object value, bool onlyshowvale)
        {
            this.key = key;
            this.value = value;
            this.onlyshowvale = onlyshowvale;
        }
        public override string ToString()
        {
            if (onlyshowvale)
                return Value;
            else
                return string.Format("{0}={1}", Key, Value);
        }
        /// <summary>
        /// 创建参数对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValue Create(string key, object value)
        {
            return new KeyValue(key, value);
        }
        /// <summary>
        /// 创建参数对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="onlyshowvale"></param>
        /// <returns></returns>
        public static KeyValue Create(string key, object value, bool onlyshowvale)
        {
            return new KeyValue(key, value, onlyshowvale);
        }
        public int CompareTo(object obj)
        {
            if (!(obj is KeyValue))
                return -1;
            return this.key.CompareTo((obj as KeyValue).key);
        }
        /// <summary>
        /// 将数组转为字符串
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static string ConvertArrayToString(Array a)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0)
                    builder.Append(",");
                builder.Append(a.GetValue(i).ToString());
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        public string EncodedValue
        {
            get
            {
                if (value is Array)
                    return System.Web.HttpUtility.UrlEncode(ConvertArrayToString(value as Array));
                else
                    return System.Web.HttpUtility.UrlEncode(value.ToString());
            }
        }
        /// <summary>
        /// 生成encode字符串
        /// </summary>
        /// <returns></returns>
        public string ToEncodedString()
        {
            return string.Format("{0}={1}", Key, EncodedValue);
        }
    }
}