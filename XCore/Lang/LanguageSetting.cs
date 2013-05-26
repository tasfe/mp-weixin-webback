//------------------------------------------------------------------------------
//	�ļ����ƣ�System\Lang\LanguageSetting.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Web;

namespace System {

    /// <summary>
    /// ĳ�����԰������ļ������ݣ�����һ�����ƺ�һ�����԰��� Dictionary
    /// </summary>
    public class LanguageSetting {

        private String name;
        private Dictionary<String, String> langMap;

        public LanguageSetting( String name, Dictionary<String, String> lang ) {
            this.name = name;
            this.langMap = lang;
        }

        /// <summary>
        /// ���� key ��ȡ����ֵ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String get( String key ) {
            return langMap[key];
        }

        /// <summary>
        /// ��ȡ���Եļ�ֵ�� Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> getLangMap() {
            return this.langMap;
        }


    }
}
