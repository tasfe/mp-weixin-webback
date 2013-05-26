//------------------------------------------------------------------------------
//	�ļ����ƣ�System\DI\MapItem.cs
//	�� �� �⣺2.0.50727.1882
//	����޸ģ�2012��9��8�� 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.ORM;
namespace System.DI {
    /// <summary>
    /// ����ע���е�������
    /// </summary>
    public class MapItem : CacheObject {
        private Boolean _singleton = true;
        private Dictionary<String, object> _maps = new Dictionary<String, object>();        
        /// <summary>
        /// 
        /// </summary>
        public Boolean Singleton {
            get { return _singleton; }
            set { _singleton = value; }
        }        
        /// <summary>
        /// ???
        /// </summary>
        public Dictionary<String, object> Map {
            get { return _maps; }
            set { _maps = value; }
        }
		private String _Type;
        /// <summary>
        /// 
        /// </summary>
		public String Type { get{return _Type;} set{_Type=value;} }

        internal void AddMap( String propertyName, MapItem item ) {
            AddMap( propertyName, item.Name );
        }
        internal void AddMap( String propertyName, String injectBy ) {
            this.Map.Add( propertyName, injectBy );
        }
        private Object _obj;
        [NotSave]
        internal Object TargetObject {
            get { return _obj; }
            set { _obj = value; }
        }
    }
}

