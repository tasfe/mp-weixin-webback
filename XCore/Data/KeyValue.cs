//------------------------------------------------------------------------------
//	文件名称：WlniaoCMS\CMS.Model\News\NewsClass.cs
//	运 行 库：2.0.50727.1882
//	代码功能：栏目分类Model类
//	最后修改：2011年12月7日 23:35:52
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.ORM;
using System.IO;
namespace System.Data
{
    /// <summary>
    /// KeyValue
    /// </summary>
    public class KeyValue
    {
		private String _Key;
        /// <summary>
        /// 键
        /// </summary>
		public string Key { get{return _Key;} set{_Key=value;} }
        
		private String _Value;
		/// <summary>
        /// 值
        /// </summary>
		public string Value { get{return _Value;} set{_Value=value;}}
        
		private String _Description;
		/// <summary>
        /// 描述
        /// </summary>
		public string Description { get{return _Description;} set{_Description=value;}}
        
		private DateTime _UpdateTime;
		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime UpdateTime { get{return _UpdateTime;} set{_UpdateTime=value;} }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValue()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
    public class KvTableUtil
    {
		private static string path = GetPath();
		private static string GetPath()
		{
			string filename=PathHelper.Map(cfgHelper.FrameworkRoot + TemplateEngine.TeConfig.Instance.TemplateFolder + "/" + TemplateEngine.TeConfig.Instance.CurrentSkin + "/BaseData.xml");
			if(file.Exists(filename))
			{
				return filename;
			}else{
				return PathHelper.Map(cfgHelper.FrameworkRoot +  "data/BaseData.xml");
			}
		}
        public static void Add(string key, string value)
        {
            Add(key, value, "");
        }
        public static void Add(string key, string value, string description)
        {
            if (GetByKey(key) == null)
            {
                List<XmlParamter> xplist = new List<XmlParamter>();
                xplist.Add(new XmlParamter("Key", key));
                xplist.Add(new XmlParamter("Value", value));
                xplist.Add(new XmlParamter("Description", ""));
                xplist.Add(new XmlParamter("UpdateTime", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss")));
                XMLHelper.AddData(path, "KeyValue", xplist.ToArray());
            }
        }
        public static void Edit(string key, string value)
        {
            Edit(key, value);
        }
        public static void Edit(string key, string value, string description)
        {
            if (GetByKey(key) != null)
            {
                XmlParamter xpKey = new XmlParamter("Key", key);
                xpKey.Direction = System.IO.ParameterDirection.Equal;
                XmlParamter xpValue = new XmlParamter("Value", value);
                xpValue.Direction = System.IO.ParameterDirection.Update;
                XmlParamter xpDescription = new XmlParamter("Description", description);
                xpDescription.Direction = System.IO.ParameterDirection.Update;
                XmlParamter xpUpdateTime = new XmlParamter("UpdateTime", DateTools.GetNow().ToString("yyyy-MM-dd HH:mm:ss"));
                xpUpdateTime.Direction = System.IO.ParameterDirection.Update;

                XMLHelper.UpdateData(path, "KeyValue", xpKey, xpValue, xpDescription, xpUpdateTime);
            }
        }
        public static void Save(string key, string value)
        {
            if (!file.Exists(path))
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            if (GetByKey(key) != null)
            {
                Edit(key, value, "");
            }
            else
            {
                Add(key, value, "");
            }
        }
        public static String GetString(string key)
        {
            try
            {
                XmlParamter xpKey = new XmlParamter("Key", key);
                xpKey.Direction = System.IO.ParameterDirection.Equal;
                Xml.XmlNode xn = XMLHelper.GetDataOne(path, "KeyValue", xpKey);
                if (xn != null)
                {
                    return xn.Attributes["Value"].Value;
                }
            }
            catch { }
            return "";
        }
        public static Data.KeyValue GetByKey(string key)
        {
            Data.KeyValue kv = new Data.KeyValue();
            try
            {
                XmlParamter xpKey = new XmlParamter("Key", key);
                xpKey.Direction = System.IO.ParameterDirection.Equal;
                Xml.XmlNode xn = XMLHelper.GetDataOne(path, "KeyValue", xpKey);
                if (xn == null)
                {
                    return null;
                }
                else
                {
                    kv.Key = xn.Attributes["Key"].Value;
                    kv.Value = xn.Attributes["Value"].Value;
                    kv.Description = xn.Attributes["Description"].Value;
                    kv.UpdateTime = Convert.ToDateTime(xn.Attributes["UpdateTime"].Value);
                }
            }
            catch { }
            return kv;
        }
        public static Int32 GetInt(string key)
        {
            try
            {
                return Convert.ToInt32(GetString(key));
            }
            catch
            {
                return 0;
            }
        }
        public static Boolean GetBool(string key)
        {
            try
            {
                return Convert.ToBoolean(GetString(key));
            }
            catch
            {
                return false;
            }
        }
    }
}