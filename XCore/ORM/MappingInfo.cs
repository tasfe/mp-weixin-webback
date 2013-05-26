//------------------------------------------------------------------------------
//	文件名称：System\ORM\MappingInfo.cs
//	运 行 库：2.0.50727.1882
//	最后修改：2012年9月8日 22:15:20
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
namespace System.ORM {
    internal class MappingInfo {
		private String _TypeName;
		public String TypeName { get{return _TypeName;} set{_TypeName=value;} }
		private String _Database;
		public String Database { get{return _Database;} set{_Database=value;} }
		private String _Table;
		public String Table { get{return _Table;} set{_Table=value;} }
    }
}